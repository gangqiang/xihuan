using BusinessEntity;
using PersistenceLayer;
using System.Data;
namespace BusinessFacade
{
    public class XiHuan_UserNotesFacade
    {

        public enum NotesState
        {
            δ�� = 0,
            �Ѷ� = 1,
            �ѻظ� = 2,
        }

        public static string FormatReiveNotesFlag(string flag)
        {
            string result = string.Empty;
            switch (flag)
            {
                case "0":
                    result = "<span class=\"graytext\">δ��</span>";
                    break;
                case "1":
                    result = "<span class=\"bluetext\">�Ѷ�</span>";
                    break;
                case "2":
                    result = "<span class=\"greentext\">�ѻظ�</span>";
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
                result = "<span class=\"graytext\" style=\"cursor:help;\" title=\"���Ի�δͨ�����\" >�����</span>";
            }
            else
            {
                switch (flag)
                {
                    case "0":
                        result = "<span class=\"graytext\">�Է�δ��</span>";
                        break;
                    case "1":
                        result = "<span class=\"bluetext\">�Է��Ѷ�</span>";
                        break;
                    case "2":
                        result = "<span class=\"greentext\">�Է��ѻظ�</span>";
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
        /// ���Ļ��Ƿ�ɼ�
        /// </summary>
        /// <param name="fromid">������Id</param>
        /// <param name="toid">���Զ���Id</param>
        /// <param name="uid">��ǰ��Id</param>
        /// <param name="issceret">�Ƿ����Ļ�</param>
        /// <returns>�Ƿ���Ҫ��ʾ��������</returns>
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
        /// ���Ե���ʾ����
        /// </summary>
        /// <param name="fromid">������Id</param>
        /// <param name="toid">���Զ���Id</param>
        /// <param name="uid">��ǰ��Id</param>
        /// <param name="issceret">�Ƿ����Ļ�</param>
        /// <param name="content">��������</param>
        /// <param name="recontent">���Իظ�����</param>
        /// <returns>���յ���ʾ</returns>
        public static string GetFinalNotesToShow(string fromid, string toid, string uid, string issceret, string content, string recontent, string ischecked)
        {
            bool isneedshow = IsSceretNoteShow(fromid, toid, uid, issceret);
            string result = string.Empty;
            if (isneedshow)
            {
                if (!ischecked.Equals("1")) //δ��˵Ĳ���ʾ����
                {
                    result = "<span class=\"graytext\">����������δͨ�����</span>";
                }
                else //�Ѿ�ͨ����˵Ľ�һ���жϣ��Ƿ����Ļ�
                {
                    if (isneedshow) //������ʾ����
                    {
                        result = content + (recontent.Trim().Length > 0 ? "<br/><span class=\"bluetext\">�����ظ���" + recontent + "</span>" : "");
                    }
                    else //���Ļ��Ҳ���ʾ
                    {
                        result = "<span class=\"bluetext\">������Ϊ���Ļ��������������˿ɼ���</span>";
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// ��ȡ�û���������Ϣ
        /// </summary>
        /// <param name="uid">�û�Id</param>
        /// <param name="gid">����0��ʾ�������ĳ����Ʒ������</param>
        /// <param name="type">0����ʾ���յ��� 1��ʾ������</param>
        /// <param name="isfilter">�Ƿ���˵�����˵�����</param>
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
