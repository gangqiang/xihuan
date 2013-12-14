using System;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace PersistenceLayer
{
	/// <summary>
	///		所有实体类的基类,其他实体类都从此继承
	///	</summary>
	[Serializable]
	public abstract class EntityObject  
	{
		
		private bool persistent=false; 
	

		/// <summary>
		/// 实体自己的数据源，在进行实体的操作时，使用此数据库
		/// 以便于实体动态的进行不同数据库的存取
		/// tintown add by 2004-10-23
		/// </summary>
		private string _databaseName;	

		/// <summary>
		/// 实体是否要保存到内存中，从XML中读取此值
		/// </summary>
		private bool _IsSaveInMemory=false;

		private string _memoryKey="";
	
//		private string _dirtyKey="";
//		private string _dirtyCode="";
//		private bool _dirtyUpdate=false;

		//
		// 当前EntityObject对象的System.Type对象
		//
		private Type thisType;

		/// <summary>
		///		返回Type类对象classType的对象名
		/// </summary>
		public static string GetClassName(Type classType)
		{
			string name=classType.ToString();
			return name.Substring(name.LastIndexOf('.')+1);
		}

		/// <summary>
		///		constructor
		/// </summary>
		public EntityObject()
		{
			this.thisType = this.GetType ();
			this._IsSaveInMemory =PersistenceBroker.Instance().GetClassMap(thisType.Name).IsSaveToMemory;
			
			
		}

	
		/// <summary>
		///		返回对象的类名 
		/// </summary>
		internal virtual string GetClassName()
		{
			string name=this.ToString();
			return name.Substring(name.LastIndexOf('.')+1);
		}

		/// <summary>
		///	使对象实体化,根据IsPersistent决定是更新还是插入操作
		///	如果IsPersistent为True，则自动会进行Update操作，在使用Retrieve()后会自动赋上此值，如：
		///	entity.Retrieve();
		///	if(entity.IsPersistent)
		///	{会进行更新操作}
		///	虽然IsPersistent是public的，但一般的情况下，不建议进行赋值。
		/// </summary>
		public int Save()
		{
			
			PersistenceBroker broker=PersistenceBroker.Instance();
			this._IsSaveInMemory =broker.GetClassMap(thisType.Name).IsSaveToMemory;
			return broker.SaveObject(this);
		}
		/// <summary>
		///	根据对象的主键获取唯一对象
		/// 注意，这个方法必须与只能通过主键来获取，因为只有主键在逻辑能确定唯一
		/// 支持双主键，如果是双主键，则两个主键属性必须都要赋上
		/// 如果不能通过主键唯一确定的，则可以通过RetrieveCriteria对象来获取。
		/// </summary>
		/// <returns></returns>
		public bool Retrieve()
		{
            PersistenceBroker broker = PersistenceBroker.Instance();
            this._IsSaveInMemory = broker.GetClassMap(thisType.Name).IsSaveToMemory;
            return broker.RetrieveObject(this, true, false);
		}

        /// <summary>
        ///	根据对象的主键获取唯一对象
        /// 注意，这个方法必须与只能通过主键来获取，因为只有主键在逻辑能确定唯一
        /// 支持双主键，如果是双主键，则两个主键属性必须都要赋上
        /// 如果不能通过主键唯一确定的，则可以通过RetrieveCriteria对象来获取。
        /// 这个方法采用With(nolock)的方式，此方法只针对SQL Server与Access有效
        /// 此方法暂时已经注销。全部的所有的查询都加上了With(nolock)了。
        /// <param name="WithNoLock">采用 with(nolock)方式</param>
        /// <returns></returns>
        //public bool Retrieve(bool WithNoLock)
        //{
        //    PersistenceBroker broker = PersistenceBroker.Instance();
        //    this._IsSaveInMemory = broker.GetClassMap(thisType.Name).IsSaveToMemory;
        //    return broker.RetrieveObject(this, true,WithNoLock);
        //}

		/// <summary>
		///		获取对象 可选择是否连同于其关联的对象一起获取
		/// </summary>
		/// <returns></returns>
//		public bool Retrieve(bool isRetrieveAssociation)	
//		{
//			PersistenceBroker broker=PersistenceBroker.Instance();
//			this._IsSaveInMemory =broker.GetClassMap(thisType.Name).IsSaveToMemory;
//			return broker.RetrieveObject(this,isRetrieveAssociation);
//		}
		/// <summary>
		///		删除 
		/// </summary>
		public int Delete()	
		{
			PersistenceBroker broker=PersistenceBroker.Instance();
			this._IsSaveInMemory =broker.GetClassMap(thisType.Name).IsSaveToMemory;
			return broker.DeleteObject(this);
		}

		//根据属性名动态得到属性值		
		internal object GetAttributeValue(string name)
		{
			object rObject=null;
			try
			{
				rObject = this.thisType.InvokeMember(name,BindingFlags.GetProperty,null,this,null);
			}
			catch
			{
				throw new PlException("不能找到"+GetClassName()+"对象的["+name+"]属性!",ErrorTypes.NotFound);
			}
			return rObject;
		}

	
		/// <summary>
		/// 根据属性名动态设置属性值
		/// </summary>
		/// <param name="name"></param>
		/// <param name="objValue"></param>
		public void SetAttributeValue(string name,object objValue)
		{
			object[] objectsValue=new object[1];
			System.Type tp=this.thisType.GetProperty(name).PropertyType;
			objectsValue[0] =Convert.ChangeType(objValue, tp);
			try
			{
				this.thisType.InvokeMember(name,BindingFlags.SetProperty,null,this,objectsValue);
			}
			catch(Exception e)
			{
				throw e;
			}

		}
		
		/*属性*/
		/// <summary>
		///		指示该实体对象是否已经实体化。
		///		true则表示在Save时执行Update操作，false则表示在Save时执行Insert操作。
		/// </summary>
		public bool IsPersistent
		{
			get{return this.persistent;}
			set{this.persistent=value;}
		}      
	

		/// <summary>
		/// 数据源名称
		/// </summary>
		public string DatabaseName
		{
			get
			{
				if(this._databaseName==null)
					this._databaseName=PersistenceBroker.Instance().GetClassMap(GetClassName()).Database.Name;
				return this._databaseName;
			}
			set
			{

				this._databaseName=value;
			}
		}

		internal bool IsSaveToMemory
		{
			set
			{
				this._IsSaveInMemory=value;
			}
			get
			{
				return this._IsSaveInMemory;
			}
		}

		internal string MemoryKey
		{
			set
			{this._memoryKey=value;}
			get
			{
				this._memoryKey=this.DatabaseName +"_"+this.thisType.Name;
				return this._memoryKey;
			}
		}

		/// <summary>
		/// 引方法用于实体的深度Clone，返回对象后进行强行转化一下就可以使用
		/// 注意：实体对象必须是“可序列化的”,即实体类要标明[Serializable]属性方可使用。
		/// </summary>
		/// <returns></returns>
		public EntityObject DeepClone()
		{
		
			using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
			{
				BinaryFormatter formatter=new BinaryFormatter();
				formatter.Serialize(stream,this);
				stream.Position = 0;
				return (EntityObject)formatter.Deserialize(stream);
			}
		
		}

		

	}
}
