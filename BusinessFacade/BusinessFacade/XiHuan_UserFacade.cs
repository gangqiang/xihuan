using System;
using System.Text;
using BusinessEntity;
using PersistenceLayer;
using System.Data;
namespace BusinessFacade
{
    public class XiHuan_UserFacade
    {

        public enum UserType
        {
            个人注册 = 0,
            公司注册 = 1
        }

        /// <summary>
        /// 判断用户名是否已经被占用,被占用返回TRUE，否则False
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>被占用返回TRUE，否则False</returns>
        public static bool IsUserNameAlreayUse(string username)
        {
            username = CommonMethodFacade.FinalString(username);
            Query checkuser = new Query(typeof(XiHuan_UserInfoEntity));
            Condition c = checkuser.GetQueryCondition();
            c.AddEqualTo(XiHuan_UserInfoEntity.__USERNAME, username);
            checkuser.SelectCount(XiHuan_UserInfoEntity.__ID, "usercount");
            return (Convert.ToInt32(checkuser.ExecuteScalar()) > 0);
        }

        public static bool IsUserValid(string uname, string upass)
        {
            uname = CommonMethodFacade.FinalString(uname);
            upass = CommonMethodFacade.FinalString(upass);
            if (uname.Length > 0 && upass.Length > 0)
            {
                string sqlLogin = @"UPDATE XiHuan_UserInfo SET LastLoginTime=getdate(), 
                                       Score=Score+(CASE WHEN LastLoginTime<'{0}' THEN {1} ELSE 0 END ) 
                                       WHERE UserName='{2}' AND  OrignalPwd='{3}' ;";
                int effcount = Query.ProcessSqlNonQuery(string.Format(sqlLogin, DateTime.Now.ToString("yyyy-MM-dd"),
                                     SystemConfigFacade.Instance().LoginAddScore(), ValidatorHelper.SafeSql(uname),
                                     ValidatorHelper.SafeSql(upass)),
                                     GlobalVar.DataBase_Name);
                return effcount > 0;
            }
            else
            {
                return false;
            }
        }

        public static int GetIdByName(string uname)
        {
            uname = CommonMethodFacade.FinalString(uname);
            RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_UserInfoEntity));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(XiHuan_UserInfoEntity.__USERNAME, uname);
            rc.AddSelect(XiHuan_UserInfoEntity.__ID);
            XiHuan_UserInfoEntity user = rc.AsEntity() as XiHuan_UserInfoEntity;
            if (user != null)
                return user.ID;
            else
                return 0;
        }

        public static bool IsCertNoChecked(int id)
        {
            byte rs = 0;
            XiHuan_UserInfoEntity user = new XiHuan_UserInfoEntity();
            user.ID = id;
            user.Retrieve();
            if (user.IsPersistent)
                rs = user.IsCertNoChecked;
            return rs == 1;
        }

        public static bool IsStartUser(int id)
        {
            byte rs = 0;
            XiHuan_UserInfoEntity user = new XiHuan_UserInfoEntity();
            user.ID = id;
            user.Retrieve();
            if (user.IsPersistent)
                rs = user.IsStarUser;
            return rs == 1;
        }

        public static DataTable SearchUser(XiHuan_UserSearchFilter f, out int rowcount)
        {
            int minId = f.PageIndex * f.PageSize;
            int maxId = (f.PageIndex + 1) * f.PageSize + 1;
            string sql = @"DECLARE @indextable table(Id int identity(1,1) PRIMARY KEY,uid int);
                           insert into @indextable(uid) select Id from XiHuan_UserInfo with(nolock) where IsLocked=0 {0};
                           select @@ROWCOUNT;
                           select {1} from XiHuan_UserInfo u with(nolock) inner join @indextable t on u.Id=t.uid
                           and t.Id>{2} and t.Id<{3} ";
            StringBuilder sqlwhere = new StringBuilder("");

            if (f.UserName.Trim().Length > 0)
                sqlwhere.AppendFormat(" AND UserName like'%{0}%' ", ValidatorHelper.SafeSql(f.UserName.Trim()));
            if (f.ProvinceId != int.MaxValue)
                sqlwhere.AppendFormat(" AND ProvinceId={0} ", f.ProvinceId);
            if (f.CityId != int.MaxValue)
                sqlwhere.AppendFormat(" AND CityId={0} ", f.CityId);
            if (f.AreaId != int.MaxValue)
                sqlwhere.AppendFormat(" AND AreaId={0} ", f.AreaId);
            if (f.SchooId != int.MaxValue)
                sqlwhere.AppendFormat(" AND SchoolId={0} ", f.SchooId);
            if (f.IsStartUser != int.MaxValue)
                sqlwhere.AppendFormat(" AND IsStarUser={0} ", f.IsStartUser);
            if (f.Gender != int.MaxValue)
                sqlwhere.AppendFormat(" AND Gender={0} ", f.Gender);
            if (f.IsHavePhoto != int.MaxValue)
                sqlwhere.Append(" AND HeadImage <> 'images/nophoto.gif'");
            if (f.CreateDateBegin != DateTime.MinValue)
                sqlwhere.AppendFormat(" AND RegisterDate>='{0}' ", f.CreateDateBegin);
            if (f.CreateDateEnd != DateTime.MaxValue)
                sqlwhere.AppendFormat(" AND RegisterDate<'{0}' ", f.CreateDateEnd.AddDays(1));
            DataSet ds = Query.ProcessMultiSql(string.Format(sql, sqlwhere.ToString() + " order by " + f.OrderByParam, f.SelectFileds, minId, maxId), GlobalVar.DataBase_Name);
            rowcount = CommonMethodFacade.ConvertToInt(ds.Tables[0].Rows[0][0], 0);
            return ds.Tables[1];
        }
    }
}
