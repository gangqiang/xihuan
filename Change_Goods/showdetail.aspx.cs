using System;
using System.Data;
using System.Web.UI.HtmlControls;
using BusinessEntity;
using BusinessFacade;
using PersistenceLayer;
public partial class showdetail : BaseWebPage
{

    #region 属性

    private XiHuan_UserGoodsEntity gooddetail = null;
    protected XiHuan_UserGoodsEntity GoodDetail
    {
        get
        {
            if (gooddetail == null)
            {
                RetrieveCriteria rcGoodInfo = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
                rcGoodInfo.GetNewCondition().AddEqualTo(XiHuan_UserGoodsEntity.__ID, Request["id"]);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__ID);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__TYPEID);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__CHILDID);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__NAME);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__OWNERID);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__OWNERNAME);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__DETAILURL);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__DEFAULTPHOTO);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__CREATEDATE);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__OWNERID);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__OWNERNAME);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__PROVINCENAME);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__CITYNAME);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__AREANAME);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__SCHOOLNAME);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__NEWDEEP);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__GOODSTATE);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__ISCHECKED);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__HOPETOCHANGETYPEID);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__HOPETOCHANGECHILDTYPEID);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__OnlyCityChange);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__OnlySchoolChange);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__HOPETOCHANGEDESC);
                rcGoodInfo.AddSelect(XiHuan_UserGoodsEntity.__DESCRIPTION);
                gooddetail = XiHuan_UserGoodsEntityAction.RetrieveAXiHuan_UserGoodsEntity(CommonMethod.ConvertToInt(Request["id"], 0));
                if (gooddetail == null)
                {
                    Response.Write("您要查看的换品已经被换主删除了,去看看其他的换品吧 ^_^！");
                    Response.End();
                }
                else
                {
                    if (gooddetail.IsChecked != 1 && !CommonMethod.FinalString(Request["action"]).Equals("isadmin"))
                    {
                        Response.Write("您要查看的换品尚未通过审核,去看看其他的换品吧 ^_^！");
                        Response.End();
                    }
                }
            }
            return gooddetail;
        }
    }

    #endregion

    #region 覆盖基类方法

    protected override void Page_PreLoad()
    {
        Header.Title = GoodDetail.Name + "--" + SystemConfigFacade.Instance().WebSiteTitle;
        GenerateMeta("Content-Type", string.Empty, "text/html;charset=GB2312", 0);
        //开启IE8兼容模式
        GenerateMeta("X-UA-Compatible", string.Empty, "IE=EmulateIE7", 1);
        GenerateMeta(string.Empty, "keywords", GoodDetail.Name + "," + XiHuan_UserGoodsFacade.GetTypeNameById(GoodDetail.TypeId.ToString()) + "," + XiHuan_UserGoodsFacade.GetSecondTypeNameById(GoodDetail.TypeId.ToString(), GoodDetail.ChildId.ToString()), 2);
        GenerateMeta(string.Empty, "description", GoodDetail.Name + "," + SystemConfigFacade.Instance().WebSiteDescription, 3);
        GenerateMeta(string.Empty, "Robots", "All", 4);
        GenerateMeta(string.Empty, "GOOGLEBOT", "All", 5);
        GenerateMeta(string.Empty, "verify-v1", "2WI+gttAG3Vgo7KoaYDT/7Fb1mXT6UGwvFpYYrVkUkU=", 6);
        GenerateMeta(string.Empty, "y_key", "f00a394915068b18", 7);

        HtmlLink maincss = new HtmlLink();
        maincss.Attributes["type"] = "text/css";
        maincss.Attributes["href"] = SrcRootPath + "App_Themes/Default/style.css";
        maincss.Attributes["rel"] = "stylesheet";
        Header.Controls.AddAt(8, maincss);

        HtmlLink ymcss = new HtmlLink();
        ymcss.Attributes["type"] = "text/css";
        ymcss.Attributes["href"] = SrcRootPath + "Js/ymPromot/skin/qq/ymPrompt.css";
        ymcss.Attributes["rel"] = "stylesheet";
        Header.Controls.AddAt(9, ymcss);

        int i = 9;
        if (GoodDetail.DefaultPhoto != null && GoodDetail.DefaultPhoto.Trim().Length > 0 && !GoodDetail.DefaultPhoto.Trim().Equals("images/none.jpg"))
        {
            HtmlLink highslidecss = new HtmlLink();
            highslidecss.Attributes["type"] = "text/css";
            highslidecss.Attributes["href"] = SrcRootPath + "Js/highslide/highslide.css";
            highslidecss.Attributes["rel"] = "stylesheet";
            Header.Controls.AddAt(10, highslidecss);
            i = 10;
        }
        GenerateScript(SrcRootPath + "Js/ymPromot/ymPrompt.js", i + 1);
        GenerateScript(SrcRootPath + "Js/common.js", i + 2);
        GenerateScript("http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js", i + 3);

        HtmlLink favicon = new HtmlLink();
        favicon.Attributes["rel"] = "shortcut icon'";
        favicon.Attributes["href"] = SrcRootPath + "images/favicon.ico";
        Header.Controls.AddAt(i + 4, favicon);

    }
    #endregion

    #region 页面初始

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    private void InitPage()
    {
        if (GoodDetail == null)
        {
            return;
        }

        #region 界面显示
        RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_UserInfoEntity));
        Condition c = rc.GetNewCondition();
        c.AddEqualTo(XiHuan_UserInfoEntity.__ID, GoodDetail.OwnerId);
        rc.AddSelect(XiHuan_UserInfoEntity.__GENDER);
        rc.AddSelect(XiHuan_UserInfoEntity.__SCORE);
        rc.AddSelect(XiHuan_UserInfoEntity.__HUANBI);
        rc.AddSelect(XiHuan_UserInfoEntity.__GOODFEED);
        rc.AddSelect(XiHuan_UserInfoEntity.__XINYU);
        rc.AddSelect(XiHuan_UserInfoEntity.__REGISTERDATE);
        rc.AddSelect(XiHuan_UserInfoEntity.__LASTLOGINTIME);
        rc.AddSelect(XiHuan_UserInfoEntity._TelePhone);
        rc.AddSelect(XiHuan_UserInfoEntity._WangWang);
        rc.AddSelect(XiHuan_UserInfoEntity.__EMAIL);
        rc.AddSelect(XiHuan_UserInfoEntity.__QQ);
        rc.AddSelect(XiHuan_UserInfoEntity.__MSN);
        rc.AddSelect(XiHuan_UserInfoEntity.__OTHERLINK);
        XiHuan_UserInfoEntity user = rc.AsEntity() as XiHuan_UserInfoEntity;
        lblScore.Text = user.Score.ToString();
        lblHB.Text = user.HuanBi.ToString();
        lblGoodFeed.Text = user.GoodFeed.ToString();
        lblXY.Text = user.XinYu.ToString();
        lblRegisterDate.Text = user.RegisterDate.ToString("yyyy-MM-dd");
        lblLastLoginTime.Text = user.LastLoginTime.ToString("yyyy-MM-dd");
        lblWW.Text = CommonMethod.FinalString(user.WangWang).Length > 0 ? "&nbsp;" + string.Format(GlobalVar.BIGSTRWW, Server.UrlEncode(user.WangWang)) : string.Empty;
        lblQQ.Text = CommonMethod.FinalString(user.QQ).Length > 0 ? string.Format(GlobalVar.QQSTR, user.QQ) : string.Empty;
        linkMethod.Text = string.Format("&nbsp;&nbsp;电话：{0}&nbsp;&nbsp;&nbsp;&nbsp;邮箱：{1}&nbsp;&nbsp;&nbsp;&nbsp;旺旺:{5}&nbsp;&nbsp;&nbsp;&nbsp;QQ:{2}&nbsp;&nbsp;&nbsp;&nbsp;MSN:{3}<br/>&nbsp;&nbsp;其他联系方式：{4}", user.TelePhone, user.Email, user.QQ + lblQQ.Text, user.Msn, user.OtherLink, lblWW.Text);
        lblGender.Text = CommonMethodFacade.FormatGender(user.Gender, SrcRootPath);
        #endregion

        #region 换主热门换品

        RetrieveCriteria rchotgoods = new RetrieveCriteria(typeof(XiHuan_UserGoodsEntity));
        Condition hotgoodscondition = rchotgoods.GetNewCondition();
        hotgoodscondition.AddEqualTo(XiHuan_UserGoodsEntity.__OWNERID, GoodDetail.OwnerId);
        hotgoodscondition.AddEqualTo(XiHuan_UserGoodsEntity.__ISCHECKED, 1);
        rchotgoods.AddSelect(XiHuan_UserGoodsEntity.__DEFAULTPHOTO);
        rchotgoods.AddSelect(XiHuan_UserGoodsEntity.__NAME);
        rchotgoods.AddSelect(XiHuan_UserGoodsEntity.__DETAILURL);
        rchotgoods.Top = 10;
        rchotgoods.OrderBy(XiHuan_UserGoodsEntity.__VIEWCOUNT, false);
        rptHotGoods.DataSource = rchotgoods.AsDataTable();
        rptHotGoods.DataBind();

        #endregion

        #region 其它相关换品

        DataTable dtrelateGoods = Query.ProcessSql(
        @"select top 10 DefaultPhoto,Name,DetailUrl from XiHuan_UserGoods 
        with(nolock) where TypeId=" + GoodDetail.TypeId + " and OwnerId<>" + GoodDetail.OwnerId + " and IsChecked=1 order by newid() ",
        GlobalVar.DataBase_Name);
        rtpRelateGoods.DataSource = dtrelateGoods;
        rtpRelateGoods.DataBind();

        #endregion

        #region 留言
        BindNotes();
        #endregion

        #region 交换请求

        BindRequire();

        #endregion

        #region 上一换品下一换品链接

        string sqlpre = "select top 1 Name,DetailUrl from XiHuan_UserGoods with(nolock) where Id<" + Request["id"] + " and IsChecked=1 order by Id desc;";
        string sqlnext = "select top 1 Name,DetailUrl from XiHuan_UserGoods with(nolock) where Id>" + Request["id"] + " and IsChecked=1 order by Id asc;";
        DataSet dt = Query.ProcessMultiSql(sqlpre + sqlnext, GlobalVar.DataBase_Name);
        lblPre.Text = dt.Tables[0].Rows.Count > 0 ? string.Format("<a href=\"{0}\" title=\"{1}\">{2}</a>", SrcRootPath + (dt.Tables[0].Rows[0][1]), dt.Tables[0].Rows[0][0], CommonMethod.GetSubString(CommonMethod.FinalString(dt.Tables[0].Rows[0][0]), 20, "")) : "没有了";
        lblNext.Text = dt.Tables[1].Rows.Count > 0 ? string.Format("<a href=\"{0}\" title=\"{1}\">{2}</a>", SrcRootPath + (dt.Tables[1].Rows[0][1]), dt.Tables[1].Rows[0][0], CommonMethod.GetSubString(CommonMethod.FinalString(dt.Tables[1].Rows[0][0]), 20, "")) : "没有了";

        #endregion

        #region 换品图片加载
        if (GoodDetail.DefaultPhoto != null && GoodDetail.DefaultPhoto.Trim().Length > 0 && !GoodDetail.DefaultPhoto.Trim().Equals("images/none.jpg"))
        {
            RetrieveCriteria rcgoodimage = new RetrieveCriteria(typeof(XiHuan_GoodsImageEntity));
            Condition cg = rcgoodimage.GetNewCondition();
            cg.AddEqualTo(XiHuan_GoodsImageEntity.__GOODSID, GoodDetail.Id);
            cg.AddEqualTo(XiHuan_GoodsImageEntity.__ISDEFAULTPHOTO, 0);
            rcgoodimage.AddSelect(XiHuan_GoodsImageEntity.__IMGSRC);
            rcgoodimage.AddSelect(XiHuan_GoodsImageEntity.__IMGDESC);
            rcgoodimage.OrderBy(XiHuan_GoodsImageEntity.__CREATEDATE, false);
            rptGoodsImage.DataSource = rcgoodimage.AsDataTable();
            rptGoodsImage.DataBind();
        }
        #endregion

    }

    #endregion

    #region 留  言
    private void BindNotes()
    {
        rptUserNotes.DataSource = XiHuan_UserNotesFacade.GetUserNotes(GoodDetail.OwnerId, GoodDetail.Id, 0, false);
        rptUserNotes.DataBind();

    }
    #endregion

    #region 交换请求
    private void BindRequire()
    {
        XiHuan_ChangeRequireSearchFilter rf = new XiHuan_ChangeRequireSearchFilter();
        rf.GoodsId = CommonMethod.ConvertToInt(Request["id"], int.MaxValue);
        rf.OwnerId = GoodDetail.OwnerId;
        rptRequire.DataSource = XiHuan_ChangeRequireFacade.GetUserRequire(rf);
        rptRequire.DataBind();
    }
    #endregion

}
