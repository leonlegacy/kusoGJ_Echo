using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CliffLeeCL
{
	public class StartGameScene: MonoBehaviour
	{
		[SerializeField]
		private Button startBtn;

		[SerializeField]
		private GameObject startPanel;

		private void Awake()
		{
			startBtn.onClick.AddListener(GameStart);
		}

		private void GameStart()
		{
			startPanel.SetActive(false);
			EventManager.Instance.OnGameStart();
		}
	}
}