﻿namespace Eliminate.Editor
{
	using System;

	[Serializable]
	public class EditorColConfig {
		public int[] colConfigs;
		public EditorColConfig()
		{
			colConfigs = new int[GlobelConfigs.MaxLayerCount];
		}
		
	}
}


