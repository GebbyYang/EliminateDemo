namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using Eliminate.Common;
	using UnityEngine.UI;
	using UnityEngine;

	public class SelectLevelWindow : AlertWindowBase {

		public const string PrefabName = "SelectLevelWindow";

		[SerializeField]
		private InputField levelNum;

		public override void OnSubmit()
		{
			TSingleTon<AlertWindowManager>.Singleton().WindowCallBack(levelNum.text);
			base.OnSubmit();
		}

		public override void OnCancle()
		{
			TSingleTon<AlertWindowManager>.Singleton().WindowCancle();
			base.OnCancle();
		}

	}

}
