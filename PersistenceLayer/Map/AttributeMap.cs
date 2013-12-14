using System;

namespace PersistenceLayer
{
	/// <summary>
	/// Attribute 的摘要说明。
	/// </summary>
	class AttributeMap
	{
		private string m_Name;
		private ColumnMap m_ColumnMap;
		private AttributeMap _reference;
		
		public SqlValueTypes SqlValueStringType = SqlValueTypes.PrototypeString;

		//Constructor
		public AttributeMap(string name)
		{
			m_Name=name;
		}
		public string Name
		{
			get{return m_Name;}
		}
		//the ColumnMap object for this attribute or null if this attribute is not mapped to any column.
		public ColumnMap Column
		{
			get{return m_ColumnMap;}
			set{m_ColumnMap=value;}
		}
		//the referenced AttributeMap for this attribute
		public AttributeMap Reference
		{
			get{return _reference;}
			set{_reference=value;}
		}

	}
}
