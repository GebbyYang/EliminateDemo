namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class AlertWindowManager : TSingleTon<AlertWindowManager> {

		private List<IWindowListener> listeners;

		private Transform windowRoot;

		private AlertWindowBase currentAlert;

		private GameObject currentWindow;
		
		private AlertWindowManager(){
			listeners = new List<IWindowListener>();
			var rootGo = GameObject.FindGameObjectWithTag("WindowRoot");
			if(rootGo == null)
			{
				Debug.LogError("Cant Find Windwo Root");
			}else
			{
				windowRoot = rootGo.transform;
			}
		}

		public void RegisterSender(IWindowListener listener)
		{
			if(!listeners.Contains(listener))
			{
				listeners.Add(listener);
			}
		}

		public void RemoveListener(IWindowListener listener)
		{
			if(listeners.Contains(listener))
			{
				listeners.Remove(listener);
			}
		}

		public void WindowCallBack(object data)
		{
			foreach(IWindowListener listener in listeners)
			{
				if(listener != null)
				{
					listener.WindowCallback(data);
				}else
				{
					listeners.Remove(listener);
				}
			}
		}

		public void WindowCancle()
		{
			foreach(IWindowListener listener in listeners)
			{
				if(listener != null)
				{
					listener.WindowCancle();
				}else{
					listeners.Remove(listener);
				}
			}
		}

		public void AlertWindow(AlertWindowType type, IWindowListener callBack = null)
		{
			if(callBack != null)
			{
				RegisterSender(callBack);
			}
			GameObject tempGo = default(GameObject);
			switch(type)
			{
				case AlertWindowType.SelectWindow:
					tempGo = TSingleTon<PrefabLoad>.Singleton().LoadFromResource("Common", SelectLevelWindow.PrefabName); 
				break;
			}
			if(tempGo != null)
			{
				currentWindow = GameObject.Instantiate(tempGo, windowRoot);
				currentWindow.transform.localPosition = Vector3.zero;
				currentAlert = currentWindow.GetComponent<SelectLevelWindow>();
			}
		}

		public void CloseWindow()
		{
			GameObject.DestroyImmediate(currentWindow);
			currentWindow = null;
			currentAlert = null;
		}
	}
}

