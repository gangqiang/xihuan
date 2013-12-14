using System;
using System.Data;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class exchange : BaseWebPage
{
    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUserGoods();
            if (CommonMethod.ConvertToInt(Request["ownerid"], 0) == CurrentUserId)
            {
                Alert("您总不能和自己交换物品吧？^_^ ！");
                ExecScript("parent.ymPrompt.close();");
            }
        }
    }

    private void BindUserGoods()
    {
        XiHuan_UserGoodsSearchFilter f = new XiHuan_UserGoodsSearchFilter();
        f.SelectFileds = " Id,DefaultPhoto,Name,GoodState,CreateDate,DetailUrl ";
        f.OwnerId = CurrentUserId;
        f.GoodsStates = XiHuan_UserGoodsFacade.GoodsState.新登记.ToString("d") + "," + XiHuan_UserGoodsFacade.GoodsState.考虑中.ToString("d");
        f.IsChecked = 1;
        DataTable dt = XiHuan_UserGoodsFacade.SearchGoods(f);
        if (dt != null && dt.Rows.Count > 0)
        {
            rptGoodsList.DataSource = dt;
            rptGoodsList.DataBind();
        }
        else
        {
            rbtMethodGoods.Enabled = false;
            rbtMethodMoney.Checked = true;
            rbtMethodGoods.ToolTip = "您目前还没有登记过换品，您可以去登记换品或选择用Money交换 ^_^！";
            ExecStartupScript("reconfirm();");
        }

    }
    #endregion

    #region 发送交换请求
    protected void lnkSend_Click(object sender, EventArgs e)
    {
        if (IsUserAlreadyLogin)
        {
            #region 交换请求

            Transaction t = new Transaction();
            string detailurl = Microsoft.JScript.GlobalObject.unescape(CommonMethod.FinalString(Request["detailurl"]));
            XiHuan_UserGoodsChangeRequireEntity NewChangeRequire = new XiHuan_UserGoodsChangeRequireEntity();
            NewChangeRequire.GoodsId = CommonMethod.ConvertToInt(Request["goodsid"], 0);
            NewChangeRequire.GoodsName = Microsoft.JScript.GlobalObject.unescape(CommonMethod.FinalString(Request["goodsname"]));
            NewChangeRequire.OwnerId = CommonMethod.ConvertToInt(Request["ownerid"], 0);
            NewChangeRequire.OwnerName = Microsoft.JScript.GlobalObject.unescape(CommonMethod.FinalString(Request["ownername"]));
            NewChangeRequire.SenderId = CurrentUserId;
            NewChangeRequire.SenderName = CurrentUserName;
            if (rbtMethodMoney.Checked)
            {
                NewChangeRequire.RequireType = (byte)XiHuan_ChangeRequireFacade.ChangeRequireType.Money交换;
                NewChangeRequire.RequireDescribe = txtMoney.Text.Trim() + "Money ";
            }
            if (rbtMethodGoods.Checked)
            {
                string goodsid = hidGoodsId.Value.Trim().TrimEnd(';');
                if (goodsid.Length > 0)
                {
                    NewChangeRequire.RequireType = (byte)XiHuan_ChangeRequireFacade.ChangeRequireType.换品交换;
                    string[] ids = goodsid.Split(';');
                    for (int i = 0; i < ids.Length; i++)
                    {
                        string[] single = ids[i].ToString().Split(',');
                        NewChangeRequire.SelectToChangeGoodsId += single[0] + ",";
                        NewChangeRequire.SelectToChangeGoodsName += single[1] + ",";
                        NewChangeRequire.RequireDescribe += string.Format("<a href=\"{0}\" title=\"查看换品信息\" target=\"_blank\" >{1}</a>", SrcRootPath + single[2], single[1]) + "&nbsp;，";
                    }

                }
            }

            NewChangeRequire.Flag = (byte)XiHuan_ChangeRequireFacade.ChangeRequireState.新发起;
            NewChangeRequire.RequireDate = DateTime.Now;
            if (chkSecret.Checked)
                NewChangeRequire.IsSecret = 1;
            t.AddSaveObject(NewChangeRequire);

            #endregion

            #region 给请求接收者发短消息

            XiHuan_MessageEntity ExchangeNoticeMesage = new XiHuan_MessageEntity();
            ExchangeNoticeMesage.ToId = NewChangeRequire.OwnerId;
            ExchangeNoticeMesage.ToName = NewChangeRequire.OwnerName;
            ExchangeNoticeMesage.FromId = 0;
            ExchangeNoticeMesage.FromName = CurrentUserName;
            string strLinkInfo = CommonMethod.FinalString(CurrentUser.TelePhone).Length > 0 ? "电话：" + CurrentUser.TelePhone.Trim() + "，" : "";
            strLinkInfo += CommonMethod.FinalString(CurrentUser.QQ).Length > 0 ? "QQ：" + CurrentUser.QQ.Trim() + "，" : "";
            strLinkInfo += CommonMethod.FinalString(CurrentUser.Msn).Length > 0 ? "MSN：" + CurrentUser.Msn.Trim() + "，" : "";
            strLinkInfo += CommonMethod.FinalString(CurrentUser.Email).Length > 0 ? "Email：" + CurrentUser.Email.Trim() + "，" : "";
            ExchangeNoticeMesage.Content = string.Format("尊敬的<strong>{0}</strong>：<br/><a href=\"{1}\" target=\"_blank\">{2}</a>想用{3}换你的<a href=\"{4}\" target=\"_blank\">{5}</a>！请注意查看，{6}祝你好运!",
                                                         NewChangeRequire.OwnerName, SrcRootPath + "xh.aspx?id=" + NewChangeRequire.SenderId,
                                                         NewChangeRequire.SenderName, NewChangeRequire.RequireDescribe, SrcRootPath + detailurl,
                                                         NewChangeRequire.GoodsName, strLinkInfo.Length > 0 ? "你可以通过" + strLinkInfo + "与" + (CurrentUser.Gender == 1 ? "他" : "她") + "联系，" : "");
            ExchangeNoticeMesage.CreateDate = DateTime.Now;
            ExchangeNoticeMesage.Flag = (byte)XiHuan_MessageFacade.MessageState.未读;
            t.AddSaveObject(ExchangeNoticeMesage);

            #endregion

            try
            {
                t.Process(); Alert("您的交换请求已经成功发送给换主,祝您好运 ^_^！您还可以通过QQ，站内信留言联系换主进行交换！");
                CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + CommonMethod.FinalString(Request["goodsid"]), detailurl);
                ExecScript(string.Format("parent.location=\"{0}\";", SrcRootPath + detailurl + "?s=" + new Random().Next(int.MaxValue)));
            }
            catch
            {
                t.RollBack();
                Alert("抱歉，发送交换请求出错，请稍候重试！");
            }
        }
    }
    #endregion
}
