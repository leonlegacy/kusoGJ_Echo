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

		[SerializeField]
		private List<BackgroundSetting> setting;

		[SerializeField]
		private List<PchanGO> pchan;

		private int stage;
		private void Awake()
		{
			EventManager.Instance.onPlayerScored += OnScoredChange;
		}

		void OnDisable()
		{
			EventManager.Instance.onPlayerScored -= OnScoredChange;
		}

		private void Start()
		{
			stage = 0;
		}

		private void OnScoredChange()
		{
			var scoreManager = ScoreManager.Instance;

			if(stage >=setting.Count)
			{
				return;
			}

			if(scoreManager.CurrentScore <= scoreManager.TotalScore *setting[stage].percent)
			{
				switch(setting[stage].type)
				{
					case BackGroundType.Audience:
						EventManager.Instance.OnStopAudience();
						break;
					case BackGroundType.Pchan1:
						pchan[0].Leave();
						pchan[4].Leave();
						break;
					case BackGroundType.Pchan2:
						pchan[1].Leave();
						pchan[3].Leave();
						break;
					case BackGroundType.Pchan3:
						pchan[2].Leave();
						break;
					case BackGroundType.BlackBg:
						background.sprite = spriteList[1];
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				stage += 1;
			}
		}
	}
}