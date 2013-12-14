using BusinessEntity;
using PersistenceLayer;
using System.Data;
namespace BusinessFacade
{
    public class XiHuan_UserNotesFacade
    {

        public enum NotesState
        {
            未读 = 0,
            已读 = 1,
            已回复 = 2,
        }

        public static string FormatReiveNotesFlag(string flag)
        {
            string result = string.Empty;
            switch (flag)
            {
                case "0":
                    result = "<span class=\"graytext\">未读</span>";
                    break;
                case "1":
                    result = "<span class=\"bluetext\">已读</span>";
                    break;
                case "2":
                    result = "<span class=\"greentext\">已回复</span>";
                    break;
                default:
                    break;
            }

            return result;
        }

        public static string FormatSendNotesFlag(string flag, string ischecked)
        {
            string result = string.Empty;
            if (ischecked.Equals("0"))
            {
                result = "<span class=\"graytext\" style=\"cursor:help;\" title=\"留言还未通过审核\" >待审核</span>";
            }
            else
            {
                switch (flag)
                {
                    case "0":
                        result = "<span class=\"graytext\">对方未读</span>";
                        break;
                    case "1":
                        result = "<span class=\"bluetext\">对方已读</span>";
                        break;
                    case "2":
                        result = "<span class=\"greentext\">对方已回复</span>";
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public static int GetNewNotesCount(int uid)
        {
            Query notes = new Query(typeof(XiHuan_GuestBookEntity));
            Condition c = notes.GetQueryCondition();
            c.AddEqualTo(XiHuan_GuestBookEntity.__TOID, uid);
            c.AddEqualTo(XiHuan_GuestBookEntity.__ISCHECKED, 1);
            notes.SelectCount(XiHuan_GuestBookEntity.__ID, "notescount");
            return CommonMethodFacade.ConvertToInt(notes.ExecuteScalar(), 0);
        }

        /// <summary>
        /// 悄悄话是否可见
        /// </summary>
        /// <param name="fromid">留言人Id</param>
        /// <param name="toid">留言对象Id</param>
        /// <param name="uid">当前人Id</param>
        /// <param name="issceret">是否悄悄话</param>
        /// <returns>是否需要显示留言内容</returns>
        public static bool IsSceretNoteShow(string fromid, string toid, string uid, string issceret)
        {
            fromid = CommonMethodFacade.FinalString(fromid);
            toid = CommonMethodFacade.FinalString(toid);
            issceret = CommonMethodFacade.FinalString(issceret);
            if (issceret.Equals("1"))
            {
                if (uid.Equals("0"))
                    return false;
                else
                    return (uid == fromid || uid == toid);
            }
            else
                return true;

        }

        /// <summary>
        /// 留言的显示处理
        /// </summary>
        /// <param name="fromid">留言人Id</param>
        /// <param name="toid">留言对象Id</param>
        /// <param name="uid">当前人Id</param>
        /// <param name="issceret">是否悄悄话</param>
        /// <param name="content">留言内容</param>
        /// <param name="recontent">留言回复内容</param>
        /// <returns>最终的显示</returns>
        public static string GetFinalNotesToShow(string fromid, string toid, string uid, string issceret, string content, string recontent, string ischecked)
        {
            bool isneedshow = IsSceretNoteShow(fromid, toid, uid, issceret);
            string result = string.Empty;
            if (isneedshow)
            {
                if (!ischecked.Equals("1")) //未审核的不显示内容
                {
                    result = "<span class=\"graytext\">此条留言尚未通过审核</span>";
                }
                else //已经通过审核的近一步判断，是否悄悄话
                {
                    if (isneedshow) //可以显示留言
                    {
                        result = content + (recontent.Trim().Length > 0 ? "<br/><span class=\"bluetext\">换主回复：" + recontent + "</span>" : "");
                    }
                    else //悄悄话且不显示
                    {
                        result = "<span class=\"bluetext\">此留言为悄悄话，换主和留言人可见。</span>";
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取用户的留言信息
        /// </summary>
        /// <param name="uid">用户Id</param>
        /// <param name="gid">大于0表示过滤针对某个换品的留言</param>
        /// <param name="type">0：表示接收到的 1表示发出的</param>
        /// <param name="isfilter">是否过滤掉待审核的留言</param>
        /// <returns></returns>
        public static DataTable GetUserNotes(int uid, int gid, int type, bool isfilter)
        {
            RetrieveCriteria rcnote = new RetrieveCriteria(typeof(XiHuan_GuestBookEntity));
            Condition cnote = rcnote.GetNewCondition();
            rcnote.OrderBy(XiHuan_GuestBookEntity.__CREATEDATE, false);
            if (type == 0)
                cnote.AddEqualTo(XiHuan_GuestBookEntity.__TOID, uid);
            if (type == 1)
                cnote.AddEqualTo(XiHuan_GuestBookEntity.__FROMID, uid);
            if (gid > 0)
                cnote.AddEqualTo(XiHuan_GuestBookEntity.__GOODSID, gid);
            if (isfilter)
                cnote.AddEqualTo(XiHuan_GuestBookEntity.__ISCHECKED, 1);
            return rcnote.AsDataTable();
        }
    }
}
