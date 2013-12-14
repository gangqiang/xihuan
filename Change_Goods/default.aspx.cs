using System;
using System.Data;
using System.Web.UI;
using BusinessFacade;
using PersistenceLayer;
using System.Text;
public partial class _default : BaseWebPage
{
    #region 初始界面呈现
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInfo();
        }
    }
    #endregion

    #region 初始加载信息

    private void LoadInfo()
    {

        #region 换品排行榜

        string sql = @"select top 18 Id,Name,DefaultPhoto,ViewCount,DetailUrl from XiHuan_UserGoods with(nolock) where GoodState!=3 and IsChecked=1 order by NewId();
         select top 15 Id,UserName,HeadImage from XiHuan_UserInfo with(nolock) where IsStarUser=1 order by GoodsNumber desc;
         select top 15 Id,DefaultPhoto,Name,DetailUrl from XiHuan_UserGoods with(nolock) where GoodState!=3 and IsChecked=1 order by CreateDate desc;
         select top 15 Id,DefaultPhoto,Name,DetailUrl from XiHuan_UserGoods with(nolock) where GoodState=3 and IsChecked=1 order by CreateDate desc;
         select top 15 Id,DefaultPhoto,Name,DetailUrl from XiHuan_UserGoods with(nolock) where IsTJ=1 and GoodState!=3  and IsChecked=1 order by  NewId();
         select Name,Url,Alt from XiHuan_Links with(nolock) order by Sort asc;
         select top 3 Title,NewsUrl from XiHuan_News with(nolock) where Type={0} order by SortNumber asc;
         select top 6 Title,NewsUrl from XiHuan_News with(nolock) where Type={1} order by SortNumber asc;
         select top 10 Title,NewsUrl from XiHuan_News with(nolock) where Type={2} order by SortNumber asc;
         select top 10 Title,NewsUrl from XiHuan_News with(nolock) where Type={3} order by SortNumber asc;
         select top 15 Id,UserName,HeadImage from XiHuan_UserInfo with(nolock) order by RegisterDate desc;";

        DataSet ds = Query.ProcessMultiSql(string.Format(sql, XiHuan_NewsFacade.NewsType.站内公告.ToString("d"), XiHuan_NewsFacade.NewsType.最新服务.ToString("d"), XiHuan_NewsFacade.NewsType.精彩资讯.ToString("d"), XiHuan_NewsFacade.NewsType.站内新闻.ToString("d")), GlobalVar.DataBase_Name);
        dlsHotGoods.DataSource = ds.Tables[0];
        dlsHotGoods.DataBind();

        #endregion

        #region 明星换客

        rptStartUser.DataSource = ds.Tables[1];
        rptStartUser.DataBind();

        #endregion

        #region 最新换品

        rptNewGoods.DataSource = ds.Tables[2];
        rptNewGoods.DataBind();

        #endregion

        #region 最近成交

        rptSuccess.DataSource = ds.Tables[3];
        rptSuccess.DataBind();

        #endregion

        #region 推荐换品

        rptTJ.DataSource = ds.Tables[4];
        rptTJ.DataBind();

        #endregion

        #region 友情链接
        rptLinks.DataSource = ds.Tables[5];
        rptLinks.DataBind();
        #endregion

        #region 公告
        rptNotic.DataSource = ds.Tables[6];
        rptNotic.DataBind();
        #endregion

        #region 最新服务
        rptNewService.DataSource = ds.Tables[7];
        rptNewService.DataBind();
        #endregion

        //#region 精彩资讯
        //rptZiXun.DataSource = ds.Tables[8];
        //rptZiXun.DataBind();
        //#endregion

        //#region 站内新闻
        //rptSiteNews.DataSource = ds.Tables[9];
        //rptSiteNews.DataBind();
        //#endregion

        #region 换品分类

        DataTable dtparenttype = XiHuan_GoodsTypeFacade.GetInstance().GetGoodsParentType();
        for (int i = 0; i <= 8; i++)
        {
            DataRow[] dr = dtparenttype.Select("FixId=" + i);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dr)
            {
                sb.AppendFormat("<li><a title=\"{0}\" href=\"searchlist.aspx?typeid={1}\" target=\"_blank\">{0}</a>", row["TypeName"], row["Id"]);
                sb.Append("<ul>");
                DataTable dtchildType = XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(row["Id"].ToString());
                foreach (DataRow childrow in dtchildType.Rows)
                {
                    sb.AppendFormat(" <li><a title=\"{0}\" href=\"searchlist.aspx?typeid={1}&childid={2}\" target=\"_blank\">{0}</a></li>", childrow["Name"], row["Id"], childrow["Id"]);
                }
                sb.Append("</ul>");
                sb.Append("</li>");
            }
            switch (i)
            {
                case 0: FixOne = sb.ToString(); break;
                case 1: FixTwo = sb.ToString(); break;
                case 2: FixThree = sb.ToString(); break;
                case 3: FixFour = sb.ToString(); break;
                case 4: FixFive = sb.ToString(); break;
                case 5: FixSix = sb.ToString(); break;
                case 6: FixServen = sb.ToString(); break;
                case 7: FixEight = sb.ToString(); break;
                case 8: FixNine = sb.ToString(); break;
                default: break;
            }
        }
        #endregion

        #region 喜换新人

        rptNewUser.DataSource = ds.Tables[10];
        rptNewUser.DataBind();

        #endregion

    }
    #endregion

    #region 登陆
    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        #region 验证
        if (txtUserName.Text.Trim().Length == 0)
        {
            Alert("请输入您的换客名！");
            Select(txtUserName);
            return;
        }

        if (txtPwd.Text.Trim().Length == 0)
        {
            Alert("请输入您的密码！");
            Select(txtPwd);
            return;
        }
        #endregion

        #region 换客登陆

        string usernmae = Microsoft.JScript.GlobalObject.unescape(hidUserName.Value.Trim());
        string pwd = Microsoft.JScript.GlobalObject.unescape(hidPwd.Value.Trim());

        if (XiHuan_UserFacade.IsUserValid(usernmae, pwd))
        {
            int uid = XiHuan_UserFacade.GetIdByName(usernmae);
            DateTime dt = DateTime.MinValue;
            if (chkAutoLogin.Checked)
            {
                dt = DateTime.Now.AddDays(14);
            }
            CommonMethod.AddLoginCookie(uid, usernmae, dt);
            Response.Redirect("membercenter.aspx");
        }
        else
        {
            Alert("用户名或密码不正确，请重试！");
            ExecScript("window.location='login.aspx';");
        }
        #endregion
    }
    #endregion

    #region  退出登陆
    protected void lnkQuit_Click(object sender, EventArgs e)
    {
        CommonMethod.AddLoginCookie(0, "", DateTime.Now.AddYears(-1));
        Alert("您已成功退出登陆！");
        Response.Redirect("index.html");
    }
    #endregion

    #region 属性

    protected string FixOne = string.Empty;
    protected string FixTwo = string.Empty;
    protected string FixThree = string.Empty;
    protected string FixFour = string.Empty;
    protected string FixFive = string.Empty;
    protected string FixSix = string.Empty;
    protected string FixServen = string.Empty;
    protected string FixEight = string.Empty;
    protected string FixNine = string.Empty;

    #endregion


}
