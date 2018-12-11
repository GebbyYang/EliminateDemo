namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEditor;

	public class PrefabLoad : TSingleTon<PrefabLoad> {
		private PrefabLoad()
		{

		}

		public GameObject LoadFromResource(string path, string name)
		{
			string fileName = string.Format(@"{0}/{1}", path, name);
			GameObject go = Resources.Load(fileName) as GameObject;
			return go;
		}

		public T LoadFromResource<T>(string path, string name)
		{
			GameObject go = LoadFromResource(path, name);
			return go.GetComponent<T>();
		}
	}
}

