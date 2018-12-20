namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using Eliminate.Common;
	using UnityEngine.UI;
	using UnityEngine;

	/// <summary>
	/// 选择关卡（单一输入框）的弹窗
	/// </summary>
	public class SelectLevelWindow : AlertWindowBase {

		public const string PrefabName = "SelectLevelWindow";

		[SerializeField]
		private InputField levelNum;

		/// <summary>
		/// 覆写父类的提交方法
		/// </summary>
		public override void OnSubmit()
		{
			TSingleTon<AlertWindowManager>.Singleton().WindowCallBack(levelNum.text);
			base.OnSubmit();
		}

		/// <summary>
		/// 覆写父类的取消方法
		/// </summary>
		public override void OnCancle()
		{
			TSingleTon<AlertWindowManager>.Singleton().WindowCancle();
			base.OnCancle();
		}

	}

}
