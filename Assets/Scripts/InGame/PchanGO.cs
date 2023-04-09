using DG.Tweening;
using System;
using UnityEngine;

namespace CliffLeeCL.InGame
{
	public class PchanGO: MonoBehaviour
	{
		[SerializeField]
		private Animator animator;

		private void Awake()
		{
			EventManager.Instance.onMusicPlay += PlayAni;
		}

        private void OnDisable()
        {
			EventManager.Instance.onMusicPlay -= PlayAni;
		}

        private void PlayAni(Song obj)
		{
			animator.Play("Pchan");
		}

		public void Leave()
		{
			transform.DOMoveY(-10, 0.5f).OnComplete(()=> gameObject.SetActive(false));
		}
	}
}