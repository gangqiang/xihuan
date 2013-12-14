using System;

namespace BusinessFacade
{
    public class XiHuan_UserSearchFilter
    {
        public string UserName = string.Empty;
        public int ProvinceId = int.MaxValue;
        public int CityId = int.MaxValue;
        public int AreaId = int.MaxValue;
        public int SchooId = int.MaxValue;
        public int IsHavePhoto = int.MaxValue;
        public int IsStartUser = int.MaxValue;
        public int Gender = int.MaxValue;
        public DateTime CreateDateBegin = DateTime.MinValue;
        public DateTime CreateDateEnd = DateTime.MaxValue;
        public string SelectFileds = string.Empty;
        public string OrderByParam = " RegisterDate desc ";
        public int PageSize = 10;
        public int PageIndex=0;
    }
}
