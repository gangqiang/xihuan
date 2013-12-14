using System;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class Admin_sys_links : BaseAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load();
        if (!IsPostBack)
        {
            BindLinks();
        }
    }

    protected override void Page_PreInit()
    {
        base.Page_PreInit();
        PageControl1.PageChanged += new PageChangedDelegate(PageControl1_PageChanged);
    }

    void PageControl1_PageChanged(object sender, EventArgs e)
    {
        BindLinks();
    }

    private void BindLinks()
    {
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = Query.ProcessSql("select *from XiHuan_Links with(nolock) ", GlobalVar.DataBase_Name).DefaultView;
        PageControl1.DataSource = pds;
        rptLinks.DataSource = pds;
        rptLinks.DataBind();
    }
    protected void lnkDelLink_Click(object sender, EventArgs e)
    {
        XiHuan_LinksEntity link = new XiHuan_LinksEntity();
        link.Id = CommonMethod.ConvertToInt(hidLinkId.Value, 0);
        link.Retrieve();
        if (link.IsPersistent)
        {
            link.Delete();
            Alert("删除成功！");
            BindLinks();
        }

    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        BindLinks();
    }
}
