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
    public class areaEntity : EntityObject
    {
        
        /// <summary>id</summary>
        public const string __ID = "id";
        
        /// <summary>areaID</summary>
        public const string __AREAID = "areaID";
        
        /// <summary>area</summary>
        public const string __AREA = "area";
        
        /// <summary>father</summary>
        public const string __FATHER = "father";
        
        private int m_id;
        
        private string m_areaID;
        
        private string m_area;
        
        private string m_father;
        
        /// <summary>构造函数</summary>
        public areaEntity()
        {
        }
        
        /// <summary>属性id </summary>
        public int id
        {
            get
            {
                return this.m_id;
            }
            set
            {
                this.m_id = value;
            }
        }
        
        /// <summary>属性areaID </summary>
        public string areaID
        {
            get
            {
                return this.m_areaID;
            }
            set
            {
                this.m_areaID = value;
            }
        }
        
        /// <summary>属性area </summary>
        public string area
        {
            get
            {
                return this.m_area;
            }
            set
            {
                this.m_area = value;
            }
        }
        
        /// <summary>属性father </summary>
        public string father
        {
            get
            {
                return this.m_father;
            }
            set
            {
                this.m_father = value;
            }
        }
    }
    
    /// areaEntity执行类
    public abstract class areaEntityAction
    {
        
        private areaEntityAction()
        {
        }
        
        public static void Save(areaEntity obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static areaEntity RetrieveAareaEntity(int id)
        {
            areaEntity obj=new areaEntity();
            obj.id=id;
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
        public static EntityContainer RetrieveareaEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(areaEntity));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable GetareaEntity()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(areaEntity));
            return rc.AsDataTable();
        }
    }
}