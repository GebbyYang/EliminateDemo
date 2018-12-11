namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.Events;
	using System;

	public class EditorFileModule : EditorModuleBase{

		private FileModuleView moduleView;

		private Action<int> submit;

		public EditorFileModule(EditorMain main):base(main)
		{
			moduleView = BuildView<FileModuleView>("FileModuleView");
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
			var pieceModule = main.Controller.GetModule<EditorPieceModule>();
			if(pieceModule == null)
			{
				Debug.LogError("Missing Require Component");
				return;
			}
			pieceModule.Init();
		}

		private void LoadOneLevel(int levelNum)
		{

		}
		
		private EditorLevelConfig GetBlankLevel(int leveNum)
		{
			EditorLevelConfig config = new EditorLevelConfig();
			config.Level = leveNum;
			config.Column = GlobelConfigs.maxColumn;
			config.Row = GlobelConfigs.maxRow;
			config.Steps = 0;
			config.GridActived = new int[GlobelConfigs.maxRow, GlobelConfigs.maxColumn];
			config.layerPieceConfig = new EditorLayerConfig[GlobelConfigs.maxLayerCount];
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

