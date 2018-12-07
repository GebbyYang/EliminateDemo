namespace Eliminate.Common
{
	using System.Collections;
	using System.Collections.Generic;

	public class StringUtils {
		
		public static int StringToInt(string str)
		{
			int result = default(int);
			if(str.Trim() == "")
			{
				return result;
			}
			int.TryParse(str, out result);
			return result;
		}

		public static float StringToFloat(string str)
		{
			float result = default(float);
			if(str.Trim() == "")
			{
				return result;
			}
			float.TryParse(str, out result);
			return result;
		}

		public static uint StringToUInt(string str)
		{
			uint result = default(uint);
			if(str.Trim() == "")
			{
				return result;
			}
			uint.TryParse(str, out result);
			return result;
		}

		public static long StringToLong(string str)
		{
			long result = default(long);
			if(str.Trim() == "")
			{
				return result;
			}
			long.TryParse(str, out result);
			return result;
		}

		public static ulong StringToULong(string str)
		{
			ulong result = default(ulong);
			if(str.Trim() == "")
			{
				return result;
			}
			ulong.TryParse(str, out result);
			return result;
		}

	}
}

