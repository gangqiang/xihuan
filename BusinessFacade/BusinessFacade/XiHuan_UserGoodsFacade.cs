using System;
using System.Text;
using System.Data;
using PersistenceLayer;
namespace BusinessFacade
{
    public class XiHuan_UserGoodsFacade
    {
        public enum IsGoodHavePhoto
        {
            无 = 0,
            有 = 1
        }

        public enum GoodsState
        {
            新登记 = 0,
            考虑中 = 1,
            交换中 = 2,
            交换成功 = 3
        }

        public static DataTable GetGoodsState()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");
            string[,] f = new string[,]{
                {"新登记", "0"},
                {"考虑中", "1"},
                {"交换中", "2"},
                {"交换成功","3"},
            };
            DataRow dr;
            for (int i = 0; i < f.GetLength(0); i++)
            {
                dr = dt.NewRow();
                dr["Text"] = f[i, 0];
                dr["Value"] = f[i, 1];
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static string FormatNewDeep(string deep)
        {
            string result = string.Empty;
            switch (deep)
            {
                case "10": result = "十成新"; break;
                case "9": result = "九成新"; break;
                case "8": result = "八成新"; break;
                case "7": result = "七成新"; break;
                case "6": result = "六成新"; break;
                case "5": result = "五成新"; break;
                case "4": result = "四成新"; break;
                case "3": result = "三成新"; break;
                case "2": result = "三成新以下"; break;
                default: break;
            }

            return result;
        }

        public static string FormatGoodsState(string state, string ischecked)
        {
            string result = string.Empty;
            if (ischecked.Equals("0"))
            {
                result = "<span class=\"graytext\" style=\"cursor:help;\" title=\"换品还未通过审核\" >待审核</span>";
            }
            else
            {
                switch (state)
                {
                    case "0":
                        result = "<span class=\"inittext\" style=\"cursor:help;\" title=\"换品刚登记\" >新登记</span>";
                        break;
                    case "1":
                        result = "<span class=\"dealingtext\" style=\"cursor:help;\" title=\"考虑是否进行交换\" >考虑中</span>";
                        break;
                    case "2":
                        result = "<span class=\"bluetext\" style=\"cursor:help;\" title=\"正在进行交换\" >交换中</span>";
                        break;
                    case "3":
                        result = "<span class=\"greentext\" style=\"cursor:help;\" title=\"成功完成交换\" >交换成功</span>";
                        break;
                    default: break;
                }
            }
            return result;
        }

        public static string GetTypeNameById(string id)
        {
            DataTable dt = XiHuan_GoodsTypeFacade.GetInstance().GetGoodsParentType();
            DataRow[] dr = dt.Select("Id=" + id);
            if (dr.Length > 0)
                return CommonMethodFacade.FinalString(dr[0]["TypeName"]);
            else
                return string.Empty;
        }

        public static string GetSecondTypeNameById(string pid, string id)
        {
            DataTable dt = XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(pid);
            DataRow[] dr = dt.Select("Id=" + id);
            if (dr.Length > 0)
                return CommonMethodFacade.FinalString(dr[0]["Name"]);
            else
                return string.Empty;
        }

        public static DataTable SearchGoods(XiHuan_UserGoodsSearchFilter f)
        {
            string sql = "select {0} from XiHuan_UserGoods with(nolock) where 1=1 ";
            StringBuilder sqlwhere = new StringBuilder("");
            if (f.OwnerId != int.MaxValue)
                sqlwhere.AppendFormat(" AND OwnerId={0} ", f.OwnerId);
            if (f.GoodsName.Trim().Length > 0)
                sqlwhere.AppendFormat(" AND Name like '%{0}%' ", ValidatorHelper.SafeSql(f.GoodsName.Trim()));
            if (f.GoodsTypeId != int.MaxValue)
                sqlwhere.AppendFormat(" AND TypeId={0} ", f.GoodsTypeId);
            if (f.GoodsSceondTypeId != int.MaxValue)
                sqlwhere.AppendFormat(" AND ChildId={0} ", f.GoodsSceondTypeId);
            if (f.GoodsState != int.MaxValue)
                sqlwhere.AppendFormat(" AND GoodState={0} ", f.GoodsState);
            if (f.GoodsStates.Trim().Length > 0)
                sqlwhere.AppendFormat(" AND GoodState in ({0}) ", f.GoodsStates);
            if (f.ProvinceId != int.MaxValue)
                sqlwhere.AppendFormat(" AND ProvinceId={0} ", f.ProvinceId);
            if (f.CityId != int.MaxValue)
                sqlwhere.AppendFormat(" AND CityId={0} ", f.CityId);
            if (f.AreaId != int.MaxValue)
                sqlwhere.AppendFormat(" AND AreaId={0} ", f.AreaId);
            if (f.SchooId != int.MaxValue)
                sqlwhere.AppendFormat(" AND SchoolId={0} ", f.SchooId);
            if (f.NewDeep != int.MaxValue)
                sqlwhere.AppendFormat(" AND NewDeep={0} ", f.NewDeep);
            if (f.IsHavePhoto != int.MaxValue)
                sqlwhere.AppendFormat(" AND IsHavePhoto={0} ", f.IsHavePhoto);
            if (f.CreateDateBegin != DateTime.MinValue)
                sqlwhere.AppendFormat(" AND CreateDate>='{0}' ", f.CreateDateBegin);
            if (f.CreateDateEnd != DateTime.MaxValue)
                sqlwhere.AppendFormat(" AND CreateDate<'{0}' ", f.CreateDateEnd.AddDays(1));
            return Query.ProcessSql(string.Format(sql, f.SelectFileds) + sqlwhere.ToString() + " order by " + f.OrderByParam, GlobalVar.DataBase_Name);
        }

        public static DataTable SearchGoods(XiHuan_UserGoodsSearchFilter f, out int rowcount)
        {
            int minId = f.PageIndex * f.PageSize;
            int maxId = (f.PageIndex + 1) * f.PageSize + 1;
            string sql = @"DECLARE @indextable table(Id int identity(1,1) PRIMARY KEY,gid int);
                           INSERT INTO @indextable(gid) select Id from XiHuan_UserGoods with(nolock) where 1=1 {0};
                           select @@ROWCOUNT;
                           select {1} from XiHuan_UserGoods g with(nolock) inner join @indextable t on t.gid=g.Id 
                           and t.Id>{2} and t.Id<{3} ";
            StringBuilder sqlwhere = new StringBuilder("");
            if (f.OwnerId != int.MaxValue)
                sqlwhere.AppendFormat(" AND OwnerId={0} ", f.OwnerId);
            if (f.OwnerName.Trim().Length > 0)
                sqlwhere.AppendFormat(" AND OwnerName='{0}' ", ValidatorHelper.SafeSql(f.OwnerName.Trim()));
            if (f.GoodsName.Trim().Length > 0)
                sqlwhere.AppendFormat(" AND Name like'%{0}%' ", ValidatorHelper.SafeSql(f.GoodsName.Trim()));
            if (f.GoodsTypeId != int.MaxValue)
                sqlwhere.AppendFormat(" AND TypeId={0} ", f.GoodsTypeId);
            if (f.GoodsSceondTypeId != int.MaxValue)
                sqlwhere.AppendFormat(" AND ChildId={0} ", f.GoodsSceondTypeId);
            if (f.GoodsState != int.MaxValue)
                sqlwhere.AppendFormat(" AND GoodState={0} ", f.GoodsState);
            if (f.GoodsStates.Trim().Length > 0)
                sqlwhere.AppendFormat(" AND GoodState in ({0}) ", f.GoodsStates);
            if (f.ProvinceId != int.MaxValue)
                sqlwhere.AppendFormat(" AND ProvinceId={0} ", f.ProvinceId);
            if (f.CityId != int.MaxValue)
                sqlwhere.AppendFormat(" AND CityId={0} ", f.CityId);
            if (f.AreaId != int.MaxValue)
                sqlwhere.AppendFormat(" AND AreaId={0} ", f.AreaId);
            if (f.SchooId != int.MaxValue)
                sqlwhere.AppendFormat(" AND SchoolId={0} ", f.SchooId);
            if (f.NewDeep != int.MaxValue)
                sqlwhere.AppendFormat(" AND NewDeep={0} ", f.NewDeep);
            if (f.IsHavePhoto != int.MaxValue)
                sqlwhere.AppendFormat(" AND IsHavePhoto={0} ", f.IsHavePhoto);
            if (f.CreateDateBegin != DateTime.MinValue)
                sqlwhere.AppendFormat(" AND CreateDate>='{0}' ", f.CreateDateBegin);
            if (f.CreateDateEnd != DateTime.MaxValue)
                sqlwhere.AppendFormat(" AND CreateDate<'{0}' ", f.CreateDateEnd.AddDays(1));
            if (f.GoodsStateNotIn.Trim().Length > 0)
                sqlwhere.AppendFormat(" AND GoodState not in({0}) ", f.GoodsStateNotIn.Trim());
            if (f.IsChecked != int.MinValue)
                sqlwhere.AppendFormat(" AND IsChecked={0} ", f.IsChecked);
            DataSet ds = Query.ProcessMultiSql(string.Format(sql, sqlwhere.ToString() + " order by " + f.OrderByParam, f.SelectFileds, minId, maxId), GlobalVar.DataBase_Name);
            rowcount = CommonMethodFacade.ConvertToInt(ds.Tables[0].Rows[0][0], 0);
            return ds.Tables[1];
        }
    }
}
