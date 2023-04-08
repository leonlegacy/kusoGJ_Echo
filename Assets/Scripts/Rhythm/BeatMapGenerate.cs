using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Pool;

namespace Rhythm{
	public class BeatMapGenerate : MonoBehaviour{
		[SerializeField]
		private Transform spawnRoot;

		[SerializeField]
		private Transform targetRoot;

		[SerializeField]
		private BeatGO beatGo;

		[SerializeField]
		private float beatSpeed = 2f;

		[SerializeField]
		private MusicDatas musicDatas;

		private MusicData musicData;

		private ObjectPool<BeatGO> beatPool;

		private void Awake(){
			beatPool = new ObjectPool<BeatGO>(InstantiateBeat,OnGenerateBeat);
		}

		private void OnGenerateBeat(BeatGO beat)
		{
			// 计算控制点的位置
			var startPos = spawnRoot.position;
			var endPos = targetRoot.position;
			var height = 2f;
			Vector3 controlPoint = startPos + (endPos - startPos) / 2f + Vector3.up * height;

			// 沿着抛物线移动物体
			beat.transform.DOPath(new Vector3[] { startPos, controlPoint, endPos }, beatSpeed, PathType.CatmullRom)
					 .SetEase(Ease.Linear);
		}

		private void Start(){
			Generate();
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
	}
}