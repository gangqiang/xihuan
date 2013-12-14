using System.Data;
using PersistenceLayer;
namespace BusinessFacade
{
    public class XiHuan_FavoriteParam
    {
        
    }

    public class XiHuan_FavoriteFacade
    {
        public static DataTable GetUserFavoriteGoods(int userid)
        {
            return Query.ProcessSql("select *from XiHuan_UserFavorate with(nolock) where UserId=" + userid + " order by FavDate desc", GlobalVar.DataBase_Name);
        }   
    }
}
