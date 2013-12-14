using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace PersistenceLayer
{
	/// <summary>
	/// ClassMap 的摘要说明。
	/// </summary>
	class ClassMap
	{
		private string _name;
		
		private SelectCommander select=null;
		private InsertCommander	insert= null;
		private UpdateCommander update= null;
		private DeleteCommander delete= null;

		
		private Hashtable _hashAttributeMaps;
		private ArrayList _attributeMaps;
		private ArrayList _keyAttributeMaps;			//主键属性集合
		private ArrayList _referenceAttributeMaps;		//
		private ArrayList _selfAttributeMaps;

		private TableMap m_TableMap=null;
		private RelationalDatabase _relationalDb;		//该类影射的数据库
	
		private ClassMap _superClass;
		private IList associations=null;
		
		private AttributeMap _timestampAttribute;
		private string _autoIdentityAttribute;			//自动标识属性

		/// <summary>
		/// tintown add at 2004-10-29
		/// 此实体是否要保存到内存中
		/// </summary>
		private bool _IsSaveToMemory=false;

		//Constructor
		public ClassMap(string name,RelationalDatabase relationalDb)
		{
			_name=name;							//类名
			_relationalDb=relationalDb;			//数据库名

			_hashAttributeMaps=new Hashtable();
			_attributeMaps=new ArrayList();
			_keyAttributeMaps=new ArrayList();
			_referenceAttributeMaps= new ArrayList();
		}

		public string Name					
		{
			get{return _name;}
		}

		public bool IsSaveToMemory
		{
			set
			{
				this._IsSaveToMemory=value;
			}
			get
			{
				return this._IsSaveToMemory;
			}
		}

		public IList Associations
		{
			get {return this.associations;}
		}

		public ArrayList Attributes
		{
			get{return _attributeMaps;}
		}
		
		//Adds attribute map to this class map
		public void AddAttributeMap(AttributeMap attribute)
		{
			_hashAttributeMaps.Add(attribute.Name,attribute);
			
			if (attribute.Column!=null)
			{
				if (this.m_TableMap==null) this.m_TableMap=attribute.Column.Table;
				
				if(attribute.Column.KeyType == ColumnKeyTypes.PrimaryKey)					
				{
					this.m_TableMap.PrimaryKeyIndex ++;
					_keyAttributeMaps.Add(attribute);
					
					_attributeMaps.Insert (m_TableMap.PrimaryKeyIndex, attribute);
				}
					//不是PrimaryKey
				else
				{
					_attributeMaps.Add(attribute);	
				}

				if (attribute.Reference!=null)
				{
					_referenceAttributeMaps.Add(attribute);
				}
				else
				{
					//该类如果有父类，且此attribute 并非引用自其他类
					if (this._superClass != null)
					{
						this._selfAttributeMaps.Add(attribute);
					}
				}
			}
		}


		//Returns AttributeMap for the given index
		public AttributeMap GetAttributeMap(int index)
		{
			return (AttributeMap)_attributeMaps[index];
		}

		//Returns AttributeMap for the given attribute name
		public AttributeMap GetAttributeMap(string name,bool isSuperClassInc)
		{
			AttributeMap oAttribute=null;
			ClassMap oClassMap=this;
			
			do
			{
				oAttribute=(AttributeMap) oClassMap._hashAttributeMaps[name];
				oClassMap=oClassMap.SuperClass;
			}
			while (isSuperClassInc&&oAttribute==null&&oClassMap!=null);
			
			if (oAttribute==null)
				throw new PlException("不能在" + this._name + "的映射信息中找到属性"+name+"!");
			return oAttribute;
		}
		
		public AttributeMap GetAttributeMap(string name)
		{
			return this.GetAttributeMap(name,false);
		}
		
		//Returns key AttributeMap for the given index
		public AttributeMap GetKeyAttributeMap(int index)
		{
			return (AttributeMap)_keyAttributeMaps[index];
		}
		//ClassMap for superclass
		public ClassMap SuperClass
		{
			get{return _superClass;}
			set{_superClass=value;}
		}

		//Returns number of attribute maps.
		public int GetSize()
		{
			return _attributeMaps.Count;
		}

		
		//Returns number of key attribute maps
		public int GetKeySize()
		{
			return _keyAttributeMaps.Count;
		}

		//Returns number of the reference attribute map
		public int GetReferenceSize()
		{
			return _referenceAttributeMaps.Count; 
		}
		
		//Return reference AttributeMap for the given index
		public AttributeMap GetReferenceAttributeMap(int index)
		{
			return (AttributeMap)_referenceAttributeMaps[index];
		}

	
		//Returns select sql statement for the timestamp attribute of the given object
		public IDbCommand GetSelectSqlFor(EntityObject obj)
		{
            return GetSelectSqlFor(obj,false);
		}

        public IDbCommand GetSelectSqlFor(EntityObject obj,bool WithNoLock)
        {
            if (select == null)
            {
                this.select = new SelectCommander(this);
            }
            return this.select.BuildForObject(obj,WithNoLock);
        }

		//Return select sql statment for association
		public IDbCommand GetSelectSqlFor(SelectionCriteria aSelectionCriteria)
		{
			if (select==null)
			{
				this.select=new SelectCommander(this);
			}
			return this.select.BuildForSelectionCriteria (aSelectionCriteria);
		}
		
		//Returns insert sql statement for the timestamp attribute of the given object
		public IDbCommand GetInsertSqlFor(EntityObject obj)
		{
			if (insert==null)
			{
				this.insert=new InsertCommander(this);
			}
			return this.insert.BuildForObject(obj);
		}


		//Returns update sql statement for the timestamp attribute of the given object
		public IDbCommand GetUpdateSqlFor(EntityObject obj)
		{
			if (update==null)
			{
				this.update=new UpdateCommander(this);
			}
			return this.update.BuildForObject(obj);
		}
		
		//Returns delete sql statement for the timestamp attribute of the given object
		public IDbCommand GetDeleteSqlFor(EntityObject obj)
		{
			if (this.delete==null)
			{
				this.delete =new DeleteCommander(this);
			}
			return this.delete.BuildForObject(obj);
		}

		//
		internal void SetObject(EntityObject obj,DataRow row)
		{
			AttributeMap am;
			ClassMap cm = this;
			int count;
			do
			{
				//tintown add at 2004-10-24
				if(obj.DatabaseName==null)
					obj.DatabaseName=cm.RelationalDatabase.Name;
				obj.IsPersistent=true;

				count = cm._selfAttributeMaps.Count;

				//判断是否全部取出对象属性
				if(row.ItemArray.Length<count)  //在非全部属性的情况下，只提取选择的属性值
				{
					for(int i=0;i<count;i++)
					{
						am = (AttributeMap) cm._selfAttributeMaps[i];
						if(row.Table.Columns.Contains(am.Column.Name) && row[row.Table.Columns.IndexOf(am.Column.Name)]!=DBNull.Value)
						{
							obj.SetAttributeValue(am.Name,row[row.Table.Columns.IndexOf(am.Column.Name)]);   //tintown modify
						}
					}
				
				}else    //对象的所有属性
				{
					for(int i=0;i<count;i++)
					{
						am = (AttributeMap) cm._selfAttributeMaps[i];
						if (row[i]!=DBNull.Value)   //tintown modify
						{
							obj.SetAttributeValue(am.Name,row[i]);   //tintown modify
						}
					}
				}
				
				
			
				cm = cm.SuperClass;
			}while (cm != null);
		}
		
		public void AddAssociation(Association aAssociation)
		{
			if (this.associations==null)
			{	
				//Association的合法性检查
				if (aAssociation.FromClass == ".")
				{
					throw new PlException("Association对象的FromClass属性未初始化！");
				}
				if (aAssociation.ToClass == ".")
				{
					throw new PlException("Association对象的FromClass属性未初始化！");
				}
				if(aAssociation.Target==".")
				{
					throw new PlException("Association对象的Target属性未初始化！");
				}
				this.associations=new ArrayList();
			}
			this.associations.Add(aAssociation);
			//this._hashAttributeMaps(
		}

		//有关联否?
		public bool HasAssociation()
		{
			return (this.associations==null)?false:true;
		}

		/*属性*/
		
		//该类映射的表名
		public TableMap Table
		{
			get
			{
				return this.m_TableMap;
			}
		}
		//该类所在数据库
		public DatabaseMap Database
		{
			get{return this.Table.Database;}
		}
		//数据库平台
		public RelationalDatabase RelationalDatabase
		{
			get{return this._relationalDb;}
		}
		
		public AttributeMap TimestampAttribute
		{
			get {return this._timestampAttribute;}
			set {this._timestampAttribute = value;}
		}

		public string AutoIdentityAttribute
		{
			get{return this._autoIdentityAttribute;}
			set{this._autoIdentityAttribute = value;}
		}

		public ArrayList SelfClassAttributes
		{
			get {return this._selfAttributeMaps;}
			set {this._selfAttributeMaps = value;}
		}

		public ArrayList AttributesMapToTable
		{
			get{return this._attributeMaps;}
		}

		public string SelectFromClause 
		{
			get 
			{
				if (select==null)
				{
					this.select=new SelectCommander(this);
				}
				return this.select.SelectClause;
			}
		}

		public string DeleteFromClause 
		{
			get 
			{
				if (delete==null)
				{
					this.delete=new DeleteCommander(this);
				}
				return this.delete.DeleteClause;
			}
		}


	
		public string StringForInherit
		{
			get 
			{
				if (select==null)
				{
					this.select=new SelectCommander(this);
				}
				return this.select.StringForInherit;
			}
		}


		/// <summary>
		/// 获取字段完整的名称，比如“[table].[column]”tintown add at 2005-4-21
		/// </summary>
		/// <param name="columnName"></param>
		/// <returns></returns>
		public string GetFullyQualifiedName(string columnName)
		{
			return this.RelationalDatabase.QuotationMarksStart + this.Table.Name +this.RelationalDatabase.QuotationMarksEnd +"."+this.RelationalDatabase.QuotationMarksStart+columnName+this.RelationalDatabase.QuotationMarksEnd;
		}
	}
}
