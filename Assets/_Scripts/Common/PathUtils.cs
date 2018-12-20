namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class PathUtils {
		
		/// <summary>
		/// 组合表的路径
		/// </summary>
		public static string GetTablePath(string tableName)
		{
			// string path = Application.streamingAssetsPath + @"/Table/" + tableName;
			string path = string.Format("{0}/Table/{1}.csv", Application.streamingAssetsPath, tableName);
			return path;
		}

		/// <summary>
		/// 组合文件的路径
		/// </summary>
		public static string GetLevelConfigPath(string configName)
		{
			string path = string.Format("{0}/Level/{1}.json", Application.streamingAssetsPath, configName);
			return path;
		}

	}
}

