using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWindowListener {
	
	void WindowCallback(object data);

	void WindowCancle();

}
