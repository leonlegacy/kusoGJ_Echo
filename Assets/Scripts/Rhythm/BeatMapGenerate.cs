using System;
using System.Collections;
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
		private MusicDatas musicDatas;

		private MusicData musicData;

		private ObjectPool<BeatGO> beatPool;

		private void Awake(){
			beatPool = new ObjectPool<BeatGO>(() => Instantiate(beatGo,spawnRoot.position,Quaternion.identity));
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
	}
}