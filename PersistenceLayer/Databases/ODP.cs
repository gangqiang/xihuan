using System;
using System.Data;
//using System.Data.Common;
using System.Collections;
using Oracle.DataAccess.Client;

namespace PersistenceLayer
{
	/// <summary>
	/// ODP 的摘要说明。
	/// </summary>
	class ODP :RelationalDatabase
	{
		//使用此值来判断日期比较的特殊性，Oracle的话，会对日期进行to_date()的转换
		private const DatabaseVendor VENDOR_NAME = DatabaseVendor.Oracle;
		public ODP ()
		{
			this.Vendor = VENDOR_NAME;
		}

		private ODP (string name ,string connectionString )
		{
			this.Vendor = VENDOR_NAME;
			this.cnnString = connectionString;
			this.connection = new OracleConnection(connectionString);
			this.Name = name;
		}

		public override int InsertRecord (IDbCommand cmd ,out object identity)
		{
			throw new PlException ("不支持列自动编号功能！");
			//return 0;
		}

		public override RelationalDatabase GetCopy()
		{	
			return new ODP (this.Name,this.cnnString);
		}

		//返回一个DataTable
		public override DataTable AsDataTable(IDbCommand cmd)
		{
			cmd.Connection=this.connection;
			cmd.Transaction = this.transaction;
			OracleDataAdapter adapter = new OracleDataAdapter((OracleCommand)cmd);
			
			DataTable dt=new DataTable();
			adapter.Fill(dt);
			return dt;
		}

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public override DataSet AsDataSet(IDbCommand cmd)
        {
            cmd.Connection = this.connection;
            cmd.Transaction = this.transaction;
            OracleDataAdapter adapter = new OracleDataAdapter((OracleCommand)cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

		/// <summary>
		/// 获取前N条记录(未实现Oracle功能)
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
			cmd.CommandText=PersistenceBroker.AddOracleTopWhere(cmdText,top);		
			OracleDataAdapter adapter = new OracleDataAdapter((OracleCommand)cmd);
			DataTable dt=new DataTable();
			adapter.Fill(dt);
			return dt;
		}

		//返回一个SqlDataAdpter
		public override IDataAdapter GetAdapter(IDbCommand cmd)
		{
			OracleDataAdapter adapter = new OracleDataAdapter();
			adapter.SelectCommand=(OracleCommand)cmd;
			cmd.Connection=connection;
			return adapter;
		}
		
		//返回一个DataRow
		public override DataRow GetDataRow(IDbCommand cmd)
		{
			cmd.Connection=this.connection;				//添加连接
			DataRow r ;

			OracleDataAdapter adapter=new OracleDataAdapter();
			adapter.SelectCommand= (OracleCommand) cmd;
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
				OracleConnection cnn=new OracleConnection(cnnString);
				this.connection=cnn;
				this.connection.Open();
			}
			catch(OracleException e)
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
			return ":"+i.ToString();
			
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
					return "nvarchar2(50)";

			}
			
		}

		//错误处理
		public override ErrorTypes ErrorHandler (Exception e,out string message) 
		{
			message = "";
			if (e is OracleException)
			{
				OracleException oleErr = (OracleException) e; 
				for(int i=0; i <oleErr.Errors.Count;i++)
				{
					message += "Index #" + i + "\n" +
						"Message: " + oleErr.Message + "\n" +
						"Number: " + oleErr.Errors[i].Number.ToString() + "\n" +
						"Source: " + oleErr.Errors[i].Source + "\n" ;
				}
				return ErrorTypes.DatabaseUnknwnError;
			}
			else
			{
				return ErrorTypes.Unknown;
			}
		}

	}
}
