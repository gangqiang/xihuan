using System;
using System.Collections;

namespace PersistenceLayer
{
	/// <summary>
	/// IConfigLoader 的摘要说明。
	/// </summary>
	interface IConfigLoader
	{
		//方法
		void LoadConfig(string databaseMap);
		//属性
		Hashtable DatabaseMaps{get;}
		Hashtable ClassMaps{get;}
		IDictionary DatabasePool{get;}
	}
}
