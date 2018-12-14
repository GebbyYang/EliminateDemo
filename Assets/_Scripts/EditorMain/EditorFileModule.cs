namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.Events;
	using System;

	public class EditorFileModule : EditorModuleBase{

		private EditorFileModuleView moduleView;

		private Action<int> submit;

		public EditorFileModule(EditorMain main):base(main)
		{
			moduleView = BuildView<EditorFileModuleView>("FileModuleView");
			InitOnClick();
		}

		private void InitOnClick()
		{
			if(moduleView != null)
			{
				// Button OnClick Listener
				moduleView.CreatNew.onClick.AddListener(CreateOneClick);
				moduleView.LoadOne.onClick.AddListener(LoadOneClick);
				moduleView.SaveOne.onClick.AddListener(SaveOneClick);
				moduleView.SelectLevel.onClick.AddListener(SelectLevelClick);
				moduleView.CancleSelect.onClick.AddListener(CancleSelect);
			}
		}

		private void CreateOneLevel(int leveNum)
		{
			main.currentLevelConfig = GetBlankLevel(leveNum);
			main.Controller.GetModule<EditorGridModule>().Init();
			main.Controller.GetModule<EditorPieceModule>().Init();
		}

		private void LoadOneLevel(int levelNum)
		{

		}
		
		private EditorLevelConfig GetBlankLevel(int leveNum)
		{
			EditorLevelConfig config = new EditorLevelConfig();
			config.Level = leveNum;
			config.Column = GlobelConfigs.MaxColumn;
			config.Row = GlobelConfigs.MaxRow;
			config.Steps = 0;
			config.GridActived = new int[GlobelConfigs.MaxRow, GlobelConfigs.MaxColumn];
			config.layerPieceConfig = new EditorLayerConfig[GlobelConfigs.MaxLayerCount];
			return config;
		}

		/// <summary>
		/// 创建一个新的关卡
		/// </summary>
		private void CreateOneClick()
		{
			moduleView.SelectPanel.SetActive(true);
			submit = CreateOneLevel;
		}

		private void LoadOneClick()
		{
			moduleView.SelectPanel.SetActive(true);
			submit = LoadOneLevel;
		}

		private void SaveOneClick()
		{
			
		}

		private void SelectLevelClick()
		{
			moduleView.SelectPanel.SetActive(false);
			int leveNum;
			if(int.TryParse(moduleView.LevelInput.text, out leveNum))
			{
				if(submit != null)
				{
					submit(leveNum);
				}
			}
		}

		private void CancleSelect()
		{
			moduleView.SelectPanel.SetActive(false);
		}

	}
}

