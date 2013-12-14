using System;
using System.Data.OleDb;
using System.Data;

namespace PersistenceLayer
{
	/// <summary>
	/// ColumnMap 的摘要说明。
	/// </summary>
	class ColumnMap
	{

		private string m_Name;
		private ColumnKeyTypes _KeyType;
		private TableMap _TableMap;
		private DbType m_Type;
		private bool m_AutoValue = false;

		//Constructor
		public ColumnMap(string name,TableMap table)
		{
			this.m_Name=name;
			this._TableMap=table;
		}
		public TableMap Table				//对应的TableMap
		{
			get{return _TableMap;}
		}
//		public string GetFullyQualifiedName()
//		{
//			return(_TableMap.Name +"."+m_Name+"");
//		}
		public string GetFullNameWithNoDot()
		{
			return(_TableMap.Name+m_Name);
		}

		/*属性*/
		
		//列名
		public string Name					
		{
			get{return m_Name;}
		}
		//数据类型
		public DbType Type
		{
			get{return this.m_Type;}
			set{this.m_Type=value;}
		}
		//键类型
		public ColumnKeyTypes KeyType
		{
			get{return _KeyType;}
			set{_KeyType=value;}
		}
		//是否自动生成值
		public bool IsAutoValue
		{
			get{return m_AutoValue;}
			set{m_AutoValue = value;}
		}
	}
}
