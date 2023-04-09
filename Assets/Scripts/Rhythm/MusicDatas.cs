using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rhythm{
	[CreateAssetMenu(fileName = "MusicDatas",menuName = "Data/MusicDatas")]
	public class MusicDatas: SerializedScriptableObject{
		[ListDrawerSettings(AlwaysAddDefaultValue = true)]
		public List<MusicData> musics;
	}
}