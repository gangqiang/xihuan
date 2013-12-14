using System.Data;
using PersistenceLayer;
namespace BusinessFacade
{
    public class ProvinceCityFacade
    {

        private static ProvinceCityFacade instance = null;

        private static readonly object olock = new object();

        private DataTable provinceDt = new DataTable();

        private DataTable cityDt = new DataTable();

        private DataTable cityInfo = new DataTable();

        private DataTable areaDt = new DataTable();

        private DataTable areaInfo = new DataTable();

        private DataTable schoolDt = new DataTable();

        private DataTable schoolInfo = new DataTable();

        /// <summary>
        /// 私有构造函数
        /// 之所有不是public类型的,因为是为了避免发生new Singleton()产生更多的实例
        /// </summary>
        private ProvinceCityFacade()
        {
            string sql = @"select provinceId,province from province with(nolock);
                           select cityId,city,father from city with(nolock) order by father;
                           select areaId,area,father from area with(nolock) order by father;
                           select Id,SchoolName,ProvinceId,CityId from XiHuan_SchoolInfo with(nolock) order by ProvinceId,CityId;";
            DataSet ds = Query.ProcessMultiSql(sql, GlobalVar.DataBase_Name);
            provinceDt = ds.Tables[0];
            cityDt = ds.Tables[1];
            areaDt = ds.Tables[2];
            schoolDt = ds.Tables[3];

        }

        /// <summary>
        /// 返回省数据信息
        /// </summary>
        /// <returns></returns>


        #region 省数据

        public DataTable GetProvince()
        {
            return this.provinceDt;
        }

        #endregion

        #region 市数据

        public DataTable GetCityInfo(string pid)
        {
            pid = CommonMethodFacade.FinalString(pid);
            if (pid.Length > 0)
            {
                this.cityInfo = cityDt.Clone();
                DataRow[] cityrow = cityDt.Select("father='" + pid + "'");
                foreach (DataRow row in cityrow)
                {
                    DataRow newcityrow = cityInfo.NewRow();
                    newcityrow["city"] = row["city"];
                    newcityrow["cityId"] = row["cityId"];
                    newcityrow["father"]=row["father"];
                    cityInfo.Rows.Add(newcityrow);
                }
                return cityInfo;
            }
            else
                return this.cityDt;
        }
        #endregion

        #region 地区信息
        public DataTable GetAreaInfo(string cid)
        {
            cid = CommonMethodFacade.FinalString(cid);
            if (cid.Length > 0)
            {
                this.areaInfo = areaDt.Clone();
                DataRow[] arearow = areaDt.Select("father='" + cid + "'");
                foreach (DataRow row in arearow)
                {
                    DataRow newarearow = areaInfo.NewRow();
                    newarearow["area"] = row["area"];
                    newarearow["areaId"] = row["areaId"];
                    areaInfo.Rows.Add(newarearow);
                }
                return areaInfo;
            }
            else
                return this.areaDt;
        }
        #endregion

        #region 学校信息
        public DataTable GetSchoolInfo(string pid, string cid)
        {
            pid = CommonMethodFacade.FinalString(pid);
            cid = CommonMethodFacade.FinalString(cid);
            if (pid.Length == 0 && cid.Length == 0)
                return this.schoolDt;
            else
            {
                string filter = string.Empty;
                schoolInfo = schoolDt.Clone();
                if (pid.Length > 0 && cid.Length > 0)
                    filter += string.Format("ProvinceId={0} and CityId={1} ", pid, cid);
                else
                {
                    if (pid.Length > 0)
                        filter += string.Format("ProvinceId={0} ", pid);
                    if (cid.Length > 0)
                        filter += string.Format("CityId={0} ", cid);
                }

                DataRow[] schoolrow = schoolDt.Select(filter);
                foreach (DataRow row in schoolrow)
                {
                    DataRow newschoolrow = schoolInfo.NewRow();
                    newschoolrow["SchoolName"] = row["SchoolName"];
                    newschoolrow["Id"] = row["Id"];
                    newschoolrow["ProvinceId"] = row["ProvinceId"];
                    newschoolrow["CityId"]=row["CityId"];
                    schoolInfo.Rows.Add(newschoolrow);
                }
                return this.schoolInfo;
            }

        }

        #endregion

        /// <summary>
        /// 生成实例方法
        /// </summary>
        /// <returns></returns>
        public static ProvinceCityFacade GetInstance()
        {
            if (instance == null)
            {
                //取得实例的时候先锁定对象,然后判定是否存在
                lock (olock)
                {
                    //如果实例没有被初始化则实例化变量
                    if (instance == null)
                    {
                        instance = new ProvinceCityFacade();
                    }

                }

            }
            return instance;
        }

        public static void Refresh()
        {
            lock (olock)
            {
                instance = new ProvinceCityFacade();
            }
        }
    }
}
