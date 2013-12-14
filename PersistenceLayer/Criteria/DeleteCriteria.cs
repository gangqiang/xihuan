using System;
using System.Collections;
using System.Data;

namespace PersistenceLayer
{
	/// <summary>
	///		DeleteCriteria 该类根据Condition对象指定过滤条件删除一个或多个实体对象
	/// </summary>
	public class DeleteCriteria : PersistentCriteria
	{
		
		private string _sqlString;
		private ClassMap _classMap ;
		private ArrayList _conditions = new ArrayList ();
		private PersistenceBroker _broker = PersistenceBroker.Instance ();
		
				
		
	
		

	
		
		
		/// <summary>
		///		生成一个DeleteCriteria
		/// </summary>
		/// <param name="classType">需要处理实体对象的Tpye对象</param>
		public DeleteCriteria(Type classType)
		{
			this.ForClass=classType;
			this.CriteriaType = CriteriaTypes.Delete;
			this._classMap = _broker.GetClassMap (classType.Name);
			this._databaseName=this._classMap.Database.Name ;
			this._IsSaveInMemory=this._classMap.IsSaveToMemory;
			
		}

		/// <summary>
		/// 生成一个DeleteCriteria
		/// </summary>
		/// <param name="classType">实体类型</param>
		/// <param name="databaseName">数据源名称</param>
		public DeleteCriteria(Type classType,string databaseName)
		{
			this.ForClass=classType;
			this.CriteriaType = CriteriaTypes.Delete;
			this._classMap = _broker.GetClassMap (classType.Name);
			this._databaseName=databaseName;
			this._IsSaveInMemory=this._classMap.IsSaveToMemory;
			
			
		}
		
		/// <summary>
		///		执行删除操作
		/// </summary>
		/// <returns>受影响的行数</returns>
		public int Perform()
		{
			//this.m_Criteria.Perform();
			//PersistenceBroker broker=PersistenceBroker.Instance();
			//return broker.ProcessDeleteCriteria(this);
			if(this._databaseName==null)
				this._databaseName=this._classMap.Database.Name;

			return _broker.ProcessCriteria(this);
			
//			IDbCommand  cmd = this._broker.GetCommand (_databaseName);
//			this.BuildStringForDelete();
//
//			cmd.CommandText=this._sqlString;
//			int infect=_broker.Execute(_databaseName,cmd);
//			if(infect>0 && this._IsSaveInMemory==true)
//			{
//					
//
//			}
//			return infect;


			//return _broker.ProcessCriteria (this.m_Criteria);
		}

		/// <summary>
		///		得到一个新条件类对象 
		/// </summary>
		/// <returns>Condition对象，可以在Condition对象指定过滤条件</returns>
		public Condition GetNewCondition()
		{
			//return this.m_Retrieve.GetNewCondition();

			Condition c = new Condition (this._classMap);
			this._conditions.Add (c);
			return c;
		}

		internal void BuildStringForDelete ()
		{
			this._sqlString = _classMap.DeleteFromClause;

			string where = SqlCommander.BuildForConditions (this._conditions);
			if (where != null)
				_sqlString += " WHERE " + where ;
			
		}

	
		
		
	
		/// <summary>
		/// 
		/// </summary>
		public string SqlString
		{
			get
			{
				if(this._sqlString==null || this._sqlString.Length==0)
					this.BuildStringForDelete();
                //Logger.Write(this._sqlString);   //add by duhu
				return this._sqlString;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public string classMapName
		{
			get
			{
				return this._classMap.Name;
			}
		}

	

	
	}
}
