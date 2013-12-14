using System.Data;
using PersistenceLayer;
namespace BusinessFacade
{
    public class XiHuan_GoodsTypeFacade
    {
        private static XiHuan_GoodsTypeFacade instance = null;

        private static readonly object olock = new object();

        private DataTable dtParentTypeInfo = new DataTable();
        private DataTable dtChildTypeInfo = new DataTable();
        private DataTable dtinfo = new DataTable();

        /// <summary>
        /// 私有构造函数
        /// 之所有不是public类型的,因为是为了避免发生new Singleton()产生更多的实例
        /// </summary>
        private XiHuan_GoodsTypeFacade()
        {
            dtParentTypeInfo = Query.ProcessSql("select Id,TypeName,FixId from XiHuan_GoodsType with(nolock) ", GlobalVar.DataBase_Name);
            dtChildTypeInfo = Query.ProcessSql("select Id,Name,ParentId from XiHuan_GoodsSecondType with(nolock) ", GlobalVar.DataBase_Name);
        }

        public DataTable GetGoodsParentType()
        {
            return this.dtParentTypeInfo;
        }

        public DataTable GetGoodsChildType(string parentid)
        {
            this.dtinfo = this.dtChildTypeInfo.Clone();
            if (CommonMethodFacade.FinalString(parentid).Length > 0)
            {
                DataRow[] dr = this.dtChildTypeInfo.Select("ParentId=" + parentid);
                foreach (DataRow row in dr)
                {
                    DataRow newrow = dtinfo.NewRow();
                    newrow["Id"] = row["Id"];
                    newrow["Name"] = row["Name"];
                    dtinfo.Rows.Add(newrow);
                }
                return this.dtinfo;
            }
            else
            {
                return this.dtChildTypeInfo;
            }
        }

        /// <summary>
        /// 生成实例方法
        /// </summary>
        /// <returns></returns>
        public static XiHuan_GoodsTypeFacade GetInstance()
        {
            if (instance == null)
            {
                //取得实例的时候先锁定对象,然后判定是否存在
                lock (olock)
                {
                    //如果实例没有被初始化则实例化变量
                    if (instance == null)
                    {
                        instance = new XiHuan_GoodsTypeFacade();
                    }

                }

            }

            return instance;
        }

        public static void Refresh()
        {
            lock (olock)
            {
                instance = new XiHuan_GoodsTypeFacade();
            }
        }
    }
}
