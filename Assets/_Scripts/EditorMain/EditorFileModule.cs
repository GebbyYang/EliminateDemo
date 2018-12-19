namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.Events;
	using System;
	using Eliminate.Common;

	public class EditorFileModule : EditorModuleBase,IWindowListener{

		private EditorFileModuleView moduleView;

		private Action<int> submit;

		public EditorFileModule(EditorMain main):base(main)
		{
			moduleView = BuildView<EditorFileModuleView>("FileModuleView");
			InitOnClick();
		}

		public void WindowCallback(object data)
		{
			if(submit != null)
			{
				int levelNum;
				if(int.TryParse(data.ToString(), out levelNum))
				{
					submit(levelNum);
				}
			}
		}

		public void WindowCancle()
		{
			submit = null;
		}

		private void InitOnClick()
		{
			if(moduleView != null)
			{
				// Button OnClick Listener
				moduleView.CreatNew.onClick.AddListener(CreateOneClick);
				moduleView.LoadOne.onClick.AddListener(LoadOneClick);
				moduleView.SaveOne.onClick.AddListener(SaveOneClick);
			}
		}

		private void CreateOneLevel(int leveNum)
		{
			main.currentLevelConfig = GetBlankLevel(leveNum);
			main.InitLevelView();
		}

		private void LoadOneLevel(int levelNum)
		{

		}

		/// <summary>
		/// 加载配置和创建新配置的时候调用
		/// 刷新数据到配节界面上
		/// </summary>
		private void UpdateLevelToView()
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
			for(int i = 0; i < config.layerPieceConfig.Length; i++)
			{
				config.layerPieceConfig[i] = new EditorLayerConfig();
			}
			return config;
		}

		/// <summary>
		/// 创建一个新的关卡
		/// </summary>
		private void CreateOneClick()
		{
			submit = CreateOneLevel;
			TSingleTon<AlertWindowManager>.Singleton().AlertWindow(AlertWindowType.SelectWindow, this);
		}

		private void LoadOneClick()
		{
			submit = LoadOneLevel;
		}

		private void SaveOneClick()
		{
			
		}

	}
}

