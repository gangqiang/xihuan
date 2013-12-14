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
            ��Ʒ���� = 0,
            Money���� = 1
        }

        public enum ChangeRequireState
        {
            �·��� = 0,
            ������ = 1,
            ������ = 2,
            �����ɹ� = 3,
            ��ȡ�� = 4,
            �����ܾ� = 5
        }

        public static string FormatState(string state, string type)
        {
            string result = string.Empty;
            switch (state)
            {
                case "0":
                    result = "<span class=\"inittext\" title=\"�·���Ľ�������\" style=\"cursor:help;\" >�·���</span>";
                    break;
                case "1":
                    result = "<span class=\"dealingtext\" title=\"���ڿ����Ƿ񽻻�\" style=\"cursor:help;\" >������..</span>";
                    break;
                case "2":
                    result = "<span class=\"bluetext\" title=\"���ڽ��н���\" style=\"cursor:help;\">������..</span>";
                    break;
                case "3":
                    result = "<span class=\"greentext\" title=\"�����ɹ�\" style=\"cursor:help;\">�����ɹ�</span>";
                    break;
                case "4":
                    if (type.Equals("receive"))
                        result = "<span class=\"graytext\" title=\"������ȡ��\" style=\"cursor:help;\">�Է���ȡ��</span>";
                    else
                        result = "<span class=\"graytext\" title=\"������ȡ��\" style=\"cursor:help;\">��ȡ��</span>";
                    break;
                case "5":
                    if (type.Equals("receive"))
                        result = "<span class=\"graytext\" title=\"�ܾ�����\" style=\"cursor:help;\">�Ѿܾ�</span>";
                    else
                        result = "<span class=\"graytext\" title=\"�ܾ�����\" style=\"cursor:help;\">�����ܾ�</span>";
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
            c.AddEqualTo(XiHuan_UserGoodsChangeRequireEntity.__FLAG, ChangeRequireState.�·���.ToString("d"));
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
