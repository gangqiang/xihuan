using System;
using System.Collections;

namespace PersistenceLayer
{
	/// <summary>
	///	UpdateCriteria 更新标准
	///	可以完成一个实体上的指定条件的指定更新字段操作
	///	生成Update table set field=value where ...
	///	这是批量更新模式
	/// </summary>
	public class UpdateCriteria : PersistentCriteria
	{
		
		private string _sqlString;
		private ClassMap _classMap ;
		private ArrayList _conditions = new ArrayList ();
		private PersistenceBroker _broker = PersistenceBroker.Instance ();
		
		

		private Hashtable m_ForUpdateCollection=new Hashtable();
		
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="classType">类型</param>
		public UpdateCriteria(Type classType)
		{
			this.ForClass=classType;
			this.CriteriaType = CriteriaTypes.Update;
			this._classMap=_broker.GetClassMap(classType.Name);
			this._databaseName=this._classMap.Database.Name ;
			this._IsSaveInMemory=this._classMap.IsSaveToMemory;
						
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="classType">类型</param>
		/// <param name="databaseName">数据源名称</param>
		public UpdateCriteria(Type classType,string databaseName)
		{
			this.ForClass=classType;
			this.CriteriaType = CriteriaTypes.Update;
			this._classMap=_broker.GetClassMap(classType.Name);
			this._databaseName=databaseName;
			this._IsSaveInMemory=this._classMap.IsSaveToMemory;
						
		}
		
		/// <summary>
		///		执行批更新操作
		/// </summary>
		/// <returns>受影响的行数</returns>
		public int Perform()
		{
			//将需要更新的值存入集合
//			for (int i =1;i< this.m_ForUpdateCollection.Count ;i=i+2)
//			{
//				this.m_ForUpdateCollection[i] = templateObjectForUpdate.GetAttributeValue (
//					this.m_ForUpdateCollection[i-1].ToString());
//			}
//		
//			PersistenceBroker broker = PersistenceBroker.Instance();
//			return broker.ProcessCriteria (this);
			if(this._databaseName==null)
				this._databaseName=this._classMap.Database.Name;
            Logger.Write(this.SqlString);
			return _broker.ProcessCriteria(this);
			 
		}

		/// <summary>
		///		得到一个新条件类对象
		/// </summary>
		/// <returns>Condition实例</returns>
		public Condition GetNewCondition()
		{
			//return this.m_Retrieve.GetNewCondition();

			Condition c = new Condition (this._classMap);
			this._conditions.Add (c);
			return c;
		}

		/// <summary>
		///	增加一个需要更新属性
		///	由此方法添加需要更新的字段与值
		/// </summary>
		public void AddAttributeForUpdate (string attributeName,object attributeValue)
		{
			
			this.m_ForUpdateCollection.Add(attributeName,attributeValue);
//			this.m_ForUpdateCollection.Add (attributeName);
//			this.m_ForUpdateCollection.Add (templateObjectForUpdate.GetAttributeValue (attributeName));
		}


		internal void BuildStringForUpdate()
		{
			string where = SqlCommander.BuildForConditions (this._conditions);

			this._sqlString="UPDATE ";
			this._sqlString +=this.classMap.RelationalDatabase.QuotationMarksStart+this.classMap.Table.Name+this.classMap.RelationalDatabase.QuotationMarksEnd;
			this._sqlString +=" SET ";
			int i=0;
			foreach(object key in this.m_ForUpdateCollection.Keys)
			{
				this._sqlString +=this._classMap.RelationalDatabase.QuotationMarksStart+this._classMap.GetAttributeMap(key.ToString()).Column.Name+this._classMap.RelationalDatabase.QuotationMarksEnd+"=";
				this._sqlString +=this._classMap.RelationalDatabase.GetStringParameter(key.ToString(),i);
				this._sqlString +=",";
				i++;
			}

//			//tintown added at 2005-3-22 添加对timestamp的更新
			//由于在更新标准时不进行timestamp判断，所以不需要更新timestamp值，要不然，会导致事务中的同一表更新不成功。
//			if(_classMap.TimestampAttribute!=null)
//			{
//				ColumnMap cm=_classMap.TimestampAttribute.Column ;
//				this._sqlString+=cm.Name+"=";
//				this._sqlString+=this._classMap.RelationalDatabase.GetStringParameter("Update"+cm.Name,i);
//				this._sqlString+=",";
//			}


			char[] chars=new char[]{','};
			this._sqlString=this._sqlString.TrimEnd(chars);

			
			
			if (where != null)
				_sqlString += " WHERE " + where ;

			
		}

	

		internal Hashtable ForUpdateCollection 
		{
			get {return this.m_ForUpdateCollection;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string SqlString
		{
			get
			{
				if(this._sqlString==null || this._sqlString.Length==0)
					BuildStringForUpdate();
                //Logger.Write(this.SqlString);   // add by duhu
				return this._sqlString;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		internal ClassMap classMap
		{
			get
			{
				return this._classMap;
			}
		}
		
	}
}
