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

		public override void InitView()
		{
			InitGrid();
		}

		public void InitGrid()
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

		public EditorPiece GetSelectPiece()
		{
			return main.Controller.GetModule<EditorPieceModule>().selectPiece;
		}

		public void SetGridPiece(int x, int y, int id)
		{
			main.currentLevelConfig.layerPieceConfig[0].rowConfigs[x].colConfigs[y].colConfigs[0] = id;
		}

		public void SelectLayer(int layerId)
		{
			for(int i = 0; i < main.currentLevelConfig.layerPieceConfig.Length; i++)
			{
				if(i == layerId)
				{
					SetGridsAlpha(1f);
				}else{
					SetGridsAlpha(.2f);
				}
			}
		}

		public void SetGridsAlpha(float value)
		{
			for(int i = 0; i < GlobelConfigs.MaxRow; i++)
			{
				for(int j = 0; j < GlobelConfigs.MaxColumn; j++)
				{
					main.Grids[i, j].SetAlpha(value);
				}
			}
		}

	}
}

