using System;
using BusinessFacade;
using BusinessEntity;
public partial class messagedetail : BaseWebPage
{
    private XiHuan_MessageEntity message = null;

    protected XiHuan_MessageEntity Message
    {
        get 
        {
            if (message == null)
            {
                message = XiHuan_MessageEntityAction.RetrieveAXiHuan_MessageEntity(Convert.ToInt32(Request["id"]));
            }
            return message;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membermessage.aspx?type=" + Request["type"]);
        }
        else
        {
            if (Message.FromId != CurrentUser.ID && Message.ToId != CurrentUserId)
            {
                Alert("您无权查看此短消息！");
                Response.End();
            }
            else
            {
                if (Message.Flag == (byte)XiHuan_MessageFacade.MessageState.未读 && Request["type"] == "receive")
                {
                    Message.Flag = (byte)XiHuan_MessageFacade.MessageState.已读;
                    Message.Save();
                }

            }
        }
    }
}
