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

		private void Awake()
		{
			startBtn.onClick.AddListener(GameStart);
		}

		private void GameStart()
		{
			SceneManager.LoadScene("Game");
		}
	}
}