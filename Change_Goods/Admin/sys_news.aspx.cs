using System;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class Admin_sys_news : BaseAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load();
        if (!IsPostBack)
        {
            BindNews();
        }
    }

    protected override void Page_PreInit()
    {
        base.Page_PreInit();

        PageControl2.PageChanged += new PageChangedDelegate(PageControl2_PageChanged);
    }

    void PageControl2_PageChanged(object sender, EventArgs e)
    {
        BindNews();
    }

    private void BindNews()
    {
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = Query.ProcessSql("select *from XiHuan_News with(nolock) order by Type asc ", GlobalVar.DataBase_Name).DefaultView;
        PageControl2.DataSource = pds;
        rptNews.DataSource = pds;
        rptNews.DataBind();
    }

    protected void lnkDelNews_Click(object sender, EventArgs e)
    {
        XiHuan_NewsEntity link = new XiHuan_NewsEntity();
        link.Id = CommonMethod.ConvertToInt(hidNewsId.Value, 0);
        link.Retrieve();
        if (link.IsPersistent)
        {
            try
            {
                if (System.IO.File.Exists(Server.MapPath(link.NewsUrl)))
                    System.IO.File.Delete(Server.MapPath(link.NewsUrl));
            }
            catch
            {

            }
            link.Delete();
            Alert("删除成功！");
            BindNews();
        }

    }

    protected void lnkRefresh_Click(object sender, EventArgs e)
    {
        BindNews();
    }
}
