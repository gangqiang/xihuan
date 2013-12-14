using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;

namespace PersistenceLayer
{
	/// <summary>
	///		MsAccess ���ݿ�
	/// </summary>
	class MsAccess : RelationalDatabase
	{
		internal const DatabaseVendor VENDOR_NAME =DatabaseVendor.MsAccess; 
		private const string GET_IDENTITY = "SELECT @@IDENTITY ";
		
		public MsAccess()
		{
			this.Vendor = VENDOR_NAME;
			this.m_QuotationMarksStart = "[";
			this.m_QuotationMarksEnd = "]";

		}

		private MsAccess (string name ,string connectionString )
		{
			this.Vendor = VENDOR_NAME;
			this.cnnString = connectionString;
			this.connection = new OleDbConnection (connectionString);
			this.Name = name;
			this.m_QuotationMarksStart = "[";
			this.m_QuotationMarksEnd = "]";
		}

		public override int InsertRecord (IDbCommand cmd ,out object identity)
		{
			int result = 0;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			result = cmd.ExecuteNonQuery();
			cmd.CommandText = GET_IDENTITY;
			identity = cmd.ExecuteScalar ();
			
			return result;
		}

		public override RelationalDatabase GetCopy()
		{	
			return new MsAccess (this.Name,this.cnnString);
		}

		//����һ��DataTable
		public override DataTable AsDataTable(IDbCommand cmd)
		{
			cmd.Connection=this.connection;
			cmd.Transaction = this.transaction;
			OleDbDataAdapter adapter = new OleDbDataAdapter((OleDbCommand)cmd);
			
			DataTable dt=new DataTable();
			adapter.Fill(dt);
			return dt;
		}

        /// <summary>
        /// ����һ��DataSet
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public override DataSet AsDataSet(IDbCommand cmd)
        {
            cmd.Connection = this.connection;
            cmd.Transaction = this.transaction;
            OleDbDataAdapter adapter = new OleDbDataAdapter((OleDbCommand)cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

		/// <summary>
		/// ��ȡǰ������¼
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
			if(cmdText.StartsWith("SELECT"))
			{
				cmdText="SELECT TOP "+ top.ToString() +cmdText.Substring(6);
			}

			cmd.CommandText=cmdText;

			OleDbDataAdapter adapter = new OleDbDataAdapter((OleDbCommand)cmd);
			
			DataTable dt=new DataTable();
			adapter.Fill(dt);
			return dt;
		}


		//����һ��SqlDataAdpter
		public override IDataAdapter GetAdapter(IDbCommand cmd)
		{
			OleDbDataAdapter adapter = new OleDbDataAdapter();
			adapter.SelectCommand=(OleDbCommand)cmd;
			cmd.Connection=connection;
			return adapter;
		}
		
		//����һ��DataRow
		public override DataRow GetDataRow(IDbCommand cmd)
		{
			cmd.Connection=this.connection;				//��������
			DataRow r ;

			OleDbDataAdapter adapter=new OleDbDataAdapter();
			adapter.SelectCommand= (OleDbCommand) cmd;
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
				//�������
				OleDbConnection cnn=new OleDbConnection( cnnString);
				this.connection=cnn;
				this.connection.Open();
			}
			catch(OleDbException e)
			{
				throw new PlException("�������ݿ�ʧ�ܣ��ο���" + e.Message ,ErrorTypes.DatabaseError);
			}
			finally
			{
				this.connection.Close();
			}
		}

		//���ز������ַ�����ʽ
		public override string GetStringParameter (string name,int i)
		{
			return "?";
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

		//������
		public override ErrorTypes ErrorHandler (Exception e,out string message) 
		{
			message = "";
			if (e is OleDbException)
			{
				OleDbException oleErr = (OleDbException)e;
				switch (oleErr.Errors[0].NativeError)
				{
					case -105121349:
						message = "�����ظ���";
						return ErrorTypes.NotUnique;
					case -68551703:
						return ErrorTypes.DataTooLong;
					case -541396598 :
						message = "�ο���" + oleErr.Message;
						return ErrorTypes.NotAllowStringEmpty;
					case -541331061:
						message = "�ο���" + oleErr.Message;
						return ErrorTypes.NotAllowDataNull;
					case -539888598:
						return ErrorTypes.DataTypeNotMatch;
				}
				message = "���ݿ�����쳣:";
				for(int i=0; i <oleErr.Errors.Count;i++)
				{
					message += "Index #" + i + "\n" +
						"Message: " + oleErr.Message + "\n" +
						"Native: " + oleErr.Errors[i].NativeError.ToString() + "\n" +
						"Source: " + oleErr.Errors[i].Source + "\n" ;
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