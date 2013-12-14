using System.Data;
using PersistenceLayer;
namespace BusinessFacade
{
    public class SystemConfigFacade
    {
        private static SystemConfigFacade instance = null;
        private static readonly object olock = new object();
        private DataTable dt = new DataTable();
        private SystemConfigFacade()
        {
            dt = Query.ProcessSql("select ConfigValue,ConfigKey from XiHuan_SystemConfig with(nolock) ", GlobalVar.DataBase_Name);
        }
        public int LoginAddScore()
        {
            DataRow[] dr = dt.Select("ConfigKey='LoginAddScore'");
            return dr.Length > 0 ? CommonMethodFacade.ConvertToInt(dr[0]["ConfigValue"], 0) : 0;
        }
        public int RegisterAddScore()
        {
            DataRow[] dr = dt.Select("ConfigKey='RegisterAddScore'");
            return dr.Length > 0 ? CommonMethodFacade.ConvertToInt(dr[0]["ConfigValue"], 0) : 0;
        }

        public int RegisterAddHuanBi()
        {
            DataRow[] dr = dt.Select("ConfigKey='RegisterAddHuanBi'");
            return dr.Length > 0 ? CommonMethodFacade.ConvertToInt(dr[0]["ConfigValue"], 0) : 0;
        }

        public int TuiJianAddScore()
        {
            DataRow[] dr = dt.Select("ConfigKey='TuiJianAddScore'");
            return dr.Length > 0 ? CommonMethodFacade.ConvertToInt(dr[0]["ConfigValue"], 0) : 0;
        }

        public int TuiJianAddHuanBi()
        {
            DataRow[] dr = dt.Select("ConfigKey='TuiJianAddHuanBi'");
            return dr.Length > 0 ? CommonMethodFacade.ConvertToInt(dr[0]["ConfigValue"], 0) : 0;
        }

        public string RegMesContent()
        {
            DataRow[] dr = dt.Select("ConfigKey='RegisterMessageContent'");
            return dr.Length > 0 ? CommonMethodFacade.FinalString(dr[0]["ConfigValue"]) : string.Empty;
        }

        public int AddScoreByAddGoods()
        {
            DataRow[] dr = dt.Select("ConfigKey='AddGoodsScore'");
            return dr.Length > 0 ? CommonMethodFacade.ConvertToInt(dr[0]["ConfigValue"], 0) : 0;
        }

        public int AddHBByAddGoods()
        {
            DataRow[] dr = dt.Select("ConfigKey='AddGoodsHuanBi'");
            return dr.Length > 0 ? CommonMethodFacade.ConvertToInt(dr[0]["ConfigValue"], 0) : 0;
        }

        public string WebSiteTitle
        {
            get
            {
                DataRow[] dr = dt.Select("ConfigKey='WebTitle'");
                return dr.Length > 0 ? CommonMethodFacade.FinalString(dr[0]["ConfigValue"]) : string.Empty;
            }
        }

        public string WebSiteKeyWords
        {
            get
            {
                DataRow[] dr = dt.Select("ConfigKey='KeyWords'");
                return dr.Length > 0 ? CommonMethodFacade.FinalString(dr[0]["ConfigValue"]) : string.Empty;
            }
        }

        public string WebSiteDescription
        {
            get
            {
                DataRow[] dr = dt.Select("ConfigKey='WebDescription'");
                return dr.Length > 0 ? CommonMethodFacade.FinalString(dr[0]["ConfigValue"]) : string.Empty;
            }
        }

        public string FFZF
        {
            get
            {
                DataRow[] dr = dt.Select("ConfigKey='FFZF'");
                return dr.Length > 0 ? CommonMethodFacade.FinalString(dr[0]["ConfigValue"]) : string.Empty;
            }
        }

        public bool IsGoodsAddNeedCheck
        {
            get
            {
                DataRow[] dr = dt.Select("ConfigKey='IsGoodsAddNeedCheck'");
                return dr.Length > 0 ? CommonMethodFacade.FinalString(dr[0]["ConfigValue"]).Equals("1") : true;
            }
        }

        public string HomeRoundPics
        {
            get
            {
                DataRow[] dr = dt.Select("ConfigKey='HomeRounPics'");
                return dr.Length > 0 ? CommonMethodFacade.FinalString(dr[0]["ConfigValue"]) : string.Empty;
            }
        }

        public static SystemConfigFacade Instance()
        {
            if (instance == null)
            {
                //取得实例的时候先锁定对象,然后判定是否存在
                lock (olock)
                {
                    //如果实例没有被初始化则实例化变量
                    if (instance == null)
                    {
                        instance = new SystemConfigFacade();
                    }

                }

            }

            return instance;

        }

        public static void Refresh()
        {
            lock (olock)
            {
                instance = new SystemConfigFacade();
            }
        }
    }
}
