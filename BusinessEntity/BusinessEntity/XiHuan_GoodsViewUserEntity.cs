﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行库版本:2.0.50727.1433
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// -------------------------------------------------------------
// 
//             Powered By： SR3.1(SmartRobot For SmartPersistenceLayer 3.1) 听棠
//             Created By： tc01029
//             Created Time： 2009-4-9 13:02:28
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
    public class XiHuan_GoodsViewUserEntity : EntityObject
    {
        
        /// <summary>Id</summary>
        public const string @__ID = "Id";
        
        /// <summary>GoodsId</summary>
        public const string @__GOODSID = "GoodsId";
        
        /// <summary>VisitorId</summary>
        public const string @__VISITORID = "VisitorId";
        
        /// <summary>VisitorName</summary>
        public const string @__VISITORNAME = "VisitorName";
        
        /// <summary>VisitorHeadImage</summary>
        public const string @__VISITORHEADIMAGE = "VisitorHeadImage";

        public const string @_TYPE = "Type";
        
        /// <summary>VisitDate</summary>
        public const string @__VISITDATE = "VisitDate";
        
        private int m_Id;
        
        private int m_GoodsId;
        
        private int m_VisitorId;
        
        private string m_VisitorName;
        
        private string m_VisitorHeadImage;

        private byte m_Type;
        
        private System.DateTime m_VisitDate = DateTime.MinValue;
        
        /// <summary>构造函数</summary>
        public XiHuan_GoodsViewUserEntity()
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
        
        /// <summary>属性GoodsId </summary>
        public int GoodsId
        {
            get
            {
                return this.m_GoodsId;
            }
            set
            {
                this.m_GoodsId = value;
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
        
        /// <summary>属性VisitorHeadImage </summary>
        public string VisitorHeadImage
        {
            get
            {
                return this.m_VisitorHeadImage;
            }
            set
            {
                this.m_VisitorHeadImage = value;
            }
        }

        public byte Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
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
    
    /// XiHuan_GoodsViewUserEntity执行类
    public abstract class XiHuan_GoodsViewUserEntityAction
    {
        
        private XiHuan_GoodsViewUserEntityAction()
        {
        }
        
        public static void Save(XiHuan_GoodsViewUserEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static XiHuan_GoodsViewUserEntity RetrieveAXiHuan_GoodsViewUserEntity(int Id)
        {
            XiHuan_GoodsViewUserEntity obj=new XiHuan_GoodsViewUserEntity();
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
        public static EntityContainer RetrieveXiHuan_GoodsViewUserEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_GoodsViewUserEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable GetXiHuan_GoodsViewUserEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_GoodsViewUserEntity));
            return rc.AsDataTable();
        }
    }
}
