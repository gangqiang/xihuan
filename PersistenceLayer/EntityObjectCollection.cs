using System;
using System.Collections;

namespace PersistenceLayer
{
	/// <summary>
	///		EntityObject对象集合。用于实现实体类之间的1-N的关联。
	///		该类是个抽象类不能直接实例化。你需要从该类派生，得到一个强类型的对象集合。
	/// </summary>
	internal abstract class EntityObjectCollection
	{
		private ArrayList m_Collection ;
		private Type m_EntityObjectType;
		private string m_FullName;

		/// <summary>
		///		生成一个EntityObjectCollection实例
		/// </summary>
		/// <param name="entityObjectType"></param>
		public EntityObjectCollection (Type entityObjectType)
		{
			m_Collection = new ArrayList();
			this.m_EntityObjectType = entityObjectType;
			this.m_FullName = this.m_EntityObjectType.ToString();
			//this.m_NameSpace = this.m_NameSpace.Substring(0,this.m_NameSpace.LastIndexOf('.'));
		}
		
		/// <summary>
		///		加入一个实体对象 
		/// </summary>
		/// <param name="aEntityObject">加入集合的实体对象</param>
		public void Add ( EntityObject aEntityObject)
		{
			this.m_Collection.Add (aEntityObject);
		}

		/// <summary>
		///		移除一个实体对象
		/// </summary>
		public void Remove (EntityObject aEntityObject)
		{
			this.m_Collection.Remove (aEntityObject);
		}
		
		/// <summary>
		///		要移除的实体对象从零开始的索引
		/// </summary>
		/// <param name="index">索引</param>
		public void RemoveAt (int index)
		{
			this.m_Collection.RemoveAt (index);
		}

		/// <summary>
		///		移出集合中的所有对象
		/// </summary>
		public void Clear()
		{
			this.m_Collection.Clear ();
		}
		/// <summary>
		///		索引器
		/// </summary>
		/// <param name="index">索引号</param>
		/// <returns>返回实体对象</returns>
		public EntityObject Items (int index)
		{
			return ((EntityObject) this.m_Collection [index]);
		}

		/// <summary>
		///		实体对象总数
		/// </summary>
		public int Count 
		{
			get
			{
				return this.m_Collection.Count;
			}
		}

		/// <summary>
		///		该集合所容纳实体对象的Type实例。
		/// </summary>
		public Type EntityObjectType
		{
			get
			{
				return this.m_EntityObjectType;
			}
		}

		/// <summary>
		///		该集合所容纳实体对象的类名。
		/// </summary>
		public string FullName
		{
			get {return this.m_FullName;}
		}

		internal ArrayList Objects
		{
			get {return this.m_Collection;}
		}

	}
}
