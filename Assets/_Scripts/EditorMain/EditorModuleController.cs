namespace Eliminate.Editor
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

		public T GetModule<T>(T t) where T: EditorModuleBase
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

		public void Remove<T>(T t) where T: EditorModuleBase
		{
			T item = GetModule<T>(t);
			if(item != null)
			{
				modules.Remove(item);
			}
		}

	}
}

