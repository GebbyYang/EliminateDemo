﻿namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class EditorModuleController {
		
		public List<EditorModuleBase> modules;

		public EditorModuleController()
		{
			modules = new List<EditorModuleBase>();
		}

		public void AddModule<T>(T t)  where T: EditorModuleBase
		{
			modules.Add(t);
		}

		public T GetModule<T>() where T: EditorModuleBase
		{
			foreach(EditorModuleBase item in modules)
			{
				if((item as T) != null)
				{
					return item as T;
				}
			}
			return default(T);
		}

		public void InitLevelConfigView()
		{
			foreach(EditorModuleBase item in modules)
			{
				item.InitView();
			}
		}

		public void Remove<T>() where T: EditorModuleBase
		{
			T item = GetModule<T>();
			if(item != null)
			{
				modules.Remove(item);
			}
		}

	}
}

