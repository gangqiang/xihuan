using System;
using BusinessEntity;
public partial class editlink : BaseAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load();
        if (!IsPostBack)
        {
            if (CommonMethod.FinalString(Request["id"]).Length > 0)
            {
                XiHuan_LinksEntity link = new XiHuan_LinksEntity();
                link.Id = CommonMethod.ConvertToInt(Request["id"], 0);
                link.Retrieve();
                if (link.IsPersistent)
                {
                    txtLinkName.Text = CommonMethod.FinalString(link.Name);
                    txtLinkUrl.Text = CommonMethod.FinalString(link.Url);
                    txtAlt.Text = CommonMethod.FinalString(link.Alt);
                    txtSort.Text = CommonMethod.FinalString(link.Sort);
                }
            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        XiHuan_LinksEntity link = null;
        if (CommonMethod.FinalString(Request["id"]).Length > 0)
        {
            link = XiHuan_LinksEntityAction.RetrieveAXiHuan_LinksEntity(CommonMethod.ConvertToInt(Request["id"], 0));
        }
        else
        {
            link = new XiHuan_LinksEntity();
        }

        link.Name = txtLinkName.Text.Trim();
        link.Url = txtLinkUrl.Text.Trim();
        link.Alt = txtAlt.Text.Trim();
        link.Sort = CommonMethod.ConvertToInt(txtSort.Text, 0);
        link.Save();
        Alert("保存成功！");
        ExecScript("parent.__doPostBack('lnkRefresh','');");
    }
}
