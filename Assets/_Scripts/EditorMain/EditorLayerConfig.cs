namespace Eliminate.Editor
{
	using System;

	[Serializable]
	public class EditorLayerConfig {
		
		public EditorRowConfig[] rowConfigs;

		public EditorLayerConfig()
		{
			rowConfigs = new EditorRowConfig[GlobelConfigs.MaxRow];
			for(int i = 0; i < rowConfigs.Length; i++)
			{
				rowConfigs[i] = new EditorRowConfig();
			}
		}

	}
}

