using System;
using System.Data;
using BusinessEntity;
using PersistenceLayer;
namespace BusinessFacade
{
    public class XiHuan_ChangeRequireFacade
    {

        public enum ChangeRequireType
        {
            换品交换 = 0,
            Money交换 = 1
        }

        public enum ChangeRequireState
        {
            新发起 = 0,
            考虑中 = 1,
            交换中 = 2,
            交换成功 = 3,
            已取消 = 4,
            换主拒绝 = 5
        }

        public static string FormatState(string state, string type)
        {
            string result = string.Empty;
            switch (state)
            {
                case "0":
                    result = "<span class=\"inittext\" title=\"新发起的交换请求\" style=\"cursor:help;\" >新发起</span>";
                    break;
                case "1":
                    result = "<span class=\"dealingtext\" title=\"正在考虑是否交换\" style=\"cursor:help;\" >考虑中..</span>";
                    break;
                case "2":
                    result = "<span class=\"bluetext\" title=\"正在进行交换\" style=\"cursor:help;\">交换中..</span>";
                    break;
                case "3":
                    result = "<span class=\"greentext\" title=\"交换成功\" style=\"cursor:help;\">交换成功</span>";
                    break;
                case "4":
                    if (type.Equals("receive"))
                        result = "<span class=\"graytext\" title=\"交换已取消\" style=\"cursor:help;\">对方已取消</span>";
                    else
                        result = "<span class=\"graytext\" title=\"交换已取消\" style=\"cursor:help;\">已取消</span>";
                    break;
                case "5":
                    if (type.Equals("receive"))
                        result = "<span class=\"graytext\" title=\"拒绝交换\" style=\"cursor:help;\">已拒绝</span>";
                    else
                        result = "<span class=\"graytext\" title=\"拒绝交换\" style=\"cursor:help;\">换主拒绝</span>";
                    break;
                default: break;
            }
            return result;
        }

        public static int GetNewChangeRequireCount(int uid)
        {
            Query require = new Query(typeof(XiHuan_UserGoodsChangeRequireEntity));
            Condition c = require.GetQueryCondition();
            c.AddEqualTo(XiHuan_UserGoodsChangeRequireEntity.__OWNERID, uid);
            c.AddEqualTo(XiHuan_UserGoodsChangeRequireEntity.__FLAG, ChangeRequireState.新发起.ToString("d"));
            require.SelectCount(XiHuan_MessageEntity.__ID, "requirecount");
            return CommonMethodFacade.ConvertToInt(require.ExecuteScalar(), 0);
        }

        public static DataTable GetUserRequire(XiHuan_ChangeRequireSearchFilter f)
        {
            string sql = "select * from XiHuan_UserGoodsChangeRequire with(nolock) where 1=1 ";
            if (f.GoodsId != int.MaxValue)
                sql += string.Format(" and GoodsId={0} ", f.GoodsId);
            if (f.OwnerId != int.MaxValue)
                sql += string.Format(" and OwnerId={0} ", f.OwnerId);
            if (f.SenderId != int.MaxValue)
                sql += string.Format(" and SenderId={0} ", f.SenderId);
            if (f.OwnerName.Trim().Length > 0)
                sql += string.Format(" and OwnerName={0} ", ValidatorHelper.SafeSql(f.OwnerName.Trim()));
            if (f.GoodsName.Trim().Length > 0)
                sql += string.Format(" and GoodsName like '%{0}%' ", ValidatorHelper.SafeSql(f.GoodsName.Trim()));
            if (f.SenderName.Trim().Length > 0)
                sql += string.Format(" and SenderName like '%{0}%' ", ValidatorHelper.SafeSql(f.SenderName.Trim()));
            if (f.RequireType != int.MaxValue)
                sql += string.Format(" and RequireType={0} ", f.RequireType);
            if (f.Flag != int.MaxValue)
                sql += string.Format(" and Flag={0} ", f.Flag);
            if (f.Flags.Trim().Length > 0)
                sql += string.Format(" and Flag in({1}) ", f.Flags.Trim());
            if (f.RequireDateBegin != DateTime.MinValue)
                sql += string.Format(" and RequireDate>='{0}' ", f.RequireDateBegin);
            if (f.RequireDateEnd != DateTime.MaxValue)
                sql += string.Format(" and RequireDate<'{1}' ", f.RequireDateEnd.AddDays(1));
            return Query.ProcessSql(sql + " order by RequireDate desc; ", GlobalVar.DataBase_Name);
        }

    }
}
