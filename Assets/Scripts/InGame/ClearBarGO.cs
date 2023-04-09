using System;
using UnityEngine;
using UnityEngine.UI;

namespace CliffLeeCL.InGame
{
	public class ClearBarGO: MonoBehaviour
	{
		[SerializeField]
		private Image clearBar;

		private void Awake()
		{
			EventManager.Instance.onPlayerScored += ScoreChanged;
		}

		private void ScoreChanged()
		{
			clearBar.fillAmount = ScoreManager.Instance.CurrentScore / ScoreManager.Instance.TotalScore;
		}
	}
}