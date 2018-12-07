namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.Events;

	public class EditorFileModule : EditorModuleBase{

		private FileModuleView moduleView;

		UnityAction createOne;

		UnityAction loadOne;

		UnityAction saveOne;

		public EditorFileModule(EditorMain main):base(main)
		{
			moduleView = BuildView<FileModuleView>("FileModuleView");
			InitOnClick();
		}

		private void InitOnClick()
		{
			if(moduleView != null)
			{
				createOne += CreateOne;
				moduleView.CreatNew.onClick.AddListener(createOne);
				loadOne += LoadOne;
				moduleView.LoadOne.onClick.AddListener(loadOne);
				saveOne += SaveOne;
				moduleView.SaveOne.onClick.AddListener(saveOne);
			}
		}

		/// <summary>
		/// 创建一个新的关卡
		/// </summary>
		private void CreateOne()
		{
			
		}

		private void LoadOne()
		{

		}

		private void SaveOne()
		{

		}

	}
}

