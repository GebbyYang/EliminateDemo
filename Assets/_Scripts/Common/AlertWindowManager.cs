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

		/// <summary>
		/// 将需要监听的对象添加到List之中
		/// </summary>
		/// <param name="listener"></param>
		public void RegisterSender(IWindowListener listener)
		{
			if(!listeners.Contains(listener))
			{
				listeners.Add(listener);
			}
		}

		/// <summary>
		/// 从List之中移除掉监听的对象
		/// </summary>
		/// <param name="listener"></param>
		public void RemoveListener(IWindowListener listener)
		{
			if(listeners.Contains(listener))
			{
				listeners.Remove(listener);
			}
		}

		/// <summary>
		/// 点击确认的回调，然后通知监听的对象数据
		/// </summary>
		/// <param name="data"></param>
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

		/// <summary>
		/// 点击取消的回调，通知监听对象取消操作
		/// </summary>
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

		/// <summary>
		/// 弹窗
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="callBack">需要回调的对象</param>
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

		/// <summary>
		/// 销毁弹窗
		/// </summary>
		public void CloseWindow()
		{
			GameObject.DestroyImmediate(currentWindow);
			currentWindow = null;
			currentAlert = null;
		}
	}
}

