using System;
using System.Collections.Generic;
using UnityEngine;

namespace CliffLeeCL.InGame
{
	public class BackGroundControl: MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer background;

		[SerializeField]
		private List<Sprite> spriteList;

		private int stage;
		private void Awake()
		{
			EventManager.Instance.onPlayerScored += OnScoredChange;
		}

		private void Start()
		{
			stage = 0;
		}

		private void OnScoredChange()
		{
			var scoreManager = ScoreManager.Instance;
			switch(stage)
			{
				case 0:
				{
					if(scoreManager.CurrentScore <= scoreManager.TotalScore / 3 * 2)
					{
						EventManager.Instance.OnStopAudience();
						stage += 1;
					}
					break;
				}
				case 1:
				{
					if(scoreManager.CurrentScore <= scoreManager.TotalScore / 2)
					{
						background.sprite = spriteList[1];
						stage += 1;
					}
					break;
				}
			}
		}
	}
}