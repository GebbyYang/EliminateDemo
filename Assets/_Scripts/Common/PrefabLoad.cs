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

		/// <summary>
		/// 读取游戏Resources文件目录下的GameObject
		/// </summary>
		public GameObject LoadFromResource(string path, string name)
		{
			string fileName = string.Format(@"{0}/{1}", path, name);
			GameObject go = Resources.Load(fileName) as GameObject;
			return go;
		}

		/// <summary>
		/// 加载Resources下的GameObject并获取目标泛型对象
		/// </summary>
		public T LoadFromResource<T>(string path, string name)
		{
			GameObject go = LoadFromResource(path, name);
			return go.GetComponent<T>();
		}
	}
}

