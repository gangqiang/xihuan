//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2407
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// -------------------------------------------------------------
// 
//             Powered By�?SR3.1(SmartRobot For SmartPersistenceLayer 3.1) 听棠
//             Created By�?Administrator
//             Created Time�?2009-3-28 17:03:34
// 
// -------------------------------------------------------------
namespace BusinessEntity
{
    using System;
    using System.Collections;
    using System.Data;
    using PersistenceLayer;
    
    
    /// <summary>该类的摘要说�?/summary>
    [Serializable()]
    public class XiHuan_GoodsTypeEntity : EntityObject
    {
        
        /// <summary>Id</summary>
        public const string __ID = "Id";
         
        /// <summary>FixId</summary>
        
        public const string _FIXID = "FixId";
        
        /// <summary>TypeName</summary>
        public const string __TYPENAME = "TypeName";
        
        /// <summary>CreateDate</summary>
        public const string __CREATEDATE = "CreateDate";
        
        private int m_Id;
        private int m_FixId;
        
        private string m_TypeName;
        
        private System.DateTime m_CreateDate = DateTime.MinValue;
        
        /// <summary>构造函�?/summary>
        public XiHuan_GoodsTypeEntity()
        {
        }
        
        /// <summary>属性Id </summary>
        public int Id
        {
            get
            {
                return this.m_Id;
            }
            set
            {
                this.m_Id = value;
            }
        }
        /// <summary>属性Id </summary>
        public int FixId
        {
            get
            {
                return this.m_FixId;
            }
            set
            {
                this.m_FixId = value;
            }
        }

        /// <summary>属性TypeName </summary>
        public string TypeName
        {
            get
            {
                return this.m_TypeName;
            }
            set
            {
                this.m_TypeName = value;
            }
        }
        
        /// <summary>属性CreateDate </summary>
        public System.DateTime CreateDate
        {
            get
            {
                return this.m_CreateDate;
            }
            set
            {
                this.m_CreateDate = value;
            }
        }
    }
    
    /// XiHuan_GoodsTypeEntity执行�?
    public abstract class XiHuan_GoodsTypeEntityAction
    {
        
        private XiHuan_GoodsTypeEntityAction()
        {
        }
        
        public static void Save(XiHuan_GoodsTypeEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实�?/summary>
        public static XiHuan_GoodsTypeEntity RetrieveAXiHuan_GoodsTypeEntity(int Id)
        {
            XiHuan_GoodsTypeEntity obj=new XiHuan_GoodsTypeEntity();
            obj.Id=Id;
            obj.Retrieve();
            if (obj.IsPersistent)
            {
                return obj;
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>获取所有实�?EntityContainer)</summary>
        public static EntityContainer RetrieveXiHuan_GoodsTypeEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_GoodsTypeEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实�?EntityContainer)</summary>
        public static DataTable GetXiHuan_GoodsTypeEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_GoodsTypeEntity));
            return rc.AsDataTable();
        }
    }
}
