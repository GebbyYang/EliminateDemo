namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;
	using System.IO;
	using System;

	public class FileUtils {
		
		public static string ReadAllText(string path, bool isEncrypt = false)
		{
			return "";
		}

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

		public static MemoryStream ReadFileByBytes(string tableName)
		{
			string tablePath = PathUtils.GetTablePath(tableName);
			// PC端
			if(File.Exists(tablePath))
			{
				using(FileStream fileStream = File.OpenRead(tableName))
				{
					if(fileStream == null)
					{
						return null;
					}
					MemoryStream memoryStream = new MemoryStream();
					StreamDataTansfer(fileStream, memoryStream);
					fileStream.Flush();
					fileStream.Close();
					fileStream.Dispose();
					return memoryStream;
				}
			}
			return null;
		}

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

