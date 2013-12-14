using System;
using System.Collections;
using System.Data;

namespace PersistenceLayer
{
	/// <summary>
	/// 标准的一个基类，UpdateCriteria与DeleteCriteria都继承自此类
	/// </summary>
	public abstract class PersistentCriteria
	{
		private CriteriaTypes m_CriteriaType;
		private Type classType=null;
		/// <summary>
		/// 指定数据源名
		/// </summary>
		protected string _databaseName=null;
		/// <summary>
		/// 内存镜像时Key值
		/// </summary>
		protected string _memoryKey="";
		/// <summary>
		/// 是否要内存镜像
		/// </summary>
		protected bool _IsSaveInMemory=false;
		

		/// <summary>
		/// 构造函数
		/// </summary>
		public PersistentCriteria()
		{
		}
		

		/// <summary>
		/// 作用的类型
		/// </summary>
		public Type ForClass
		{
			get{return this.classType;}
			set{this.classType=value;}
		}
		
		/// <summary>
		/// 标准的类型
		/// </summary>
		internal CriteriaTypes CriteriaType
		{
			get
			{
				return this.m_CriteriaType;
			}
			set
			{
				this.m_CriteriaType = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string DatabaseName
		{
			set
			{
				this._databaseName=value;
			}
			get
			{
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

	
	}

		
}