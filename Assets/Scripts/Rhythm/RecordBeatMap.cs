using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rhythm
{
	public class RecordBeatMap: MonoBehaviour
	{
		[SerializeField]
		private MusicDatas musicDatas;

		[SerializeField]
		private int index;

		[SerializeField]
		private Button recordBtn;

		private MusicData musicData;
		private bool isRecord;
		private float lastTime;
		private float lastCreateTime;

		private void Start()
		{
			recordBtn.onClick.AddListener(Record);
		}

		private void Update()
		{
			if(Input.GetKeyUp(KeyCode.R))
			{
				Record();
				return;
			}

			if(!isRecord || musicData == null) return;

			if(Input.anyKeyDown)
			{
				lastCreateTime = Time.time - lastTime + lastCreateTime;
				musicData.beatMap.Add(new Beat() { createTime = lastCreateTime });
				lastTime = Time.time;
			}
		}

		public void Record()
		{
			if(!isRecord)
			{
				musicData = musicDatas.musics.Find(x => x.musicId == index);

				if(musicData == null)
				{
					musicDatas.musics.Add(new MusicData() { musicId = index, beatMap = new List<Beat>() });
				}
				else
				{
					musicData.beatMap = new List<Beat>();
				}

				isRecord = true;
				lastTime = Time.time;
				lastCreateTime = 0;
				recordBtn.GetComponent<Image>().color = Color.red;
			}
			else
			{
				isRecord = false;
				recordBtn.GetComponent<Image>().color = Color.white;
			}
		}
	}
}