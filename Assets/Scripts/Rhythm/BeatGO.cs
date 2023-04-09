using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rhythm{
	public class BeatGO : MonoBehaviour{
		[SerializeField]
		private SpriteRenderer sprite;

		[SerializeField]
		private List<Sprite> spriteList;
		public void Init(BeatType beatType)
		{
			switch(beatType)
			{
				case BeatType.Red:
					sprite.sprite = spriteList[0];
					break;
				case BeatType.Blue:
					sprite.sprite = spriteList[1];
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(beatType), beatType, null);
			}

			sprite.sortingOrder += 1;
		}
	}
}