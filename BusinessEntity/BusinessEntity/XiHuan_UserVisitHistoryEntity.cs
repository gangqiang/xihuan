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
//             Powered By： SR3.1(SmartRobot For SmartPersistenceLayer 3.1) 听棠
//             Created By： Administrator
//             Created Time： 2009-3-28 17:03:34
// 
// -------------------------------------------------------------
namespace BusinessEntity
{
    using System;
    using System.Collections;
    using System.Data;
    using PersistenceLayer;
    
    
    /// <summary>该类的摘要说明</summary>
    [Serializable()]
    public class XiHuan_UserVisitHistoryEntity : EntityObject
    {
        
        /// <summary>Id</summary>
        public const string __ID = "Id";
        
        /// <summary>OwnerId</summary>
        public const string __OWNERID = "OwnerId";
        
        /// <summary>VisitorId</summary>
        public const string __VISITORID = "VisitorId";
        
        /// <summary>OwnerName</summary>
        public const string __OWNERNAME = "OwnerName";
        
        /// <summary>VisitorName</summary>
        public const string __VISITORNAME = "VisitorName";
        
        /// <summary>VisitDate</summary>
        public const string __VISITDATE = "VisitDate";
        
        private int m_Id;
        
        private int m_OwnerId;
        
        private int m_VisitorId;
        
        private string m_OwnerName;
        
        private string m_VisitorName;
        
        private System.DateTime m_VisitDate = DateTime.MinValue;
        
        /// <summary>构造函数</summary>
        public XiHuan_UserVisitHistoryEntity()
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
        
        /// <summary>属性OwnerId </summary>
        public int OwnerId
        {
            get
            {
                return this.m_OwnerId;
            }
            set
            {
                this.m_OwnerId = value;
            }
        }
        
        /// <summary>属性VisitorId </summary>
        public int VisitorId
        {
            get
            {
                return this.m_VisitorId;
            }
            set
            {
                this.m_VisitorId = value;
            }
        }
        
        /// <summary>属性OwnerName </summary>
        public string OwnerName
        {
            get
            {
                return this.m_OwnerName;
            }
            set
            {
                this.m_OwnerName = value;
            }
        }
        
        /// <summary>属性VisitorName </summary>
        public string VisitorName
        {
            get
            {
                return this.m_VisitorName;
            }
            set
            {
                this.m_VisitorName = value;
            }
        }
        
        /// <summary>属性VisitDate </summary>
        public System.DateTime VisitDate
        {
            get
            {
                return this.m_VisitDate;
            }
            set
            {
                this.m_VisitDate = value;
            }
        }
    }
    
    /// XiHuan_UserVisitHistoryEntity执行类
    public abstract class XiHuan_UserVisitHistoryEntityAction
    {
        
        private XiHuan_UserVisitHistoryEntityAction()
        {
        }
        
        public static void Save(XiHuan_UserVisitHistoryEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static XiHuan_UserVisitHistoryEntity RetrieveAXiHuan_UserVisitHistoryEntity(int Id)
        {
            XiHuan_UserVisitHistoryEntity obj=new XiHuan_UserVisitHistoryEntity();
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
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static EntityContainer RetrieveXiHuan_UserVisitHistoryEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_UserVisitHistoryEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable GetXiHuan_UserVisitHistoryEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_UserVisitHistoryEntity));
            return rc.AsDataTable();
        }
    }
}