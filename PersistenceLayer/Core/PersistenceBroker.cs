using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace PersistenceLayer
{
	/// <summary>
	/// 实体层的关键类 
	/// 他保持与Persistence Mechanisms的连接
	/// 并管理他们的程序和他们的交互
	/// 使用Singleton模式 
	/// </summary>
	class PersistenceBroker
	{
		private static PersistenceBroker _instance;
		
		private IDictionary DatabasePool;							//数据库集合
		private IDictionary ClassMaps;		
		public bool MultiDatabase = false;

		//Constructor
		/// <summary>
		/// 构造方法.在使用PL层时,必定要进行这个构造方式.把数据库映射与表映射都载入内存.
		/// </summary>
		protected PersistenceBroker()
		{

			Setting oSetting=Setting.Instance();
			//未指定Xml文件 路径
			if(oSetting.DatabaseMapFile=="*")
			{
				//throw new PlException("未设置Xml文件路径！");
				IConfigLoader config=XmlConfigLoader.Instance();
				this.DatabasePool=config.DatabasePool;
				this.ClassMaps=config.ClassMaps;
			}
			else
			{
				//从读取Xml文件各个类的映射信息
				IConfigLoader config=XmlConfigLoader.Instance();
				config.LoadConfig(oSetting.DatabaseMapFile);
				this.DatabasePool=config.DatabasePool;
				this.ClassMaps=config.ClassMaps;
				if (this.DatabasePool.Count > 1) this.MultiDatabase = true;

				//设置Setting 类的对象
				foreach(RelationalDatabase rdb in this.DatabasePool.Values)
				{
					oSetting.SetConnectionString(rdb.Name,rdb.ConnectionString);
				}
			}
		}

		/// <summary>
		///		得到PersistenceBroker的实例
		/// </summary>
		public static PersistenceBroker Instance()
		{
			if(_instance==null)
			{
				lock(typeof(PersistenceBroker))
				{
					if(_instance==null) 
					{
						_instance=new PersistenceBroker();
					}
				}
			}
			return _instance;
		}

		//返回一Command
		public IDbCommand GetCommand (string dbName)
		{
			RelationalDatabase rdb = this.GetDatabase(dbName);
			return rdb.GetCommand();
		}

		//根据Command返回一个DataTable
		public DataTable ExecuteQuery(IDbCommand cmd,string rdbName)
		{
			RelationalDatabase rdb = null;
			if (rdbName == "")
			{
				rdb = this.GetDatabase().GetCopy ();
			}
			else
			{
				rdb=this.GetDatabase (rdbName).GetCopy();
			}
			try
			{
				rdb.Open();
				DataTable dt=rdb.AsDataTable(cmd);
                cmd.Parameters.Clear();
				return dt;
			}
			catch(Exception e)
			{
				this.ErrorHandle(e,null);
			}
			finally
			{
				rdb.Close();
			}
			return null;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="rdbName"></param>
        /// <returns></returns>
        public DataSet ExecuteMultiQuery(IDbCommand cmd, string rdbName)
        {
            RelationalDatabase rdb = null;
            if (rdbName == "")
            {
                rdb = this.GetDatabase().GetCopy();
            }
            else
            {
                rdb = this.GetDatabase(rdbName).GetCopy();
            }
            try
            {
                rdb.Open();
                DataSet ds = rdb.AsDataSet(cmd);
                return ds;
            }
            catch (Exception e)
            {
                this.ErrorHandle(e, null);
            }
            finally
            {
                rdb.Close();
            }
            return null;
        }
		
		

		//根据Command返回一个DataTable
		/// <summary>
		/// 返回前N条记录
		/// add by tintown at 2004-09-06
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="rdbName"></param>
		/// <param name="top">N</param>
		/// <returns></returns>
		public DataTable ExecuteQuery(IDbCommand cmd,string rdbName,int top)
		{
			RelationalDatabase rdb = null;
			if (rdbName == "")
			{
				rdb = this.GetDatabase().GetCopy ();
			}
			else
			{
				rdb=this.GetDatabase (rdbName).GetCopy();
			}
			try
			{
				rdb.Open();
				DataTable dt=rdb.AsDataTable(cmd,top);
				return dt;
			}
			catch(Exception e)
			{
				this.ErrorHandle(e,null);
			}
			finally
			{
				rdb.Close();
			}
			return null;
		}

		//取一个实体对象
		public bool RetrieveObject(EntityObject obj,bool isRetrieveAssociation)
		{
            return RetrieveObject(obj, isRetrieveAssociation, false);
		}


        public bool RetrieveObject(EntityObject obj, bool isRetrieveAssociation,bool WithNoLock)
        {
            ClassMap cm = this.GetClassMap(obj.GetClassName());
            RelationalDatabase rdb;
            //tintown add at 2004-10-23
            //要是指定了数据源，则从指定的数据源里取对象
            if (obj.DatabaseName == null)
                rdb = cm.RelationalDatabase.GetCopy();
            else
                rdb = this.GetDatabaseCopy(obj.DatabaseName);
            bool result = true;
            try
            {
                rdb.Open();
                result = this.RetrievePrivate(obj, cm, rdb, isRetrieveAssociation,WithNoLock);
            }
            catch (Exception e)
            {
                this.ErrorHandle(e, obj);
            }
            finally
            {
                rdb.Close();
            }
            return result;
        }

		//在事务中：取一个实体对象 
		private bool RetrieveObject(EntityObject obj ,RelationalDatabase rdb)
		{
			ClassMap cm=this.GetClassMap(obj.GetClassName());
			bool result=true;
			result = this.RetrievePrivate(obj,cm,rdb,true);
			return result;		
		}

		private bool RetrievePrivate(EntityObject obj,ClassMap cm,RelationalDatabase rdb,bool isRetrieveAssociation)
		{
            return RetrievePrivate(obj,cm,rdb,isRetrieveAssociation,false);
		}

        private bool RetrievePrivate(EntityObject obj, ClassMap cm, RelationalDatabase rdb, bool isRetrieveAssociation,bool WithNoLock)
        {
            IDbCommand cmd = cm.GetSelectSqlFor(obj,WithNoLock);
            IDataReader reader = rdb.GetDataReader(cmd);
            if (reader.Read())
            {
                this.SetObject(obj, reader, cm);
                reader.Close();
                obj.IsPersistent = true;

                //				obj.DirtyCode=DirtyControl.Instance().GetDirtyCode(obj.DirtyKey);
               
                
                if (obj.IsSaveToMemory)
                    SaveToMemory(obj.MemoryKey, cm, rdb);
            }
            else
            {
                reader.Close();
                obj.IsPersistent = false;
                return false;
            }

            /*处理Associations*/
            if (!isRetrieveAssociation) return true;

            EntityObject aEntityObject;
            object aValue;
            string aName;
            string target;
            Association a;
            ClassMap clsMap;

            if (cm.HasAssociation())
            {
                for (int i = 0; i < cm.Associations.Count; i++)
                {
                    a = (Association)cm.Associations[i];
                    if (!a.RetrieveAutomatic) break;
                    aName = a.ToAttribute;
                    target = a.Target;
                    aValue = obj.GetAttributeValue(a.FromAttribute);
                    if (a.Cardinality == CardinalityTypes.OneToOne)
                    {
                        //获取单个关联对象并初始化
                        aEntityObject = (EntityObject)obj.GetAttributeValue(target);
                        if (aEntityObject == null) break;
                        aEntityObject.SetAttributeValue(aName, aValue);
                        clsMap = this.GetClassMap(aEntityObject.GetClassName());
                        this.RetrievePrivate(aEntityObject, clsMap, rdb, true);
                    }
                    else
                    {
                        //获取多个对象:
                        EntityObjectCollection col;
                        col = (EntityObjectCollection)obj.GetAttributeValue(a.Target);
                        if (col == null) break;
                        clsMap = (ClassMap)this.ClassMaps[a.ToClass];
                        AttributeMap am = clsMap.GetAttributeMap(aName, true);
                        SelectionCriteria selCriteria = new SelectionCriteria(SelectionTypes.EqualTo, am, aValue, clsMap);

                        IDbCommand command = clsMap.GetSelectSqlFor(selCriteria);
                        DataTable dt = rdb.AsDataTable(command);
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            aEntityObject = (EntityObject)obj.GetType().Assembly.CreateInstance(col.FullName);
                            clsMap.SetObject(aEntityObject, dt.Rows[j]);
                            this.RetrievePrivate(aEntityObject, clsMap, rdb, true);
                            col.Add(aEntityObject);
                        }
                    }
                }//End of for
            }//end of if
            return true;
        }


		public void SaveToMemory(string memoryKey,ClassMap cm,RelationalDatabase rdb)
		{
				IDbCommand cmd=rdb.GetCommand();
				cmd.CommandText=cm.SelectFromClause;
				DataTable dt=rdb.AsDataTable(cmd);
				GlobalCacheControl.Instance().Add(memoryKey,dt);
			
		}
		
		//根据DataReader 设置EntityObject对象
		private void SetObject(EntityObject obj,IDataReader reader,ClassMap cm)
		{
			AttributeMap am;
			int count;
			int i,j = 0;
			object objTmp;
			do
			{
				//tintown add by 2004-10-23
				//给实体的数据库名属性赋上值
				if(obj.DatabaseName==null)
                    obj.DatabaseName=cm.RelationalDatabase.Name;

				obj.IsSaveToMemory=cm.IsSaveToMemory;

				count = cm.SelfClassAttributes.Count;
				for(i = 0;i<count;i++)
				{
					am= (AttributeMap) cm.SelfClassAttributes[i];//GetAttributeMap(i);
					objTmp = reader[j++];
					if (objTmp!=DBNull.Value)
					{
						obj.SetAttributeValue(am.Name,objTmp);
					}
				}
				cm = cm.SuperClass;
			}while (cm != null);
		}
		
		//删除一个实体对象
		public int DeleteObject(EntityObject obj)
		{
			int result = 0;
			ClassMap cm=this.GetClassMap(obj.GetClassName());
			IDbCommand cmd=cm.GetDeleteSqlFor(obj);
			RelationalDatabase rdb;
			//tintown add at 2004-10-23
			//判断如果实体的数据库名不为null，则使用实体的数据库连接
			if(obj.DatabaseName==null)
			{
				rdb = cm.RelationalDatabase.GetCopy ();
			}
			else
			{
				rdb=this.GetDatabase(obj.DatabaseName).GetCopy();
			}
			try
			{
				rdb.Open();
				if(cm.HasAssociation()) rdb.BeginTransaction();
				result=this.DeletePrivate(obj,cm,rdb);
				if(cm.HasAssociation()) rdb.CommitTransaction();


			}
			catch(Exception e)
			{
				if (cm.HasAssociation()) rdb.RollbackTransaction();
				this.ErrorHandle (e,obj);
			}
			finally
			{
				rdb.Close();
			}
			return result;
		}


		//删除一个实体对象(在事务中)
		public int DeleteObject(EntityObject obj,RelationalDatabase rdb)
		{
			int result=0;
			ClassMap cm=this.GetClassMap(obj.GetClassName());			
			result=this.DeletePrivate(obj,cm,rdb);
			if(result==0)
                obj.IsPersistent=false;
			return result;
			
		}

		/// <summary>
		/// add by tintown at 2004-09-06
		/// 执行SQL语句
		/// </summary>
		/// <param name="sqlstring">语句</param>
		/// <param name="rdb">数据库</param>
		public int ExecuteSql(string sqlstring,RelationalDatabase rdb)
		{
			return rdb.DoSql(sqlstring);
		}

	

		private int DeletePrivate(EntityObject obj,ClassMap cm,RelationalDatabase rdb)
		{
			/*处理Associations*/
			if(cm.HasAssociation())
			{
				int i;
				int c=cm.Associations.Count;
				Association a;
				EntityObject aObject;
				ClassMap clsMap;

				for(i=0;i<c;i++)
				{
					a=(Association)cm.Associations[i];
					if (!a.DeleteAutomatic) break;
					if(a.Cardinality==CardinalityTypes.OneToOne)
					{
						aObject=(EntityObject)obj.GetAttributeValue(a.Target);
						if (aObject==null)break;
						clsMap=this.GetClassMap(aObject.GetClassName());
						this.DeletePrivate(aObject,clsMap,rdb);
					}
					else
					{
						EntityObjectCollection col=(EntityObjectCollection)obj.GetAttributeValue(a.Target);
						if (col==null) break;
						int size = col.Objects.Count;
						for(int j=0;j< size;j++)
						{
							aObject=(EntityObject)col.Objects[j];
							clsMap=this.GetClassMap(aObject.GetClassName());							
							this.DeletePrivate(aObject,clsMap,rdb);
						}
						//col.Clear();
					}
				}

			}
			

			//Delete self including base objects
		//	do
			//{
			int result=0;
			result=rdb.DoCommand(cm.GetDeleteSqlFor(obj));
				//cm = cm.SuperClass;
			//}while (cm != null);

			if(obj.IsSaveToMemory && result>0)
                SaveToMemory(obj.MemoryKey,cm,rdb);
		

			return result;
		}
		
		//根据类名获得相应的ClassMap
		public ClassMap GetClassMap(string name)
		{
			ClassMap cm;
			cm=(ClassMap)ClassMaps[name];

			if(cm==null)
			{
				throw new PlException("未找到名为["+name+"]实体类相对应类影射信息！",ErrorTypes.PesistentError);
			}
			return cm;
		}
		


		/// <summary>
		/// DataBase操作区 开始
		/// tintown add at 2004-10-24
		/// </summary>
	

		//根据数据库名返回一个RelationalDatabase
		internal RelationalDatabase GetDatabase(string name)
		{
			RelationalDatabase rdb;
			rdb=(RelationalDatabase)this.DatabasePool[name];
			if(!this.DatabasePool.Contains(name))
			{
				throw new PlException("未找到名为"+name+"对应数据库信息！",ErrorTypes.PesistentError);
			}
			return rdb;
		}
		private RelationalDatabase GetDatabase()
		{
			RelationalDatabase rdb;
			IEnumerator myEnumerator = DatabasePool.Values.GetEnumerator();
			myEnumerator.MoveNext();
			rdb=(RelationalDatabase)myEnumerator.Current;
			return rdb;
		}

		//根据数据库名返回一个RelationalDatabase
		internal RelationalDatabase GetDatabaseCopy(string name)
		{
			RelationalDatabase rdb;
			rdb=(RelationalDatabase)this.DatabasePool[name];
			if(!this.DatabasePool.Contains(name))
			{
				throw new PlException("未找到名为"+name+"对应数据库信息！",ErrorTypes.PesistentError);
			}
			rdb=rdb.GetCopy();
			return rdb;
		}

		/// <summary>
		/// 追加数据连接
		/// add by tintown
		/// </summary>
		/// <param name="name"></param>
		/// <param name="DatabaseType"></param>
		/// <param name="connectionString"></param>
		internal void AppendDatabase(string name,string DatabaseType,string connectionString)
		{

			if(this.DatabasePool.Contains(name))
			{
				this.DatabasePool.Remove(name);
			}
//			if(!this.DatabasePool.Contains(name))
//			{
				RelationalDatabase rdb=null;
				try
				{
					rdb = (RelationalDatabase)this.GetType().Assembly.CreateInstance (DatabaseType);
					if (rdb == null) throw new Exception();
				}
				catch
				{
					string error = "创建类型为：" + DatabaseType + "的数据库出错！";
					throw new PlException (error,ErrorTypes.XmlError);
				}

				rdb.Name=name;
				//rdb.ConnectionString=connectionString;
				rdb.Initialize(connectionString);

				if (rdb!=null) this.DatabasePool.Add(rdb.Name,rdb);

				if (this.DatabasePool.Count > 1) this.MultiDatabase = true;

				//设置Setting中的数据库连接
				Setting.Instance().SetConnectionString(rdb.Name,rdb.ConnectionString);
				if(XmlConfigLoader.Instance().DatabaseMaps.Contains(rdb.Name))
					XmlConfigLoader.Instance().DatabaseMaps.Remove(rdb.Name);
				XmlConfigLoader.Instance().DatabaseMaps.Add(rdb.Name,new DatabaseMap(rdb.Name));
//			}
		}

		/// <summary>
		/// 追加数据连接并载入指定的ClassMap文件信息
		/// </summary>
		/// <param name="name"></param>
		/// <param name="DatabaseType"></param>
		/// <param name="connectionString"></param>
		/// <param name="classMapPath"></param>
		internal void AppendDatabase(string name,string DatabaseType,string connectionString,string classMapPath)
		{

			if(this.DatabasePool.Contains(name))
			{
				this.DatabasePool.Remove(name);
			}

//			if(!this.DatabasePool.Contains(name))
//			{
				RelationalDatabase rdb=null;
				try
				{
					rdb = (RelationalDatabase)this.GetType().Assembly.CreateInstance (DatabaseType);
					if (rdb == null) throw new Exception();
				}
				catch
				{
					string error = "创建类型为：" + DatabaseType + "的数据库出错！";
					throw new PlException (error,ErrorTypes.XmlError);
				}

				rdb.Name=name;
				//rdb.ConnectionString=connectionString;
				rdb.Initialize(connectionString);

				if (rdb!=null) this.DatabasePool.Add(rdb.Name,rdb);

				if (this.DatabasePool.Count > 1) this.MultiDatabase = true;

				//设置Setting中的数据库连接
				Setting.Instance().SetConnectionString(rdb.Name,rdb.ConnectionString);
				if(XmlConfigLoader.Instance().DatabaseMaps.Contains(rdb.Name))
					XmlConfigLoader.Instance().DatabaseMaps.Remove(rdb.Name);
				XmlConfigLoader.Instance().DatabaseMaps.Add(rdb.Name,new DatabaseMap(rdb.Name));

				

//			}
			//载入ClassMap信息
			XmlConfigLoader.Instance().LoadClassMapInformation(classMapPath);
		}


		internal void LoadClassMap(string classMapPath)
		{
			//载入ClassMap信息
			XmlConfigLoader.Instance().LoadClassMapInformation(classMapPath);
		}

		/// <summary>
		/// DataBase 操作区结束
		/// </summary>
	

		
		//返回一个实体对象
		public EntityObject GetEntityObject(Type classType,string className,DataRow row)
		{
			EntityObject aObject=null;
			aObject=(EntityObject)classType.Assembly.CreateInstance(classType.ToString());
			ClassMap cm=(ClassMap)this.ClassMaps[className];
			cm.SetObject(aObject,row);
			return aObject;
		}

		
		/// <summary>
		/// 处理一个事务(只适用于单数据库)
		/// add by tintown at 2004-09-06
		/// 添加执行SQL语句的事务处理
		/// 由于可以进行自行配置数据库连接，所以在事务中，都采用MultiDatabase的方法。
		/// 此方法暂时无效
		/// </summary>
		/// <param name="tran"></param>
		public void ProcessTransactionSingleDatabase(Transaction tran)
		{
			RelationalDatabase rdb;
			object obj=null;

			rdb=this.GetDatabase();
			rdb=rdb.GetCopy();
			try
			{
				rdb.Open();
				rdb.BeginTransaction();
				for(int i=0;i<tran.Tasks.Count;i++)
				{
					obj=tran.Tasks[i];
					switch ((ActionTypes)tran.Actions[i])
					{
						case ActionTypes.DeleteCommand:
							this.DeleteObject((EntityObject)obj,rdb);
							break;
						case ActionTypes.InsertCommand:
							this.SaveObject((EntityObject)obj,rdb);
							break;
						case ActionTypes.UpdateCommand:
							this.SaveObject((EntityObject)obj,rdb);
							break;
						case ActionTypes.PesistentCriteria:
							this.ProcessCriteria ((PersistentCriteria)obj,rdb);
							break;
						case ActionTypes.SelectCommand:
							this.RetrieveObject( (EntityObject)obj,rdb);
							break;
						case ActionTypes.SqlCommand:
							this.ExecuteSql(((Hashtable)obj)["String"].ToString(),rdb);
							break;
						default:
							break;
					}
				}
				rdb.CommitTransaction();
			}
			catch(Exception e)
			{
				rdb.RollbackTransaction();
				if (obj is EntityObject) 
					this.ErrorHandle(e,(EntityObject)obj);
				else
					this.ErrorHandle (e,null);
			}	
			finally
			{
				rdb.Close();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="rc"></param>
		public DataTable ProcessRetrieveCriteria(RetrieveCriteria rc)
		{
			if(rc.DatabaseName==null)
				rc.DatabaseName=rc.classMap.Database.Name;

			RelationalDatabase rdb;
			if(rc.Rdb!=null)
				rdb=rc.Rdb;
			else
                rdb=this.GetDatabaseCopy(rc.DatabaseName);
			IDbCommand  cmd = rdb.GetCommand();
			rc.BuildStringForRetrieve ();

			if(rc.IsSaveInMemory)
			{
				if(GlobalCacheControl.Instance().Contains(rc.MemoryKey))
				{
					DataTable dt=GlobalCacheControl.Instance().GetEntityContainer(rc.MemoryKey);
					int whereInd=rc.SqlString.LastIndexOf("WHERE");
					int orderInd=rc.SqlString.LastIndexOf("ORDER BY");
					string where="";
					string order="";
					if(whereInd!=-1)
					{
						where=rc.SqlString.Substring(whereInd+5).Replace(rc.classMap.RelationalDatabase.QuotationMarksStart+rc.classMap.Table.Name+rc.classMap.RelationalDatabase.QuotationMarksEnd+".","");
					}
					if(orderInd!=-1)
					{
						if(where!="")
							where=where.Substring(0,where.LastIndexOf("ORDER BY"));
						order=rc.SqlString.Substring(orderInd+9).Replace(rc.classMap.RelationalDatabase.QuotationMarksStart+rc.classMap.Table.Name+rc.classMap.RelationalDatabase.QuotationMarksEnd+".","");
					}			

					DataRow[] drs=dt.Select(where,order);
					DataTable dt2=dt.Copy();
					dt2.Rows.Clear();
					foreach(DataRow row in drs)
					{
						
						DataRow row2=dt2.NewRow();
						for(int i=0;i<row2.Table.Columns.Count;i++)
							row2[i]=row[i];
						dt2.Rows.Add(row2);
					}
					return dt2;
				}
				else
				{
					//添加到内存中begin\

					SaveToMemory(rc.MemoryKey,rc.classMap,rdb);


					//返回Datatable
					cmd.CommandText=rc.SqlString;
					return rdb.AsDataTable(cmd);
					

				}
				
			}
			else
			{
				
				cmd.CommandText = rc.SqlString;

				if(rc.Top==0)
					return rdb.AsDataTable(cmd);
				else
					return rdb.AsDataTable(cmd,rc.Top);
			}
		}

		/// <summary>
		/// 自动进行分页功能的数据获取
		/// </summary>
		/// <param name="rc"></param>
		/// <param name="pageSize"></param>
		/// <param name="page"></param>
		/// <param name="counts"></param>
		/// <returns></returns>
		public DataTable ProcessRetrieveCriteria(RetrieveCriteria rc,int pageSize,int page,out int counts)
		{
			if(rc.DatabaseName==null)
				rc.DatabaseName=rc.classMap.Database.Name;

			RelationalDatabase rdb;
			if(rc.Rdb!=null)
				rdb=rc.Rdb;
			else
				rdb=this.GetDatabaseCopy(rc.DatabaseName);
			IDbCommand  cmd = rdb.GetCommand();
			rc.BuildStringForRetrieve ();

			counts=0;//设置默认是0条
			if(rc.IsSaveInMemory)
			{
				if(GlobalCacheControl.Instance().Contains(rc.MemoryKey))
				{
					DataTable dt=GlobalCacheControl.Instance().GetEntityContainer(rc.MemoryKey);
					int whereInd=rc.SqlString.LastIndexOf("WHERE");
					int orderInd=rc.SqlString.LastIndexOf("ORDER BY");
					string where="";
					string order="";
					if(whereInd!=-1)
					{
						where=rc.SqlString.Substring(whereInd+5).Replace(rc.classMap.RelationalDatabase.QuotationMarksStart+rc.classMap.Table.Name+rc.classMap.RelationalDatabase.QuotationMarksEnd+".","");
					}
					if(orderInd!=-1)
					{
						if(where!="")
							where=where.Substring(0,where.LastIndexOf("ORDER BY"));
						order=rc.SqlString.Substring(orderInd+9).Replace(rc.classMap.RelationalDatabase.QuotationMarksStart+rc.classMap.Table.Name+rc.classMap.RelationalDatabase.QuotationMarksEnd+".","");
					}			

					DataRow[] drs=dt.Select(where,order);
					DataTable dt2=dt.Copy();
					dt2.Rows.Clear();
					foreach(DataRow row in drs)
					{
						
						DataRow row2=dt2.NewRow();
						for(int i=0;i<row2.Table.Columns.Count;i++)
							row2[i]=row[i];
						dt2.Rows.Add(row2);
					}
					counts=dt.Rows.Count;
					return dt2;
				}
				else
				{
					//添加到内存中begin\

					SaveToMemory(rc.MemoryKey,rc.classMap,rdb);


					//返回Datatable 分页功能
					string orgSql=rc.SqlString; //取得原始的SQL语句
					//cmd.CommandText=rc.SqlString;
					string KeySql=rc.BuildKeyStringForRetrieve();//获取得到主键的语句

					string start=rc.classMap.RelationalDatabase.QuotationMarksStart;
					string end=rc.classMap.RelationalDatabase.QuotationMarksEnd;
					string tempKey="";
					string tempKey2="";
					string tempKey3="";
					for(int i=0;i<rc.classMap.GetKeySize();i++)
					{
						ColumnMap cm= rc.classMap.GetKeyAttributeMap(i).Column;
						tempKey +=start+cm.Name+end+" "+rc.classMap.RelationalDatabase.GetDbTypeString(cm.Type)+",";
						tempKey2 +=start+cm.Name+end+",";
						tempKey3+=" and "+start+"t0"+end+"."+start+cm.Name+end+"="+start+"t1"+end+"."+start+cm.Name+end;

					}
					tempKey=tempKey.TrimEnd(new char[]{','});
					tempKey2=tempKey2.TrimEnd(new char[]{','});
					string sqlCreate = "declare @indextable table(indextableid int identity(1,1) PRIMARY KEY,"+tempKey+") ;";

					string sqlKeyinsert = "insert into @indextable("+tempKey2+") "+KeySql+";";

				
					string sqlCt=sqlCreate+ sqlKeyinsert + "select recrowcount= @@rowcount;";

					cmd.CommandText=sqlCt;
					DataTable dtct=rdb.AsDataTable(cmd);
					
					if(dtct.Rows.Count>0)
						counts=int.Parse(dtct.Rows[0][0].ToString());

					//sql = sql + "select recrowcount= @@rowcount;";
					//这是执行的获取数据的语句
					
					string sqlSearch="select t1.* from @indextable t0,("+orgSql+") t1 where t0.indextableid>="+((page-1)*pageSize+1).ToString()+" and t0.indextableid<"+ (page*pageSize+1).ToString() +tempKey3;
					string sqlDt = sqlCreate+ sqlKeyinsert + sqlSearch;

					cmd.CommandText=sqlDt;
					return rdb.AsDataTable(cmd);
					

				}
				
			}
			else
			{
				
				//cmd.CommandText = rc.SqlString;

				if(rc.Top==0)
				{


					//返回Datatable 分页功能
					string orgSql=rc.SqlString; //取得原始的SQL语句
					//cmd.CommandText=rc.SqlString;
					string KeySql=rc.BuildKeyStringForRetrieve();//获取得到主键的语句

					string start=rc.classMap.RelationalDatabase.QuotationMarksStart;
					string end=rc.classMap.RelationalDatabase.QuotationMarksEnd;
					string tempKey="";
					string tempKey2="";
					string tempKey3="";
					for(int i=0;i<rc.classMap.GetKeySize();i++)
					{
						ColumnMap cm= rc.classMap.GetKeyAttributeMap(i).Column;
						tempKey +=start+cm.Name+end+" "+rc.classMap.RelationalDatabase.GetDbTypeString(cm.Type)+",";
						tempKey2 +=start+cm.Name+end+",";
						tempKey3+=" and "+start+"t0"+end+"."+start+cm.Name+end+"="+start+"t1"+end+"."+start+cm.Name+end;

					}
					tempKey=tempKey.TrimEnd(new char[]{','});
					tempKey2=tempKey2.TrimEnd(new char[]{','});
					string sqlCreate = "declare @indextable table(indextableid int identity(1,1) PRIMARY KEY,"+tempKey+") ;";

					string sqlKeyinsert = "insert into @indextable("+tempKey2+") "+KeySql+";";

				
					string sqlCt=sqlCreate+ sqlKeyinsert + "select recrowcount= @@rowcount;";

					cmd.CommandText=sqlCt;
					DataTable dtct=rdb.AsDataTable(cmd);
					
					if(dtct.Rows.Count>0)
						counts=int.Parse(dtct.Rows[0][0].ToString());

					//sql = sql + "select recrowcount= @@rowcount;";
					//这是执行的获取数据的语句
					
					string sqlSearch="select t1.* from @indextable t0,("+orgSql+") t1 where t0.indextableid>="+((page-1)*pageSize+1).ToString()+" and t0.indextableid<"+ (page*pageSize+1).ToString() +tempKey3;
					string sqlDt = sqlCreate+ sqlKeyinsert + sqlSearch;

					cmd.CommandText=sqlDt;
					return rdb.AsDataTable(cmd);

				}
				else
					return rdb.AsDataTable(cmd,rc.Top); //有TOP功能的还不知道如何实现分页呢！
			}
		}


		

		//处理一个事务(适用多个数据库)
		/// <summary>
		/// 处理多数据库的事务处理
		/// add by tintown at 2004-09-06
		/// 处理SQL的事务处理
		/// </summary>
		/// <param name="tran"></param>
		public bool ProcessTransactionMultiDatabases (Transaction tran)
		{
			bool roolback=false;
			Hashtable databases = new Hashtable ();
			RelationalDatabase rdb = null;
			EntityObject eobj = null;
			PersistentCriteria c = null;
			ActionTypes actionType ;
			string dbName ;
			string className;
			ClassMap cm;
			//Add by tintown ＳＱＬ语句的数据库名
			Hashtable htsqlstring;
			string sqlstring="";
			//用于存放那些需保存到内存的类名，在事务RoolBack时可以进行清掉
			ArrayList saveToMemoryList=new ArrayList();
		
			try
			{
				for(int i=0;i<tran.Tasks.Count;i++)
				{
					actionType = (ActionTypes)tran.Actions[i];
					if(actionType == ActionTypes.PesistentCriteria)
					{
						c = (PersistentCriteria) (tran.Tasks[i]) ;
						className = EntityObject.GetClassName (c.ForClass);
						cm=this.GetClassMap(className);
						//如果类要保存到内存，则添加到回滚内存列表中tintown added at 2005-3-23
						if(c.IsSaveInMemory)
                            saveToMemoryList.Add(c.MemoryKey);

						if(c.DatabaseName==null)
						{
							dbName = cm.Database.Name;
							c.DatabaseName=dbName;
						}else
							dbName=c.DatabaseName;
						
												
					}
					else if(actionType==ActionTypes.SqlCommand)
					{
						htsqlstring=(Hashtable)(tran.Tasks[i]);
						dbName = htsqlstring["DB"].ToString();
						sqlstring=htsqlstring["String"].ToString();
						
					}
					else
					{
						eobj = (EntityObject) (tran.Tasks [i]);
						cm=this.GetClassMap(eobj.GetClassName ());
						//如果对象是要保存到内存的，就加到回滚内存列表中
						if(eobj.IsSaveToMemory)
							saveToMemoryList.Add(eobj.MemoryKey);
							
						//tintown add at 2004-10-23
						//在事务中，如果是实体的话，也要取实体自己的数据源
						if(eobj.DatabaseName==null)
						{
							dbName = cm.Database.Name;
							eobj.DatabaseName=dbName;
						}
						else
							dbName = eobj.DatabaseName;
					}
					rdb = (RelationalDatabase) databases[dbName];
					if (rdb == null)
					{
						rdb = this.GetDatabase (dbName).GetCopy ();
						databases.Add (dbName,rdb);
						rdb.Open ();
						rdb.BeginTransaction ();
					}
					switch (actionType)
					{
						case ActionTypes.DeleteCommand:
							if(this.DeleteObject(eobj,rdb)<=0 && !tran.IsForceCommit)
								{roolback=true;goto RollBackPoint;}
							break;
						case ActionTypes.InsertCommand:
							if(this.SaveObject(eobj,rdb)<=0 && !tran.IsForceCommit)
							{roolback=true;goto RollBackPoint;}
							break;
						case ActionTypes.UpdateCommand:
							if(this.SaveObject(eobj,rdb)<=0 && !tran.IsForceCommit)
							{roolback=true;goto RollBackPoint;}
							break;
						case ActionTypes.PesistentCriteria:
							this.ProcessCriteria (c,rdb);
							//this.ProcessDeleteCriteria(c,rdb);
							break;
						case ActionTypes.SelectCommand:
							this.RetrieveObject( eobj,rdb);
							break;
						case ActionTypes.SqlCommand :
							this.ExecuteSql(sqlstring,rdb);
							break;
						default:
							break;
					}
				}

				//在前面的事务处理不成功时，直接跳到此处进行roolback，不需要再提交其他事务了
			RollBackPoint:

				//提交所有事务
				if(!roolback)
				{
					foreach (RelationalDatabase r in databases.Values)
					{
						r.CommitTransaction ();
					}

				}
				else
				{
					//事务在回滚的同时需要把保存到内存的清掉
					foreach (RelationalDatabase r in databases.Values)
					{
						r.RollbackTransaction ();
					}
					
					//清掉内存中的数据，让数据到时重新绑到内存
					for(int i=0;i<saveToMemoryList.Count;i++)
					{
						GlobalCacheControl.Instance().RemoveIt(saveToMemoryList[i].ToString());
					}

//					throw(new PlException("事务中有更新条数为零的操作，可能是并发错误.",ErrorTypes.DirtyEntity));

				}
				
				
			}
			catch (Exception e)
			{
				//回滚所有事务
				foreach (RelationalDatabase r in databases.Values)
				{
					r.RollbackTransaction ();
				}

				//清掉内存中的数据，让数据到时重新绑到内存
				for(int i=0;i<saveToMemoryList.Count;i++)
				{
					GlobalCacheControl.Instance().RemoveIt(saveToMemoryList[i].ToString());
				}

				//if (actionType == obj is EntityObject) this.ErrorHandle(e,(EntityObject)obj);
				throw e;
			}
			finally
			{
				//关闭所有连接
				foreach (RelationalDatabase r in databases.Values)
				{
					r.Close ();
				}
			}
			return !roolback;
		}


		/// <summary>
		/// 在事务中执行查询SQL语句
		/// </summary>
		/// <param name="htSql">语句</param>
		/// <param name="rdbs">连接集</param>
		/// <returns></returns>
		internal DataTable DoQueryTransaction(Hashtable htSql,Hashtable rdbs)
		{
			Hashtable htsqlstring=htSql;
			string dbName=htSql["DB"].ToString();
			string sqlstring=htSql["String"].ToString();
			RelationalDatabase rdb = null;
			try
			{
				//打开连接
				if(rdbs.Contains(dbName))
					rdb=(RelationalDatabase)rdbs[dbName];
				else
				{
					rdb=this.GetDatabase(dbName).GetCopy();
					rdbs.Add(dbName,rdb);
					rdb.Open();
					rdb.BeginTransaction();
					
				}
				IDbCommand cmd = GetCommand (dbName);
				cmd.CommandText = sqlstring;
				return rdb.AsDataTable(cmd);
			}
			catch(Exception e)
			{

				foreach (string key in rdbs.Keys)
				{
					rdb=(RelationalDatabase)rdbs[key];
					rdb.RollbackTransaction();
					rdb.Close();
				}
				throw e;
			}
		}

		/// <summary>
		/// 立即执行事务的操作
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="actionType">类型</param>
		/// <param name="rdbs">连接集</param>
		/// <param name="isForceCommit">强行执行</param>
		internal void DoTransaction(object obj,ActionTypes actionType,Hashtable rdbs,bool isForceCommit)
		{
			EntityObject eobj=null;
			ClassMap cm=null;
			string dbName=null;
			string className;
			PersistentCriteria c = null;
			RelationalDatabase rdb = null;
			RetrieveCriteria rc=null;
			string sqlstring="";
			try
			{
			
				if(actionType==ActionTypes.PesistentCriteria)
				{
					c=(PersistentCriteria)obj;
					className=EntityObject.GetClassName(c.ForClass);
					cm=this.GetClassMap(className);
					if(c.DatabaseName==null)
					{
						dbName=cm.Database.Name;
						c.DatabaseName=dbName;
					}
					else
					{
						dbName=c.DatabaseName;
					}
				}
				else if(actionType==ActionTypes.SqlCommand)
				{
					Hashtable htsqlstring=(Hashtable)obj;
					dbName=htsqlstring["DB"].ToString();
					sqlstring=htsqlstring["String"].ToString();
				}
				else
				{
					eobj=(EntityObject)obj;
					cm=this.GetClassMap(eobj.GetClassName());
					if(eobj.DatabaseName==null)
					{
						dbName=cm.Database.Name;
						eobj.DatabaseName=dbName;
					}
					else
						dbName=eobj.DatabaseName;
				}

				//打开连接
				if(rdbs.Contains(dbName))
					rdb=(RelationalDatabase)rdbs[dbName];
				else
				{
					rdb=this.GetDatabase(dbName).GetCopy();
					rdbs.Add(dbName,rdb);
					rdb.Open();
					rdb.BeginTransaction();
					
				}

				switch (actionType)
				{
					case ActionTypes.SelectCommand:
						this.RetrieveObject(eobj,rdb);
						break;
					case ActionTypes.DeleteCommand:
						if(this.DeleteObject(eobj,rdb)<=0 && !isForceCommit)
						{
							throw new PlException("存在并发处理错误，请重新提交.",ErrorTypes.DirtyEntity);
							
						}
						else
						{
							//保存成功了，如果是内存映像的，则从内存中清除，宁愿以后重新载入
							if(eobj.IsSaveToMemory)
								GlobalCacheControl.Instance().RemoveIt(eobj.MemoryKey);
							
						}
						break;
					case ActionTypes.InsertCommand:
					case ActionTypes.UpdateCommand:                        
						if(this.SaveObject(eobj,rdb)<=0 && !isForceCommit)
						{
							throw new PlException("存在并发处理错误，请重新提交.",ErrorTypes.DirtyEntity);
							
						}
						else
						{
							//保存成功了，如果是内存映像的，则从内存中清除，宁愿以后重新载入
							if(eobj.IsSaveToMemory)
								GlobalCacheControl.Instance().RemoveIt(eobj.MemoryKey);
							
						}
						break;
					case ActionTypes.PesistentCriteria:
						this.ProcessCriteria(c,rdb);
						break;
					case ActionTypes.SqlCommand:
						this.ExecuteSql(sqlstring,rdb);
						break;
				}
			}
			catch(Exception e)
			{

				foreach (string key in rdbs.Keys)
				{
					rdb=(RelationalDatabase)rdbs[key];
					rdb.RollbackTransaction();
					rdb.Close();
				}
				throw e;
			}
			

		}

		/// <summary>
		/// 在事务中执行获取标准
		/// </summary>
		/// <param name="obj">标准</param>
		/// <param name="rdbs">连接集</param>
		/// <returns></returns>
		public DataTable DoRetrieveCriteraTransaction(object obj,Hashtable rdbs)
		{
			RetrieveCriteria rc=(RetrieveCriteria)obj;
			string className=EntityObject.GetClassName(rc.ForClass);
			ClassMap cm=this.GetClassMap(className);
			string dbName;
			RelationalDatabase rdb=null;
			try
			{
				if(rc.DatabaseName==null)
				{
					dbName=cm.Database.Name;
					rc.DatabaseName=dbName;
				}
				else
				{
					dbName=rc.DatabaseName;
				}

				//打开连接
				if(rdbs.Contains(dbName))
					rdb=(RelationalDatabase)rdbs[dbName];
				else
				{
					rdb=this.GetDatabase(dbName).GetCopy();
					rdbs.Add(dbName,rdb);
					rdb.Open();
					rdb.BeginTransaction();
					
				}
				rc.Rdb=rdb;
				return ProcessRetrieveCriteria(rc);
			}
			catch(Exception e)
			{

				foreach (string key in rdbs.Keys)
				{
					rdb=(RelationalDatabase)rdbs[key];
					rdb.RollbackTransaction();
					rdb.Close();
				}
				throw e;
			}
		}


		/// <summary>
		///		返回实体对象所在数据库名
		/// </summary>
		public string GetDatabaseName(EntityObject obj)
		{
			ClassMap cm=this.GetClassMap(obj.GetClassName());
			return cm.Database.Name;			
		}

		/// <summary>
		///		处理DeleteCriteria,UpdateCriteria
		/// </summary>
		/// <param name="criteria">对应的PesistentCriteria</param>
		/// <returns>受影响的行数</returns>
		public int ProcessCriteria(PersistentCriteria criteria)
		{
			
			RelationalDatabase rdb;
			IDbCommand  cmd;
			int infect;

			switch (criteria.CriteriaType)
			{
				case CriteriaTypes.Update:
					//取得criteria对应的UpdateCriteria
					UpdateCriteria uCriteria = (UpdateCriteria) criteria;
					rdb=this.GetDatabase(uCriteria.DatabaseName).GetCopy();
					rdb.Open();
					cmd = rdb.GetCommand();
					uCriteria.BuildStringForUpdate();
					cmd.CommandText=uCriteria.SqlString;
					Hashtable ht=uCriteria.ForUpdateCollection;
					if(ht.Count<=0)
						return 0;
					foreach(object key in ht.Keys)
					{
						IDataParameter p= cmd.CreateParameter();
						if(rdb.Vendor==DatabaseVendor.MySql)
							p.ParameterName="?"+ key.ToString();
						else
							p.ParameterName = "@" +  key.ToString();
						
						p.DbType=uCriteria.classMap.GetAttributeMap(key.ToString()).Column.Type;										//列类型				
						p.Value=ht[key.ToString()].ToString();
						cmd.Parameters.Add (p);
					}

//					//tintown added at 2005-3-22 添加对timestamp的更新参数
					//取消了更新标准对timestamp的更新
//					if(uCriteria.classMap.TimestampAttribute!=null)
//					{
//						ColumnMap cm=uCriteria.classMap.TimestampAttribute.Column;
//						IDataParameter p= cmd.CreateParameter();
//						//p.SourceColumn=attrMap.Column.GetFullyQualifiedName();					//列名												//列名
//						p.ParameterName = "@Update" + cm.Name;
//						p.DbType=cm.Type;										//列类型				
//						p.Value=DateTime.Now.Ticks;	   			//此timestamp由now的ticks生成一串数字
//						cmd.Parameters.Add (p);
//					}

					

					try
					{
						infect=rdb.DoCommand(cmd);
						if(infect>0 && uCriteria.IsSaveInMemory)
						{
							SaveToMemory(uCriteria.MemoryKey,this.GetClassMap(uCriteria.classMap.Name),rdb);
		
						}
						rdb.Close();
                        //Logger.Write(cmd.CommandText);
						return infect;	
					}
					catch(Exception e)
					{
						string s="";
						ErrorTypes error=rdb.ErrorHandler (e,out s);
						throw new PlException(s,error);
					}


//					cursor = uCriteria.Retrieve.AsCursor ();
//					while (cursor.HasObject())
//					{
//						obj = cursor.NextObject();
//						int l = uCriteria.ForUpdateCollection.Count;
//						for (int i = 0;i < l;i ++)
//						{
//							obj.SetAttributeValue (uCriteria.ForUpdateCollection[i].ToString(),
//								uCriteria.ForUpdateCollection[++i]);
//						}
//						this.SaveObject (obj);
//					}
//					return cursor.Count;
					
					//处理删除标准
				default:
					//取得criteria对应的DeleteCriteria
					DeleteCriteria dCriteria = (DeleteCriteria) criteria;
					
					rdb=this.GetDatabase(dCriteria.DatabaseName).GetCopy();
					rdb.Open();
					cmd = rdb.GetCommand();
					dCriteria.BuildStringForDelete();
		
					cmd.CommandText=dCriteria.SqlString;
					try
					{
						infect=rdb.DoCommand(cmd);
						if(infect>0 && dCriteria.IsSaveInMemory)
						{
							SaveToMemory(dCriteria.MemoryKey,this.GetClassMap(dCriteria.classMapName),rdb);
		
						}
						rdb.Close();
                        //Logger.Write(cmd.CommandText);
						return infect;	
					}
					catch(Exception e)
					{
						string s="";
						ErrorTypes error=rdb.ErrorHandler (e,out s);
						throw new PlException(s,error);
					}
					
					
			}
		}
		
		/// <summary>
		///		在事务中处理DeleteCriteria,UpdateCriteria
		/// </summary>
		/// <param name="criteria">对应的PesistentCriteria</param>
		/// <param name="rdb">需要处理实体对象所在数据库</param>
		/// <returns>受影响的行数</returns>
		public int ProcessCriteria(PersistentCriteria criteria ,RelationalDatabase rdb)
		{
			
			
			IDbCommand  cmd;
			int infect;

			switch (criteria.CriteriaType)
			{
				case CriteriaTypes.Update:
					UpdateCriteria uCriteria = (UpdateCriteria) criteria;
					cmd = rdb.GetCommand();
					uCriteria.BuildStringForUpdate();
					cmd.CommandText=uCriteria.SqlString;
					Hashtable ht=uCriteria.ForUpdateCollection;
					if(ht.Count<=0)
						return 0;
					foreach(object key in ht.Keys)
					{
						IDataParameter p= cmd.CreateParameter();
						if(rdb.Vendor==DatabaseVendor.MySql)
                            p.ParameterName = "?" + key.ToString();
						else
							p.ParameterName = "@" + key.ToString();
						p.DbType=uCriteria.classMap.GetAttributeMap(key.ToString()).Column.Type;										//列类型				
						p.Value=ht[key.ToString()].ToString();
						cmd.Parameters.Add (p);
					}

//					//tintown added at 2005-3-22 添加对timestamp的更新参数
					//取消了更新标准 对timestamp的值更新
//					if(uCriteria.classMap.TimestampAttribute!=null)
//					{
//						ColumnMap cm=uCriteria.classMap.TimestampAttribute.Column;
//						IDataParameter p= cmd.CreateParameter();
//						//p.SourceColumn=attrMap.Column.GetFullyQualifiedName();					//列名												//列名
//						p.ParameterName = "@Update" + cm.Name;
//						p.DbType=cm.Type;										//列类型				
//						p.Value=DateTime.Now.Ticks;	   			//此timestamp由now的ticks生成一串数字
//						cmd.Parameters.Add (p);
//					}
					
					

					try
					{
						infect=rdb.DoCommand(cmd);
						if(infect>0 && uCriteria.IsSaveInMemory)
						{
							SaveToMemory(uCriteria.MemoryKey,this.GetClassMap(uCriteria.classMap.Name),rdb);
		
						}
						return infect;	
					}
					catch(Exception e)
					{
						string s="";
						ErrorTypes error=rdb.ErrorHandler (e,out s);
						throw new PlException(s,error);
					}
					//处理删除标准
				default:
					//取得criteria对应的DeleteCriteria
					DeleteCriteria dCriteria = (DeleteCriteria) criteria;
				
					cmd = rdb.GetCommand();
					dCriteria.BuildStringForDelete();
		
					cmd.CommandText=dCriteria.SqlString;
					try
					{
						infect=rdb.DoCommand(cmd);
						if(infect>0 && dCriteria.IsSaveInMemory)
						{
							SaveToMemory(dCriteria.MemoryKey,this.GetClassMap(dCriteria.classMapName),rdb);
		
						}
						return infect;	
					}
					catch(Exception e)
					{
						string s="";
						ErrorTypes error=rdb.ErrorHandler (e,out s);
						throw new PlException(s,error);
					}
								
			}
		}

		
		
		//保存一个实体对象
		public int SaveObject(EntityObject obj)
		{
			int result=0;
			ClassMap cm=this.GetClassMap(obj.GetClassName());
			//RelationalDatabase rdb = cm.RelationalDatabase.GetCopy ();
			RelationalDatabase rdb;
			//tintown add at 2004-10-23
			//判断如果实体的数据库名不为null，则使用实体的数据库连接
			if(obj.DatabaseName==null)
			{
				rdb = cm.RelationalDatabase.GetCopy ();
				obj.DatabaseName=cm.Database.Name;
			}
			else
			{
				rdb=this.GetDatabase(obj.DatabaseName).GetCopy();
			}
			try
			{
				rdb.Open();
				if(cm.HasAssociation()) rdb.BeginTransaction();
				result=this.SavePrivate(obj,cm,rdb);
				if(cm.HasAssociation()) rdb.CommitTransaction();
				
			}
			catch(Exception e)
			{
				if (cm.HasAssociation()) rdb.RollbackTransaction();
				this.ErrorHandle(e,obj);
				//throw e;
			}
			finally
			{
				rdb.Close();
			}

			return result;
		}
		//保存一个实体对象(在事务中)
		public int SaveObject(EntityObject obj,RelationalDatabase rdb)
		{
			ClassMap cm=this.GetClassMap(obj.GetClassName());
			return this.SavePrivate(obj,cm,rdb);
		}
		
		private int SavePrivate(EntityObject obj,ClassMap cm,RelationalDatabase rdb)
		{
			int result=0;
			/*处理Associations*/
			if(cm.HasAssociation())
			{
				int i;
				int c=cm.Associations.Count;
				Association a;
				EntityObject aObject;
				ClassMap clsMap;

				for(i=0;i<c;i++)
				{
					a=(Association)cm.Associations[i];
					if (!a.SaveAutomatic) break;
					if(a.Cardinality==CardinalityTypes.OneToOne)
					{
						aObject=(EntityObject)obj.GetAttributeValue(a.Target);
						if (aObject==null)break;
						clsMap=this.GetClassMap(aObject.GetClassName());
						this.SavePrivate(aObject,clsMap,rdb);
					}
					else
					{
						EntityObjectCollection col=(EntityObjectCollection)obj.GetAttributeValue(a.Target);
						if (col==null) break;
						for(int j=0;j<col.Count;j++)
						{
							aObject=(EntityObject)col.Objects[j];
							clsMap=this.GetClassMap(aObject.GetClassName());							
							this.SavePrivate(aObject,clsMap,rdb);
						}
					}
				}

			}
			//处理对象本身
			IDbCommand cmd;
			if(obj.IsPersistent)
			{
				
				//Update

//				if(!(obj.DirtyCode==DirtyControl.Instance().GetDirtyCode(obj.DirtyKey)))
//				{
//					throw new PlException("数据已经被更新",ErrorTypes.DirtyEntity);
//				}else
//					obj.DirtyCode=DirtyControl.Instance().ResetDirtyCode(obj.DirtyKey);
				
				//do
				//{
					cmd = cm.GetUpdateSqlFor (obj);
				result=rdb.DoCommand (cmd);
//					if (rdb.DoCommand (cmd) < 1)
//					{
//						throw new PlException ("对" + obj.GetClassName () + "对象更新失败！",ErrorTypes.UpdateFail);
//					}
				//	cm = cm.SuperClass;
				//}while (cm != null);

				if(obj.IsSaveToMemory && result>0)
                    SaveToMemory(obj.MemoryKey,cm,rdb);
				

			}
			else
			{
				//Insert
				//do
				//{
					cmd = cm.GetInsertSqlFor (obj);
					if (cm.Table.AutoIdentityIndex < 0)
					{
						result=rdb.DoCommand(cmd);
					}
						//若该表有Identity列
					else
					{
						object id;
						result=rdb.InsertRecord (cmd, out id);

						obj.SetAttributeValue (cm.AutoIdentityAttribute,id);
					}
					//cm = cm.SuperClass;
				//}while (cm != null);

				if(obj.IsSaveToMemory && result>0)
                    SaveToMemory(obj.MemoryKey ,cm,rdb);
			}

			return result;
		}

		//		public void test()
		//		{
		//			foreach (RelationalDatabase rdb in this.DatabasePool.Values)
		//			{
		//				Console.WriteLine(rdb.Name);
		//			}
		//		}

		//处理一个删除标准
		//		public int ProcessDeleteCriteria(DeleteCriteria aCriteria)
		//		{
		//			string name=EntityObject.GetClassName(aCriteria.ForClass);
		//			ClassMap cm=this.GetClassMap(name);
		//			RelationalDatabase rdb=this.GetDatabaseCopy(cm.Database.Name);
		//			rdb.Open();
		//			IDbCommand cmd=cm.GetDeleteSqlFor(aCriteria);
		//			int result=rdb.DoCommand(cmd);
		//			rdb.Close();
		//			return result;
		//		}
		//		public int ProcessDeleteCriteria(DeleteCriteria aCriteria,RelationalDatabase rdb)
		//		{
		//			aCriteria.Criteria.Perform();
		//			string name=EntityObject.GetClassName(aCriteria.ForClass);
		//			ClassMap cm=this.GetClassMap(name);
		//			IDbCommand cmd=cm.GetDeleteSqlFor(aCriteria);
		//			int result=rdb.DoCommand(cmd);
		//			return result;
		//		}


		//不返回记录的Command的处理
		public int Execute(string dbName,IDbCommand cmd)
		{
			RelationalDatabase rdb=null;
			rdb=this.GetDatabase (dbName).GetCopy();
			rdb.Open();
			int r=rdb.DoCommand(cmd);
			rdb.Close();
			return r;
		}
		
		private void ErrorHandle(Exception e,EntityObject obj)
		{
			if (e is PlException )
			{
				throw e;
			}

			if (obj == null)
			{
				throw new PlException (e.Message,ErrorTypes.DatabaseUnknwnError);
			}

			RelationalDatabase rdb = this.GetDatabase(GetDatabaseName(obj));
			
			string s="";
			ErrorTypes error = rdb.ErrorHandler (e,out s);

			//如果不是数据库的错误
			if (error == ErrorTypes.Unknown)
			{
				//如果是类型转换异常
				if (e is InvalidCastException)
				{
					switch (e.TargetSite.Name )
					{
						case "RetrievePrivate":
							s = obj.GetClassName () + "对象在执行Retrive()方法时，发生了类型转换异常！检查对象关联的兼容性！";
							throw new PlException (s);
					}
				}
				throw new PlException (e.Message,ErrorTypes.Unknown);
				
			}
				//处理数据库错误
			else
			{
				switch(error)
				{
					case ErrorTypes.NotUnique:
						s = "标识,索引"+obj.GetClassName()+"对象或其关联对象的数据与已存在数据冲突！";
						throw new PlException (s,error);
					case ErrorTypes.DataTooLong:
						s = obj.GetClassName() + "对象或其关联对象中某些属性的数据超出数据库容纳范围！";
						throw new PlException (s,error);
					case ErrorTypes.NotAllowStringEmpty:
						s = "对象：" + obj.GetClassName() + "或其关联对象中某些数据类型为string的属性不能为零长度！" + s;
						throw new PlException (s,error);
					case ErrorTypes.NotAllowDataNull:
						s = "对象：" + obj.GetClassName() + "或其关联对象中某些属性不能为Null！" + s;
						throw new PlException (s,error);
					case ErrorTypes.DataTypeNotMatch:
						s = "对象：" + obj.GetClassName() + "或其关联对象中某些属性数据类型于数据库数据类型不兼容！";
						throw new PlException (s,error);
					case ErrorTypes.AutoValueOn:
						s = obj.GetClassName () + "对象或其关联对象中有自动编号属性，不能指定特定值！" + s;
						throw new PlException (s,error);
					case ErrorTypes.RestrictError:
						s = "试图对" + obj.GetClassName() + "对象操作，由于约束机制，操作被终止！" + s;
						throw new PlException (s,error);
					case ErrorTypes.RequireAttribute:
						s = "试图对" + obj.GetClassName() + "对象操作，但必要的属性未指定或为null！" + s;
						throw new PlException (s,error);
				}
				//其他错误
				s = obj.GetClassName () + "对象操作出错！参考：" + s;
				throw new PlException (s,error);
			}
		}


		/// <summary>
		/// 静态的给ORACLE添加TOP功能的方法，在WHERE后添加rownum
		/// </summary>
		/// <param name="sqlString"></param>
		/// <param name="top"></param>
		/// <returns></returns>
		internal static string AddOracleTopWhere(string sqlString,int top)
		{
			int whereInd=sqlString.LastIndexOf("WHERE");
			int orderInd=sqlString.LastIndexOf("ORDER BY");
			string where="";
			if(whereInd!=-1)
			{
				where=sqlString.Substring(whereInd+5);
				where =" rownum<="+top.ToString()+" and "+where;
				sqlString=sqlString.Substring(0,whereInd+5)+where;
			}
			else
			{
				if(orderInd!=-1)
				{
					sqlString=sqlString.Substring(0,orderInd)+ " WHERE rownum<="+top.ToString()+" "+sqlString.Substring(orderInd);
				}
				else
				{
					sqlString +=" WHERE rownum<="+top.ToString();
				}
			}

			return sqlString;
		}

		/// <summary>
		/// 静态的给MySql添加TOP功能的方法????
		/// </summary>
		/// <param name="sqlString"></param>
		/// <param name="top"></param>
		/// <returns></returns>
		internal static string AddMySqlTopWhere(string sqlString,int top)
		{
	
			sqlString +=" limit "+top.ToString();		

			return sqlString;
		}

		

	
	}

}
