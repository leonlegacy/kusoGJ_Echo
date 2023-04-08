using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rhythm{
	[CreateAssetMenu(fileName = "MusicDatas",menuName = "Data/MusicDatas")]
	public class MusicDatas: SerializedScriptableObject{
		[ListDrawerSettings(AlwaysAddDefaultValue = true)]
		public List<MusicData> musics;
	}
}