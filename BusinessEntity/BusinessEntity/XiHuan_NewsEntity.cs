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
//             Created Time： 2009-5-19 15:26:01
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
    public class XiHuan_NewsEntity : EntityObject
    {
        
        /// <summary>Id</summary>
        public const string @__ID = "Id";
        
        /// <summary>Title</summary>
        public const string @__TITLE = "Title";
        
        /// <summary>Content</summary>
        public const string @__CONTENT = "Content";
        
        /// <summary>ViewCount</summary>
        public const string @__VIEWCOUNT = "ViewCount";
        
        /// <summary>Type</summary>
        public const string @__TYPE = "Type";
        
        /// <summary>NewsUrl</summary>
        public const string @__NEWSURL = "NewsUrl";
        
        /// <summary>SortNumber</summary>
        public const string @__SORTNUMBER = "SortNumber";
        
        /// <summary>CreateDate</summary>
        public const string @__CREATEDATE = "CreateDate";
        
        private int m_Id;
        
        private string m_Title;
        
        private string m_Content;
        
        private int m_ViewCount;
        
        private byte m_Type;
        
        private string m_NewsUrl;
        
        private int m_SortNumber;
        
        private System.DateTime m_CreateDate = DateTime.MinValue;
        
        /// <summary>构造函数</summary>
        public XiHuan_NewsEntity()
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
        
        /// <summary>属性Title </summary>
        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
        
        /// <summary>属性Content </summary>
        public string Content
        {
            get
            {
                return this.m_Content;
            }
            set
            {
                this.m_Content = value;
            }
        }
        
        /// <summary>属性ViewCount </summary>
        public int ViewCount
        {
            get
            {
                return this.m_ViewCount;
            }
            set
            {
                this.m_ViewCount = value;
            }
        }
        
        /// <summary>属性Type </summary>
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
        
        /// <summary>属性NewsUrl </summary>
        public string NewsUrl
        {
            get
            {
                return this.m_NewsUrl;
            }
            set
            {
                this.m_NewsUrl = value;
            }
        }
        
        /// <summary>属性SortNumber </summary>
        public int SortNumber
        {
            get
            {
                return this.m_SortNumber;
            }
            set
            {
                this.m_SortNumber = value;
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
    
    /// XiHuan_NewsEntity执行类
    public abstract class XiHuan_NewsEntityAction
    {
        
        private XiHuan_NewsEntityAction()
        {
        }
        
        public static void Save(XiHuan_NewsEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static XiHuan_NewsEntity RetrieveAXiHuan_NewsEntity(int Id)
        {
            XiHuan_NewsEntity obj=new XiHuan_NewsEntity();
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
        public static EntityContainer RetrieveXiHuan_NewsEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_NewsEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable GetXiHuan_NewsEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_NewsEntity));
            return rc.AsDataTable();
        }
    }
}
