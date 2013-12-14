using System;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class editnews : BaseAdminPage
{
    #region 初始化

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load();
        if (!IsPostBack)
        {
            CommonMethod.ListContolDataBindFromEnum(ddlNewsType, typeof(XiHuan_NewsFacade.NewsType), "公告", "0", true);
            if (IsEdit)
            {
                XiHuan_NewsEntity news = XiHuan_NewsEntityAction.RetrieveAXiHuan_NewsEntity(CommonMethod.ConvertToInt(Request["id"], 0));
                txtLinkName.Text = news.Title;
                CommonMethod.SelectFlg(ddlNewsType, news.Type.ToString());
                txtLinkUrl.Text = news.NewsUrl;
                NewContent.Value = news.Content;
                txtSort.Text = news.SortNumber.ToString();
            }
        }
    }

    #endregion

    #region 属性
    public bool IsEdit
    {
        get
        {
            return CommonMethod.ConvertToInt(Request["id"], 0) > 0;
        }
    }
    #endregion

    #region 保存

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Transaction t = new Transaction();
        XiHuan_NewsEntity news = null;
        if (IsEdit)
        {
            news = XiHuan_NewsEntityAction.RetrieveAXiHuan_NewsEntity(CommonMethod.ConvertToInt(Request["id"], 0));
        }
        else
        {
            news = new XiHuan_NewsEntity();
        }
        news.Title = txtLinkName.Text.Trim();
        news.Content = rbtContent.Checked ? NewContent.Value.Trim() : txtLinkUrl.Text.Trim();
        news.Type = byte.Parse(ddlNewsType.SelectedValue.Trim());
        if (!IsEdit)
            news.CreateDate = DateTime.Now;
        news.SortNumber = CommonMethod.ConvertToInt(txtSort.Text.Trim(), 0);
        if (rbtLink.Checked)
            news.NewsUrl = txtLinkUrl.Text.Trim();
        t.DoSaveObject(news);
        string newsurl = string.Empty;
        if (!rbtLink.Checked)
        {
            newsurl = "news/" + news.CreateDate.Year.ToString() + "/" + news.CreateDate.Month.ToString() + "/" + news.CreateDate.Day.ToString() + "/newsshow" + news.Id.ToString() + ".html";
            t.DoSqlNonQueryString(string.Format("update XiHuan_News set NewsUrl='{0}' where Id=" + news.Id, newsurl), GlobalVar.DataBase_Name);
            CommonMethod.readAspxAndWriteHtmlSoruce("../shownews.aspx?id=" + news.Id, "../" + newsurl);
        }
        try
        {
            t.Commit();
            Alert("恭喜： 保存成功！");
            ExecScript("parent.__doPostBack('lnkRefresh','');");
        }
        catch
        {
            t.RollBack();
            Alert("抱歉：保存出现错误！");
        }

    }

    #endregion
}
