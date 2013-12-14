using System.Data;
using PersistenceLayer;
namespace BusinessFacade
{
    public class XiHuan_UserFriendsFacade
    {
        public static DataTable GetUserFriends(int uid)
        {
            return Query.ProcessSql("select *from XiHuan_UserFriends with(nolock) where OwnerId=" + uid.ToString()+" order by AddDate desc;", GlobalVar.DataBase_Name);
        }
    }
}
