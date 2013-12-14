using System;
using System.Data;
using System.Collections;
using System.Text;

namespace PersistenceLayer
{
	/// <summary>
	///		Query类是RetrieveCriteria的一个增强类。
	///		RetrieveCriteria不能完成的工作可以由Query来完成
	///		Query可以进行联合查询、执行Sql语句、执行存储过程等
	/// </summary>
	public class Query
	{
		private ArrayList m_Conditions=new ArrayList();
		private ArrayList m_Querys=new ArrayList();
		private Query m_FatherQuery=null;
		
		private ClassMap queryClass=null;
		
		private string selectClause="";
		private string orderClause="";
		private string fromClause="";
		private string groupClause="";
		private string whereClause = "";

		private string sTemp ;
		private string endQuostationMarks;

		private string selectString = "SELECT ";
		private string _sqlString ;
		
		private int m_top=0;

		private string _databaseName;
		

		/// <summary>
		/// 	生成一个Query实例
		/// </summary>
		/// <param name="className">EntityObject的类名</param>
		public Query(string className)
		{
			ClassMap cm=PersistenceBroker.Instance().GetClassMap(className);
			this.queryClass=cm;
			sTemp = " AS " + cm.RelationalDatabase.QuotationMarksStart;
			endQuostationMarks = cm.RelationalDatabase.QuotationMarksEnd;
		}
		
		/// <summary>
		///		生成一个Query实例
		/// </summary>
		/// <param name="classType">EntityObject对象的Type实例</param>
		public Query(Type classType)
		{
			string className = EntityObject.GetClassName(classType);
			ClassMap cm=PersistenceBroker.Instance().GetClassMap(className);
			this.queryClass=cm;
			sTemp = " AS " + cm.RelationalDatabase.QuotationMarksStart;
			endQuostationMarks = cm.RelationalDatabase.QuotationMarksEnd;

		}

		/// <summary>
		///		生成一个Query实例
		/// </summary>
		/// <param name="classType">EntityObject对象的Type实例</param>
		/// <param name="DatabaseName">多帐套数据库的数据库名</param>
		public Query(Type classType,string DatabaseName)
		{
			string className = EntityObject.GetClassName(classType);
			ClassMap cm=PersistenceBroker.Instance().GetClassMap(className);
			this.queryClass=cm;
			sTemp = " AS " + cm.RelationalDatabase.QuotationMarksStart;
			endQuostationMarks = cm.RelationalDatabase.QuotationMarksEnd;
			this.DatabaseName=DatabaseName;

		}

		/// <summary>
		///		执行存储过程
		/// </summary>
		/// <param name="storeProcName">存储过程名</param>
		/// <param name="param">存储过程所需参数数组</param>
		/// <param name="affected">存储过程影响的行数</param>
		/// <param name="dbName">数据库名</param>
		/// <returns>存储过程的返回代码</returns>
		public static int RunProcedure(string storeProcName,IDataParameter[] param,out int affected,string dbName)
		{
			int result;
			PersistenceBroker broker;
			
			broker=PersistenceBroker.Instance();
			IDbCommand cmd = Query.BuildQueryCommand(storeProcName,param,dbName);
			//增加返回值参数
			IDataParameter p = cmd.CreateParameter();
			p.Direction=ParameterDirection.ReturnValue;
			p.ParameterName = "ReturnValue";
			p.DbType = DbType.Int32;
			cmd.Parameters.Insert(0,p);

			affected = broker.Execute (dbName,cmd);
			p = (IDataParameter)cmd.Parameters["ReturnValue"];
			result= (int) p.Value;
			return result;
		}

		/// <summary>
		///		执行存储过程(可以指定存储过程Timeout时间:0代表永远)
		/// </summary>
		/// <param name="storeProcName">存储过程名</param>
		/// <param name="param">存储过程所需参数数组</param>
		/// <param name="affected">存储过程影响的行数</param>
		/// <param name="dbName">数据库名</param>
		/// <param name="commandTimeout">执行时间(秒)，默认为30,如果设为0则无限制</param>
		/// <returns>存储过程的返回代码</returns>
		public static int RunProcedure(string storeProcName,IDataParameter[] param,out int affected,string dbName,int commandTimeout)
		{
			int result;
			PersistenceBroker broker;
			
			broker=PersistenceBroker.Instance();
			IDbCommand cmd = Query.BuildQueryCommand(storeProcName,param,dbName);
			cmd.CommandTimeout=commandTimeout;
			//增加返回值参数
			IDataParameter p = cmd.CreateParameter();
			p.Direction=ParameterDirection.ReturnValue;
			p.ParameterName = "ReturnValue";
			p.DbType = DbType.Int32;
			cmd.Parameters.Insert(0,p);

			affected = broker.Execute (dbName,cmd);
			p = (IDataParameter)cmd.Parameters["ReturnValue"];
			result= (int) p.Value;
			return result;
		}

		/// <summary>
		///		执行存储过程,返回DataTable 
		/// </summary>
		/// <param name="storeProcName">存储过程名</param>
		/// <param name="ps">存储过程所需参数数组</param>
		/// <param name="dbName">数据库名</param>
		/// <returns>存储过程返回的DataTable</returns>
		public static DataTable RunProcedure(string storeProcName,IDataParameter[] ps,string dbName)
		{
			IDbCommand cmd = Query.BuildQueryCommand(storeProcName,ps,dbName);
			PersistenceBroker broker=PersistenceBroker.Instance();
			return broker.ExecuteQuery(cmd ,dbName);
		}

		/// <summary>
		///		执行存储过程,返回DataTable (指定Command执行时间，0永不过期)
		/// </summary>
		/// <param name="storeProcName">存储过程名</param>
		/// <param name="ps">存储过程所需参数数组</param>
		/// <param name="dbName">数据库名</param>
		/// <param name="commandTimeout">timeout秒数</param>
		/// <returns>存储过程返回的DataTable</returns>
		public static DataTable RunProcedure(string storeProcName,IDataParameter[] ps,string dbName,int commandTimeout)
		{
			IDbCommand cmd = Query.BuildQueryCommand(storeProcName,ps,dbName);
			cmd.CommandTimeout=commandTimeout;
			PersistenceBroker broker=PersistenceBroker.Instance();
			return broker.ExecuteQuery(cmd ,dbName);
		}
		
		/// <summary>
		/// 	执行Sql语句返回DataTable
		/// </summary>
		/// <param name="sql">SQL语句</param>
		/// <param name="dbName">操作的数据库名</param>
		/// <returns>SQL语句查询结果集</returns>
		public static DataTable ProcessSql(string sql,string dbName)
		{
			PersistenceBroker broker = PersistenceBroker.Instance();
			IDbCommand cmd = broker.GetCommand (dbName);
			cmd.CommandTimeout=120;
			cmd.CommandText = sql;
            Logger.Write(sql);    //add by duhu
			return broker.ExecuteQuery(cmd,dbName);
		}

        /// <summary>
        /// add by tintown 2008-7-3使用Parameter的方式进行传参执行SQL
        ///如：string sql = "select top 100 * from Line_TCPF_Line where memberid=@id";
        ///IDataParameter para = PersistenceLayer.Query.GetParameter("hotel");
        ///para.ParameterName = "@id";
        /// para.Value = 6632;
        /// IDataParameter[] paras = new IDataParameter[1];
        ///paras[0] = para;
        /// DataTable dt = PersistenceLayer.Query.ProcessSql(sql, paras, "hotel");
        /// </summary>
        /// <param name="sql">自己写的SQL语句</param>
        /// <param name="param">参数数组</param>
        /// <param name="dbName">数据源名</param>
        /// <returns>查询结果集</returns>
        public static DataTable ProcessSql(string sql, IDataParameter[] param, string dbName)
        {
            PersistenceBroker broker = PersistenceBroker.Instance();
            IDbCommand cmd = broker.GetCommand(dbName);
            cmd.CommandTimeout = 120;
            cmd.CommandText = sql;
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    IDataParameter p = param[i];
                    cmd.Parameters.Add(p);
                }
            }
            Logger.Write(sql); // add by duhu
            return broker.ExecuteQuery(cmd, dbName);
        }

        /// <summary>
        /// 	执行Sql语句返回DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="dbName">操作的数据库名</param>
        /// <returns>SQL语句查询结果集</returns>
        public static DataSet ProcessMultiSql(string sql, string dbName)
        {
            PersistenceBroker broker = PersistenceBroker.Instance();
            IDbCommand cmd = broker.GetCommand(dbName);
            cmd.CommandTimeout = 120;
            cmd.CommandText = sql;
            Logger.Write(sql);   //add by duhu
            return broker.ExecuteMultiQuery(cmd, dbName);
        }

        //public static DataTable PageQuery(string needfields, string fromcondition,string keyfields ,string keyfieldsdeclare, int pagesize, int pages, out int counts, string db)
        //{
        //    string sqlCreate = "declare @indextable table(indextableid int identity(1,1) PRIMARY KEY," + keyfieldsdeclare + ") ;";
        //    string KeySql = "select "+keyfields+" from "+fromcondition+" ";
        //    string sqlKeyinsert = "insert into @indextable(" + keyfields + ") " + KeySql + ";";
        //    string sqlCt = "select recrowcount= @@rowcount;";
        //    string[] needf = needfields.Split(new char[] { ',' });
        //    for (int i = 0; i < needf.Length; i++)
        //    {
        //        needf[i] = "t1."+needf[i].ToString();
        //    }
        //    string sqlSearch = "select "+string.Join(",",needf)+" from @indextable t0,(" + orgSql + ") t1 where t0.indextableid>=" + ((page - 1) * pageSize + 1).ToString() + " and t0.indextableid<" + (page * pageSize + 1).ToString() + tempKey3;

        //}


        /// <summary>
        /// 	执行Sql语句返回影响条数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="dbName">操作的数据库名</param>
        /// <returns>SQL语句影响条数</returns>
        public static int ProcessSqlNonQuery(string sql, string dbName)
		{
			PersistenceBroker broker = PersistenceBroker.Instance();
			IDbCommand cmd = broker.GetCommand (dbName);
			cmd.CommandText = sql;
            Logger.Write(sql);   //add by duhu
			return broker.Execute(dbName,cmd);
		}
		
		/// <summary>
		///		返回一个IDataParameter,是SqlParameter,还是OleDbParameter
		///		由databaseName对应数据库的数据库类型确定
		/// </summary>
		/// <param name="databaseName">数据库名</param>
		/// <returns></returns>
		public static IDataParameter GetParameter (string databaseName)
		{
			PersistenceBroker broker = PersistenceBroker.Instance();
			return broker.GetCommand (databaseName).CreateParameter ();
		}

		//创建新的Sql命令对象：需要的参数加入IDbCommand.Parameters集合
		private static IDbCommand BuildQueryCommand(string storedProcName,IDataParameter[] param,string dbName)
		{
			PersistenceBroker broker = PersistenceBroker.Instance();
			IDbCommand cmd = broker.GetCommand (dbName);
			cmd.CommandText = storedProcName;
			cmd.CommandType=CommandType.StoredProcedure;
			
			if (param != null) 
			{
				for (int i = 0; i < param.Length ;i++)
				{
					IDataParameter p = param[i];
					cmd.Parameters.Add(p);
				}
			}
			return cmd;
		}

		/*===========================实例方法=====================================*/
		/// <summary>
		///		返回一个条件类对象 
		/// </summary>
		/// <returns></returns>
		public Condition GetQueryCondition()
		{
			Condition c = new Condition (this.queryClass);
			this.m_Conditions.Add(c);
			return c;
		}

		/// <summary>
		/// 头N条属性
		/// </summary>
		public int Top
		{
			set
			{
				this.m_top=value;
			}
			get
			{
				return this.m_top;
			}
		}

		/// <summary>
		///增加一个属性，并以属性名作为返回DataTable的列名
		/// </summary>
		/// <param name="attributeName"></param>
		public void AddAttribute(string attributeName)
		{
			AttributeMap am=this.queryClass.GetAttributeMap(attributeName,true);
			selectClause+=","+queryClass.GetFullyQualifiedName(am.Column.Name) + sTemp + am.Name + endQuostationMarks;
		}
		/// <summary>
		///		增加一个属性 并以asName作为返回DataTable的列名 
		/// </summary>
		public void AddAttribute(string attributeName,string asName)
		{
			AttributeMap am=this.queryClass.GetAttributeMap(attributeName,true);		
			selectClause+=","+queryClass.GetFullyQualifiedName(am.Column.Name) + sTemp + asName + endQuostationMarks;
		}

		/// <summary>
		/// 定义选择所有的属性
		/// </summary>
		/// <param name="all"></param>
		public void AddAttribute(AttributeType all)
		{
			if(all==AttributeType.All)
			{
				AttributeMap am;
				for(int i=0;i<this.queryClass.Attributes.Count;i++)
				{
					am=this.queryClass.GetAttributeMap(i);
					selectClause+=","+queryClass.GetFullyQualifiedName(am.Column.Name) + sTemp + am.Name + endQuostationMarks;
				}
			}
		}
			   
		
		/// <summary>
		///		返回所选对象中指定属性的Max
		/// </summary>
		/// <param name="attributeName">取Max值的属性名</param>
		/// <param name="asName">返回Max值在DataTable中的列名</param>
		public void SelectMax(string attributeName,string asName)
		{
			AttributeMap am = this.queryClass.GetAttributeMap(attributeName);
			selectClause+=",MAX("+queryClass.GetFullyQualifiedName(am.Column.Name)+") " + sTemp + asName + endQuostationMarks;
		}

		/// <summary>
		///		返回所选对象中指定属性的Count值
		/// </summary>
		/// <param name="attributeName">取Count值的属性名</param>
		/// <param name="asName">返回Count值在DataTable中的列名</param>
		public void SelectCount(string attributeName,string asName)
		{
			AttributeMap am = this.queryClass.GetAttributeMap(attributeName);
			selectClause+=",COUNT("+queryClass.GetFullyQualifiedName(am.Column.Name)+") "+sTemp+asName  + endQuostationMarks;
		}

		/// <summary>
		///		返回所选对象中指定属性的Sum值
		/// </summary>
		/// <param name="attributeName">取Sum值的属性名</param>
		/// <param name="asName">返回Sum值在DataTable中的列名</param>
		public void SelectSum(string attributeName,string asName)
		{
			AttributeMap am = this.queryClass.GetAttributeMap(attributeName);
			selectClause+=",SUM("+queryClass.GetFullyQualifiedName(am.Column.Name)+") "+sTemp+asName + endQuostationMarks;
		}

		/// <summary>
		///		返回所选对象中指定属性的Avg值
		/// </summary>
		/// <param name="attributeName">取Avg值的属性名</param>
		/// <param name="asName">返回Avg值在DataTable中的列名</param>
		public void SelectAvg(string attributeName,string asName)
		{
			AttributeMap am = this.queryClass.GetAttributeMap(attributeName);
			selectClause+=",AVG("+queryClass.GetFullyQualifiedName(am.Column.Name)+") "+sTemp+asName+ endQuostationMarks;
		}

		/// <summary>
		///		等值连接一个Query类对象
		/// </summary>
		/// <param name="fromAttribute"></param>
		/// <param name="q"></param>
		/// <param name="toAttribute"></param>
		public void AddJoinQuery(string fromAttribute,Query q,string toAttribute)
		{
			this.AddQueryIn(q);
			
			q.m_FatherQuery = this;

			ClassMap cm=q.QueryClassMap;
			AttributeMap amFrom=this.queryClass.GetAttributeMap(fromAttribute,false);
			AttributeMap amTo=cm.GetAttributeMap(toAttribute,false);
			
			this.m_Querys.AddRange(q.Querys);

			this.whereClause += " AND " + queryClass.GetFullyQualifiedName(amFrom.Column.Name ) + "=" 
				+ cm.GetFullyQualifiedName(amTo.Column.Name );
		}
		
		//增加一个关联查询类
		private void AddQueryIn(Query q)
		{
			m_Querys.Add(q);
			if (this.m_FatherQuery != null)
			{
				this.m_FatherQuery.AddQueryIn (q);
			}
		}
		
		private string BuildForQuery(IDbCommand cmd)
		{
			string whereClause;

			fromClause = " FROM " + queryClass.RelationalDatabase.QuotationMarksStart+queryClass.Table.Name+queryClass.RelationalDatabase.QuotationMarksEnd;
			ClassMap cm = this.queryClass.SuperClass;
			while( cm != null)
			{
				fromClause += "," + queryClass.RelationalDatabase.QuotationMarksStart+cm.Table.Name+queryClass.RelationalDatabase.QuotationMarksEnd ;
				cm = cm.SuperClass;
			}

			whereClause = " WHERE "+ GetSqlForConditions () ;
			if (this.queryClass.SuperClass != null)
			{
				whereClause += " AND " + this.queryClass.StringForInherit;
			}
												  
			//orderClause = " ORDER BY ''"+orderClause;
			
			//生成Sql语句
			Query q;
			for(int j= 0;j<m_Querys.Count;j++)
			{
				q =(Query) m_Querys[j];
				//Select Clause
				selectClause += q.selectClause;
				//From Clause
				cm = q.queryClass;
				do
				{
					fromClause += "," + queryClass.RelationalDatabase.QuotationMarksStart+cm.Table.Name+queryClass.RelationalDatabase.QuotationMarksEnd;
					cm = cm.SuperClass;
				}while (cm != null);
				//Where Clause
				whereClause += " AND "+ q.GetSqlForConditions ();
				if (q.queryClass.SuperClass != null)
				{
					whereClause += " AND " + q.queryClass.StringForInherit;
				}
				//Order Clause
				orderClause += q.orderClause;
				//Group Clause
				groupClause += q.groupClause;
			}

			string selectC="";
			if(this.m_top!=0)
			{
				selectString +=" Top "+this.m_top.ToString()+" ";
			}	

			if (selectClause != "")
			{
				selectC = this.selectString + selectClause.Remove(0,1);
			}
			else
			{
				throw new PlException ("构建Sql错误！",ErrorTypes.PesistentError);
			}
			if(groupClause!="")
			{
				groupClause=" GROUP BY "+groupClause.Remove(0,1);
				orderClause = "";
			}

			//			Console.WriteLine(selectClause);
			//			Console.WriteLine(fromClause);
			//			Console.WriteLine(whereClause);
			//			Console.WriteLine(orderClause);
			//			Console.WriteLine(groupClause);
			if (this.orderClause != "")
			{
				orderClause = " ORDER BY " + orderClause.Remove (0,1);
			}
			this._sqlString = selectC+fromClause+whereClause+orderClause+groupClause; 
			return this._sqlString;
		}

		/// <summary>
		///		执行查询，并返回查询结果集
		/// </summary>
		/// <returns>查询结果</returns>
		public DataTable Execute()
		{
			PersistenceBroker broker=PersistenceBroker.Instance();
			RelationalDatabase rdb=broker.GetDatabaseCopy(this.DatabaseName);
			IDbCommand cmd = rdb.GetCommand();
			cmd.CommandText=this.BuildForQuery(cmd);
			string cmdText=cmd.CommandText;
			if(rdb.Vendor==DatabaseVendor.Oracle)
			{
				int topInd=cmdText.IndexOf("Top");
				if(topInd!=-1)
				{
					cmdText=cmdText.Substring(0,topInd)+cmdText.Substring(topInd+6);

					//使用ORCALE添加rownum的方法重装SQL语句
					cmdText=PersistenceBroker.AddOracleTopWhere(cmdText,this.Top);	
				}
			}
			cmd.CommandText=cmdText;
			return broker.ExecuteQuery(cmd,this.DatabaseName);
		}

		/// <summary>
		///		执行查询，并返回查询所返回结果集的第一行第一列。忽略额外的行或列
		/// </summary>
		/// <returns></returns>
		public object ExecuteScalar()
		{
			PersistenceBroker broker = PersistenceBroker.Instance();
			RelationalDatabase rdb=broker.GetDatabaseCopy(this.DatabaseName);
			IDbCommand cmd = rdb.GetCommand ();
			cmd.CommandText=this.BuildForQuery(cmd);		
			DataTable dt = broker.ExecuteQuery(cmd,this.DatabaseName);
			
			if(dt.Rows.Count==0)
			{
				return null;
			}
			else
			{
				return dt.Rows[0][0];
			}
		}

		/// <summary>
		///		清除所有条件和连接
		/// </summary>
		public void Clear()
		{
			this.selectClause	= "";
			this.fromClause		= "";
			this.orderClause	= "";
			this.m_Querys=null;
		}

		/// <summary>
		///		结果排序，升序
		/// </summary>
		/// <param name="attributeName">按attributeName排序</param>
		public void OrderBy(string attributeName)
		{
			this.OrderBy(attributeName,true);
		}
		
		/// <summary>
		///		结果排序
		/// </summary>
		/// <param name="attributeName">按attributeName排序</param>
		/// <param name="isAsc">是否升序</param>
		public void OrderBy(string attributeName ,bool isAsc)
		{
			AttributeMap am=queryClass.GetAttributeMap(attributeName);			
			if(isAsc)
			{
				orderClause+=","+queryClass.GetFullyQualifiedName(am.Column.Name)+" ASC ";
			}
			else
			{
				orderClause+=","+queryClass.GetFullyQualifiedName(am.Column.Name)+" DESC ";
			}
		}
		
		/// <summary>
		/// 对查询结果集按属性分组
		/// </summary>
		/// <param name="name">分组的属性名</param>
		public void GroupBy(string name)
		{
			AttributeMap am=queryClass.GetAttributeMap(name);
			this.groupClause+=","+queryClass.GetFullyQualifiedName(am.Column.Name);
		}

		//构造Query的条件 返回string 
		private string GetSqlForConditions ()
		{
			string where = SqlCommander.BuildForConditions (this.m_Conditions);
			if  (where == null)
			{
				return " 1=1 " + this.whereClause ;
			}
			else
			{
				return "( " + where +this.whereClause + " )";
			}
		}

		/*属性*/
		/// <summary>
		///		查询名
		/// </summary>
		public string ClassName
		{
			get {return this.queryClass.Name;}
		}
		//父查询
		internal Query FatherQuery
		{
			get{return m_FatherQuery;}
			set{m_FatherQuery=value;}
		}
		internal ClassMap QueryClassMap
		{
			get{return this.queryClass;}
		}
		internal ArrayList Conditions
		{
			get{return this.m_Conditions;}
		}
		private ArrayList Querys
		{
			get{return this.m_Querys;}
		}

		/// <summary>
		///		是否排除重复行？
		/// </summary>
		public bool IsDistinct
		{
			set 
			{
				if (value)
				{
					this.selectString = "SELECT DISTINCT ";
				}
				else
				{
					this.selectString = "SELECT ";
				}
			}
		}

		/// <summary>
		///		返回Sql语句
		/// </summary>
		public string SqlString
		{
			get 
			{
				if(this._sqlString==null || this._sqlString.Length==0)
					BuildForQuery( PersistenceBroker.Instance().GetDatabaseCopy(this.DatabaseName).GetCommand());
				return this._sqlString;}
		}

		/// <summary>
		/// 数据源名称
		/// </summary>
		public string DatabaseName
		{
			get
			{
				if(this._databaseName==null)
					this._databaseName=this.queryClass.Database.Name;
				return this._databaseName;
			}
			set
			{
				this._databaseName=value;
			}
				
		}

	}
}
