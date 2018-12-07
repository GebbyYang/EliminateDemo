namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class PathUtils {
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="tableName">获取表绝对地址</param>
		/// <returns></returns>
		public static string GetTablePath(string tableName)
		{
			return string.Format("{0}/Table/{1}", Application.streamingAssetsPath, tableName);
		}

	}
}

