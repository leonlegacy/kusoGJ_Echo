using DG.Tweening;
using UnityEngine;

namespace CliffLeeCL.InGame
{
	public class PchanGO: MonoBehaviour
	{
		public void Leave()
		{
			transform.DOMoveY(-10, 0.5f).OnComplete(()=> gameObject.SetActive(false));
		}
	}
}