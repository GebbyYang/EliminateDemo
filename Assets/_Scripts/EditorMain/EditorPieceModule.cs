namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Common;

	public class EditorPieceModule : EditorModuleBase {

		private PieceModuleView moduleView;
		
		public EditorPieceModule(EditorMain mainIns) : base(mainIns)
		{
			moduleView = BuildView<PieceModuleView>("PieceModuleView");
		}

		public void Init()
		{
			if(main.currentLevelConfig != null)
			{
				main.Grids = new EditorGrid[GlobelConfigs.maxRow, GlobelConfigs.maxColumn];
				var go = TSingleTon<PrefabLoad>.Singleton().LoadFromResource("EditorView", "GridView");
				for(int i = 0; i < GlobelConfigs.maxRow; i++)
				{
					for(int j = 0; j < GlobelConfigs.maxColumn; j++)
					{
						main.Grids[i, j] = new EditorGrid(i, j);
						main.Grids[i, j].BuidGridView(go, moduleView.transform);
					}
				}
			}
		}

	}
}

