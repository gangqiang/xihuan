using System;

namespace BusinessFacade
{
    public class XiHuan_UserGoodsSearchFilter
    {
        public string GoodsName = string.Empty;
        public int OwnerId = int.MaxValue;
        public string OwnerName = string.Empty;
        public int ProvinceId = int.MaxValue;
        public int CityId = int.MaxValue;
        public int AreaId = int.MaxValue;
        public int SchooId = int.MaxValue;
        public int GoodsTypeId = int.MaxValue;
        public int GoodsSceondTypeId = int.MaxValue;
        public int GoodsState = int.MaxValue;
        public string GoodsStates = string.Empty;
        public int NewDeep = int.MaxValue;
        public int IsHavePhoto = int.MaxValue;
        public DateTime CreateDateBegin = DateTime.MinValue;
        public DateTime CreateDateEnd = DateTime.MaxValue;
        public string SelectFileds = string.Empty;
        public string GoodsStateNotIn = string.Empty;
        public string OrderByParam = " CreateDate desc ";
        public int IsChecked = int.MinValue;
        public int PageSize = 10;
        public int PageIndex = 0;
    }
}
