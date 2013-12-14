using System;
using System.Data;
using System.Web.UI.HtmlControls;
using BusinessFacade;
using BusinessEntity;
using PersistenceLayer;
using System.Text;
public partial class xh : BaseWebPage
{
    private XiHuan_UserInfoEntity gooddetail = null;
    protected XiHuan_UserInfoEntity GoodDetail
    {
        get
        {
            if (gooddetail == null)
            {
                gooddetail = XiHuan_UserInfoEntityAction.RetrieveAXiHuan_UserInfoEntity(CommonMethod.ConvertToInt(Request["id"], 0));
                if (gooddetail == null)
                {
                    Response.Write("您要查看的换铺不存在,去看看其他的换铺吧 ^_^！");
                    Response.End();
                }
                else
                {
                    if (gooddetail.IsLocked == 1)
                    {
                        Response.Write("您要查看的换客账号已被锁定,去看看其他的换铺吧 ^_^！");
                        Response.End();
                    }
                }
            }
            return gooddetail;
        }
    }


    #region 重写基类方法

    protected override void Page_PreLoad()
    {
        GenerateMeta("Content-Type", string.Empty, "text/html;charset=GB2312", 0);
        //开启IE8兼容模式
        GenerateMeta("X-UA-Compatible", string.Empty, "IE=EmulateIE7", 1);
        GenerateMeta(string.Empty, "keywords", GoodDetail.UserName + "的换铺," + SystemConfigFacade.Instance().WebSiteKeyWords, 2);
        GenerateMeta(string.Empty, "description", GoodDetail.UserName + "的换铺,换客,交换,物品交换,喜换网换客换铺是换客的免费交换平台,供换客用来展示换品最快最好的完成交换。", 3);
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
        GenerateScript(SrcRootPath + "Js/ymPromot/ymPrompt.js", 10);
        GenerateScript(SrcRootPath + "Js/common.js", 11);
        GenerateScript("http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js", 12);

        HtmlLink favicon = new HtmlLink();
        favicon.Attributes["rel"] = "shortcut icon'";
        favicon.Attributes["href"] = SrcRootPath + "images/favicon.ico";
        Header.Controls.AddAt(13, favicon);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    private void InitPage()
    {
        int uid = CommonMethod.ConvertToInt(Request["Id"], 0);
        if (uid > 0)
        {
            #region 浏览过该换品的人

            #region 浏览历史

            int gid = GoodDetail.ID;
            if (IsUserAlreadyLogin)
            {
                RetrieveCriteria rcview = new RetrieveCriteria(typeof(XiHuan_GoodsViewUserEntity));
                Condition cview = rcview.GetNewCondition();
                cview.AddEqualTo(XiHuan_GoodsViewUserEntity.__GOODSID, gid);
                cview.AddEqualTo(XiHuan_GoodsViewUserEntity.__VISITORID, CurrentUserId);
                cview.AddEqualTo(XiHuan_GoodsViewUserEntity._TYPE, 1);
                XiHuan_GoodsViewUserEntity newview = rcview.AsEntity() as XiHuan_GoodsViewUserEntity;
                if (CurrentUserId != gid)
                {
                    if (newview == null)
                    {
                        newview = new XiHuan_GoodsViewUserEntity();
                        newview.GoodsId = gid;
                        newview.VisitorId = CurrentUserId;
                        newview.VisitorName = CurrentUserName;
                        newview.Type = 1;
                    }
                    newview.VisitorHeadImage = CurrentUser.HeadImage;
                    newview.VisitDate = DateTime.Now;
                    newview.Save();
                }
            }
            #endregion

            #endregion

            StringBuilder sbSql = new StringBuilder("select top 18 Id,Name,DefaultPhoto,ViewCount,DetailUrl from XiHuan_UserGoods with(nolock) where OwnerId={0} and IsChecked=1 order by CreateDate desc, ViewCount desc ;");
            sbSql.Append("select * from XiHuan_UserGoodsChangeRequire with(nolock) where OwnerId={0} order by RequireDate desc; ");
            sbSql.Append("select * from XiHuan_UserGoodsChangeRequire with(nolock) where SenderId={0} order by RequireDate desc; ");
            sbSql.Append("select * from XiHuan_GuestBook with(nolock) where ToId={0} order by CreateDate desc; ");
            sbSql.Append("select FriendId,FriendName from XiHuan_UserFriends with(nolock) where OwnerId={0} order by AddDate desc; ");
            sbSql.Append("select top 10 VisitorId,VisitorName,VisitorHeadImage,VisitDate from XiHuan_GoodsViewUser with(nolock) where Type=1 and GoodsId=" + gid + " order by VisitDate desc;");
            DataSet ds = Query.ProcessMultiSql(string.Format(sbSql.ToString(), uid), GlobalVar.DataBase_Name);
            dlsHotGoods.DataSource = ds.Tables[0];
            dlsHotGoods.DataBind();
            rptSend.DataSource = ds.Tables[1];
            rptSend.DataBind();
            rptRequire.DataSource = ds.Tables[2];
            rptRequire.DataBind();
            rptUserNotes.DataSource = ds.Tables[3];
            rptUserNotes.DataBind();
            rptGoodFriends.DataSource = ds.Tables[4];
            rptGoodFriends.DataBind();
            rptVisitor.DataSource = ds.Tables[5];
            rptVisitor.DataBind();
        }
    }

    protected string ShowReply(bool isneedshow, string replycontent, string ischecked)
    {
        if (isneedshow && replycontent.Length > 0 && !ischecked.Equals("0"))
            return string.Format("<br/><span class=\"bluetext\">换主回复：{0}</span>", replycontent);
        else
            return string.Empty;
    }

    protected string showNoteContent(bool isneedshow, string ownername, string goodsname, string goodsid, string notecontent, string ischecked)
    {
        return (ownername + (!goodsid.Equals("0") ? "看了换主的<span class=\"inittext\">" + goodsname + "</span>" : "给换主")) + "留言说：" + (ischecked.Trim().Equals("0") ? "<span class=\"graytext\">此留言尚未通过审核</span>" : (isneedshow ? notecontent : "<span class=\"bluetext\">此留言为悄悄话，换主和留言人可见。</span>"));
    }
}
