using System;
using System.Collections;
using System.Data;

namespace PersistenceLayer
{
	/// <summary>
	/// EntityContainer实体对类容器类
	/// 由RetrieveCriteria生成：假设rc为一个RetrieveCriteria实例对象
	/// EntityContainer ec=rc.AsEntityContainer();
	/// for(int i=0;i<ec.Count;i++)
	/// {
	///		aEntity a=(aEntity)ec[i];
	///		.....
	/// }
	/// </summary>
	[Serializable]
	public class EntityContainer : ArrayList
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public EntityContainer()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 添加对象
		/// </summary>
		/// <param name="entity"></param>
		public void Add(EntityObject entity)
		{
			base.Add(entity);
		}

	
		/// <summary>
		/// 第N个对象
		/// </summary>
		public new EntityObject this[int index]
		{
			get
			{
				return (EntityObject)base[index];
				
			}
						
		}

//		public DataTable AsDataTable()
//		{
//			DataTable dt=new DataTable();
//			DataRow dr;
//			AttributeMap am;
//			EntityObject entity;
//			ClassMap cm=PersistenceBroker.Instance().GetClassMap(entity.GetClassName());
//			int count = cm.SelfClassAttributes.Count;
//			for(int i=0;i<count;i++)
//			{
//				am = (AttributeMap) cm.SelfClassAttributes[i];
//				dt.Columns.Add(am.Name,typeof(string));
//					
//			}
//
//			for(int i=0;i<this.Count;i++)
//			{
//				dr=dt.NewRow();
//				
//				for(int ii=0;ii<count;ii++)
//				{
//					am = (AttributeMap) cm.SelfClassAttributes[i];
//					dr[am.Name]=entity.GetAttributeValue(am.Name);
//				}
//			}
//			return dt;
//		}
	
			
	}
}
