namespace Eliminate.Editor
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class EditorModuleBase {

		protected EditorMain main;

		protected GameObject view;
		
		public EditorModuleBase(EditorMain mianIns, string name = "")
		{
			main = mianIns;
		}

		public virtual T BuildView<T>(string prefabName)
		{
			if(prefabName.Trim() == "")
			{
				return default(T);
			}
			string prefabPath = @"EditorView/" + prefabName;
			var obj = Resources.Load(prefabPath);
			view = GameObject.Instantiate(obj, main.EditorRoot) as GameObject;
			return view.GetComponent<T>();
		}

		public virtual void InitView()
		{
			
		}

		public virtual void LoadFromJson()
		{

		}

		public virtual void SaveToJson()
		{
			
		}

	}
}

