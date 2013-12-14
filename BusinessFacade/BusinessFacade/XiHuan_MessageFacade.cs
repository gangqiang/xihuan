using System;
using BusinessEntity;
using PersistenceLayer;
using System.Data;
namespace BusinessFacade
{
    public class XiHuan_MessageFacade
    {

        public enum MessageState
        {
            未读 = 0,
            已读 = 1,
            已回复 = 2,

        }

        public static string FormatReiveMessageFlag(string flag)
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

        public static string FormatSendMessageFlag(string flag)
        {
            string result = string.Empty;
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

            return result;
        }

        public static int GetNewMessageCount(int uid)
        {
            Query message = new Query(typeof(XiHuan_MessageEntity));
            Condition c = message.GetQueryCondition();
            c.AddEqualTo(XiHuan_MessageEntity.__TOID, uid);
            c.AddEqualTo(XiHuan_MessageEntity.__FLAG, MessageState.未读.ToString("d"));
            message.SelectCount(XiHuan_MessageEntity.__ID, "messagecount");
            return CommonMethodFacade.ConvertToInt(message.ExecuteScalar(), 0);
        }

        public static void SendNewMessage(int fromid, int toid, string fromname, string toname, string content, Transaction t, bool isdo)
        {
            XiHuan_MessageEntity newmessage = new XiHuan_MessageEntity();
            newmessage.FromId = fromid;
            newmessage.ToId = toid;
            newmessage.FromName = CommonMethodFacade.FinalString(fromname);
            newmessage.ToName = CommonMethodFacade.FinalString(toname);
            newmessage.Content = CommonMethodFacade.FinalString(content);
            newmessage.Flag = (byte)MessageState.未读;
            newmessage.CreateDate = DateTime.Now;
            if (t != null)
            {
                if (isdo)
                    t.DoSaveObject(newmessage);
                else
                    t.AddSaveObject(newmessage);
            }
            else
            {
                newmessage.Save();
            }

        }

        public static DataTable GetUserMessage(int uid, int type)
        {
            RetrieveCriteria rcmessage = new RetrieveCriteria(typeof(XiHuan_MessageEntity));
            Condition c = rcmessage.GetNewCondition();
            rcmessage.OrderBy(XiHuan_MessageEntity.__CREATEDATE, false);
            if (type == 0)
                c.AddEqualTo(XiHuan_MessageEntity.__TOID, uid);
            else
                c.AddEqualTo(XiHuan_MessageEntity.__FROMID, uid);
            return rcmessage.AsDataTable();
        }
    }
}
