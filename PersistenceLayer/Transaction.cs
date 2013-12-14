using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace PersistenceLayer
{
	/// <summary>
	///	事务处理类
	///	此事务处理支持两种机制，一种是使用Add...与Process()的一次性提交机制
	///	另一种是使用Do..与Commit()或RollBack()的实时提交机制
	///	两种机制不能混合使用，即要是使用Process()方式 的，那么只能使用Add的方法
	///	如果是使用Commit()的话，则只能用Do方法，因此在使用时需要注意。
	///	此两种机制各有优势，都支持多异构数据库组合。
	/// </summary>
	public class Transaction
	{
		private IList tasks=null;
		private IList actions=null;
		/// <summary>
		/// tintown added at 2005-3-23
		/// 用于判断是否强行Commit，默认为false
		/// flase:不强行Commit，是指在实体Save()或是实体Delete()时，返回false就回滚整个事务,这用于并发性处理
		/// true:强行Commit,是指忽略实体Save()与Delete()返回的false状态，因为有时这在业务上本身就是允许的。
		/// </summary>
		private bool m_IsForceCommit=false;

		PersistenceBroker broker=PersistenceBroker.Instance();
		private Hashtable rdbs=new Hashtable();

		//
		/// <summary>
		///		Constructor
		/// </summary>
		public Transaction()
		{
			this.tasks=new ArrayList();
			this.actions=new ArrayList();
			
		}

		/// <summary>
		///		在任务中增加一个Save任务 
		/// </summary>
		/// <param name="obj">需要执行Save的实体对象</param>
		public void AddSaveObject(EntityObject obj)
		{
			this.tasks.Add(obj);
			if (obj.IsPersistent)
			{
				this.actions.Add(ActionTypes.UpdateCommand);
			}
			else
			{
				this.actions.Add(ActionTypes.InsertCommand);
			}
		}

		/// <summary>
		/// 执行保存对象操作
		/// </summary>
		/// <param name="obj">实体对象</param>
		/// <param name="isForceCommit">
		/// 是否强制执行,默认为false
		/// true:强制执行，即就算遇到更新为零的操作，也不会抛出异常
		/// flase:非强制执行，如果遇到更新为零，则抛出异常PLException，类型为DirthEntity，表示存在并发错误
		/// </param>
		/// <returns></returns>
		public void DoSaveObject(EntityObject obj,bool isForceCommit)
		{

			if(obj.IsPersistent)
			{
				broker.DoTransaction(obj,ActionTypes.UpdateCommand,rdbs,isForceCommit);
			}
			else
			{
				broker.DoTransaction(obj,ActionTypes.InsertCommand,rdbs,isForceCommit);
			}
		}

		/// <summary>
		/// 执行保存对象操作（采用Transaction设定的IsForceCommit）
		/// </summary>
		/// <param name="obj">对象</param>
		public void DoSaveObject(EntityObject obj)
		{
			if(obj.IsPersistent)
			{
				broker.DoTransaction(obj,ActionTypes.UpdateCommand,rdbs,m_IsForceCommit);
			}
			else
			{
				broker.DoTransaction(obj,ActionTypes.InsertCommand,rdbs,m_IsForceCommit);
			}
		}


		/// <summary>
		/// 在事务中执行实体删除操作
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="isForceCommit">
		/// 是否强制执行,默认为false
		/// true:强制执行，即就算遇到更新为零的操作，也不会抛出异常
		/// flase:非强制执行，如果遇到更新为零，则抛出异常PLException，类型为DirthEntity，表示存在并发错误
		///</param>
		public void DoDeleteObject(EntityObject obj,bool isForceCommit)
		{
			broker.DoTransaction(obj,ActionTypes.DeleteCommand,rdbs,isForceCommit);
		}

		/// <summary>
		/// 在事务中执行实体删除操作
		/// </summary>
		/// <param name="obj"></param>
		public void DoDeleteObject(EntityObject obj)
		{
			broker.DoTransaction(obj,ActionTypes.DeleteCommand,rdbs,m_IsForceCommit);
		}

		/// <summary>
		/// 在事务中进行实体查询（在执行后实体本身会被赋上值）
		/// </summary>
		/// <param name="obj">实体</param>
		public void DoRetrieveObject(EntityObject obj)
		{
			broker.DoTransaction(obj,ActionTypes.SelectCommand,rdbs,m_IsForceCommit);
		}


		/// <summary>
		/// 在事务中执行删除标准操作
		/// </summary>
		/// <param name="delete">删除标准</param>
		public void DoDeleteCriteria(DeleteCriteria delete)
		{
			broker.DoTransaction(delete,ActionTypes.PesistentCriteria,rdbs,m_IsForceCommit);
		}

		/// <summary>
		/// 在事务中执行删除标准操作
		/// </summary>
		/// <param name="delete">删除标准</param>
		/// <param name="isForceCommit">
		/// 是否强制执行,默认为false
		/// true:强制执行，即就算遇到更新为零的操作，也不会抛出异常
		/// flase:非强制执行，如果遇到更新为零，则抛出异常PLException，类型为DirthEntity，表示存在并发错误
		/// </param>
		public void DoDeleteCriteria(DeleteCriteria delete,bool isForceCommit)
		{
			broker.DoTransaction(delete,ActionTypes.PesistentCriteria,rdbs,isForceCommit);
		}

		/// <summary>
		/// 在事务中执行更新标准操作
		/// </summary>
		/// <param name="update">更新标准</param>
		public void DoUpdateCriteria(UpdateCriteria update)
		{
			broker.DoTransaction(update,ActionTypes.PesistentCriteria,rdbs,m_IsForceCommit);
		}


		/// <summary>
		/// 在事务中执行RetrieveCriteria查询
		/// </summary>
		/// <param name="retrieve">查询标准</param>
		/// <returns></returns>
		public DataTable DoRetrieveCriteria(RetrieveCriteria retrieve)
		{
			return broker.DoRetrieveCriteraTransaction(retrieve,rdbs);
		}

		/// <summary>
		/// 在事务中执行更新标准操作
		/// </summary>
		/// <param name="update">更新标准</param>
		/// <param name="isForceCommit">
		/// 是否强制执行,默认为false
		/// true:强制执行，即就算遇到更新为零的操作，也不会抛出异常
		/// flase:非强制执行，如果遇到更新为零，则抛出异常PLException，类型为DirthEntity，表示存在并发错误
		/// </param>
		public void DoUpdateCriteria(UpdateCriteria update,bool isForceCommit)
		{
			broker.DoTransaction(update,ActionTypes.PesistentCriteria,rdbs,isForceCommit);
		}

		/// <summary>
		/// 在事务中执行非查询SQL语句
		/// </summary>
		/// <param name="strSql">sql语句</param>
		/// <param name="dbName">数据源名称</param>
		public void DoSqlNonQueryString(string strSql,string dbName)
		{
			Hashtable ht=new Hashtable();
			ht.Add("DB",dbName);
			ht.Add("String",strSql);
			broker.DoTransaction(ht,ActionTypes.SqlCommand,rdbs,m_IsForceCommit);
		}

		/// <summary>
		/// 在事务中执行查询SQL语句
		/// </summary>
		/// <param name="strSql">SQL语句</param>
		/// <param name="dbName">数据源名称</param>
		/// <returns></returns>
		public DataTable DoSqlQueryString(string strSql,string dbName)
		{
			Hashtable ht=new Hashtable();
			ht.Add("DB",dbName);
			ht.Add("String",strSql);
			return broker.DoQueryTransaction(ht,rdbs);
		}

		/// <summary>
		/// 在事务中执行SQL语句
		/// </summary>
		/// <param name="strSql">sql语句</param>
		/// <param name="dbName">数据源名称</param>
		/// <param name="isForceCommit">
		/// 是否强制执行,默认为false
		/// true:强制执行，即就算遇到更新为零的操作，也不会抛出异常
		/// flase:非强制执行，如果遇到更新为零，则抛出异常PLException，类型为DirthEntity，表示存在并发错误 
		/// </param>
		public void DoSqlNonQueryString(string strSql,string dbName,bool isForceCommit)
		{
			Hashtable ht=new Hashtable();
			ht.Add("DB",dbName);
			ht.Add("String",strSql);
			broker.DoTransaction(ht,ActionTypes.SqlCommand,rdbs,isForceCommit);
		}


		/// <summary>
		/// 提交事务
		/// </summary>
		/// <returns></returns>
		public bool Commit()
		{
			bool result=true;
			try
			{
				foreach(string key in rdbs.Keys)
				{
					RelationalDatabase rdb=(RelationalDatabase)rdbs[key];
					rdb.CommitTransaction();
				}

				return result;
			}
			catch(Exception e)
			{
				foreach(string key in rdbs.Keys)
				{
					RelationalDatabase rdb=(RelationalDatabase)rdbs[key];
					rdb.RollbackTransaction();
				}
				throw e;

			}
			finally
			{
				//关闭所有连接
				foreach (string key in rdbs.Keys)
				{
					RelationalDatabase rdb=(RelationalDatabase)rdbs[key];
					rdb.Close();
				}
			
				
			}
			
			
		}

		/// <summary>
		/// 回滚事务
		/// </summary>
		public void RollBack()
		{
			try
			{
			
				foreach (string key in rdbs.Keys)
				{
					RelationalDatabase rdb=(RelationalDatabase)rdbs[key];
					rdb.RollbackTransaction();
					
				}
			}
			finally
			{
				//关闭所有连接
				foreach (string key in rdbs.Keys)
				{
					RelationalDatabase rdb=(RelationalDatabase)rdbs[key];
					rdb.Close();
				}
			
				
			}
		}




		/// <summary>
		///		在任务中增加一个Delete任务 
		/// </summary>
		/// <param name="obj">需要执行Delete的实体对象</param>
		public void AddDeleteObject(EntityObject obj)
		{
			this.tasks.Add(obj);
			this.actions.Add(ActionTypes.DeleteCommand);
		}
		
		/// <summary>
		///		在任务中增加一个Retrieve任务 
		/// </summary>
		/// <param name="obj">需要执行Retrieve的实体对象</param>
		internal void AddRetrieveObject (EntityObject obj)
		{
			this.tasks.Add (obj);
			this.actions.Add (ActionTypes.SelectCommand);
		}

		/// <summary>
		///		在任务中增加一个DeleteCriteria任务
		/// </summary>
		/// <param name="delete">DeleteCriteria实例</param>
		public void AddDeleteCriteria(DeleteCriteria delete)
		{
			this.tasks.Add(delete);
			this.actions.Add(ActionTypes.PesistentCriteria);
			//Console.WriteLine("Transaction.AddDeleteCriteria构建中！");
		}

		/// <summary>
		///		在任务中增加一个UpdateCriteria任务
		/// </summary>
		/// <param name="update">UpdateCriteria实例</param>
		public void AddUpdateCriteria(UpdateCriteria update)
		{
			this.tasks.Add(update);
			this.actions.Add(ActionTypes.PesistentCriteria);

		}

		/// <summary>
		/// 添加SQL的事务处理功能(单数据库)
		/// add by tintown
		/// </summary>
		/// <param name="sqlstring">执行的语句</param>
		/// <param name="DbName">数据库名称{逻辑数据库名}</param>
		public void AddSqlString(string sqlstring,string DbName)
		{
			Hashtable ht=new Hashtable();
			ht.Add("DB",DbName);
			ht.Add("String",sqlstring);
			this.tasks.Add(ht);
			this.actions.Add(ActionTypes.SqlCommand);
		}

		/// <summary>
		///		清除所有加入到事务里的对象 
		/// </summary>
		public void Clear()
		{
			this.tasks.Clear();
			this.actions.Clear();
		}
		
		/// <summary>
		///		事务处理 
		/// </summary>
		/// <returns>如果事务成功返回true，失败返回false</returns>
		public bool Process()
		{
			bool result=true;
			try
			{
				PersistenceBroker broker=PersistenceBroker.Instance();
				//if (broker.MultiDatabase )
				//{
					result=broker.ProcessTransactionMultiDatabases (this);
				//}
				//else
				//{
				//	broker.ProcessTransactionSingleDatabase (this);
				//}
			}
			catch(Exception e)
			{
				result=false;
				throw e;
			}
			return result;
		}



		/*属性*/
		/// <summary>
		///		返回事务处理队列中的对象数目
		/// </summary>
		public int Count
		{
			get
			{
				return this.tasks.Count;
			}
		}

		/// <summary>
		/// tintown added at 2005-3-23
		/// 用于判断是否强行Commit，默认为false
		/// flase:不强行Commit，是指在实体Save()或是实体Delete()时，返回false就回滚整个事务,这用于并发性处理
		/// true:强行Commit,是指忽略实体Save()与Delete()返回的false状态，因为有时这在业务上本身就是允许的。
		/// </summary>
		public bool IsForceCommit
		{
			get
			{
				return this.m_IsForceCommit;
			}
			set
			{
				this.m_IsForceCommit=true;
			}
		}

			internal IList Tasks
		{
			get{return this.tasks;}
		}
		internal IList Actions
		{
			get{return this.actions;}
		}
	}


}
