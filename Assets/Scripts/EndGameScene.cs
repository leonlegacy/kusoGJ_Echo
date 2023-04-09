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
		private Button backTitleBtn;

		[SerializeField]
		private Button gameStartBtn;
		
		[SerializeField]
		private Button exitBtn;

		[SerializeField]
		private Button startBtn;

		[SerializeField]
		private AudioSource audio;

		[SerializeField]
		private List<Button> NonEffectBtn;

		[SerializeField]
		private GameObject EndPanel;
		
		private void Awake()
		{
			exitBtn.onClick.AddListener(ExitGame);
			startBtn.onClick.AddListener(CloseTitle);
			backTitleBtn.onClick.AddListener(BackTitle);
			gameStartBtn.onClick.AddListener(StartGame);
			foreach(var button in NonEffectBtn)
			{
				button.onClick.AddListener(NormalBtnClickSound);
			}
		}

		private void CloseTitle()
		{
			EndPanel.SetActive(false);
		}

		private void BackTitle()
		{
			EndPanel.SetActive(true);
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