namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;

	public class EditorLayerModule : EditorModuleBase {

		private LayerModuleView moduleView;

		
		public EditorLayerModule(EditorMain mainIns) : base(mainIns)
		{

		}

		public void Init()
		{
			if(main.currentLevelConfig != null)
			{
				moduleView = BuildView<LayerModuleView>("LayerModuleView");
				moduleView.LevelNum.text = main.currentLevelConfig.Level.ToString();
				moduleView.LevelStep.text = main.currentLevelConfig.Steps.ToString();
			}
		}

		private void InitBoard()
		{
			// 显示默认层的配置
			InitLayer(0);
		}

		public void InitLayer(int layer)
		{
			
		}
	}
}

