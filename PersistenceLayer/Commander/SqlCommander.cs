using System;
using System.Collections;
//using System.Data.OleDb;
using System.Data;
using System.Text;

namespace PersistenceLayer
{
	/// <summary>
	/// SqlCommand 的摘要说明。
	/// </summary>
	abstract class SqlCommander
	{
		
		protected string sql="";								//保存Sql语句
		protected string partForObject = ""; 
		protected ClassMap clsMap;							//保存ClassMap

		//Constructor
		public SqlCommander(ClassMap cm)
		{
			clsMap=cm;
		}

		public ClassMap ThisClassMap
		{
			get{return clsMap;}	
		}
		
		public string SqlString
		{
			get{return sql;}
		}
		
		public abstract IDbCommand BuildForObject(EntityObject obj);

        public abstract IDbCommand BuildForObject(EntityObject obj, bool WithNoLock);
		
		//添加Sql Clause
		protected void AddSqlClause(string s)
		{
			sql=sql+s;
		}
	
		//获取条件查询字符串
		public static string BuildForConditions (ArrayList conditions)
		{
//			Condition c;
//			SelectionCriteria selection;
//			int sizeOfConditions;
//			sizeOfConditions = conditions.Count;
//			string sql = null;
//
//			for (int i = 0;i < sizeOfConditions; i++)
//			{
//				c = (Condition) conditions[i];
//				int size = c.Parameters.Count;
//				//对一个Condition构建
//				if (size > 0)
//				{
//					if (sql == null) 
//						sql = "(";
//					else
//						sql += " OR (";
//					selection = (SelectionCriteria)c.Parameters [0];
//					sql += selection.AsSqlClause ();
//					for (int j = 1 ; j < size;j ++)
//					{
//						selection = (SelectionCriteria)c.Parameters [j];
//						sql += c.BooleanOperator + selection.AsSqlClause ();
//					}
//					sql += ")";
//				}
//			}
//			return sql;




			//tintown add 

			Condition c;
			//SelectionCriteria selection;
			
			int sizeOfConditions;
			sizeOfConditions = conditions.Count;
			string sql = null;

			for (int i = 0;i < sizeOfConditions; i++)
			{
				c = (Condition) conditions[i];
				int size = c.Parameters.Count;
				//对一个Condition构建
				if (size > 0)
				{
					if (sql == null) 
						sql = "(";
					else
						sql += " OR (";

					sql +=GetConditionUnit(c.Parameters,0);
					
					for (int j = 1 ; j < size;j ++)
					{
						sql += c.BooleanOperator + GetConditionUnit(c.Parameters,j);
					}
					sql += ")";
				}
			}
			return sql;
		}

		private static string GetConditionUnit(IList parameters,int index)
		{
			OrGroup orgroup;
			SelectionCriteria selection;
			string sql="";
			if(parameters[index] is SelectionCriteria)
			{
				selection = (SelectionCriteria)parameters [index];
				sql += selection.AsSqlClause ();
			}
			if(parameters[index] is OrGroup)
			{
				orgroup=(OrGroup)parameters[index];
				sql +=orgroup.AsSqlClause();
						
			}

			return sql;
		}
	}
}
