using System;
public partial class adminlogin : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Url.Host.Equals("localhost"))
            {
                txtUserName.Text = "lybwgq";
                txtPwd.Attributes["value"] = "lybwgq8486";
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim().Length == 0)
        {
            Alert("请输入用户名！");
            return;
        }
        if (txtPwd.Text.Trim().Length == 0)
        {
            Alert("请输入密码！");
            return;
        }

        string username = txtUserName.Text.Trim();
        string pwd = txtPwd.Text.Trim();
        if ((username == "lybwgq" && pwd == "lybwgq8486") || (username == "tsc8" && pwd == "tsc8hntxglj"))
        {
            CommonMethod.AddAdminCookie(DateTime.Now.AddDays(1));
            Response.Redirect("admin.aspx");
        }
        else
        {
            Alert("用户名密码不正确！");
            return;
        }
    }
}
