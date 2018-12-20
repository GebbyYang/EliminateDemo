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

		/// <summary>
		/// 初始化默认的棋盘
		/// </summary>
		public void InitGrid()
		{
			if(main.Grids != null)
			{
				return;
			}
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

		/// <summary>
		/// 获取当前选中的Piece
		/// </summary>
		public EditorPiece GetSelectPiece()
		{
			return main.Controller.GetModule<EditorPieceModule>().selectPiece;
		}

		public int GetCurrentLayer()
		{
			return main.Controller.GetModule<EditorLayerModule>().CurrentLayer;
		}

		/// <summary>
		/// 设置对应X和Y坐标的Grid的Piece
		/// </summary>
		public void SetGridPiece(int x, int y, int id)
		{
			int curLayer = main.Controller.GetModule<EditorLayerModule>().CurrentLayer;
			main.currentLevelConfig.layerPieceConfig[curLayer].rowConfigs[x].colConfigs[y].colConfigs[curLayer] = id;
		}

		/// <summary>
		/// 选择显示的层
		/// </summary>
		public void SelectLayer(int layerId)
		{
			for(int i = 0; i < main.currentLevelConfig.layerPieceConfig.Length; i++)
			{
				if(i == layerId)
				{
					SetGridsAlpha(1f, i);
				}else{
					SetGridsAlpha(.2f, i);
				}
			}
		}

		/// <summary>
		/// 设置Grid的图片透明度
		/// </summary>
		public void SetGridsAlpha(float value, int layer)
		{
			for(int i = 0; i < GlobelConfigs.MaxRow; i++)
			{
				for(int j = 0; j < GlobelConfigs.MaxColumn; j++)
				{
					main.Grids[i, j].SetAlpha(value, layer);
				}
			}
		}

		/// <summary>
		/// 取消/激活Grid
		/// </summary>
		public void SwitchGrid(int x, int y, bool enable)
		{
			int index = (x -1) * GlobelConfigs.MaxRow + (y - 1) * GlobelConfigs.MaxColumn;
			main.currentLevelConfig.GridActived[index] = enable ? 0 : 1;
		}

	}
}

