using System;
using BusinessFacade;
public partial class GenericErrorPage : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SendMailFacade.sendEmail(CommonMethodFacade.GetConfigValue("NoticeEmail"), "系统报错,页面：" + CommonMethod.FinalString(Request["aspxerrorpath"]), "系统报错,页面：" + CommonMethod.FinalString(Request["aspxerrorpath"]));
        }
    }
}

