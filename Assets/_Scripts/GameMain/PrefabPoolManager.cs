namespace Eliminate.Main
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using Eliminate.Common;

	/// <summary>
	/// 缓存池管理类
	/// </summary>
	public class PrefabPoolManager : TSingleTon<PrefabPool> {
		
		private Dictionary<string, PrefabPool>[] prefabPools = new Dictionary<string, PrefabPool>[3];

		/// <summary>
		/// 阻止类的实例化
		/// </summary>
		private PrefabPoolManager()
		{

		}

		public void ClearPrefabPools()
		{
			for(int i = 0; i < prefabPools.Length; i++)
			{
				foreach(KeyValuePair<string, PrefabPool> pair in prefabPools[i])
				{
					pair.Value.DestroyPool();
				}
				prefabPools[i].Clear();
			}
		}

		public void Duplicate(string name, PrefabType type)
		{
			// 目标文件夹地址
			string folderPath = string.Empty;
			switch(type)
			{
				case PrefabType.PIECE:
				folderPath = "Pieces";
				break;
				case PrefabType.EFFECT:
				folderPath = "Pieces";
				break;
				case PrefabType.UI:
				folderPath = "UI";
				break;
			}
			PrefabPool pool = new PrefabPool(folderPath, name);
			prefabPools[(int)type].Add(name, pool);
		}

	}
}

