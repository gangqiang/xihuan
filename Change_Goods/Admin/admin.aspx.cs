using System;

public partial class addschool : BaseAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.Page_Load();
        }
    }
    protected void lnkQuit_Click(object sender, EventArgs e)
    {
        CommonMethod.AddAdminCookie(DateTime.MinValue);
        Response.Redirect("../index.html");
    }
}
