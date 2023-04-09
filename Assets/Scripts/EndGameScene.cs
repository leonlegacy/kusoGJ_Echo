using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CliffLeeCL
{
	public class EndGameScene: MonoBehaviour
	{
		[SerializeField]
		private Button exitBtn;

		[SerializeField]
		private Button startBtn;

		private void Awake()
		{
			exitBtn.onClick.AddListener(()=>ExitGame());
			startBtn.onClick.AddListener(()=>StartGame());
		}

		private void StartGame()
		{
			SceneManager.LoadScene("Game");
		}

		private void ExitGame()
		{
			Application.Quit();
		}
	}
}