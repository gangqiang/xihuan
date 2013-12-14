using System;
using System.Data;

namespace PersistenceLayer
{
	/// <summary>
	/// 对象集合游标类，一般在RetrieveCriteria中创建：
	///  ObjectCursor oc=rc.AsObjectCursor();此游标指向获取到的对象集
	///  for(oc.HasObject()) //循环判断，这跟使用EntityContainer很类似
	/// {
	///	 entity e=(entity)oc.NextObject();
	/// }
	/// </summary>
	public class ObjectCursor
	{
		private int size;
		private int pointer;
		private bool m_IsIncludeAssociation = false;
		private DataSet results;
		private Type typeOfClass;
		private string className;
		private PersistenceBroker broker = PersistenceBroker.Instance();
		
		

		internal ObjectCursor(Type classType,DataSet ds,bool isIncludeAssociation)
		{
			this.results=ds;
			this.size=ds.Tables[0].Rows.Count;
			this.pointer=0;
			this.typeOfClass=classType;
			string name=classType.ToString();
			this.className=name.Substring(name.LastIndexOf('.')+1);
			this.m_IsIncludeAssociation = isIncludeAssociation;
			

		}

		internal ObjectCursor(Type classType,DataTable dt,bool isIncludeAssociation)
		{
			this.results = new DataSet ();
			this.results.Tables.Add (dt);
			this.size=dt.Rows.Count;
			this.pointer=0;
			this.typeOfClass=classType;
			string name=classType.ToString();
			this.className=name.Substring(name.LastIndexOf('.')+1);
			this.m_IsIncludeAssociation = isIncludeAssociation;
			

		}

		internal ObjectCursor (string name,DataTable dt,bool isIncludeAssociation)
		{
			this.results = new DataSet ();
			this.results.Tables.Add (dt);
			this.m_IsIncludeAssociation = isIncludeAssociation;
		}

		/// <summary>
		///		返回下一个对象对象
		/// </summary>
		/// <returns>EntityObject</returns>
		public EntityObject NextObject()
		{	
			EntityObject aObject=null;
			try
			{
				aObject=broker.GetEntityObject(typeOfClass,className,results.Tables[0].Rows[pointer]);
				//broker.RetrieveObject(aObject,this.m_IsIncludeAssociation);//以后再做修改
            }
			catch (IndexOutOfRangeException)
			{
				throw new PlException("已经没有实体对象可以枚举了！");
			}
			
			this.pointer++;
			return aObject;
		}


		/// <summary>
		///		是否还有对象	 
		/// </summary>
		/// <returns>true：仍有对象，false：无对象</returns>
		public bool HasObject()
		{
			bool r=true;
			if(this.pointer>=this.size) r=false;
			return r;
		}

		/// <summary>
		///		将指针移动到第一个实体对象处 
		/// </summary>
		public void MoveFirst()
		{
			this.pointer = 0;
		}

		//索引器
//		public EntityObject this [int index]
//		{
//			get
//			{
//				return ((EntityObject) this.m_Collection [index]);
//			}
//		}

		
		/*属性*/
		/// <summary>
		///		所包含实体对象的总数
		/// </summary>
		public int Count
		{
			get {return this.size;}
		}

	}
}
