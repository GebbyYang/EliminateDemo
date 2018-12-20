namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System;
	using System.Text;

	public class FileUtils {

		/// <summary>
		/// 使用数据表
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <param name="isEncrypt">是否加密</param>
		/// <returns></returns>
		public static MemoryStream ReadTable(string tableName, bool isEncrypt = false)
		{
			try{
				MemoryStream buffer = ReadFileByBytes(tableName);
				return buffer;
			}catch(Exception e)
			{
				ToolFunctions.LogException(e);
				return new MemoryStream();
			}
		}

		/// <summary>
		/// 使用FileStream读取文件，然后转换到MemoryStream中
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <returns></returns>
		public static MemoryStream ReadFileByBytes(string tableName)
		{
			string tablePath = PathUtils.GetTablePath(tableName);
			// PC端
			if(File.Exists(tablePath))
			{
				using(FileStream fileStream = File.OpenRead(tablePath))
				{
					if(fileStream == null)
					{
						return null;
					}
					MemoryStream memoryStream = new MemoryStream();
					StreamDataTansfer(fileStream, memoryStream);
					return memoryStream;
				}
			}
			return null;
		}

		/// <summary>
		/// 输出数据到文件
		/// </summary>
		/// <param name="data">数据</param>
		/// <param name="name">文件名</param>
		public static void WriteStringToFile(string data, string name)
		{
			string configPath = PathUtils.GetLevelConfigPath(name);
			using(FileStream fileStream = File.Open(configPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
			{
				// With BOM
				byte[] byteDate = new UTF8Encoding(true).GetBytes(data);
				fileStream.Write(byteDate, 0, byteDate.Length);
			}
		}

		/// <summary>
		/// 将数据从一个Stream转移到另一个Stream
		/// </summary>
		public static void StreamDataTansfer(Stream sour, Stream des, int bufferSize = 1024)
		{
			byte[] buffer = new byte[bufferSize];
			int hasRead;
			while((hasRead = sour.Read(buffer, 0, bufferSize)) > 0)
			{
				des.Write(buffer, 0, hasRead);
			}
		}

	}
}

