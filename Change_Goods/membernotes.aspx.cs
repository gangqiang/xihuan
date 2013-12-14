using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class membernotes : BaseWebPage
{
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = CommonMethod.FinalString(Request["type"]);
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membernotes.aspx?type=" + type);
        }
        if (!IsPostBack)
        {
            if (type == "receive")
            {
                BindReceive();
            }
            else
            {
                BindSend();
            }

            ExecStartupScript("function handler2(tp){if(tp=='close'){parent.location='membercenter.aspx?action=" + Server.UrlEncode("membernotes.aspx?type=" + type) + "'}}");
        }
    }

    #endregion

    #region  留言数据绑定
    protected override void Page_PreInit()
    {
        base.Page_PreInit();
        PageControl1.PageChanged += new PageChangedDelegate(PageControl1_PageChanged);
        PageControl2.PageChanged += new PageChangedDelegate(PageControl2_PageChanged);
    }

    void PageControl1_PageChanged(object sender, EventArgs e)
    {
        BindReceive();
    }

    void PageControl2_PageChanged(object sender, EventArgs e)
    {
        BindSend();
    }

    private void BindReceive()
    {
        DataTable dt = XiHuan_UserNotesFacade.GetUserNotes(CurrentUserId, 0, 0, true);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = dt.DefaultView;
        PageControl1.DataSource = pds;
        rptGoodsList.DataSource = pds;
        rptGoodsList.DataBind();
        ExecStartupScript("$('#receive').show();");
        ExecStartupScript("$('#send').hide();");
        lblType.Text = "收到";
    }
    private void BindSend()
    {
        DataTable dt = XiHuan_UserNotesFacade.GetUserNotes(CurrentUserId, 0, 1, false);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = dt.DefaultView;
        PageControl2.DataSource = pds;
        rptSend.DataSource = pds;
        rptSend.DataBind();
        ExecStartupScript("$('#send').show();");
        ExecStartupScript("$('#receive').hide();");
        lblType.Text = "发出";
    }

    protected string showReceiveNoteContent(string ownername, string goodsname, string goodsid, string notecontent)
    {
        return (ownername + (!goodsid.Equals("0") ? "看了您的<span class=\"inittext\">" + goodsname + "</span>" : "给您")) + "留言说：" + CommonMethod.GetSubString(notecontent, 20, "..");
    }
    protected string showSendNoteContent(string toname, string goodsname, string goodsid, string notecontent)
    {
        return ((!goodsid.Equals("0") ? "您看了" + toname + "的<span class=\"inittext\">" + goodsname + "</span>" : "您给" + toname)) + "留言说：" + CommonMethod.GetSubString(notecontent, 20, "..");
    }
    #endregion

    #region 删除留言
    protected void lnkDelMessage_Click(object sender, EventArgs e)
    {
        DeleteCriteria dcnote = new DeleteCriteria(typeof(XiHuan_GuestBookEntity));
        Condition cnote = dcnote.GetNewCondition();
        cnote.AddEqualTo(XiHuan_GuestBookEntity.__ID, hidId.Value);
        if (dcnote.Perform() > 0)
        {
            Alert("恭喜：留言已成功删除！");
            if (CommonMethod.FinalString(Request["type"]) == "receive")
                BindReceive();
            else
                BindSend();
        }

    }

    protected void lnkDelMultiMessage_Click(object sender, EventArgs e)
    {
        string[] mid = hidId.Value.Trim().TrimEnd(',').Split(',');
        if (mid.Length > 0)
        {
            DeleteCriteria dcnote = new DeleteCriteria(typeof(XiHuan_GuestBookEntity));
            Condition cnote = dcnote.GetNewCondition();
            cnote.AddIn(XiHuan_GuestBookEntity.__ID, mid);
            if (dcnote.Perform() > 0)
            {
                Alert("恭喜：选中的留言已成功删除！");
                if (CommonMethod.FinalString(Request["type"]) == "receive")
                    BindReceive();
                else
                    BindSend();
            }
        }
    }
    #endregion
}
