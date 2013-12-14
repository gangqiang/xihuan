using System;

namespace PersistenceLayer
{
	/// <summary>
	/// DatabaseMap 的摘要说明。
	/// </summary>
	class DatabaseMap
	{
		private string m_Name;
		public DatabaseMap(string Name)
		{
			m_Name=Name;
		}
		public string Name
		{
			get{return m_Name;}
		}
	}
}
