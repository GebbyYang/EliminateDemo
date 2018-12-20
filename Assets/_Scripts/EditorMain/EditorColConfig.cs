namespace Eliminate.Editor
{
	using System;

	[Serializable]
	public class EditorColConfig {
		public int[] colConfigs;

		/// <summary>
		/// 行对应的列的每个层级的Piece ID 记录
		/// </summary>
		public EditorColConfig()
		{
			colConfigs = new int[GlobelConfigs.MaxLayerCount];
		}
		
	}
}


