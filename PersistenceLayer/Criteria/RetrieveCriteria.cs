using System;
using System.Collections;
using System.Data;
using System.Text;

namespace PersistenceLayer
{
	/// <summary>
	///		RetrieveCriteria �����װ��һ�λ�ȡһ������EntityObject�������Ϊ��
	/// </summary>
	public class RetrieveCriteria
	{
		private bool m_IsIncludeAssociation = false;
		private string forClassName ;
		private ClassMap _classMap ;
		private PersistenceBroker _broker = PersistenceBroker.Instance ();
		private ArrayList _conditions = new ArrayList ();
		private ArrayList _orderList = null;
		private string _sqlString;
		private Type _forClass ;
		private int m_top=0;
		private string _databaseName=null;
		private string selectClause="";

		private string sTemp ;
		private string endQuostationMarks;

		private string selectString = "SELECT ";

		//�������д���RetrieveCriteriaʱʹ��(tintown)
		private RelationalDatabase m_rdb=null;

		internal RelationalDatabase Rdb
		{
			set
			{
				m_rdb=value;
			}
			get
			{
				return m_rdb;
			}
		}
		
		
		/// <summary>
		/// ���ڿ��ƴ˷����Ƿ�Ҫ�������ڴ�
		/// false:���������ڴ��С���ÿ�ζ������ݿ��ȡ����ΪĬ��ֵ
		/// true:���浽�ڴ��У�����ڴ��д��ھ�ֱ�Ӵ��ڴ��ж�ȡ�����ڻ������ݽ���ʹ�ô˹��ܡ�
		/// ���ڲ��������ݲ��Ƽ����⵼�´����ڴ汻ռ
		/// </summary>
		private bool _IsSaveInMemory=false;

		/// <summary>
		/// ���Ҫ���浽�ڴ�Ļ������Ѵ�ֵ��ΪΨһ��KEYֵ��
		/// </summary>
		private string _memoryKey="";

		/// <summary>
		///		����һ��RetrieveCriteriaʵ��
		/// </summary>
		/// <param name="classType">EntityObject��Type����</param>
		public RetrieveCriteria(Type classType)
		{
			this._forClass = classType;
			this.forClassName = EntityObject.GetClassName (classType);
			this._classMap = _broker.GetClassMap (forClassName);
			this._databaseName=this._classMap.Database.Name;
			this._IsSaveInMemory=this._classMap.IsSaveToMemory;
			sTemp = " AS " + _classMap.RelationalDatabase.QuotationMarksStart;
			endQuostationMarks = _classMap.RelationalDatabase.QuotationMarksEnd;
			
			
		}


		/// <summary>
		/// add by tintown at 2004-10-23
		/// </summary>
		/// <param name="classType"></param>
		/// <param name="databaseName"></param>
		public RetrieveCriteria (Type classType,string databaseName)
		{
			this._forClass = classType;
			this.forClassName = EntityObject.GetClassName (classType);
			this._classMap = _broker.GetClassMap (forClassName);
			this._databaseName=databaseName;
			this._IsSaveInMemory=this._classMap.IsSaveToMemory;
			sTemp = " AS " + _classMap.RelationalDatabase.QuotationMarksStart;
			endQuostationMarks = _classMap.RelationalDatabase.QuotationMarksEnd;
		}


		/// <summary>
		///����һ�����ԣ�������������Ϊ����DataTable������
		/// </summary>
		/// <param name="attributeName"></param>
		public void AddSelect(string attributeName)
		{
			AttributeMap am=this._classMap.GetAttributeMap(attributeName,true);
			selectClause+=","+_classMap.GetFullyQualifiedName(am.Column.Name) + sTemp + am.Name + endQuostationMarks;
		}

		/// <summary>
		///		����һ������ ����asName��Ϊ����DataTable������ 
		/// </summary>
		public void AddSelect(string attributeName,string asName)
		{
			AttributeMap am=this._classMap.GetAttributeMap(attributeName,true);		
			selectClause+=","+_classMap.GetFullyQualifiedName(am.Column.Name) + sTemp + asName + endQuostationMarks;
		}

		/// <summary>
		/// ����ѡ�����е�����
		/// </summary>
		/// <param name="all"></param>
		public void AddSelect(AttributeType all)
		{
			if(all==AttributeType.All)
			{
				AttributeMap am;
				for(int i=0;i<this._classMap.Attributes.Count;i++)
				{
					am=this._classMap.GetAttributeMap(i);
					selectClause+=","+_classMap.GetFullyQualifiedName(am.Column.Name) + sTemp + am.Name + endQuostationMarks;
				}
			}
		}

		private string GetSelectClause()
		{
			string tempSelect="";
			if (selectClause != "")
			{
				tempSelect = this.selectString + selectClause.Remove(0,1);
				tempSelect +=" FROM " + this._classMap.RelationalDatabase.QuotationMarksStart+this._classMap.Table.Name+this._classMap.RelationalDatabase.QuotationMarksEnd;
                //��־��2008-6-30����ӣ���nolock
                if (this._classMap.RelationalDatabase.Vendor == DatabaseVendor.MsSqlServer)
                    tempSelect += " With(nolock) ";
			}
			else
			{
				tempSelect=_classMap.SelectFromClause;
			}
			return tempSelect;
		}

		//Ϊ��ʵ�ַ�ҳ���ܣ���Ҫ��ȡ��������
		private string GetSelectKeyClause()
		{
			string tempSelect="select ";
			for(int i=0;i<this.classMap.GetKeySize();i++)
			{
				tempSelect += this._classMap.RelationalDatabase.QuotationMarksStart+this.classMap.GetKeyAttributeMap(i).Column.Name+this._classMap.RelationalDatabase.QuotationMarksEnd+",";
			}
			
			tempSelect = tempSelect.TrimEnd(new char[]{','});
			tempSelect +=" FROM " + this._classMap.RelationalDatabase.QuotationMarksStart+this._classMap.Table.Name+this._classMap.RelationalDatabase.QuotationMarksEnd;
            //��־��2008-6-30����ӣ���nolock
            if (this._classMap.RelationalDatabase.Vendor == DatabaseVendor.MsSqlServer)
                tempSelect += " With(nolock) ";
		
			return tempSelect;
		}

		

		internal void BuildStringForRetrieve ()
		{
				this._sqlString = SqlCommander.BuildForConditions ( _conditions);

				//�������ʹ��Where�Ӿ�
				if (_classMap.SuperClass == null && _sqlString == null)
				{
					_sqlString = GetSelectClause();
				}
				else
				{
					//���δָ��������������_classMap.SuperClass !=null
					if (_sqlString == null)
					{
						_sqlString = GetSelectClause() + " WHERE " + _classMap.StringForInherit;
					}
						//���ָ���˹�������(_sqlString!= null)
					else
					{
						_sqlString = GetSelectClause() + " WHERE " + _sqlString ;
						//���и���
						if (_classMap.SuperClass != null)
						{
							_sqlString += " AND " + _classMap.StringForInherit;
						}
					}
				}
				//Order by
				if (this._orderList != null)
				{
					this._sqlString += SelectCommander.GetOrderSql (_orderList,_classMap);
				}
			
		}

		/// <summary>
		/// ��ȡֻȡ������SQL���
		/// </summary>
		internal string BuildKeyStringForRetrieve ()
		{
			this._sqlString = SqlCommander.BuildForConditions ( _conditions);
			string tempre="";

			//�������ʹ��Where�Ӿ�
			if (_classMap.SuperClass == null && _sqlString == null)
			{
				tempre = this.GetSelectKeyClause();
			}
			else
			{
				//���δָ��������������_classMap.SuperClass !=null
				if (_sqlString == null)
				{
					tempre = GetSelectKeyClause() + " WHERE " + _classMap.StringForInherit;
				}
					//���ָ���˹�������(_sqlString!= null)
				else
				{
					tempre = GetSelectKeyClause() + " WHERE " + _sqlString ;
					
				}
			}
			//Order by
			if (this._orderList != null)
			{
				tempre += SelectCommander.GetOrderSql (_orderList,_classMap);
			}
			return tempre;
			
		}


		/// <summary>
		///		����һ��ObjectCursor���� 
		/// </summary>
		/// <returns>ObjectCursor</returns>
		public ObjectCursor AsCursor()
		{
			//tintown add at 2004-10-23
//			if(this._databaseName==null)
//				this._databaseName=this._classMap.Database.Name;
//			IDbCommand  cmd = this._broker.GetCommand (_databaseName);
//			this.BuildStringForRetrieve ();
//			cmd.CommandText = this._sqlString;
//			DataTable dt = _broker.ExecuteQuery (cmd,_databaseName);
			DataTable dt=AsDataTable();
			//DataSet ds = new DataSet();
			//ds.Tables.Add (dt);
			ObjectCursor aCursor= new ObjectCursor(this.ForClass,dt,this.m_IsIncludeAssociation);			
			return aCursor;
		}

		/// <summary>
		/// tintown add at 2004-10-24
		/// �˷�����Ҫ��������Щ��������ѯ���ܼ�ʱ��ȡΨһ����
		/// �����ѯ�д��ڶ������ϵͳ�򷵻ص�һ����¼
		/// </summary>
		/// <returns></returns>
		public EntityObject AsEntity()
		{
			EntityObject eo=null;
			ObjectCursor oc=AsCursor();
			if(oc.HasObject())
			{
				eo=(EntityObject)oc.NextObject();
				
			}

			return eo;
		}

		/// <summary>
		/// ����ʵ�弯
		/// </summary>
		/// <returns></returns>
		public EntityContainer AsEntityContainer()
		{
			EntityContainer ec=new EntityContainer();
			ObjectCursor oc=AsCursor();
			for(int i=0;i<oc.Count;i++)
			{
				ec.Add((EntityObject)oc.NextObject());
			}
			return ec;
			

		}
		
		/// <summary>
		///		���ݲ�ѯ��������һ�������
		/// </summary>
		/// <returns>DataTable</returns>
		public DataTable AsDataTable()
		{
			//tintown add at 2004-10-23

			return _broker.ProcessRetrieveCriteria(this);

		}

		/// <summary>
		/// �Զ���ҳ���˹��ܣ�Ч�����
		/// Ŀǰֻ��SQL SERVER���Թ����������д�����
		/// ע������TOP���˵Ĳ����ã�
		/// </summary>
		/// <param name="pageSize">ÿҳ������</param>
		/// <param name="pages">�ڼ�ҳ</param>
		/// <param name="counts">��������������з�ҳҳ����ʾ��</param>
		/// <returns></returns>
		public DataTable AsDataTable(int pageSize,int pages,out int counts)
		{
			return _broker.ProcessRetrieveCriteria(this,pageSize,pages,out counts);
		}
		
		/// <summary>
		///		�õ�һ�������������
		/// </summary>
		public Condition GetNewCondition()
		{
			Condition c = new Condition (this._classMap);
			this._conditions.Add (c);
			return c;
		}

		/// <summary>
		///		�ȼ���OrderBy(attributeName,true)
		/// </summary>
		public void OrderBy(string attributeName)
		{
			this.OrderBy(attributeName,true);
		}
		/// <summary>
		///		��ȡ�Ķ�������
		/// </summary>
		/// <param name="attributeName">�������ݵ�����</param>
		/// <param name="isAsc">isAsc=true ,����������isAsc=false �򰴽�������</param>
		public void OrderBy(string attributeName ,bool isAsc)
		{
			OrderEntry order=new OrderEntry(attributeName,isAsc);
			if (this._orderList == null)
			{
				_orderList = new ArrayList();
			}
			this._orderList.Add (order);
		}
		
		/// <summary>
		///		������������õ�Condition����
		/// </summary>
		public void Clear()
		{
			this._conditions.Clear ();
		}

		/*����*/
		/// <summary>
		///		ָ�������Type����
		/// </summary>
		public Type ForClass
		{
			get
			{
				return this._forClass;
			}
		}
		/// <summary>
		///		�Ƿ��ȡ������������ 
		/// </summary>
		internal bool IsIncludeAssociation
		{
			get
			{
				return this.m_IsIncludeAssociation;
			}
			set
			{
				this.m_IsIncludeAssociation = value;
			}
		}
	
		/// <summary>
		///		����Sql���
		/// </summary>
		public string SqlString
		{
			get {
				if(this._sqlString==null || this._sqlString.Length==0)
					BuildStringForRetrieve();
                Logger.Write(this._sqlString);  //add by duhu
				return this._sqlString;}
		}

		/// <summary>
		/// ͷN������
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
		/// ����Դ����
		/// </summary>
		public string DatabaseName
		{
			set
			{
				this._databaseName=value;
			}
			get
			{
				if(this._databaseName==null)
					this._databaseName=this._classMap.Database.Name;
				return this._databaseName;
			}
		}

		internal bool IsSaveInMemory
		{
			set
			{this._IsSaveInMemory=value;}
			get
			{return this._IsSaveInMemory;}
		}


		/// <summary>
		/// 
		/// </summary>
		internal string MemoryKey
		{
			set
			{this._memoryKey=value;}
			get
			{
				if(this._memoryKey=="")
					this._memoryKey=this.DatabaseName +"_"+this.ForClass.Name;
				return this._memoryKey;
			}
		}
	

		internal ClassMap classMap
		{
			get
			{
				return this._classMap;
			}
		}
	}
}
