using System;
using System.Data;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class shownews : BaseWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    #region 页面呈现
    private void InitPage()
    {
        int id = CommonMethod.ConvertToInt(Request["id"], 0);
        XiHuan_NewsEntity news = new XiHuan_NewsEntity();
        news.Id = id;
        news.Retrieve();
        if (news.IsPersistent)
        {
            lblType.Text = Enum.GetName(typeof(XiHuan_NewsFacade.NewsType), news.Type);
            lblTitle.Text = lblTitle2.Text = news.Title;
            lblTime.Text = news.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
            lblContent.Text = news.Content;
            Header.Title = news.Title + "-" + SystemConfigFacade.Instance().WebSiteTitle;
            hidId.Value = id.ToString();
        }

        string sql = string.Format(@"select top 1 Title,NewsUrl from XiHuan_News with(nolock) where Id<{0}  order by Id desc;
                                                    select top 1 Title,NewsUrl from XiHuan_News with(nolock) where Id>{0} ;
                                                    select Top 10 Title,NewsUrl from XiHuan_News with(nolock) order by ViewCount desc;  ", id);
        DataSet ds = Query.ProcessMultiSql(sql, GlobalVar.DataBase_Name);
        DataTable dtpre = ds.Tables[0];
        string url = string.Empty;
        if (dtpre != null && dtpre.Rows.Count > 0)
        {
            url = CommonMethod.FinalString(dtpre.Rows[0][1]);
            bool res = (url.ToLower().IndexOf("http") > -1);
            lblPre.Text = string.Format("<a title=\"{0}\" href=\"{1}\" target=\"{2}\">{0}</a>", dtpre.Rows[0][0], res ? url : SrcRootPath + url, res ? "_blank" : "_self");
        }
        else
        {
            lblPre.Text = "没有了";
        }
        DataTable dtnext = ds.Tables[1];
        if (dtnext != null && dtnext.Rows.Count > 0)
        {
            url = CommonMethod.FinalString(dtnext.Rows[0][1]);
            bool res = (url.ToLower().IndexOf("http") > -1);
            lblNext.Text = string.Format("<a title=\"{0}\" href=\"{1}\">{0}</a>", dtnext.Rows[0][0], res ? url : SrcRootPath + url, res ? "_blank" : "_self");
        }
        else
        {
            lblNext.Text = "没有了";
        }
        rptHotNews.DataSource = ds.Tables[2];
        rptHotNews.DataBind();
    }
    #endregion

}
