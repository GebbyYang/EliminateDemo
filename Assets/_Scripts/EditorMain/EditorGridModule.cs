namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Common;

	public class EditorGridModule : EditorModuleBase {

		private EditorGridModuleView moduleView;
		
		public EditorGridModule(EditorMain mainIns) : base(mainIns)
		{
			moduleView = BuildView<EditorGridModuleView>("GridModuleView");
		}

		public void Init()
		{
			if(main.currentLevelConfig != null)
			{
				main.Grids = new EditorGrid[GlobelConfigs.MaxRow, GlobelConfigs.MaxColumn];
				var go = TSingleTon<PrefabLoad>.Singleton().LoadFromResource("EditorView", "GridView");
				for(int i = 0; i < GlobelConfigs.MaxRow; i++)
				{
					for(int j = 0; j < GlobelConfigs.MaxColumn; j++)
					{
						main.Grids[i, j] = new EditorGrid(i, j, this);
						main.Grids[i, j].BuidGridView(go, moduleView.transform);
					}
				}
			}
		}

		public EditorPiece GetSelectPiece()
		{
			return main.Controller.GetModule<EditorPieceModule>().selectPiece;
		}

	}
}

