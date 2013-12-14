using System;
using System.Collections;
using System.Data;


namespace PersistenceLayer
{
	/// <summary>
	///		RelationalDatabase 
	/// </summary>
	abstract class  RelationalDatabase
	{
		private string m_Name;								//数据库名字
		
		protected string m_QuotationMarksStart = "\"";
		protected string m_QuotationMarksEnd = "\"";
		protected DatabaseVendor m_vendor = DatabaseVendor.MsSqlServer;				//数据库平台

		protected IDbConnection connection=null;			//数据库连接
		protected IDbTransaction transaction=null;		//数据库事务
		protected bool isInTransaction=false;				//是否在事务中	
		protected string cnnString="";

	
		//Constructor
		public RelationalDatabase()
		{

		}

		

				
		public RelationalDatabase(string name)
		{
			m_Name=name;
		}
		
		public IDbCommand GetCommand ()
		{
			return this.connection.CreateCommand ();
		}


		//打开数据库
		public void Open()
		{
			if (connection.State==ConnectionState.Closed) 
			{
				connection.Open();
			}
		}
		
		//关闭数据库
		public void Close()
		{
			if (this.connection.State==ConnectionState.Open)
				connection.Close();
			this.isInTransaction=false;
		}
		
		//启动一个事务
		public void BeginTransaction()
		{
			if ((connection==null)||(connection.State==ConnectionState.Closed))
			{
				throw new PlException("数据库未打开或未初始化！");
			}
			else
			{
				transaction=connection.BeginTransaction();
				isInTransaction=true;
			}
		}

		//提交一个事务
		public void CommitTransaction()
		{
			if (transaction!=null)
			{
				transaction.Commit();
				isInTransaction=false;
				//transaction=null;
			}
			else
			{
				throw new PlException("无可用事务！");
			}
		}
		//回滚一个事务
		public void RollbackTransaction()
		{
			if (transaction!=null)
			{
				transaction.Rollback();
				isInTransaction=false;
				transaction=null;
			}
//			else
//			{
//				throw new PlException("无可用事务！");
//			}
		}

		//执行一个命令
		public int DoCommand(IDbCommand cmd)
		{
			int result=0;
			if(isInTransaction) cmd.Transaction=transaction;
			cmd.Connection=connection;
			result = cmd.ExecuteNonQuery();
			return result;
		}

		/// <summary>
		/// 执行一条SQL语句 
		/// add by tintown at 2004-09-06
		/// </summary>
		/// <param name="sqlstring">语句</param>
		/// <returns>影响条数</returns>
		public int DoSql(string sqlstring)
		{
			int result=0;
			IDbCommand cmd=GetCommand();
			if(isInTransaction) cmd.Transaction=transaction;
			cmd.Connection=connection;
			cmd.CommandText=sqlstring;
			result = cmd.ExecuteNonQuery();
			return result;
		}

		public IDataReader GetDataReader(IDbCommand cmd)
		{
			cmd.Connection = this.connection;
			cmd.Transaction = this.transaction;
			IDataReader reader = cmd.ExecuteReader();
			return reader;
		}

		//根据属性值得到正确的Sql值
		public string GetSqlValue(object obj)
		{
			string type = obj.GetType().ToString();
			type = type.Substring (type.LastIndexOf('.')+1,type.Length);
			
			switch(type)
			{
				case "Int16":
					return obj.ToString();
				case "Int32":
					return obj.ToString();
				case "Int64":
					return obj.ToString();
				case "Single":
					return obj.ToString();
				case "String":
					return "'" + obj.ToString() + "'";
				case "Date":
					return "'" +obj.ToString() + "'";
				case "Guid":
					return "'" + obj.ToString() + "'";
				default:
					throw new PlException("目前不支持的数据库类型！");
			}
		}
		public string GetStringAnd()
		{
			return "AND";
		}
		public string GetStringAscend()
		{
			return "ASC";
		}
		public string GetStringBetween()
		{
			return "BETWEEN";
		}
		public string GetStringDelete()
		{
			return "DELETE";
		}
		public string GetStringDescend()
		{
			return "DESC";
		}

		public string GetStringEuqalTo()
		{
			return "=";
		}
		public string GetStringForUpdate()
		{
			return "FOR UPDATE";
		}
		public string GetStringFrom()
		{
			return "FROM";
		}
		public string GetStringHaving()
		{
			return "HAVING";
		}
		public string GetStringIn()
		{
			return "IN";
		}
		public string GetStringInsertInto()
		{
			return "INSERT INTO";
		}
		public string GetStringIs()
		{
			return "IS";
		}
		public string GetStringLike()
		{
			return "LIKE";
		}
		public string GetStringNot()
		{
			return "NOT";
		}
		public string GetStringOr()
		{
			return "OR";
		}
		public string GetStringOrderBy()
		{
			return "ORDER BY";
		}
		public string GetStringSelect()
		{
			return "SELECT";
		}
		public string GetStringSet()
		{
			return "SET";
		}
		public string GetStringUpdate()
		{
			return "UPDATE";
		}
		public string GetStringValues()
		{
			return "VALUES";
		}
		public string GetStringWhere()
		{
			return "WHERE";
		}
		public string GetLeftJoin()
		{
			return "Left Join";
		}

		/// <summary>
		/// 根据列名生成带有间隔符的列名
		/// 如Password 生成为[Password]或者"Password"
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public string GetQuotationColumn(string columnName)
		{
			return QuotationMarksStart + columnName +QuotationMarksEnd ;
		}


		/*抽象方法*/
		//返回一个RelationalDatabase 
		public abstract RelationalDatabase GetCopy();

		//错误处理
		public abstract ErrorTypes ErrorHandler (Exception e,out string message) ;

		//执行插入一条记录 适用于有 自动生成标识的列
		public abstract int InsertRecord (IDbCommand cmd , out object identity);

		//数据库初始化
		public abstract void Initialize(string connectionString);

		//得到参数的字符串形式
		public abstract string GetStringParameter (string name,int i);
		
		//返回一个IDataAdpter 
		public abstract IDataAdapter GetAdapter(IDbCommand cmd);

		//返回一个DataTable
		public abstract DataTable AsDataTable(IDbCommand cmd);
        //返回一个DataTable
        public abstract DataSet AsDataSet(IDbCommand cmd);

		//返回前N条记录DataTable
		public abstract DataTable AsDataTable(IDbCommand cmd,int top);

		//返回一个DataRow
		public abstract DataRow GetDataRow(IDbCommand cmd);
		
		public abstract SqlValueTypes SqlValueType (DbType type);

		public abstract string GetDbTypeString(DbType type);

		#region /*属性*/

		public string ConnectionString
		{
			get{return this.cnnString;}
		}
		//数据库名
		public string Name
		{
			get{return m_Name;}
			set{m_Name=value;}
		}
	
		public bool IsInTransaction
		{
			get{return this.isInTransaction;}
		}

		//数据库平台名
		public DatabaseVendor Vendor
		{
			get {return this.m_vendor;}
			set {this.m_vendor = value;}
		}
		
		public string QuotationMarksStart
		{
			get{return this.m_QuotationMarksStart;}
			set{this.m_QuotationMarksStart = value;}
		}
		public string QuotationMarksEnd
		{
			get {return this.m_QuotationMarksEnd;}
			set {this.m_QuotationMarksEnd = value;}
		}
		
		#endregion

	}
}
