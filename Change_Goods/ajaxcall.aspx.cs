using System;
using System.Data;
using PersistenceLayer;
using BusinessEntity;
using BusinessFacade;
using System.Text;
using Microsoft.JScript;
using System.IO;
public partial class ajaxcall : BaseWebPage
{
    #region 页面初始

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = CommonMethod.FinalString(Request["action"]);
            if (action.Length == 0)
            {
                return;
            }
            else
            {
                switch (action)
                {
                    case "chkusername":
                        CheckUserName(CommonMethod.FinalString(Request["username"]));
                        break;
                    case "area":
                        GetInfo(Request["type"], Request["value"], Request["id"]);
                        break;
                    case "getgoodschildtype":
                        GetGoodsChildType(Request["ddlid"], Request["parentid"]);
                        break;
                    case "addFavorite":
                        AddFavorite(Request["gid"], Request["gname"]);
                        break;
                    case "addFriend":
                        AddFriend(Request["fid"], Request["fname"]);
                        break;
                    case "LoadDefaultInfo":
                        LoadDefualtInfo();
                        break;
                    case "UpdateIndex":
                        UpdateIndex();
                        break;
                    case "checkLogin":
                        CheckLogin();
                        break;
                    case "addNotes":
                        AddNotes();
                        break;
                    case "loadDetail":
                        LoadDetail();
                        break;
                    case "GetViewCount":
                        GetViewCount();
                        break;
                    default: break;

                }
            }
        }
    }

    #endregion

    #region 首页的更新

    private void UpdateIndex()
    {
        DateTime dt = File.GetLastWriteTime(Server.MapPath("index.html"));
        if (dt.AddMinutes(30).CompareTo(DateTime.Now) == -1)
        {
            CommonMethod.readAspxAndWriteHtmlSoruce("default.aspx", "index.html");
            CommonMethod.ResponseAjaxContent(this.Page, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        else
            CommonMethod.ResponseAjaxContent(this.Page, dt.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    #endregion

    #region 首页登录等动态信息的加载
    private void LoadDefualtInfo()
    {

        if (!IsUserAlreadyLogin)
            CommonMethod.ResponseAjaxContent(this.Page, "notlogin");
        else
        {
            CommonMethod.ResponseAjaxContent(this.Page, CurrentUserId.ToString() + ";" + Microsoft.JScript.GlobalObject.escape(CurrentUserName) + ";" + XiHuan_MessageFacade.GetNewMessageCount(CurrentUserId).ToString());
        }

    }
    #endregion

    #region 检验用户名
    private void CheckUserName(string username)
    {
        username = Microsoft.JScript.GlobalObject.unescape(username);
        string ffzf = CommonMethodFacade.ValidFFZF(username);
        if (ffzf.Length > 0)
            CommonMethod.ResponseAjaxContent(this, "您输入的用户名含有非法字符：" + ffzf);
        if (XiHuan_UserFacade.IsUserNameAlreayUse(username))
            CommonMethod.ResponseAjaxContent(this, "0");
        else
            CommonMethod.ResponseAjaxContent(this, "1");
    }
    #endregion

    #region 省市学校下拉框
    private void GetInfo(string type, string value, string id)
    {
        DataTable dt = new DataTable();
        DataTable dtschool = new DataTable();
        if (type.Equals("getcity"))
        {
            dt = ProvinceCityFacade.GetInstance().GetCityInfo(value);
            dtschool = ProvinceCityFacade.GetInstance().GetSchoolInfo(value, "");
        }
        if (type.Equals("getarea"))
        {
            dt = ProvinceCityFacade.GetInstance().GetAreaInfo(value);
            dtschool = ProvinceCityFacade.GetInstance().GetSchoolInfo("", value);
        }

        StringBuilder ostr = new StringBuilder("$('" + id + "').options.length =0;");
        string addname = "city";
        string addvalue = "cityId";
        if (type.Equals("getcity"))
            ostr.Append("$('" + id + "').options.add(new Option('请选择城市',''));");
        if (type.Equals("getarea"))
        {
            addname = "area";
            addvalue = "areaId";
            ostr.Append("$('" + id + "').options.add(new Option('请选择地区',''));");
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ostr.Append("$('" + id + "').options.add(new Option('" + dt.Rows[i][addname] + "','" + dt.Rows[i][addvalue] + "'));");
        }

        ostr.Append("$('ddlSchool').options.length=0;$('ddlSchool').options.add(new Option('请选择学校',''));");
        for (int j = 0; j < dtschool.Rows.Count; j++)
        {
            ostr.Append("$('ddlSchool').options.add(new Option('" + dtschool.Rows[j][XiHuan_SchoolInfoEntity.__SCHOOLNAME] + "','" + dtschool.Rows[j][XiHuan_SchoolInfoEntity.__ID] + "'));");
        }

        CommonMethod.ResponseAjaxContent(this, ostr.ToString());
    }
    #endregion

    #region 物品类别下拉联动

    private void GetGoodsChildType(string ddlid, string parentid)
    {
        DataTable dtchildtypeInfo = XiHuan_GoodsTypeFacade.GetInstance().GetGoodsChildType(parentid);
        StringBuilder sbChildInfo = new StringBuilder("$('" + ddlid + "').options.length=0;");
        sbChildInfo.Append("$('" + ddlid + "').options.add(new Option('选择小类',''));");
        for (int i = 0; i < dtchildtypeInfo.Rows.Count; i++)
        {
            sbChildInfo.Append("$('" + ddlid + "').options.add(new Option('" + dtchildtypeInfo.Rows[i]["Name"] + "','" + dtchildtypeInfo.Rows[i]["Id"] + "'));");
        }
        CommonMethod.ResponseAjaxContent(this.Page, sbChildInfo.ToString());
    }

    #endregion

    #region 添加收藏

    private void AddFavorite(string id, string name)
    {
        if (!IsUserAlreadyLogin)
        {
            CommonMethod.ResponseAjaxContent(this.Page, "needlogin");
            return;
        }
        else
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_UserFavorateEntity));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(XiHuan_UserFavorateEntity.__USERID, CurrentUserId);
            c.AddEqualTo(XiHuan_UserFavorateEntity.__GOODSID, id);
            rc.AddSelect(XiHuan_UserFavorateEntity.__ID);
            DataTable dt = rc.AsDataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                CommonMethod.ResponseAjaxContent(this.Page, "already");
                return;
            }
            else
            {
                XiHuan_UserFavorateEntity newfavorite = new XiHuan_UserFavorateEntity();
                newfavorite.UserId = CurrentUserId;
                newfavorite.GoodsId = CommonMethod.ConvertToInt(id, 0);
                newfavorite.GoodsName = GlobalObject.unescape(CommonMethod.FinalString(name));
                newfavorite.FacRemark = "";
                newfavorite.FavDate = DateTime.Now;
                newfavorite.Save();
                CommonMethod.ResponseAjaxContent(this.Page, "ok");
            }
        }
    }

    #endregion

    #region 添加好友

    private void AddFriend(string fid, string fname)
    {
        if (!IsUserAlreadyLogin)
        {
            CommonMethod.ResponseAjaxContent(this.Page, "needlogin");
            return;
        }
        else
        {
            fname = GlobalObject.unescape(fname);
            if (fname.Equals(CurrentUserName))
            {
                CommonMethod.ResponseAjaxContent(this.Page, "self");
                return;
            }

            else
            {
                RetrieveCriteria rc = new RetrieveCriteria(typeof(XiHuan_UserFriendsEntity));
                Condition c = rc.GetNewCondition();
                c.AddEqualTo(XiHuan_UserFriendsEntity.__OWNERID, CurrentUserId);
                c.AddEqualTo(XiHuan_UserFriendsEntity.__FRIENDID, fid);
                rc.AddSelect(XiHuan_UserFriendsEntity.__ID);
                DataTable dt = rc.AsDataTable();
                if (dt != null && dt.Rows.Count > 0)
                {
                    CommonMethod.ResponseAjaxContent(this.Page, "already");
                    return;
                }
                else
                {
                    XiHuan_UserFriendsEntity newfriend = new XiHuan_UserFriendsEntity();
                    newfriend.OwnerId = CurrentUserId;
                    newfriend.OwnerName = CurrentUserName;
                    newfriend.FriendId = CommonMethod.ConvertToInt(fid, 0);
                    newfriend.FriendName = fname;
                    newfriend.AddDate = DateTime.Now;
                    newfriend.FriendDesc = "";
                    newfriend.Save();
                    CommonMethod.ResponseAjaxContent(this.Page, "ok");
                }
            }
        }
    }

    #endregion

    #region 判断是否已经登录

    private void CheckLogin()
    {
        if (IsUserAlreadyLogin)
            CommonMethod.ResponseAjaxContent(this.Page, "already");
        else
            CommonMethod.ResponseAjaxContent(this.Page, "needlogin");
    }

    #endregion

    #region 留言添加

    private void AddNotes()
    {
        #region 验证
        string username = CommonMethod.FinalString(Request["username"]);
        string userpass = CommonMethod.FinalString(Request["pwd"]);
        if (!IsUserAlreadyLogin)
        {
            if (username.Length == 0)
            {
                CommonMethod.ResponseAjaxContent(this.Page, "needusername");
                return;
            }

            if (userpass.Length == 0)
            {
                CommonMethod.ResponseAjaxContent(this.Page, "needpwd");
                return;
            }
        }

        #endregion

        #region 留言提交

        XiHuan_GuestBookEntity newguest = new XiHuan_GuestBookEntity();
        newguest.ToId = CommonMethod.ConvertToInt(Request["oid"], 0);
        newguest.ToName = GlobalObject.unescape(CommonMethod.FinalString(Request["oname"]));
        newguest.GoodsId = CommonMethod.ConvertToInt(Request["gid"], 0); ;
        newguest.GoodsName = GlobalObject.unescape(CommonMethod.FinalString(Request["gname"]));
        if (IsUserAlreadyLogin)
        {
            newguest.FromId = CurrentUserId;
            newguest.FromName = CurrentUserName;
        }
        else
        {
            if (XiHuan_UserFacade.IsUserValid(username, userpass))
            {
                CommonMethod.AddLoginCookie(XiHuan_UserFacade.GetIdByName(username), username, DateTime.MinValue);
                newguest.FromName = username;
                newguest.FromId = XiHuan_UserFacade.GetIdByName(username);
            }
            else
            {
                CommonMethod.ResponseAjaxContent(this.Page, "notvaliduser");
                return;
            }
        }

        newguest.Content = GlobalObject.unescape(CommonMethod.FinalString(Request["content"]));
        newguest.IsScerect = CommonMethod.FinalString(Request["issceret"]) == "true" ? (byte)1 : (byte)0;
        newguest.CreateDate = DateTime.Now;
        newguest.IsChecked = 0;
        newguest.Save();
        SendMailFacade.sendEmail(CommonMethodFacade.GetConfigValue("NoticeEmail"), "有人在喜换网留言了", "有人在喜换网留言了，快去审核吧！");
        if (CommonMethod.FinalString(Request["type"]).Equals("1"))
        {
            CommonMethod.readAspxAndWriteHtmlSoruce("showdetail.aspx?id=" + newguest.GoodsId, CommonMethod.FinalString(Request["url"]));
        }
        CommonMethod.ResponseAjaxContent(this.Page, "ok");

        #endregion
    }

    #endregion

    #region 详细页面的信息加载

    private void LoadDetail()
    {
        #region 浏览过该换品的人

        #region 浏览历史

        int gid = CommonMethod.ConvertToInt(Request["gid"], 0);
        if (gid > 0)
        {
            if (IsUserAlreadyLogin)
            {
                RetrieveCriteria rcview = new RetrieveCriteria(typeof(XiHuan_GoodsViewUserEntity));
                Condition cview = rcview.GetNewCondition();
                cview.AddEqualTo(XiHuan_GoodsViewUserEntity.__GOODSID, gid);
                cview.AddEqualTo(XiHuan_GoodsViewUserEntity.__VISITORID, CurrentUserId);
                cview.AddEqualTo(XiHuan_GoodsViewUserEntity._TYPE, 0);
                XiHuan_GoodsViewUserEntity newview = rcview.AsEntity() as XiHuan_GoodsViewUserEntity;
                if (newview == null)
                {
                    newview = new XiHuan_GoodsViewUserEntity();
                    newview.GoodsId = gid;
                    newview.VisitorId = CurrentUserId;
                    newview.VisitorName = CurrentUserName;
                    newview.Type = 0;
                }
                newview.VisitorHeadImage = CurrentUser.HeadImage;
                newview.VisitDate = DateTime.Now;
                newview.Save();
            }
        #endregion

            string sql = @"select  top 10 VisitorId,VisitorName,VisitorHeadImage,VisitDate from XiHuan_GoodsViewUser with(nolock) where Type=0 and GoodsId={0} order by VisitDate desc;
                              declare  @vcount int;
                              update xihuan_usergoods set viewcount=viewcount+1,@vcount=viewcount+1 where id= {0};
                              select @vcount;";
            DataSet ds = Query.ProcessMultiSql(string.Format(sql, gid), GlobalVar.DataBase_Name);
            DataTable dt = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];
            int viewcount = (dt2 != null && dt2.Rows.Count > 0 ? CommonMethod.ConvertToInt(dt2.Rows[0][0], 0) : 0);
            StringBuilder sbViewContent = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sbViewContent.AppendFormat("<li style=\"width: 170px; text-align: left;\"><a title=\"浏览时间{0}\" href=\"{1}\" target=\"_blank\"><img style=\"border: 0px; vertical-align: middle;\" title=\"{2}\" src=\"{3}\" width=\"50\" height=\"40\" />&nbsp;&nbsp;{4}</a></li>",
                        dr["VisitDate"], SrcRootPath + "xh.aspx?id=" + dr["VisitorId"], dr["VisitorName"], SrcRootPath + dr["VisitorHeadImage"], dr["VisitorName"]);
                }
            }
            else
            {
                CommonMethod.ResponseAjaxContent(this.Page, "尚无登录用户浏览过...$" + viewcount);
                return;
            }

            CommonMethod.ResponseAjaxContent(this.Page, "<ul>" + sbViewContent + "</ul>$" + viewcount);
        }
        #endregion
    }

    #endregion

    #region 新闻浏览次数

    private void GetViewCount()
    {
        int nid = CommonMethod.ConvertToInt(Request["nid"], 0);
        if (nid > 0)
        {
            DataTable dt = Query.ProcessSql(string.Format(@"declare  @vcount int;
                                                        update xihuan_news set viewcount=viewcount+1,@vcount=viewcount+1 where id= {0};
                                                        select @vcount;", nid), GlobalVar.DataBase_Name);
            CommonMethod.ResponseAjaxContent(this.Page, dt != null && dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty);
        } 
    }

    #endregion
}
