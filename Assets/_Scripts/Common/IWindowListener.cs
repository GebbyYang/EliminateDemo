using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弹窗使用的接口方法
/// </summary>
public interface IWindowListener {
	
	void WindowCallback(object data);

	void WindowCancle();

}
