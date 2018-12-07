namespace Eliminate.Main
{
	using System.Reflection;
	using System;

	/// <summary>
	/// 单例泛型类
	/// </summary>
	public class TSingleTon<T>
	{
		private static object _singletonLock;

		private static T _singleton;

		static TSingleTon()
		{
			_singletonLock = new object();
		}

		public static T Singleton()
		{
			if(_singleton == null)
			{
				lock(_singletonLock)
				{
					if(_singleton == null)
					{
						// 反射实例化，访问私有构造函数
						ConstructorInfo info = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
						if(info != null)
						{
							_singleton = (T)info.Invoke(null);
						}
					}
				}
			}
			return _singleton;
		}
	}
}


