using System;
using System.Xml;
using System.Collections;
using System.Data.OleDb;
using System.Data;

namespace PersistenceLayer
{
	/// <summary>
	/// XmlConfigLoader 的摘要说明。
	/// </summary>
	class XmlConfigLoader:IConfigLoader
	{
		private string thisNameSpace = "";			//实体层名字空间
		private string databaseXmlFile;				//数据库信息文件

		private Hashtable classMaps;				//Class Hash表
		private Hashtable databaseMaps;				//数据库Hash表
		private IDictionary databaseCollection;
		private ArrayList classMapFiles;
		private static XmlConfigLoader xmlConfigLoader;
		
		private XmlConfigLoader()
		{
			//XmlFile=fileName;
			//this.databaseXmlFile=databaseMapFile;

			classMaps=new Hashtable();
			databaseMaps=new Hashtable();
			databaseCollection=new Hashtable();
			this.classMapFiles = new ArrayList();

			this.thisNameSpace = this.ToString ().Substring (0,this.ToString ().LastIndexOf('.')+1);
		}

		public static XmlConfigLoader Instance()
		{
			if(xmlConfigLoader==null)
				xmlConfigLoader=new XmlConfigLoader();
			return xmlConfigLoader;

		}
		
		public Hashtable ClassMaps
		{
			get{return classMaps;}
		}
		public Hashtable DatabaseMaps
		{
			get{return databaseMaps;}
		}
		public IDictionary DatabasePool
		{
			get{return databaseCollection;}
		}
		
		//从指定文件中读取所有映射信息
		public void LoadConfig(string databaseMapFile)
		{
			this.databaseXmlFile=databaseMapFile;
			this.LoadDatabaseInformation();
			for(int i=0;i<this.classMapFiles.Count;i++)
			{
				string file=classMapFiles[i].ToString().Trim();
				if (file.IndexOf(':')<0)
				{
					file = databaseXmlFile.Substring (0,databaseXmlFile.LastIndexOf ('\\')+1) + file;
				}
				if(file=="")
				{
					throw new PlException("ClassMapFile路径不能为空！");
				}
				else
				{
					this.LoadClassMapInformation(file);
				}
			}
		}
		
		//从指定文件中读取类映射信息
		internal void LoadClassMapInformation(string xmlFile)
		{
			XmlDocument oXmlDocument=new XmlDocument();
			//string file=this.XmlFile;
			string file=xmlFile;
			try
			{
				oXmlDocument.Load(file);
				XmlNodeReader oXmlReader=new XmlNodeReader(oXmlDocument);
				while(oXmlReader.Read())
				{
					if (oXmlReader.NodeType!=XmlNodeType.Element) continue;
					ClassMap cls;
					//若是数据库映射Node
					switch (oXmlReader.Name.ToLower())
					{
						case "class":
							cls=GetClassMap(oXmlReader);
							if (cls!=null && (!classMaps.Contains(cls.Name))) classMaps.Add(cls.Name,cls);
							break;
							//若是关联关系
						case "association":
							Association a = GetAssociation (oXmlReader); 
							cls = (ClassMap)this.classMaps [a.FromClass];
							cls.AddAssociation (a);
							break;
					}
				}
			}
			catch(PlException ple)
			{
				throw ple;
			}
			catch(Exception e)
			{
				string s="读取类映射文件"+file+"发生错误,请确认你的文件路径和格式!";
				s=s+e.Message;
				throw new PlException(s,ErrorTypes.XmlError);
			}		
		}

		
		//从指定文件读取数据库映射信息及连接参数
		/// <summary>
		/// 从XML文件中获取数据库连接信息
		/// 
		/// </summary>
		private void LoadDatabaseInformation()
		{
			XmlDocument oXmlDocument=new XmlDocument();
			string file=this.databaseXmlFile;
			try
			{
				oXmlDocument.Load(file);
				XmlNodeReader oXmlReader=new XmlNodeReader(oXmlDocument);
				while(oXmlReader.Read())
				{
					if (oXmlReader.NodeType!=XmlNodeType.Element) continue;
					//若是数据库映射Node
					if (oXmlReader.Name.ToLower()=="database")
					{
						//根据数据库结点,生成一个raletionDatabase对象
						RelationalDatabase rdb=GetRelationalDatabase(oXmlReader);
						if (rdb!=null)
						{
							if(databaseCollection.Contains(rdb.Name))
								databaseCollection.Remove(rdb.Name);
							databaseCollection.Add(rdb.Name,rdb);
						}
					}
				}
			}
			catch(PlException ple)
			{
				throw ple;
			}
			catch(Exception e)
			{
				string s="读取数据库映射文件"+file+"时发生错误,请确认你的文件路径和文件格式！";
				s=s+e.Message;
				throw new PlException(s,ErrorTypes.XmlError);
				//throw e;
			}
		}
		//Xml文件流分析
		private void AddMap(XmlNodeReader oReader)
		{
			if (oReader.NodeType!=XmlNodeType.Element) return;
			
			switch (oReader.Name.ToLower())
			{
				case "class":					//如果是类映射结点
					ClassMap cls=GetClassMap(oReader);
					if (cls!=null) classMaps.Add(cls.Name,cls);
					break;
				case "database":
					RelationalDatabase rdb=GetRelationalDatabase(oReader);
					if (rdb!=null) databaseCollection.Add(rdb.Name,rdb);
					break;
				default:
					break;
			}
		}

		//从Xml Node结点(Name=class)得到相应ClassMap
		private ClassMap GetClassMap(XmlNodeReader node)
		{
			string className=node.GetAttribute("name");										//类名
			string tableName=node.GetAttribute("table");									//表名
			string databaseName=node.GetAttribute("database");								//数据库名
			string superClassName=node.GetAttribute("superclass");							//父类名
			//string timeStamp = node.GetAttribute ("timeStamp");							//时间戳
			
			string IsSaveToMemory=node.GetAttribute("IsSaveToMemory");
			if(IsSaveToMemory==null)
				IsSaveToMemory="false";

			int c = 0;
			string err;

			ClassMap clsMap=null;
			if ((className!=null) &&(databaseName!=null)&&(tableName!=null))
			{
				RelationalDatabase r=(RelationalDatabase)this.DatabasePool[databaseName];
				if ( r == null)
				{
					err = "名为：" + databaseName + "的数据库映射信息未找到！";
					throw new PlException (err,ErrorTypes.XmlError);
				}
				clsMap=new ClassMap(className,r);
				//从XML读取是否保存到内存值
				clsMap.IsSaveToMemory=IsSaveToMemory.ToLower()=="true"?true:false;

				if (superClassName!=null)
				{
					clsMap.SuperClass=(ClassMap)classMaps[superClassName];
					if (clsMap.SuperClass == null)
					{
						err = "在superclass中引用 " + superClassName + "前，必须对该类映射！";
						throw new PlException (err,ErrorTypes.XmlError);
					}
					clsMap.SelfClassAttributes = new ArrayList ();
				}
				else
				{
					//如果该类没有父类，则把SelfClassAttributes指向AttributesMapToTable
					clsMap.SelfClassAttributes = clsMap.AttributesMapToTable;
				}
				//生成一个DatabaseMap对象，并加如databaseMaps
				//若它已存在则直接引用
				if (!databaseMaps.ContainsKey(databaseName))
				{
					err = "在映射类：" + className + "时，名为：" + databaseName + "的数据库映射信息找不到！";
					throw new PlException (err,ErrorTypes.XmlError);
				}
				//
				TableMap tblMap=new TableMap(tableName,(DatabaseMap)databaseMaps[databaseName]);

				int clsDep=node.Depth;
				while(node.Read()&&node.Depth>clsDep)
				{
					if ((node.NodeType==XmlNodeType.Element)&&(node.Name=="attribute"))
					{
						//若该Node是Element类型能 且 它的名字是attribute
						AttributeMap attrMap = GetAttributeMap(node,clsMap,tblMap,c);
						if (attrMap!=null) 
						{
							clsMap.AddAttributeMap(attrMap);
						}
						c++;
					}
				}
			}
			else
			{
				err = "ClassMap 缺少className,databaseName,tableName 这些必要属性！";
				throw new PlException (err,ErrorTypes.XmlError);
			}
			//缺乏主键
			if (clsMap.Table.PrimaryKeyIndex < 0)
			{
				err = "表 " + clsMap.Table.Name + " 未定义主键！";
				throw new PlException (err,ErrorTypes.XmlError);
			}
			return clsMap;
		}

		private AttributeMap GetAttributeMap(XmlReader reader,ClassMap clsMap,TableMap tblMap,int colIndex)
		{
			string attrName=reader.GetAttribute("name");				//类属性名
			string attrColumn=reader.GetAttribute("column");			//属性对应列名
			string attrKey=reader.GetAttribute("key");					//键类型
			string attrReference=reader.GetAttribute("reference");		//引用父类属性
			string dataType=reader.GetAttribute("type");				//数据类型
			string autoIncrement=reader.GetAttribute("increment");		//是否自动增加
			string timestamp = reader.GetAttribute ("timestamp");		//时间戳
			string autoValue = reader.GetAttribute ("auto");			//是否自动值

			AttributeMap attrMap=null;
			
			if (attrName!=null)
			{
				ColumnMap colMap=null;
				//生成一个AttributeMap
				attrMap=new AttributeMap(attrName);
				if (attrColumn!=null)
				{
					colMap=new ColumnMap(attrColumn,tblMap);

					if (attrKey!=null)
					{
						//设置键类型
						if (attrKey.ToUpper()=="PRIMARY")
						{
							colMap.KeyType=ColumnKeyTypes.PrimaryKey;
						}
						else if (attrKey.ToUpper()=="FOREIGN")
						{
							colMap.KeyType=ColumnKeyTypes.ForeignKey;
						}
					}
					if(dataType==null)
					{
						string s="不能确定列"+clsMap.GetFullyQualifiedName(colMap.Name)+"的数据类型！";
						throw new PlException(s,ErrorTypes.XmlError);
					}
					//确定数据列的数据类型
					//Xml中所有数据类型为OleDbType中的枚举类型
					//将其映射到 DbType
					switch (dataType.ToLower())
					{
						case "boolean":
							colMap.Type=DbType.Boolean;
							attrMap.SqlValueStringType = clsMap.RelationalDatabase.SqlValueType (DbType.Boolean);
							break;
							//Map to Int64
						case "bigint":
							colMap.Type=DbType.Int64;
							break;
							//Map to Binary
						case "binary":
							colMap.Type = DbType.Binary;
							attrMap.SqlValueStringType = SqlValueTypes.NotSupport;
							break;
							//Map to Decimal
						case "currency":
							colMap.Type=DbType.Currency;
							break;
							//Map to DateTime
						case "date":
							//对Access的数据库做特殊处理
							if (clsMap.RelationalDatabase.Vendor == MsAccess.VENDOR_NAME)
							{
								colMap.Type=DbType.Object;
								attrMap.SqlValueStringType = SqlValueTypes.AccessDateString;
							}
							else if(clsMap.RelationalDatabase.Vendor==DatabaseVendor.Oracle) //如果是oracle的话，则会对日期使用to_date转换
							{
								colMap.Type=DbType.DateTime;
								attrMap.SqlValueStringType = SqlValueTypes.OracleDate;
							}
							else
							{
								colMap.Type=DbType.DateTime;
								attrMap.SqlValueStringType = SqlValueTypes.SimpleQuotesString;
							}
							break;
							//Mapt to Date
						case "dbdate":
							colMap.Type = DbType.Date;
							attrMap.SqlValueStringType = SqlValueTypes.SimpleQuotesString;
							break;
							//Map to Decimal
						case "decimal":
							colMap.Type=DbType.Decimal;
							break;
							//Map to Double
						case "double":
							colMap.Type=DbType.Double;
							break;
							//Map to Guid
						case "guid":
							colMap.Type=DbType.Guid;
							attrMap.SqlValueStringType = SqlValueTypes.SimpleQuotesString;
							break;
							//Map to Object
						case "object":
							colMap.Type = DbType.Object;
							attrMap.SqlValueStringType = SqlValueTypes.NotSupport;
							break;
							//Map to Single
						case "single":
							colMap.Type = DbType.Single;
							break;
							//Map to Int16
						case "smallint":
							colMap.Type=DbType.Int16;
							break;
							//Map to SBtype
						case "tinyint":
							colMap.Type = DbType.Byte;
							break;
							//Map to Int32
						case "integer":
							colMap.Type=DbType.Int32;
							break;
							//Map to String
						case "varchar":
						case "string":
							colMap.Type=DbType.String;
							attrMap.SqlValueStringType = SqlValueTypes.String;
							break;
						
							
						default:
							throw new PlException("目前仍不支持的数据类型！",ErrorTypes.XmlError);
					}
					//该列不是自动增加
					if(autoIncrement!=null && autoIncrement.ToLower()=="true")
					{
						colMap.IsAutoValue=true;
						tblMap.AutoIdentityIndex = colIndex;
						clsMap.AutoIdentityAttribute = attrName;
					}

					//是不是timestamp
					if (timestamp != null && timestamp.ToLower()=="true")
					{
						tblMap.TimestampColumn = attrColumn;
						if(clsMap.RelationalDatabase.Vendor==DatabaseVendor.MySql)
							tblMap.TimestampParameter = "?" + tblMap.TimestampColumn ;
						else
							tblMap.TimestampParameter = "@" + tblMap.TimestampColumn ;
						//tblMap.TimestampParameter = "@" + tblMap.TimestampColumn ;
						clsMap.TimestampAttribute = attrMap;
						colMap.IsAutoValue = true ;
					}

					//是不是自动值
					if (autoValue !=null)
					{
						colMap.IsAutoValue = true;
					}
				}// end of if (column!=null)
				
				attrMap.Column=colMap;
				
				if ((attrReference!=null)&&(clsMap.SuperClass!=null))
				{
					//若该属性来自父类，则它的Reference属性指向父类的AttributeMap
					AttributeMap r=clsMap.SuperClass.GetAttributeMap(attrReference,true);
					if (r!=null) attrMap.Reference=r;
				}

			}
			return attrMap;
		}
	
		//从Xml Element(association)得到相应 Association
		private Association GetAssociation (XmlNodeReader node)
		{
			int dep = node.Depth;
			Association a = new Association ();
			//取得关联名
			string associationName = node.GetAttribute ("name").Trim ();
			if (associationName == null||associationName == "")
			{
				throw new PlException ("Association定义错误：关联未设置 name！");
			}
			
			while( node.Read () && node.Depth > dep)
			{
				//若该Node是Element类型
				if (node.NodeType==XmlNodeType.Element)
				{
					string elementName = node.Name;
					switch (elementName)
					{
						case "fromClass":
							a.FromClass = node.ReadString ().Trim ();
							//未找到名为：FromClass的类的Map信息
							if (classMaps[a.FromClass] == null)
							{
								string err = "关联" + associationName + "的FromClass:" + 
									a.FromClass + "未找到Map信息！";
								throw new PlException (err);
							}
							break;
						case "toClass":
							a.ToClass = node.ReadString ().Trim ();
							//未找到名为：ToClass的类的Map信息
							if (classMaps[a.ToClass] == null)
							{
								string err = "关联" + associationName + "的ToClass:" + 
									a.ToClass + "未找到Map信息！";
								throw new PlException (err);
							}
							break;
						case "cardinality":
							string t = node.ReadString ().Trim ();
							if (t == "oneToMany")
							{
								a.Cardinality = CardinalityTypes.OneToMany;
							}
							else
							{
								if (t == "oneToOne")
									a.Cardinality = CardinalityTypes.OneToOne;
								else
									throw new PlException(associationName +"中不能识别的Cardinality定义！");
							}
							break;							 
						case "target":
							a.Target = node.ReadString ().Trim ();
							break;
						case "retrieveAutomatic":
							if (node.ReadString ().Trim () == "true")
							{
								a.RetrieveAutomatic = true;
							}
							break;
						case "deleteAutomatic":
							if (node.ReadString ().Trim () == "true")
							{
								a.DeleteAutomatic = true;
							}
							break;
						case "saveAutomatic":
							if (node.ReadString ().Trim () == "true")
							{
								a.SaveAutomatic = true;
							}
							break;
						case "fromAttribute":
							a.FromAttribute = node.ReadString ().Trim ();
							break;
						case "toAttribute":
							a.ToAttribute = node.ReadString ().Trim ();
							break;
						default:
							throw new PlException ("associationName" +"中不能识别定义:" + elementName);
					}
				}
			}
			if (a.Cardinality == CardinalityTypes.None)
			{
				throw new PlException (associationName +"关联中cardinality属性未设置！");
			}
			return a;
		}
	
		//根据Node结点生成一个相应的RelationalDatabase对象
		/// <summary>
		/// 从一个数据库配置节点,获取一个数据库连接
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		private RelationalDatabase GetRelationalDatabase(XmlNodeReader node)
		{
			string dbName=node.GetAttribute("name");
			string dbType = node.GetAttribute ("type");
			
			RelationalDatabase rdb=null;
			if ((dbName!=null))								//Mark!!!!!!	
			{
				if (dbType == null || dbType =="")
				{
					throw new PlException ("不支持的数据库类型！",ErrorTypes.XmlError);
					//rdb=new OtherDatabase ();				
				}
				else
				{
					string dbClassName = thisNameSpace + dbType;
					try
					{
						//根据数据库的类型名称.创建对应的数据库对象.
						//如:MsSqlServer,则自动生成此实例
						rdb = (RelationalDatabase)this.GetType().Assembly.CreateInstance (dbClassName);
						if (rdb == null) throw new Exception();
					}
					catch
					{
						string error = "创建类型为：" + dbClassName + "的数据库出错！";
						throw new PlException (error,ErrorTypes.XmlError);
					}
				}
				rdb.Name = dbName;
				if(databaseMaps.Contains(dbName))
					databaseMaps.Remove(dbName);
				databaseMaps.Add(dbName,new DatabaseMap(dbName));
				int i=node.Depth;

				string pName,pValue,connectionString = "";
				while(node.Read()&&node.Depth>i)
				{
					//若该Node是Element类型能 且 它的名字是parameter
					if((node.NodeType==XmlNodeType.Element)&&(node.Name=="parameter"))
					{
						pName = node.GetAttribute("name");
						pValue = node.GetAttribute("value");

						//tintown add 2005-3-10 支持access数据库相对地址 ,laser add 2005-4-15 对网络绝对路径的修改
						if(pValue.EndsWith("mdb") && pValue.IndexOf(':')<0&&pValue.IndexOf('\\')<0)
							pValue=databaseXmlFile.Substring (0,databaseXmlFile.LastIndexOf ('\\')+1) +pValue;

						if((pName != null)&&( pValue!=null)) 
						{
							connectionString += pName + "=" + pValue + ";";
						}
					}
					else if(node.Name.ToLower()=="classmapfile")
					{	
						if(node.GetAttribute("path")!=null)
						{
							this.classMapFiles.Add(node.GetAttribute("path"));
						}
					}
				}
				rdb.Initialize (connectionString);
				//Setting.Instance().SetConnectionString(rdb.Name,rdb.ConnectionString);
			}
			return rdb;
		}
	}
}
