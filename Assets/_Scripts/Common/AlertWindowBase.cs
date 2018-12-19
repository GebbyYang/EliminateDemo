namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.Events;

	public class AlertWindowBase : MonoBehaviour {

		[SerializeField]
		private Button onSubmitBtn;
		[SerializeField]
		private Button onCancleBtn;

		public UnityAction SubmitAction;

		void Start()
		{
			onSubmitBtn.onClick.AddListener(OnSubmit);
			onCancleBtn.onClick.AddListener(OnCancle);
		}
		
		public virtual void OnSubmit()
		{
			TSingleTon<AlertWindowManager>.Singleton().CloseWindow();
		}

		public virtual void OnCancle()
		{
			TSingleTon<AlertWindowManager>.Singleton().CloseWindow();
		}

		

	}
}

