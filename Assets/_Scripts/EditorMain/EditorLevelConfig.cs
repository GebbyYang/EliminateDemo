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
		public int Column{set; get;}
		public int Row{set; get;}
		public int Steps{set; get;}
		public int[,] GridActived;

		public EditorLayerConfig[] layerPieceConfig;

		public EditorLevelConfig()
		{

		}
		
		
	}
}

