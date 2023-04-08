using DG.Tweening;
using System;
using UnityEngine;

namespace CliffLeeCL.InGame
{
	public class UpDownLoop: MonoBehaviour
	{
		private void Start()
		{
			transform.DOLocalMoveY(0.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
		}
	}
}