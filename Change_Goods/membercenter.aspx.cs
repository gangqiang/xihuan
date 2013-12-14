using System;
using BusinessFacade;
public partial class membercenter : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "");
        }
        if (!IsPostBack)
        {
            string action = CommonMethod.FinalString(Request["action"]);
            if (action.Length > 0)
                ExecStartupScript(string.Format("$('#main').attr('src','{0}');", Server.UrlDecode(action)));
            int a = XiHuan_MessageFacade.GetNewMessageCount(CurrentUserId);
            if (a > 0)
                ExecScript("ymPrompt.alert({title:'喜换网温馨提示：',message:'<span class=\"highlight\">" + CurrentUserName + "</span>,欢迎光临喜换网<br/><a href=\"membercenter.aspx?action=membermessage.aspx?type=receive\"><img src=\"images/UnRead.gif\" style=\"border:0px;cursor:pointer;vertical-align:middle;\" />&nbsp;您有<span class=\"highlight\">" + a + "</span>条消息未读！',fixPosition:true,winPos:'rb',showMask:false})");
        }
    }

    protected void lnkQuit_Click(object sender, EventArgs e)
    {
        CommonMethod.AddLoginCookie(0, "", DateTime.Now.AddYears(-1));
        Response.Redirect("index.html");
    }
}
