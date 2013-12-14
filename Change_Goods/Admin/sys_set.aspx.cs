using System;
using System.Data;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class Admin_sys_set : BaseAdminPage
{
    #region 初始化

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            base.Page_Load();
            txtFFZF.Text = SystemConfigFacade.Instance().FFZF;
            rbtIsGoodsAddNeedCheck.SelectedValue = SystemConfigFacade.Instance().IsGoodsAddNeedCheck ? "1" : "0";
            txtStartDate.Text = DateTime.Now.AddMonths(-3).ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
        }
    }

    #endregion

    #region 刷新省市信息
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ProvinceCityFacade.Refresh();
        Alert("刷新成功！");
    }
    #endregion

    #region 生成首页
    protected void btnGenerateIndex_Click(object sender, EventArgs e)
    {
        CommonMethod.readAspxAndWriteHtmlSoruce("../default.aspx", "../index.html");
        Alert("首页生成成功！");
    }
    #endregion

    #region 重新生成换品页面
    protected void btnGenerateGoodsDetail_Click(object sender, EventArgs e)
    {
        DataTable dt = Query.ProcessSql(string.Format(@"select Id,Name,CreateDate from XiHuan_UserGoods with(nolock) Where IsChecked=1 
                                                                          AND CreateDate>='{0}' AND CreateDate<='{1}' ", txtStartDate.Text.Trim(), txtEndDate.Text.Trim()), GlobalVar.DataBase_Name);
        foreach (DataRow dr in dt.Rows)
        {
            DateTime dt2 = CommonMethod.ConvertToDateTime(dr["CreateDate"], DateTime.MinValue);
            string path = "goods/" + dt2.Year + "/" + dt2.Month + "/" + dt2.Day + "/goods" + dr["Id"] + ".html";
            CommonMethod.readAspxAndWriteHtmlSoruce("../showdetail.aspx?id=" + dr["Id"].ToString(), "../" + path);
            Query.ProcessSqlNonQuery("update XiHuan_UserGoods set DetailUrl='" + path + "' where Id=" + dr["Id"], GlobalVar.DataBase_Name);
        }
        txtStartDate.Text = Convert.ToDateTime(txtStartDate.Text).AddMonths(-3).ToShortDateString();
        txtEndDate.Text = Convert.ToDateTime(txtEndDate.Text).AddMonths(-3).ToShortDateString();
        Alert("已经重新生成！");
    }
    #endregion

    #region 刷新换品类别信息
    protected void btnRefreshType_Click(object sender, EventArgs e)
    {
        XiHuan_GoodsTypeFacade.Refresh();
        Alert("刷新成功！");
    }
    #endregion

    #region 刷新系统配置
    protected void btnSystemConfig_Click(object sender, EventArgs e)
    {
        SystemConfigFacade.Refresh();
        Alert("刷新成功！");
    }
    #endregion

    #region 重新生成学校信息
    protected void btnGenerateSchool_Click(object sender, EventArgs e)
    {
        CommonMethod.readAspxAndWriteHtmlSoruce("../schoolchange.aspx", "../schoolchange.html");
        Alert("生成成功！");
    }
    #endregion

    #region 重新生成省市信息
    protected void btnGenerateCity_Click(object sender, EventArgs e)
    {
        CommonMethod.readAspxAndWriteHtmlSoruce("../citychange.aspx", "../citychange.html");
        Alert("生成成功！");
    }
    #endregion

    #region 重新生成新闻页面
    protected void btnGenerateNews_Click(object sender, EventArgs e)
    {
        DataTable dt = Query.ProcessSql("select Id,NewsUrl,CreateDate from XiHuan_News with(nolock) ", GlobalVar.DataBase_Name);
        foreach (DataRow dr in dt.Rows)
        {
            if (CommonMethod.FinalString(dr["NewsUrl"]).ToLower().IndexOf("http") == -1)
            {
                DateTime dt2 = CommonMethod.ConvertToDateTime(dr["CreateDate"], DateTime.MinValue);
                string path = "news/" + dt2.Year + "/" + dt2.Month + "/" + dt2.Day + "/newsshow" + dr["Id"] + ".html";
                CommonMethod.readAspxAndWriteHtmlSoruce("../shownews.aspx?id=" + dr["Id"].ToString(), "../" + path);
                Query.ProcessSqlNonQuery("update XiHuan_News set NewsUrl='" + path + "' where Id=" + dr["Id"], GlobalVar.DataBase_Name);
            }
        }

        Alert("已经重新生成！");
    }
    #endregion

    #region 非法字符设置
    protected void btnSaveFFZF_Click(object sender, EventArgs e)
    {
        Query.ProcessSql("update XiHuan_SystemConfig set ConfigValue='" + txtFFZF.Text.Trim() + "' where ConfigKey='FFZF' ", GlobalVar.DataBase_Name);
        SystemConfigFacade.Refresh();
        Alert("设置成功！");
    }
    #endregion

    #region 设置发布换品是否需要审核
    protected void btnSetGoodsAdd_Click(object sender, EventArgs e)
    {
        Query.ProcessSql("update XiHuan_SystemConfig set ConfigValue='" + rbtIsGoodsAddNeedCheck.SelectedValue + "' where ConfigKey='IsGoodsAddNeedCheck' ", GlobalVar.DataBase_Name);
        SystemConfigFacade.Refresh();
        Alert("设置成功！");
    }

    #endregion

    #region 幻灯片更新
    protected void btnUpDatePic_Click(object sender, EventArgs e)
    {
        RetrieveCriteria rcGoodsPics = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__DEFAULTPHOTO);
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__NAME);
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__DETAILURL);
        Condition c = rcGoodsPics.GetNewCondition();
        c.AddEqualTo(XiHuan_UserGoodsEntity.__ISCHECKED, 1);
        c.AddNotEqualTo(XiHuan_UserGoodsEntity.__DEFAULTPHOTO, "images/none.jpg");
        rcGoodsPics.OrderBy(XiHuan_UserGoodsEntity.__CREATEDATE, false);
        rcGoodsPics.Top = 6;
        EntityContainer ecGoods = rcGoodsPics.AsEntityContainer();
        string pics = "var pics=\"";
        string links = "var links=\"";
        string texts = "var texts=\"";
        foreach (XiHuan_UserGoodsEntity goods in ecGoods)
        {
            pics += goods.DefaultPhoto.Replace(GlobalVar.DefaultPhotoSize, GlobalVar.BigPhotoSize) + "|";
            links += goods.DetailUrl + "|";
            texts += ValidatorHelper.SafeSql(goods.Name) + "|";
        }
        pics = pics.TrimEnd('|') + "\";";
        links = links.TrimEnd('|') + "\";";
        texts = texts.TrimEnd('|') + "\";";
        Query.ProcessSqlNonQuery(string.Format("update xihuan_systemconfig set configvalue='{0}' where configkey='HomeRounPics'", pics + links + texts), GlobalVar.DataBase_Name);
        SystemConfigFacade.Refresh();
        Alert("幻灯片成功更新！");
    }
    #endregion

    #region 发送邮件
    protected void btnSendMaiil_Click(object sender, EventArgs e)
    {
        string mailcontent = " <table style=\"width:98%;border: #ccc 1px solid;padding-right: 10px;padding-left: 10px;padding-bottom: 10px;padding-top: 10px;background-color: #f7f7f7; font-size:14px; color:black;\">";
        RetrieveCriteria rcGoodsPics = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__DEFAULTPHOTO);
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__NAME);
        rcGoodsPics.AddSelect(XiHuan_UserGoodsEntity.__DETAILURL);
        Condition c = rcGoodsPics.GetNewCondition();
        c.AddEqualTo(XiHuan_UserGoodsEntity.__ISCHECKED, 1);
        c.AddNotEqualTo(XiHuan_UserGoodsEntity.__DEFAULTPHOTO, "images/none.jpg");
        rcGoodsPics.OrderBy(XiHuan_UserGoodsEntity.__CREATEDATE, false);
        rcGoodsPics.Top = 70;
        EntityContainer ecGoods = rcGoodsPics.AsEntityContainer();
        for (int i = 0; i < ecGoods.Count; i++)
        {
            int rem;
            XiHuan_UserGoodsEntity goods = ecGoods[i] as XiHuan_UserGoodsEntity;
            Math.DivRem(i, 7, out rem);
            if (rem == 0)
            {
                mailcontent += "<tr>";
            }
            else
            {
                mailcontent += string.Format("<td style=\"border: solid 1px #CCCCCC;height: 30px;text-align:center;padding: 2px 5px;\"> <a title=\"{0}\" href=\"{1}\" target=\"_blank\"><img style=\"width: 85px;height: 85px;border: solid 1px #CCCCCC;\" title=\"{0}\" src=\"{2}\" /><br/><br/>{3}</a></td>", goods.Name, "http://www.tsc8.com/" + goods.DetailUrl + "?from=email", "http://www.tsc8.com/" + goods.DefaultPhoto, CommonMethod.GetSubString(goods.Name, 6, "..."));
            }

            if (rem == 0)
            {
                mailcontent += "</tr>";
            }

        }

        mailcontent += "</table>";
        DataTable dt = PersistenceLayer.Query.ProcessSql("select Id,UserName,Email,QQ,Msn from XiHuan_UserInfo with(nolock) where LastLoginTime<='" + DateTime.Now.AddDays(-Math.Abs(Convert.ToDouble(txtDays.Text))).ToString("yyyy-MM-dd") + "' and (Email>'' or QQ >'' or Msn >'')", GlobalVar.DataBase_Name);
        string strMailAdd = string.Empty;
        string email = string.Empty;
        string qq = string.Empty;
        string msn = string.Empty;
        foreach (DataRow dr in dt.Rows)
        {
            strMailAdd = string.Empty;
            email = CommonMethod.FinalString(dr[XiHuan_UserInfoEntity.__EMAIL]).ToLower();
            qq = GetFinalQQ(CommonMethod.FinalString(dr[XiHuan_UserInfoEntity.__QQ]));
            msn = CommonMethod.FinalString(dr[XiHuan_UserInfoEntity.__MSN]);
            if (IsEmail(email))
                strMailAdd += email + ",";
            if (IsInt(qq))
                strMailAdd += qq + "@qq.com,";
            if (IsEmail(msn))
                strMailAdd += msn + ",";
            if (strMailAdd.Length > 0)
            {
                SendMailFacade.sendEmail(strMailAdd.TrimEnd(','), "喜换网-物品交换，节约，时尚，好玩，精彩不容错过！", "<span style=\"color:blue;font-weight:bold;\">尊敬的换友<a href=\"http://www.tsc8.com/xh.aspx?id=" + dr[XiHuan_UserInfoEntity.__ID] + "&from=email\" target=\"_blank\" style=\"color:red;\">" + dr[XiHuan_UserInfoEntity.__USERNAME] + "</a>，我们注意到你有段时间没来<a href=\"http://www.tsc8.com/?from=eamil&id=" + dr[XiHuan_UserInfoEntity.__ID] + "\" target=\"_blank\" style=\"color:red;\">喜换网</a>逛逛了啊，<br/><br/>你不在的这段时间里好多朋友发布了很多好玩的换品：</span><br/><br/>" + mailcontent + "<br/><br/>现在快<a href=\"http://www.tsc8.com/?from=eamil\" target=\"_blank\">去看看</a>吧！");
            }
        }

        Alert("恭喜：邮件已经成功发送！");
    }

    private bool IsEmail(object o)
    {
        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        return r.IsMatch(o.ToString());
    }
    private bool IsInt(string str)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str, @"^[0-9]*$");
    }
    private string GetFinalQQ(string qq)
    {
        return qq.Replace('０', '0').Replace('１', '1').Replace('２', '2').Replace('3', '3').Replace('４', '4').Replace('５', '5').Replace('６', '6').Replace('７', '7').Replace('８', '8').Replace('９', '9');
    }
    #endregion

}
