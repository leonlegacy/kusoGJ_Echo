using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Rhythm{
	[Serializable]
	public class MusicData{
		public int musicId;
		
		[ListDrawerSettings(DraggableItems = false,OnTitleBarGUI = "SortButton",AlwaysAddDefaultValue = true)]
		public List<Beat> beatMap;

		public void Sort(){
			beatMap = beatMap.OrderBy(x => x.createTime).ToList();
		}
	#if UNITY_EDITOR
		private void SortButton(){
			if (Sirenix.Utilities.Editor.SirenixEditorGUI.ToolbarButton(Sirenix.Utilities.Editor.EditorIcons.Refresh)){
				Sort();
			}
		}
	#endif

	}
}