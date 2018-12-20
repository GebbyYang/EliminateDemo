namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.Events;

	/// <summary>
	/// 弹出框基类
	/// </summary>
	public class AlertWindowBase : MonoBehaviour {

		[SerializeField]
		private Button onSubmitBtn;
		[SerializeField]
		private Button onCancleBtn;

		void Start()
		{
			onSubmitBtn.onClick.AddListener(OnSubmit);
			onCancleBtn.onClick.AddListener(OnCancle);
		}
		
		/// <summary>
		/// 点击确认后调用关闭弹窗
		/// </summary>
		public virtual void OnSubmit()
		{
			TSingleTon<AlertWindowManager>.Singleton().CloseWindow();
		}

		/// <summary>
		/// 点击取消调用关闭弹窗
		/// </summary>
		public virtual void OnCancle()
		{
			TSingleTon<AlertWindowManager>.Singleton().CloseWindow();
		}

		

	}
}

