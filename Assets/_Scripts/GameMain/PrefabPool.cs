namespace GameMain
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class PrefabPool {
		// 预设
		private GameObject _prefab;
		// 自定义缓存名
		private string _name;
		// 是否是UI预设
		private bool _isUIPrefab = false;
		// 缓存UI预设
		public static GameObject UIRootInstance{set; get;}
		// 缓存非UI预设
		public static GameObject RootInstance{set; get;}
		// 记录所有该预设的实例
		private Queue<GameObject> _myQueue;
		// 
		private int Spawan;

		/// <summary>
		/// 缓存池对象，负责创建缓存
		/// </summary>
		/// <param name="gameObject"></param>
		public PrefabPool(string path, string name){
			PoolInit(path, name);
			HidePrefab(_prefab);
		}
	
		public PrefabPool(string path, string name, bool isUIPrefab)
		{
			_isUIPrefab = isUIPrefab;
			PoolInit(path, name);
			HidePrefab(_prefab);
		}

		public void DestroyPool()
		{
			_prefab = null;
			foreach(GameObject go in _myQueue)
			{
				GameObject.Destroy(go);
			}
			_myQueue.Clear();
			_myQueue = null;
		}

		private void HidePrefab(GameObject go)
		{
			go.SetActive(false);
		}

		private void PoolInit(string path, string name)
		{
			_myQueue = new Queue<GameObject>();
			_name = name;
			_prefab = Resources.Load(path) as GameObject;
		}

		public static GameObject GetRoot(bool isUIPrefab)
		{
			if(isUIPrefab)
			{
				if(UIRootInstance == null)
				{
					UIRootInstance = GameObject.Find("_UIPrefabPoolInstance");
				}
				return UIRootInstance;
			}else{
				if(RootInstance == null)
				{
					RootInstance = GameObject.Find("_PrefabPoolInstance");
				}
				return RootInstance;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private GameObject Create()
		{
			if(_prefab != null)
			{
				GameObject go = GameObject.Instantiate(_prefab);
				_myQueue.Enqueue(go);
				return go;
			}else{
				return null;
			}
		}

	}

}

