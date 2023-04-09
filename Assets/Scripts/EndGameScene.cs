using System;
using System.Collections.Generic;
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

		[SerializeField]
		private AudioSource audio;

		[SerializeField]
		private List<Button> NonEffectBtn;

		private void Awake()
		{
			exitBtn.onClick.AddListener(ExitGame);
			startBtn.onClick.AddListener(StartGame);
			foreach(var button in NonEffectBtn)
			{
				button.onClick.AddListener(NormalBtnClickSound);
			}
		}

		private void StartGame()
		{
			//SceneManager.LoadScene("Game");
			EventManager.Instance.OnNewGame();
		}

		private void ExitGame()
		{
			Application.Quit();
		}

		private void NormalBtnClickSound()
		{
			audio.Play();
		}
	}
}