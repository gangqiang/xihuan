using System;
using BusinessFacade;
public partial class loginiframe : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click1(object sender, EventArgs e)
    {
        #region 服务器端验证

        if (txtUserName.Text.Trim().Length == 0)
        {
            Alert("请输入您的换客名！");
            Select(txtUserName);
            return;
        }

        if (txtPwd.Text.Trim().Length == 0)
        {
            Alert("请输入您的密码！");
            Select(txtPwd);
            return;
        }


        #endregion

        #region  系统登陆

        if (XiHuan_UserFacade.IsUserValid(txtUserName.Text, txtPwd.Text))
        {
            int uid = XiHuan_UserFacade.GetIdByName(txtUserName.Text);
            DateTime dt = DateTime.MinValue;
            if (chkAutoLogin.Checked)
            {
                dt = DateTime.Now.AddDays(14);
            }
            CommonMethod.AddLoginCookie(uid, txtUserName.Text, dt);
            Alert("您已成功登录，请在窗口关闭后继续刚才的操作 ^_^！");
            ExecScript("parent.ymPrompt.close();");
        }
        else
        {
            Alert("换客名或密码不正确，请重试！");
            return;
        }
        #endregion
    }
}
