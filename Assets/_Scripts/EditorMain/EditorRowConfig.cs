namespace Eliminate.Editor
{
	using System;

	[Serializable]
	public class EditorRowConfig {
		public EditorColConfig[] colConfigs;

		public EditorRowConfig()
		{
			colConfigs = new EditorColConfig[GlobelConfigs.MaxColumn];
			for(int i = 0; i < colConfigs.Length; i++)
			{
				colConfigs[i] = new EditorColConfig();
			}
		}
	}
}


