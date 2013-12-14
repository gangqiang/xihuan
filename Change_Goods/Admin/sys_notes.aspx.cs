using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class Admin_sys_notes : BaseAdminPage
{
    #region 初始化页面
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load();
        if (!IsPostBack)
        {
            BindNotes(0);
            string action = CommonMethod.FinalString(Request.QueryString["action"]);
            if (action.Length > 0)
            {
                switch (action)
                {
                    case "del": DelNotes(Request["id"]); break;
                    case "check": CheckNotes(Request["id"], Request["gid"]); break;
                    default: break;
                }
            }
        }
    }
    #endregion

    #region 绑定留言

    protected override void Page_PreInit()
    {
        base.Page_PreInit();
        PageControl1.PageChanged += new PageChangedDelegate(PageControl1_PageChanged);
    }

    void PageControl1_PageChanged(object sender, EventArgs e)
    {
        BindNotes(CommonMethod.ConvertToInt(hidValue.Value, 0));
    }

    private void BindNotes(int ischecked)
    {
        string strSQL = @"select Id,FromId,FromName,ToId,ToName,Content,ReplyContent,GoodsId,
                        CreateDate,Flag,IsChecked from XiHuan_GuestBook with(nolock) ";
        if (ischecked == 0)
            strSQL += " where IsChecked=" + ischecked;
        strSQL += " order by CreateDate desc ";
        DataTable dt = Query.ProcessSql(strSQL, GlobalVar.DataBase_Name);
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
        pds.PageSize = 10;
        pds.DataSource = dt.DefaultView;
        PageControl1.DataSource = pds;
        rpNotesList.DataSource = pds;
        rpNotesList.DataBind();
        hidValue.Value = ischecked.ToString();
    }

    protected void btnAll_Click(object sender, EventArgs e)
    {
        BindNotes(1);
        btnAll.Visible = false;
        btnChecked.Visible = true;
    }

    protected void btnChecked_Click(object sender, EventArgs e)
    {
        BindNotes(0);
        btnAll.Visible = true;
        btnChecked.Visible = false;
    }
    #endregion

    #region 删除留言
    private void DelNotes(string id)
    {
        DeleteCriteria dcnote = new DeleteCriteria(typeof(XiHuan_GuestBookEntity));
        Condition cnote = dcnote.GetNewCondition();
        cnote.AddEqualTo(XiHuan_GuestBookEntity.__ID, id);
        if (dcnote.Perform() > 0)
        {
            Alert("恭喜：留言已成功删除！");
            BindNotes(0);
        }
    }
    #endregion

    #region 审核留言
    private void CheckNotes(string id, string gid)
    {
        #region 留言通过审核,给留言人发送提醒短消息，提醒留言已通过审核

        Transaction t = new Transaction();
        XiHuan_GuestBookEntity notesinfo = new XiHuan_GuestBookEntity();
        notesinfo.Id = CommonMethod.ConvertToInt(id, 0);
        notesinfo.Retrieve();
        if (notesinfo.IsPersistent)
        {
            notesinfo.IsChecked = 1;
            t.AddSaveObject(notesinfo);
            XiHuan_MessageEntity notechecknoticemessage = new XiHuan_MessageEntity();
            notechecknoticemessage.FromId = 1;
            notechecknoticemessage.FromName = "喜换网";
            notechecknoticemessage.ToId = notesinfo.FromId;
            notechecknoticemessage.ToName = notesinfo.FromName;
            notechecknoticemessage.Content = string.Format("尊敬的喜换网会员<strong>{0}</strong>,您好：<br/>您给\"{1}\"的留言\"{2}\",已经通过审核，请注意查看!",
                notesinfo.FromName, notesinfo.ToName, notesinfo.Content);
            notechecknoticemessage.Flag = byte.Parse(XiHuan_MessageFacade.MessageState.未读.ToString("d"));
            notechecknoticemessage.CreateDate = DateTime.Now;
            t.AddSaveObject(notechecknoticemessage);

        }

        #endregion

        if (t.Process())
        {
            #region   如果是对换品页面的留言，重新生成换品页
            if (CommonMethod.ConvertToInt(gid, 0) > 0)
            {
                RetrieveCriteria rcgoods = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
                rcgoods.AddSelect(XiHuan_UserGoodsEntity.__DETAILURL);
                Condition cgoods = rcgoods.GetNewCondition();
                cgoods.AddEqualTo(XiHuan_UserGoodsEntity.__ID, gid);
                cgoods.AddEqualTo(XiHuan_UserGoodsEntity.__ISCHECKED, 1);
                XiHuan_UserGoodsEntity goods = rcgoods.AsEntity() as XiHuan_UserGoodsEntity;
                if (goods != null)
                    CommonMethod.readAspxAndWriteHtmlSoruce("../showdetail.aspx?id=" + gid, "../" + goods.DetailUrl);
            }
            #endregion

            Alert("恭喜：留言审核成功！");
            BindNotes(0);
        }
        else
        {
            t.RollBack();
        }
    }
    #endregion
}
