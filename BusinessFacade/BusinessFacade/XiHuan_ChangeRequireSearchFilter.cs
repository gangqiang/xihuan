using System;

namespace BusinessFacade
{
    public class XiHuan_ChangeRequireSearchFilter
    {
        public string SenderName = string.Empty;
        public int OwnerId = int.MaxValue;
        public int SenderId = int.MaxValue;
        public string OwnerName = string.Empty;
        public int GoodsId = int.MaxValue;
        public string GoodsName = string.Empty;
        public int RequireType = int.MaxValue;
        public int Flag = int.MaxValue;
        public string Flags = string.Empty;
        public DateTime RequireDateBegin = DateTime.MinValue;
        public DateTime RequireDateEnd = DateTime.MaxValue;
        public int PageSize = 10;
        public int PageIndex = 0;
    }
}
