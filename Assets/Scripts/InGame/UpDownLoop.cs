using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

namespace CliffLeeCL.InGame
{
	public class UpDownLoop: MonoBehaviour
	{
		private TweenerCore<Vector3, Vector3, VectorOptions> tween;

		private void Awake()
		{
			EventManager.Instance.onStopAudience += StopAudience;
		}

		private void StopAudience()
		{
			tween.Kill();
			transform.DOLocalMoveY(-10, 0.5f);
		}

		private void Start()
		{
			tween = transform.DOLocalMoveY(0.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
		}
	}
}