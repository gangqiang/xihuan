using System;
using System.Data;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class userequst : BaseWebPage
{
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = CommonMethod.FinalString(Request["type"]);
        if (!IsUserAlreadyLogin)
        {
            MemberCenterPageRedirect("", "userequest.aspx?type=" + type);
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
        }
    }

    #endregion

    #region  请求数据绑定
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
        XiHuan_ChangeRequireSearchFilter f = new XiHuan_ChangeRequireSearchFilter();
        f.OwnerId = CurrentUserId;
        DataTable dt = XiHuan_ChangeRequireFacade.GetUserRequire(f);
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
        XiHuan_ChangeRequireSearchFilter f = new XiHuan_ChangeRequireSearchFilter();
        f.SenderId = CurrentUserId;
        DataTable dt = XiHuan_ChangeRequireFacade.GetUserRequire(f);
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

    #region 请求处理
    protected void lnkDelMessage_Click(object sender, EventArgs e)
    {
        int id = CommonMethod.ConvertToInt(hidId.Value, 0);
        if (id > 0)
        {
            XiHuan_UserGoodsChangeRequireEntity note = new XiHuan_UserGoodsChangeRequireEntity();
            note.Id = id;
            note.Retrieve();
            if (note.IsPersistent)
            {
                if (CommonMethod.FinalString(Request["type"]) == "receive")
                {
                    if (note.Flag != (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.已取消)
                    {
                        if (hidType.Value.Trim() != "Ref")
                        {
                            string ids = (CommonMethod.FinalString(note.SelectToChangeGoodsId).Length > 0 ? note.SelectToChangeGoodsId + note.GoodsId.ToString() : note.GoodsId.ToString());
                            Query.ProcessSqlNonQuery(string.Format("update XiHuan_UserGoods set GoodState=" + XiHuan_UserGoodsFacade.GoodsState.考虑中.ToString("d") + " where Id in ({0}) ",
                                  ids), GlobalVar.DataBase_Name);
                            note.Flag = (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.考虑中;
                        }
                        else
                        {
                            note.Flag = (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.换主拒绝;
                        }
                    }
                    note.Save();
                    XiHuan_UserGoodsEntity goods = new XiHuan_UserGoodsEntity();
                    goods.Id = note.GoodsId;
                    goods.Retrieve();
                    if (goods.IsPersistent)
                    {
                        CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + note.GoodsId, goods.DetailUrl);
                    }
                    Alert("恭喜：操作成功！");
                    BindReceive();
                }
                else
                {
                    if (note.Flag != (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.新发起)
                    {
                        if (CommonMethod.FinalString(note.SelectToChangeGoodsId).Length > 0)
                        {
                            Query.ProcessSqlNonQuery(string.Format("update XiHuan_UserGoods set GoodState=" + XiHuan_UserGoodsFacade.GoodsState.新登记.ToString("d") + " where Id in ({0}) ",
                            note.SelectToChangeGoodsId.TrimEnd(',')), GlobalVar.DataBase_Name);
                        }
                    }
                    if (hidType.Value.Trim() != "Recover")
                        note.Flag = (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.已取消;
                    else
                        note.Flag = (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.新发起;
                    note.Save();
                    XiHuan_UserGoodsEntity goods = new XiHuan_UserGoodsEntity();
                    goods.Id = note.GoodsId;
                    goods.Retrieve();
                    if (goods.IsPersistent)
                    {
                        CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + note.GoodsId, goods.DetailUrl);
                    }
                    Alert("恭喜：操作成功！");
                    BindSend();
                }
            }
        }
    }

    protected void lnkDelMultiMessage_Click(object sender, EventArgs e)
    {
        string[] mid = hidId.Value.Trim().TrimEnd(',').Split(',');
        if (mid.Length > 0)
        {
            for (int i = 0; i < mid.Length; i++)
            {
                int id = CommonMethod.ConvertToInt(mid[i], 0);
                if (id > 0)
                {
                    XiHuan_UserGoodsChangeRequireEntity note = new XiHuan_UserGoodsChangeRequireEntity();
                    note.Id = id;
                    note.Retrieve();
                    if (note.IsPersistent && note.Flag == (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.新发起)
                    {
                        if (hidType.Value.Trim().Equals("Send"))
                            note.Flag = (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.已取消;
                        else
                            note.Flag = (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.考虑中;
                        note.Save();
                        XiHuan_UserGoodsEntity goods = new XiHuan_UserGoodsEntity();
                        goods.Id = note.GoodsId;
                        goods.Retrieve();
                        if (goods.IsPersistent)
                        {
                            CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + note.GoodsId, goods.DetailUrl);
                        }
                    }
                }
            }

            Alert("恭喜：操作成功！");
            if (CommonMethod.FinalString(Request["type"]) == "receive")
                BindReceive();
            else
                BindSend();
        }
    }
    #endregion
}
