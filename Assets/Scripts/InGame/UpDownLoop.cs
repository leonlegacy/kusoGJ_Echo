using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

namespace CliffLeeCL.InGame
{
	public class UpDownLoop: MonoBehaviour
	{
		[SerializeField] Ease ease = Ease.InOutSine;
		private TweenerCore<Vector3, Vector3, VectorOptions> tween;

		private void Awake()
		{
			EventManager.Instance.onMusicPlay += StartAudience;
			EventManager.Instance.onStopAudience += StopAudience;
		}

		void StartAudience(Song song)
		{
			tween = transform.DOLocalMoveY(0.2f, song.BeatPeriod).SetDelay(song.reversedBeatOffset).SetLoops(-1, LoopType.Yoyo).SetEase(ease);
		}

		private void StopAudience()
		{
			tween.Kill();
			transform.DOLocalMoveY(-10, 0.5f);
		}
	}
}