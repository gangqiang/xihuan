using System;
using System.Data;
//using System.Data.Common;
using System.Collections;
using MySql.Data.MySqlClient;

namespace PersistenceLayer
{
	/// <summary>
	/// ODP 的摘要说明。
	/// </summary>
	class MySql :RelationalDatabase
	{
		//使用此值来判断日期比较的特殊性，MySql的话，会对日期进行to_date()的转换
		private const string GET_IDENTITY = "SELECT @@IDENTITY ";
		private const DatabaseVendor VENDOR_NAME = DatabaseVendor.MySql;
		public MySql ()
		{
			this.Vendor = VENDOR_NAME;
			this.m_QuotationMarksStart = "`";
			this.m_QuotationMarksEnd = "`";
		}

		private MySql (string name ,string connectionString )
		{
			this.Vendor = VENDOR_NAME;
			this.cnnString = connectionString;
			this.connection = new MySqlConnection(connectionString);
			this.Name = name;
		}

		public override int InsertRecord (IDbCommand cmd ,out object identity)
		{
			int result = 0;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			result = cmd.ExecuteNonQuery();
	
			cmd.CommandText = GET_IDENTITY;
			identity =cmd.ExecuteScalar();
			
			return result;
		}

		public override RelationalDatabase GetCopy()
		{	
			return new MySql (this.Name,this.cnnString);
		}

		//返回一个DataTable
		public override DataTable AsDataTable(IDbCommand cmd)
		{
			cmd.Connection=this.connection;
			cmd.Transaction = this.transaction;
			MySqlDataAdapter adapter = new MySqlDataAdapter((MySqlCommand)cmd);
			
			DataTable dt=new DataTable();
			adapter.Fill(dt);
			return dt;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public override DataSet AsDataSet(IDbCommand cmd)
        {
            cmd.Connection = this.connection;
            cmd.Transaction = this.transaction;
            MySqlDataAdapter adapter = new MySqlDataAdapter((MySqlCommand)cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

		/// <summary>
		/// 获取前N条记录(未实现MySql功能)
		/// add by tintown at 2004-09-06
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="top"></param>
		/// <returns></returns>
		public override DataTable AsDataTable(IDbCommand cmd,int top)
		{
			cmd.Connection=this.connection;
			cmd.Transaction = this.transaction;
			string cmdText=cmd.CommandText;

			
			//使用ORCALE添加rownum的方法重装SQL语句
			cmd.CommandText=PersistenceBroker.AddMySqlTopWhere(cmdText,top);		
			MySqlDataAdapter adapter = new MySqlDataAdapter((MySqlCommand)cmd);
			DataTable dt=new DataTable();
			adapter.Fill(dt);
			return dt;
		}

		//返回一个SqlDataAdpter
		public override IDataAdapter GetAdapter(IDbCommand cmd)
		{
			MySqlDataAdapter adapter = new MySqlDataAdapter();
			adapter.SelectCommand=(MySqlCommand)cmd;
			cmd.Connection=connection;
			return adapter;
		}
		
		//返回一个DataRow
		public override DataRow GetDataRow(IDbCommand cmd)
		{
			cmd.Connection=this.connection;				//添加连接
			DataRow r ;

			MySqlDataAdapter adapter=new MySqlDataAdapter();
			adapter.SelectCommand= (MySqlCommand) cmd;
			DataTable dt=new DataTable();
			adapter.Fill(dt);
			
			if (dt.Rows.Count>0)
			{
				r = dt.Rows[0];
			}
			else
			{
				r = null;
			}
			return r;
		}

		public override void Initialize(string connectionString)
		{
			cnnString = connectionString;//.Replace("Provider=SQLOLEDB.1;","");;
			try
			{
				//获得连接
				MySqlConnection cnn=new MySqlConnection(cnnString);
				this.connection=cnn;
				this.connection.Open();
			}
			catch(MySqlException e)
			{
				throw new PlException("连接数据库失败！参考：" + e.Message ,ErrorTypes.DatabaseError);
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
				this.connection.Close();
			}
		}

		//返回参数的字符串形式
		public override string GetStringParameter (string name,int i)
		{
			return "?"+name;
			//return "?";
			
		}
		public override SqlValueTypes SqlValueType (DbType type)
		{
			if (type == DbType.Boolean)
			{
				return SqlValueTypes.PrototypeString;
			}
			else
			{
				return SqlValueTypes.PrototypeString;
			}
		}

		public override string GetDbTypeString(DbType type)
		{
			switch(type)
			{
				case DbType.Int32:
					return "Int";
				case DbType.Decimal:
					return "decimal(18,2)";
				default :
					return "nvarchar(50)";

			}
			
		}

		//错误处理
		public override ErrorTypes ErrorHandler (Exception e,out string message) 
		{
			message = "";
			if (e is MySqlException)
			{
				MySqlException oleErr = (MySqlException) e; 
				
					message += "Message: " + oleErr.Message + "\n" +
						"Number: " + oleErr.Number.ToString() + "\n" +
						"Source: " + oleErr.Source + "\n" ;
				
				return ErrorTypes.DatabaseUnknwnError;
			}
			else
			{
				return ErrorTypes.Unknown;
			}
		}

	}
}
