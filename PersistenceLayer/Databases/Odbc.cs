using System;
using System.Data;
using System.Data.Odbc;
using System.Collections;


namespace PersistenceLayer
{
	/// <summary>
	///		MsSqlServer 数据库
	/// </summary>
	class Odbc : RelationalDatabase
	{
		private const string GET_IDENTITY = "SELECT @@IDENTITY ";
		private const DatabaseVendor VENDOR_NAME = DatabaseVendor.Odbc;
		
		public Odbc()
		{
			this.Vendor = VENDOR_NAME;
		}

		private Odbc (string name ,string connectionString )
		{
			this.Vendor = VENDOR_NAME;
			this.cnnString = connectionString;
			this.connection = new OdbcConnection (connectionString);
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
			return new Odbc (this.Name,this.cnnString);
		}


		//返回一个DataTable
		public override DataTable AsDataTable(IDbCommand cmd)
		{
			cmd.Connection=this.connection;
			cmd.Transaction = this.transaction;
			
			OdbcDataAdapter adapter = new OdbcDataAdapter ((OdbcCommand)cmd);

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
            OdbcDataAdapter adapter = new OdbcDataAdapter((OdbcCommand)cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

		/// <summary>
		/// 获取前N条记录(类似于AsDataTable的方法)
		/// </summary>
		/// <param name="cmd">Command</param>
		/// <param name="top">前N条</param>
		/// <returns></returns>
		public override DataTable AsDataTable(IDbCommand cmd,int top)
		{
			cmd.Connection=this.connection;
			cmd.Transaction = this.transaction;
			
			string cmdText=cmd.CommandText;
			if(cmdText.StartsWith("SELECT"))
			{
				cmdText="SELECT TOP "+ top.ToString() +cmdText.Substring(6);
			}

			cmd.CommandText=cmdText;
						
			OdbcDataAdapter adapter = new OdbcDataAdapter ((OdbcCommand)cmd);

			DataTable dt=new DataTable();
			adapter.Fill(dt);
			return dt;
			
		}

		//返回一个OdbcDataAdpter
		public override IDataAdapter GetAdapter(IDbCommand cmd)
		{
			OdbcDataAdapter adapter = new OdbcDataAdapter ();
			adapter.SelectCommand= (OdbcCommand)cmd;
			cmd.Connection=connection;
			return adapter;
		}
		
		public override DataRow GetDataRow(IDbCommand cmd)
		{
			cmd.Connection=this.connection;				//添加连接
			DataRow r ;

			OdbcDataAdapter adapter=new OdbcDataAdapter();
			adapter.SelectCommand= (OdbcCommand) cmd;
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
			cnnString = connectionString.Replace("Provider=SQLOLEDB.1;","");;
			
			try
			{
				//获得连接
				OdbcConnection cnn=new OdbcConnection( cnnString);
				this.connection = cnn;
				this.connection.Open();
			}
			catch(OdbcException e)
			{
				if(e.Errors[0].NativeError==17)
					throw new PlException("数据库不存在！参考：" + e.Message ,ErrorTypes.DatabaseConnectionError);
				else
					throw new PlException("连接数据库失败！参考：" + e.Message ,ErrorTypes.DatabaseError);
			}
			finally
			{
				this.connection.Close();
			}
		}

		//返回参数的字符串形式
		public override string GetStringParameter (string name,int i)
		{
			//return "@" + name;
			return "?";

		}
		
		public override SqlValueTypes SqlValueType (DbType type)
		{
			if (type == DbType.Boolean)
			{
				return SqlValueTypes.BoolToInterger;
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
			if (e is OdbcException)
			{
				OdbcException sqlErr = (OdbcException)e;
				int j = 0;
				for (j = 0;j < sqlErr.Errors.Count ;j++)
				{
					if (sqlErr.Errors[j].NativeError != 3621) break;
				}
				switch (sqlErr.Errors[j].NativeError)
				{
					case 2627:
						message = "数据重复！";
						return ErrorTypes.NotUnique;
					case 8152:
						return ErrorTypes.DataTooLong;
					case 515:
						message = "参考：" + sqlErr.Message;
						return ErrorTypes.NotAllowDataNull;
					case 0:
						return ErrorTypes.DataTypeNotMatch;
					case 544:
						message = "参考：" + sqlErr.Message;
						return ErrorTypes.AutoValueOn;
					case 547:
						message = "参考：" + sqlErr.Message;
						return ErrorTypes.RestrictError;
					case 8178:
						message = "参考：" + sqlErr.Message;
						return ErrorTypes.RequireAttribute;
				}
				message = "数据库操作异常:";
				for(int i =0; i <sqlErr.Errors.Count;i++)
				{
					message += "Index #" + i + "\n" +
						"Message: " + sqlErr.Message + "\n" +
						"Native: " + sqlErr.Errors[i].NativeError.ToString() + "\n" +
						"Source: " + sqlErr.Errors[i].Source + "\n" ;
				}
				return ErrorTypes.DatabaseUnknwnError;
			}
			else
			{
				message = "";
				return ErrorTypes.Unknown;
			}
		}
	}
}
