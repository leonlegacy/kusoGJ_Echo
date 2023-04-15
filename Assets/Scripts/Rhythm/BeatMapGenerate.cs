using CliffLeeCL;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Rhythm{
	public class BeatMapGenerate : MonoBehaviour{
		[SerializeField]
		private Transform spawnRoot;

		[SerializeField]
		private Transform targetRoot;

		[SerializeField]
		private BeatGO beatGo;

		[FormerlySerializedAs("beatSpeed")]
		[SerializeField]
		private float inSpeed = 2f;

		[SerializeField]
		private float outSpeed = 2f;

		[SerializeField]
		private MusicDatas musicDatas;

		private MusicData musicData;

		private ObjectPool<BeatGO> beatPool;
		
		int lastGenerateBeatIndex = 0;

		bool canGenerateBeat;

		private void Awake(){
			beatPool = new ObjectPool<BeatGO>(InstantiateBeat,OnGenerateBeat);
			EventManager.Instance.onMusicPlay += Generate;
			EventManager.Instance.onGameOver += StopGenerate;
		}

		void Update()
		{
			if (!canGenerateBeat || lastGenerateBeatIndex >= musicData.beatMap.Count)
				return;
		
			var lastBeat = musicData.beatMap[lastGenerateBeatIndex];
			var currentSongTime = AudioSettings.dspTime - MusicPlayer.Instance.lastPlayedDspTime;
			if (currentSongTime > lastBeat.createTime - inSpeed)
			{
				var go = beatPool.Get();
				go.Init(lastBeat.beatType);
				lastGenerateBeatIndex++;
				EventManager.Instance.OnBeatGenerate();
			}
		}

		private void OnDisable()
        {
			EventManager.Instance.onMusicPlay -= Generate;
			EventManager.Instance.onGameOver -= StopGenerate;
		}

        public void Generate(Song song){
			musicData = musicDatas.musics.Find(x => x.musicId == song.songId);
			musicData.Sort();
			lastGenerateBeatIndex = 0;
			canGenerateBeat = true;
        }

		void StopGenerate()
		{
			canGenerateBeat = false;
		}
		
		private IEnumerator GenerateCoroutine(){
			float lastSpawnTime = inSpeed;

			foreach (var beat in musicData.beatMap){
				yield return new WaitForSeconds(beat.createTime - lastSpawnTime);

				lastSpawnTime = beat.createTime;
				var go = beatPool.Get();
				go.Init(beat.beatType);
				EventManager.Instance.OnBeatGenerate();
			}
		}

		private BeatGO InstantiateBeat() {
			return Instantiate(beatGo,spawnRoot.position,Quaternion.identity);
		}

		private void OnGenerateBeat(BeatGO beat)
		{
			// 计算控制点的位置
			var startPos = spawnRoot.position;
			var endPos = targetRoot.position;
			var height = 2f;
			Vector3 controlPoint = startPos + (endPos - startPos) / 2f + Vector3.up * height;
			beat.transform.position = startPos;
			// 拋物線移動
			var curvePath = beat.transform.DOPath(new[] { startPos, controlPoint, endPos }, inSpeed, PathType.CatmullRom)
								.SetEase(Ease.Linear);

			var moveToEnd = beat.transform.DOMoveX(15f, outSpeed).SetSpeedBased();

			Sequence sequence = DOTween.Sequence();
			var secondSeq = DOTween.Sequence().AppendInterval(0.1f).AppendCallback(()=>StartCoroutine(CallBeatRecycle()));
			sequence.Append(curvePath).Append(moveToEnd).Join(secondSeq).AppendCallback(()=>ReleaseBeat(beat));
		}

		private IEnumerator CallBeatRecycle()
		{
			yield return null;
			EventManager.Instance.OnBeatRecycle();

		}
		private void ReleaseBeat(BeatGO beat)
		{
			beatPool.Release(beat);
		}
	}
}