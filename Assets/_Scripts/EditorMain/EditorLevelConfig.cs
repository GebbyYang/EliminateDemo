namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using System;
	using UnityEngine;
	using Eliminate.Common;

	[Serializable]
	public class EditorLevelConfig {
		
		public int Level;
		public int Column;
		public int Row;
		public int Steps;
		public int[] GridActived;

		public EditorLayerConfig[] layerPieceConfig;

		public EditorLevelConfig()
		{

		}
		
		
	}
}

