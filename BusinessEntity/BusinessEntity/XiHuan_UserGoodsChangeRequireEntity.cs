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
//             Created Time： 2009-4-17 14:43:28
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
    public class XiHuan_UserGoodsChangeRequireEntity : EntityObject
    {
        
        /// <summary>Id</summary>
        public const string @__ID = "Id";
        
        /// <summary>OwnerId</summary>
        public const string @__OWNERID = "OwnerId";
        
        /// <summary>OwnerName</summary>
        public const string @__OWNERNAME = "OwnerName";
        
        /// <summary>SenderId</summary>
        public const string @__SENDERID = "SenderId";
        
        /// <summary>SenderName</summary>
        public const string @__SENDERNAME = "SenderName";
        
        /// <summary>GoodsId</summary>
        public const string @__GOODSID = "GoodsId";
        
        /// <summary>GoodsName</summary>
        public const string @__GOODSNAME = "GoodsName";

        /// <summary>SelectToChangeGoodsId</summary>
        
        public const string @_SelectToChangeGoodsId = "SelectToChangeGoodsId";

        /// <summary>SelectToChangeGoodsName</summary>
       
        public const string @_SelectToChangeGoodsName = "SelectToChangeGoodsName";


        /// <summary>RequireType</summary>
        public const string @__REQUIRETYPE = "RequireType";

        public const string @_IsSecret = "IsSecret";
        
        /// <summary>RequireDescribe</summary>
        public const string @__REQUIREDESCRIBE = "RequireDescribe";
        
        /// <summary>Flag</summary>
        public const string @__FLAG = "Flag";
        
        /// <summary>RequireDate</summary>
        public const string @__REQUIREDATE = "RequireDate";
        
        private int m_Id;
        
        private int m_OwnerId;
        
        private string m_OwnerName;
        
        private int m_SenderId;
        
        private string m_SenderName;
        
        private int m_GoodsId;
        
        private string m_GoodsName;

        private string m_SelectToChangeGoodsId;

        private string m_SelectToChangeGoodsName;
        
        private byte m_RequireType;

        private byte m_IsSecret;
        
        private string m_RequireDescribe;
        
        private byte m_Flag;
        
        private System.DateTime m_RequireDate = DateTime.MinValue;
        
        /// <summary>构造函数</summary>
        public XiHuan_UserGoodsChangeRequireEntity()
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

        /// <summary>属性SelectToChangeGoodsId </summary>
        public string SelectToChangeGoodsId
        {
            get
            {
                return this.m_SelectToChangeGoodsId;
            }
            set
            {
                this.m_SelectToChangeGoodsId = value;
            }
        }

        /// <summary>属性OwnerName </summary>
        public string SelectToChangeGoodsName
        {
            get
            {
                return this.m_SelectToChangeGoodsName;
            }
            set
            {
                this.m_SelectToChangeGoodsName = value;
            }
        }
        
        /// <summary>属性SenderId </summary>
        public int SenderId
        {
            get
            {
                return this.m_SenderId;
            }
            set
            {
                this.m_SenderId = value;
            }
        }
        
        /// <summary>属性SenderName </summary>
        public string SenderName
        {
            get
            {
                return this.m_SenderName;
            }
            set
            {
                this.m_SenderName = value;
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
        
        /// <summary>属性RequireType </summary>
        public byte RequireType
        {
            get
            {
                return this.m_RequireType;
            }
            set
            {
                this.m_RequireType = value;
            }
        }

        /// <summary>属性IsSecret </summary>
        public byte IsSecret
        {
            get
            {
                return this.m_IsSecret;
            }
            set
            {
                this.m_IsSecret = value;
            }
        }
        
        /// <summary>属性RequireDescribe </summary>
        public string RequireDescribe
        {
            get
            {
                return this.m_RequireDescribe;
            }
            set
            {
                this.m_RequireDescribe = value;
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
        
        /// <summary>属性RequireDate </summary>
        public System.DateTime RequireDate
        {
            get
            {
                return this.m_RequireDate;
            }
            set
            {
                this.m_RequireDate = value;
            }
        }
    }
    
    /// XiHuan_UserGoodsChangeRequireEntity执行类
    public abstract class XiHuan_UserGoodsChangeRequireEntityAction
    {
        
        private XiHuan_UserGoodsChangeRequireEntityAction()
        {
        }
        
        public static void Save(XiHuan_UserGoodsChangeRequireEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static XiHuan_UserGoodsChangeRequireEntity RetrieveAXiHuan_UserGoodsChangeRequireEntity(int Id)
        {
            XiHuan_UserGoodsChangeRequireEntity obj=new XiHuan_UserGoodsChangeRequireEntity();
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
        public static EntityContainer RetrieveXiHuan_UserGoodsChangeRequireEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_UserGoodsChangeRequireEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable GetXiHuan_UserGoodsChangeRequireEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(XiHuan_UserGoodsChangeRequireEntity));
            return rc.AsDataTable();
        }
    }
}