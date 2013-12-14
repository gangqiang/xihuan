using System;
using BusinessFacade;
public partial class login : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lnkLogin_Click(object sender, EventArgs e)
    {
        #region 验证
        if (username.Value.Trim().Length == 0)
        {
            Alert("请输入您的换客名！");
            Select(username);
            return;
        }

        if (userpwd.Value.Trim().Length == 0)
        {
            Alert("请输入您的密码！");
            Select(userpwd);
            return;
        }
        #endregion

        #region  系统登陆

        if (XiHuan_UserFacade.IsUserValid(username.Value, userpwd.Value))
        {
            int uid = XiHuan_UserFacade.GetIdByName(username.Value);
            DateTime dt = DateTime.MinValue;
            if (autologin.Checked)
            {
                dt = DateTime.Now.AddDays(14);
            }
            CommonMethod.AddLoginCookie(uid, username.Value, dt);
            string path = CommonMethod.FinalString(Request["path"]);
            if (path.Length > 0)
            {
                Response.Redirect(Server.UrlDecode(path));
            }
            else
            {
                Response.Redirect("membercenter.aspx");
            }
        }
        else
        {
            Alert("换客名或密码不正确，请重试！");
            return;
        }
        #endregion
    }
}
