using System;
using BusinessEntity;
using BusinessFacade;
public partial class messagereply : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membermessage.aspx?type=" + Request["type"]);
        }
        else
        {
            if (!IsPostBack)
            {

                string type = CommonMethod.FinalString(Request["type"]);
                txtSender.Text = CurrentUserName;
                txtSender.Enabled = false;
                if (type.Equals("receive"))
                {
                    XiHuan_MessageEntity message = new XiHuan_MessageEntity();
                    message.Id = CommonMethod.ConvertToInt(Request["id"], 0);
                    message.Retrieve();
                    if (message.IsPersistent)
                    {
                        txtReceiver.Text = message.FromName;
                        txtReceiver.Enabled = false;
                        btnSend.Text = "立即回复短消息";
                    }
                }
                else if (type.Equals("send") || type.Equals("other"))
                {
                    btnSend.Text = "立即发送短消息";
                }

                if (type.Equals("other"))
                {
                    txtReceiver.Text = Microsoft.JScript.GlobalObject.unescape(Request["toname"]);
                    txtReceiver.Enabled = false;
                }
            }
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        #region 服务器端验证

        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membermessage.aspx?type=" + Request["type"]);
        }
        else
        {
            if (txtReceiver.Text.Trim().Length == 0)
            {
                Alert("请填写接收人！");
                Select(txtReceiver);
                return;
            }
            if (txtContent.Text.Trim().Length == 0)
            {
                Alert("请填写短消息内容！");
                Select(txtContent);
                return;
            }
            if (txtContent.Text.Trim().Length > 200)
            {
                Alert("短消息内容不能超过200字！");
                Select(txtContent);
                return;
            }
            if (txtReceiver.Enabled && !XiHuan_UserFacade.IsUserNameAlreayUse(txtReceiver.Text))
            {
                Alert("收件人不存在，请查证后再发！");
                Select(txtReceiver);
                return;
            }
        }
        #endregion

        #region 短消息发送操作

        XiHuan_MessageFacade.SendNewMessage(CurrentUserId, XiHuan_UserFacade.GetIdByName(txtReceiver.Text), CurrentUserName, txtReceiver.Text, txtContent.Text, null, false);
        if (Request["type"].Equals("receive"))
        {
            XiHuan_MessageEntity message = new XiHuan_MessageEntity();
            message.Id = CommonMethod.ConvertToInt(Request["id"], 0);
            message.Retrieve();
            if (message.IsPersistent)
            {
                message.Flag = (byte)XiHuan_MessageFacade.MessageState.已回复;
                message.Save();
            }
        }

        Alert("恭喜：短消息发送成功！");
        if (Request["type"].Equals("other"))
            ExecScript("parent.ymPrompt.close();");
        else
            ExecScript(string.Format("parent.location='membermessage.aspx?type={0}&s='+Math.random();", Request["type"]));
        #endregion
    }
}
