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

		private void Awake(){
			beatPool = new ObjectPool<BeatGO>(InstantiateBeat,OnGenerateBeat);
			EventManager.Instance.onMusicPlay += Generate;
		}

		public void Generate(int musicId = 0){
			musicData = musicDatas.musics.Find(x => x.musicId == musicId);
			musicData.Sort();
			StartCoroutine(GenerateCoroutine());
		}

		private IEnumerator GenerateCoroutine(){
			float lastSpawnTime = 0;

			foreach (var beat in musicData.beatMap){
				yield return new WaitForSeconds(beat.createTime - lastSpawnTime);

				lastSpawnTime = beat.createTime;
				beatPool.Get();
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
			sequence.Append(curvePath).Append(moveToEnd).AppendCallback(()=>ReleaseBeat(beat));
		}

		private void ReleaseBeat(BeatGO beat)
		{
			beatPool.Release(beat);
		}
	}
}