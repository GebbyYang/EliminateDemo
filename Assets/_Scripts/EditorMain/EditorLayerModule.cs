namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System;

	public class EditorLayerModule : EditorModuleBase {

		private EditorLayerModuleView moduleView;

		
		public EditorLayerModule(EditorMain mainIns) : base(mainIns)
		{
			
		}

		public override void InitView()
		{
			InitLayerSelect();
			InitClick();
		}

		public void InitLayerSelect()
		{
			moduleView = BuildView<EditorLayerModuleView>("LayerModuleView");
			moduleView.LevelNum.text = main.currentLevelConfig.Level.ToString();
			moduleView.LevelStep.text = main.currentLevelConfig.Steps.ToString();
		}

		private void InitClick()
		{
			moduleView.SelectNormalLayer.onClick.AddListener(SelectNormal);
		}

		private void SelectNormal()
		{
			main.Controller.GetModule<EditorGridModule>().SelectLayer(1);
		}

	}
}

