﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.3615
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// -------------------------------------------------------------
//             Created By： tc01029
//             Created Time： 2010-12-7 13:33:30
// -------------------------------------------------------------
namespace BusinessEntity
{
    using System;
    using System.Collections;
    using System.Data;
    using PersistenceLayer;
    
    
    /// <summary>
    /// 实体XiHuan_ActivityInfoEntity，对应表XiHuan_ActivityInfo
    /// </summary>
    [Serializable()]
    public sealed class XiHuan_ActivityInfoEntity : EntityObject
    {
        
        /// <summary>
        /// 对应表字段Id
        /// </summary>
        private int m_Id = 0;
        
        /// <summary>
        /// 对应表字段ActivityName
        /// </summary>
        private string m_ActivityName = string.Empty;
        
        /// <summary>
        /// 对应表字段CreateUserName
        /// </summary>
        private string m_CreateUserName = string.Empty;
        
        /// <summary>
        /// 对应表字段ActivityTime
        /// </summary>
        private string m_ActivityTime = string.Empty;
        
        /// <summary>
        /// 对应表字段ProvinceId
        /// </summary>
        private int m_ProvinceId = 0;
        
        /// <summary>
        /// 对应表字段CityId
        /// </summary>
        private int m_CityId = 0;
        
        /// <summary>
        /// 对应表字段AreaId
        /// </summary>
        private int m_AreaId = 0;
        
        /// <summary>
        /// 对应表字段SchoolId
        /// </summary>
        private int m_SchoolId = 0;
        
        /// <summary>
        /// 对应表字段ActivityAddress
        /// </summary>
        private string m_ActivityAddress = string.Empty;
        
        /// <summary>
        /// 对应表字段AllowPeoPle
        /// </summary>
        private int m_AllowPeoPle = 0;
        
        /// <summary>
        /// 对应表字段ActityDesc
        /// </summary>
        private string m_ActityDesc = string.Empty;
        
        /// <summary>
        /// 对应表字段CreateDate
        /// </summary>
        private System.DateTime m_CreateDate = DateTime.MinValue;
        
        /// <summary>
        /// 对应表字段ViewCount
        /// </summary>
        private int m_ViewCount = 0;
        
        /// <summary>
        /// 对应表字段PeopleCount
        /// </summary>
        private int m_PeopleCount = 0;
        
        /// <summary>
        /// 对应表字段IsChecked
        /// </summary>
        private byte m_IsChecked = 0;
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public XiHuan_ActivityInfoEntity()
        {
        }
        
        /// <summary>
        /// 属性Id，对应数据库字段Id
        /// </summary>
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
        
        /// <summary>
        /// 属性ActivityName，对应数据库字段ActivityName
        /// </summary>
        public string ActivityName
        {
            get
            {
                return this.m_ActivityName;
            }
            set
            {
                this.m_ActivityName = value;
            }
        }
        
        /// <summary>
        /// 属性CreateUserName，对应数据库字段CreateUserName
        /// </summary>
        public string CreateUserName
        {
            get
            {
                return this.m_CreateUserName;
            }
            set
            {
                this.m_CreateUserName = value;
            }
        }
        
        /// <summary>
        /// 属性ActivityTime，对应数据库字段ActivityTime
        /// </summary>
        public string ActivityTime
        {
            get
            {
                return this.m_ActivityTime;
            }
            set
            {
                this.m_ActivityTime = value;
            }
        }
        
        /// <summary>
        /// 属性ProvinceId，对应数据库字段ProvinceId
        /// </summary>
        public int ProvinceId
        {
            get
            {
                return this.m_ProvinceId;
            }
            set
            {
                this.m_ProvinceId = value;
            }
        }
        
        /// <summary>
        /// 属性CityId，对应数据库字段CityId
        /// </summary>
        public int CityId
        {
            get
            {
                return this.m_CityId;
            }
            set
            {
                this.m_CityId = value;
            }
        }
        
        /// <summary>
        /// 属性AreaId，对应数据库字段AreaId
        /// </summary>
        public int AreaId
        {
            get
            {
                return this.m_AreaId;
            }
            set
            {
                this.m_AreaId = value;
            }
        }
        
        /// <summary>
        /// 属性SchoolId，对应数据库字段SchoolId
        /// </summary>
        public int SchoolId
        {
            get
            {
                return this.m_SchoolId;
            }
            set
            {
                this.m_SchoolId = value;
            }
        }
        
        /// <summary>
        /// 属性ActivityAddress，对应数据库字段ActivityAddress
        /// </summary>
        public string ActivityAddress
        {
            get
            {
                return this.m_ActivityAddress;
            }
            set
            {
                this.m_ActivityAddress = value;
            }
        }
        
        /// <summary>
        /// 属性AllowPeoPle，对应数据库字段AllowPeoPle
        /// </summary>
        public int AllowPeoPle
        {
            get
            {
                return this.m_AllowPeoPle;
            }
            set
            {
                this.m_AllowPeoPle = value;
            }
        }
        
        /// <summary>
        /// 属性ActityDesc，对应数据库字段ActityDesc
        /// </summary>
        public string ActityDesc
        {
            get
            {
                return this.m_ActityDesc;
            }
            set
            {
                this.m_ActityDesc = value;
            }
        }
        
        /// <summary>
        /// 属性CreateDate，对应数据库字段CreateDate
        /// </summary>
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
        
        /// <summary>
        /// 属性ViewCount，对应数据库字段ViewCount
        /// </summary>
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
        
        /// <summary>
        /// 属性PeopleCount，对应数据库字段PeopleCount
        /// </summary>
        public int PeopleCount
        {
            get
            {
                return this.m_PeopleCount;
            }
            set
            {
                this.m_PeopleCount = value;
            }
        }
        
        /// <summary>
        /// 属性IsChecked，对应数据库字段IsChecked
        /// </summary>
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
        
        /// <summary>
        /// 静态方法，根据主键来获取实体,如果没有获取到则返回null
        /// </summary>
        public static XiHuan_ActivityInfoEntity GetEntityByPrimaryKey(int Id)
        {
            XiHuan_ActivityInfoEntity obj = new XiHuan_ActivityInfoEntity();
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
        
        /// <summary>
        /// 重新OnEuqal方法，通过主键来判断
        /// </summary>
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is XiHuan_ActivityInfoEntity))
            {
                return false;
            }
            XiHuan_ActivityInfoEntity tmpObj = (XiHuan_ActivityInfoEntity)obj;
            if ((this.m_Id == tmpObj.m_Id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 重新GetHashCode方法，主键的Hash值累积
        /// </summary>
        public override int GetHashCode()
        {
             return this.m_Id.GetHashCode();
        }
        
        /// <summary>
        /// 表字段结构，封装了实体对应数据表的所有字段
        /// </summary>
        public struct Columns
        {
            
            /// <summary>
            /// 表字段Id
            /// </summary>
            public const string Id = "Id";
            
            /// <summary>
            /// 表字段ActivityName
            /// </summary>
            public const string ActivityName = "ActivityName";
            
            /// <summary>
            /// 表字段CreateUserName
            /// </summary>
            public const string CreateUserName = "CreateUserName";
            
            /// <summary>
            /// 表字段ActivityTime
            /// </summary>
            public const string ActivityTime = "ActivityTime";
            
            /// <summary>
            /// 表字段ProvinceId
            /// </summary>
            public const string ProvinceId = "ProvinceId";
            
            /// <summary>
            /// 表字段CityId
            /// </summary>
            public const string CityId = "CityId";
            
            /// <summary>
            /// 表字段AreaId
            /// </summary>
            public const string AreaId = "AreaId";
            
            /// <summary>
            /// 表字段SchoolId
            /// </summary>
            public const string SchoolId = "SchoolId";
            
            /// <summary>
            /// 表字段ActivityAddress
            /// </summary>
            public const string ActivityAddress = "ActivityAddress";
            
            /// <summary>
            /// 表字段AllowPeoPle
            /// </summary>
            public const string AllowPeoPle = "AllowPeoPle";
            
            /// <summary>
            /// 表字段ActityDesc
            /// </summary>
            public const string ActityDesc = "ActityDesc";
            
            /// <summary>
            /// 表字段CreateDate
            /// </summary>
            public const string CreateDate = "CreateDate";
            
            /// <summary>
            /// 表字段ViewCount
            /// </summary>
            public const string ViewCount = "ViewCount";
            
            /// <summary>
            /// 表字段PeopleCount
            /// </summary>
            public const string PeopleCount = "PeopleCount";
            
            /// <summary>
            /// 表字段IsChecked
            /// </summary>
            public const string IsChecked = "IsChecked";
        }
    }
}