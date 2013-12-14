using System;

namespace PersistenceLayer
{
	/// <summary>
	/// OrderEntry 的摘要说明。
	/// </summary>
	struct OrderEntry
	{
		public string AttributeName;
		public bool   IsAscend;

		public OrderEntry(string attrName,bool isAsc)
		{
			AttributeName=attrName;
			IsAscend=isAsc;
		}
	}
}
