using System;

namespace CliffLeeCL.InGame
{
	[Serializable]
	public class BackgroundSetting
	{
		public BackGroundType type;
		public float percent;
	}

	public enum BackGroundType
	{
		Audience,
		Pchan1,
		Pchan2,
		Pchan3,
		BlackBg
	}
}