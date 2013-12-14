using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class membermessage : BaseWebPage
{

    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = CommonMethod.FinalString(Request["type"]);

        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "membermessage.aspx?type=" + type);
        }

        if (!IsPostBack)
        {
            if (type == "receive")
                BindReceive();
            else
                BindSend();
            ExecStartupScript("function handler2(tp){if(tp=='close'){parent.location='membercenter.aspx?action=" + Server.UrlEncode("membermessage.aspx?type=" + type) + "'}}");
        }

    }

    #endregion

    #region  短消息数据绑定
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
        DataTable dt = XiHuan_MessageFacade.GetUserMessage(CurrentUserId, 0);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = dt.DefaultView;
        PageControl1.DataSource = pds;
        rptReceive.DataSource = pds;
        rptReceive.DataBind();
        ExecStartupScript("$('#receive').show();");
        ExecStartupScript("$('#send').hide();");
        lblType.Text = "收到";
    }
    private void BindSend()
    {
        DataTable dt = XiHuan_MessageFacade.GetUserMessage(CurrentUserId, 1);
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
    #endregion

    #region 删除短消息
    protected void lnkDelMessage_Click(object sender, EventArgs e)
    {
        DeleteCriteria dcmessage = new DeleteCriteria(typeof(XiHuan_MessageEntity));
        Condition cmessage = dcmessage.GetNewCondition();
        cmessage.AddEqualTo(XiHuan_MessageEntity.__ID, hidId.Value);
        if (dcmessage.Perform() > 0)
        {
            Alert("恭喜：短消息已成功删除！");
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
            DeleteCriteria dcnotes = new DeleteCriteria(typeof(XiHuan_MessageEntity));
            Condition cnotes = dcnotes.GetNewCondition();
            cnotes.AddIn(XiHuan_MessageEntity.__ID, mid);
            if (dcnotes.Perform() > 0)
            {
                Alert("恭喜：选中的短消息已成功删除！");
                if (CommonMethod.FinalString(Request["type"]) == "receive")
                    BindReceive();
                else
                    BindSend();
            }
        }
    }
    #endregion


}
