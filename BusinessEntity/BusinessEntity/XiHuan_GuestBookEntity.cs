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
//             Created Time： 2009-4-9 16:45:30
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
    public class XiHuan_GuestBookEntity : EntityObject
    {

        /// <summary>Id</summary>
        public const string @__ID = "Id";

        /// <summary>GoodsId</summary>
        public const string @__GOODSID = "GoodsId";

        /// <summary>GoodsName</summary>
        public const string @__GOODSNAME = "GoodsName";

        /// <summary>FromId</summary>
        public const string @__FROMID = "FromId";

        /// <summary>FromName</summary>
        public const string @__FROMNAME = "FromName";

        /// <summary>ToId</summary>
        public const string @__TOID = "ToId";

        /// <summary>ToName</summary>
        public const string @__TONAME = "ToName";

        /// <summary>Flag</summary>
        public const string @__FLAG = "Flag";

        /// <summary>Content</summary>
        public const string @__CONTENT = "Content";

        /// <summary>ReplyContent</summary>
        public const string @__REPLYCONTENT = "ReplyContent";

        /// <summary>IsScerect</summary>
        public const string @__ISSCERECT = "IsScerect";

        /// <summary>CreateDate</summary>
        public const string @__CREATEDATE = "CreateDate";

        /// <summary>IsChecked</summary>
        public const string @__ISCHECKED = "IsChecked";

        private int m_Id;

        private int m_GoodsId;

        private string m_GoodsName;

        private int m_FromId;

        private string m_FromName;

        private int m_ToId;

        private string m_ToName;

        private byte m_Flag;

        private string m_Content;

        private string m_ReplyContent;

        private byte m_IsScerect;

        private byte m_IsChecked;

        private System.DateTime m_CreateDate = DateTime.MinValue;

        /// <summary>构造函数</summary>
        public XiHuan_GuestBookEntity()
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

        /// <summary>属性GoodsName </summary>
        public string GoodsName
        {
            get
            {
                return this.m_GoodsName;
            }
            set
            {
                this.m_GoodsName = value;
            }
        }

        /// <summary>属性FromId </summary>
        public int FromId
        {
            get
            {
                return this.m_FromId;
            }
            set
            {
                this.m_FromId = value;
            }
        }

        /// <summary>属性FromName </summary>
        public string FromName
        {
            get
            {
                return this.m_FromName;
            }
            set
            {
                this.m_FromName = value;
            }
        }

        /// <summary>属性ToId </summary>
        public int ToId
        {
            get
            {
                return this.m_ToId;
            }
            set
            {
                this.m_ToId = value;
            }
        }

        /// <summary>属性ToName </summary>
        public string ToName
        {
            get
            {
                return this.m_ToName;
            }
            set
            {
                this.m_ToName = value;
            }
        }

        /// <summary>属性Flag </summary>
        public byte Flag
        {
            get
            {
                return this.m_Flag;
            }
            set
            {
                this.m_Flag = value;
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

        /// <summary>属性ReplyContent </summary>
        public string ReplyContent
        {
            get
            {
                return this.m_ReplyContent;
            }
            set
            {
                this.m_ReplyContent = value;
            }
        }

        /// <summary>属性IsScerect </summary>
        public byte IsScerect
        {
            get
            {
                return this.m_IsScerect;
            }
            set
            {
                this.m_IsScerect = value;
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

        /// <summary>属性IsChecked </summary>
        public byte IsChecked
        {
            get
            {
                return this.m_IsChecked;
            }
            set
            {
                this.m_IsChecked = value;
            }
        }
    }

    /// XiHuan_GuestBookEntity执行类
    public abstract class XiHuan_GuestBookEntityAction
    {

        private XiHuan_GuestBookEntityAction()
        {
        }

        public static void Save(XiHuan_GuestBookEntity obj)
        {
            if (obj != null)
            {
                obj.Save();
            }
        }

        /// <summary>根据主键获取一个实体</summary>
        public static XiHuan_GuestBookEntity RetrieveAXiHuan_GuestBookEntity(int Id)
        {
            XiHuan_GuestBookEntity obj = new XiHuan_GuestBookEntity();
            obj.Id = Id;
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
        public static EntityContainer RetrieveXiHuan_GuestBookEntity()
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_GuestBookEntity));
            return rc.AsEntityContainer();
        }

        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable GetXiHuan_GuestBookEntity()
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_GuestBookEntity));
            return rc.AsDataTable();
        }
    }
}