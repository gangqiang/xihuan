using System;
using System.Collections;

namespace PersistenceLayer
{
	/// <summary>
	///		该类封装了设置、得到xml文件路径，以及取得数据库连接字符串的功能
	///		不能直接生成这个类的实例，只能通过静态方法Instance()得到。
	/// </summary>
	public class Setting
	{
		private static Setting _instance=null;
		private string m_DatabaseMapFile="*";
		private Hashtable m_ConnectionStrings ;

		

		/// <summary>
		///		生成一个Setting类的实例
		/// </summary>
		protected Setting()
		{
			this.m_ConnectionStrings = new Hashtable ();
		}

		/// <summary>
		///		该类的实例只能同过本方法取得
		/// </summary>
		/// <returns>返回一个Setting实例</returns>
		public static Setting Instance()
		{
			if(Setting._instance==null)
			{
				Setting._instance=new Setting();
			}
			return Setting._instance;
		}

		/// <summary>
		///		得到一个指定数据库的连接字串 
		/// </summary>
		/// <param name="databaseName">数据库映射名</param>
		/// <returns>返回连接字符串</returns>
		public string GetConnectionString(string databaseName)
		{
			return this.m_ConnectionStrings [databaseName].ToString ();
		}
		
		internal void SetConnectionString(string databaseName,string connectionString)
		{
			if(this.m_ConnectionStrings.Contains(databaseName))
			{
				this.m_ConnectionStrings.Remove(databaseName);
			}
			this.m_ConnectionStrings.Add (databaseName,connectionString);
		}
		
		/// <summary>
		///		环境初始化
		/// </summary>
		public void Initialize()
		{
			if(this.m_DatabaseMapFile=="*")
			{
				throw new PlException("请先设置xml文件的位置!",ErrorTypes.PesistentError);
			}
			else
			{
				PersistenceBroker.Instance ();
			}
		}

		/// <summary>
		/// 追加数据源，此方法提供代码追加数据源的可能性
		/// 在多帐套的情况下，一般是需要动态追加数据源的
		/// 如果数据源名称重复，则会自动覆盖原连接，如果数据源名称不同，则会自动追加进去。
		/// 
		/// </summary>
		/// <param name="name">数据源名</param>
		/// <param name="databaseType">类型</param>
		/// <param name="connectionString">连接字符串</param>
		public void AppendDatabase(string name,DatabaseType databaseType,string connectionString)
		{
			string thisNameSpace = this.ToString ().Substring (0,this.ToString ().LastIndexOf('.')+1);

			PersistenceBroker.Instance().AppendDatabase(name,thisNameSpace+databaseType.ToString(),connectionString);
		}

		/// <summary>
		/// 手动加载ClassMap文件。
		/// </summary>
		/// <param name="classMapPath">ClassMap地址</param>
		public void LoadClassMap(string classMapPath)
		{
			PersistenceBroker.Instance().LoadClassMap(classMapPath);
		}

		/// <summary>
		/// 追加数据源，此方法提供代码追加数据源的可能性
		/// 在多帐套的情况下，一般是需要动态追加数据源的
		/// 如果数据源名称重复，则会自动覆盖原连接，如果数据源名称不同，则会自动追加进去。
		/// 同时会加载ClassMap信息。
		/// </summary>
		/// <param name="name">数据源名称</param>
		/// <param name="databaseType">数据源类型</param>
		/// <param name="connectionString">连接字符串</param>
		/// <param name="ClassMapPath">ClassMap地址</param>
		public void AppendDatabase(string name,DatabaseType databaseType,string connectionString,string ClassMapPath)
		{
			string thisNameSpace = this.ToString ().Substring (0,this.ToString ().LastIndexOf('.')+1);
			PersistenceBroker.Instance().AppendDatabase(name,thisNameSpace+databaseType.ToString(),connectionString,ClassMapPath);
		}

		
		/*属性*/
		/// <summary>
		///		设置、返回数据库映射文件
		/// </summary>
		public string DatabaseMapFile
		{
			get{return this.m_DatabaseMapFile;}
			set{
				this.m_DatabaseMapFile=value;
				PersistenceBroker.Instance();
			}
		}

		/// <summary>
		/// 获取数据源的数据库提供者
		/// </summary>
		/// <param name="dbName">数据源名称</param>
		/// <returns></returns>
		public DatabaseVendor GetDatabaseVendor(string dbName)
		{
			return PersistenceBroker.Instance().GetDatabase(dbName).Vendor;
		}


		
	}

	/// <summary>
	/// 数据库类型
	/// </summary>
	public enum DatabaseType
	{
		/// <summary>
		/// SQL数据库
		/// </summary>
		 MsSqlServer,
		/// <summary>
		/// Access数据库
		/// </summary>
		MsAccess,
		/// <summary>
		/// Oracle数据库
		/// </summary>
		Oracle,

		/// <summary>
		/// Odbc数据库
		/// </summary>
		Odbc,

		/// <summary>
		/// ODP.NET连接Oracle
		/// </summary>
		ODP,
		/// <summary>
		/// 用.NET Connector连接MySql
		/// </summary>
		MySql

	
    }

	/// <summary>
	/// 数据库提供者类型
	/// </summary>
	public enum DatabaseVendor
	{
		/// <summary>
		/// Microsoft SQL Server 数据库
		/// </summary>
		MsSqlServer,
		/// <summary>
		/// Microsoft Access 数据库
		/// </summary>
		MsAccess,
		/// <summary>
		/// Oracle 数据库
		/// </summary>
		Oracle,
		/// <summary>
		/// ODBC连接方式
		/// </summary>
		Odbc,
		/// <summary>
		/// Informix数据库
		/// </summary>
		Informix,
		/// <summary>
		/// MySql 数据库（采用.NET连接）
		/// </summary>
		MySql
	}
	
}
