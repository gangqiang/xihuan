using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using BusinessEntity;
using BusinessFacade;
/// <summary>
/// BasePage 的摘要说明
/// </summary>
public class BasePage : Page
{
    private string m_WebRootPath;

    public BasePage()
    {

    }

    /// <summary>
    /// 用户是否已经登陆
    /// </summary>
    public bool IsUserAlreadyLogin
    {
        get
        {
            HttpCookie XIHuan8 = HttpContext.Current.Request.Cookies["XIHuan8"];
            return !(null == XIHuan8);
        }
    }

    /// <summary>
    ///  当前用户Id
    /// </summary>
    public int CurrentUserId
    {
        get
        {
            if (IsUserAlreadyLogin)
            {
                HttpCookie XIHuan8 = HttpContext.Current.Request.Cookies["XIHuan8"];
                return CommonMethod.ConvertToInt(CommonMethod.FinalString(XIHuan8.Values["UserId"]), 0);
            }
            else
            {
                return 0;
            }
        }
    }

    /// <summary>
    ///  当前用户名
    /// </summary>
    public string CurrentUserName
    {
        get
        {
            if (IsUserAlreadyLogin)
            {
                HttpCookie XIHuan8 = HttpContext.Current.Request.Cookies["XIHuan8"];
                return Server.UrlDecode(CommonMethod.FinalString(XIHuan8.Values["UserName"]));
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public XiHuan_UserInfoEntity CurrentUser
    {
        get
        {
            return XiHuan_UserInfoEntityAction.RetrieveAXiHuan_UserInfoEntity(CurrentUserId);
        }
    }


    /// <summary>
    /// 获取网站根目录路径（例如：http://www.tsc8.com/）
    /// </summary>
    public string SrcRootPath
    {
        get
        {
            m_WebRootPath = Request.ApplicationPath;
            if (m_WebRootPath.Substring(0, 1) != "/")
                m_WebRootPath = "/" + m_WebRootPath;
            if (!m_WebRootPath.EndsWith("/"))
                m_WebRootPath += "/";
            m_WebRootPath = string.Concat("http://", Request.Url.Authority + m_WebRootPath);
            return m_WebRootPath;
        }
    }

    #region 页面Page_Load之前执行

    protected virtual void Page_PreLoad()
    {
        Header.Title = Header.Title + "-" + SystemConfigFacade.Instance().WebSiteTitle;
        GenerateMeta("Content-Type", string.Empty, "text/html;charset=GB2312", 0);
        //开启IE8兼容模式
        GenerateMeta("X-UA-Compatible", string.Empty, "IE=EmulateIE7", 1);
        GenerateMeta(string.Empty, "keywords", SystemConfigFacade.Instance().WebSiteKeyWords, 2);
        GenerateMeta(string.Empty, "description", SystemConfigFacade.Instance().WebSiteDescription, 3);
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

    protected virtual void Page_PreInit()
    {

    }

    #endregion

    #region Meta,Js 标签

    public void GenerateMeta(string httpequive, string name, string content, int index)
    {
        HtmlMeta meta = new HtmlMeta();
        if (httpequive.Length > 0)
            meta.HttpEquiv = httpequive;
        if (name.Length > 0)
            meta.Name = name;
        meta.Content = content;
        Header.Controls.AddAt(index, meta);
    }

    public void GenerateScript(string src, int index)
    {
        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes["type"] = "text/javascript";
        script.Attributes["language"] = "javascript";
        script.Attributes["src"] = src;
        Header.Controls.AddAt(index, script);
    }
    #endregion

    #region 客户端脚本助手
    /// <summary>
    /// 弹出提示框
    /// </summary>
    /// <param name="s">内容</param>
    public void Alert(string s)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">alert(\"" + s + "\");</script>");
    }

    /// <summary>
    /// 弹出提示框 并中止当前页面执行
    /// </summary>
    /// <param name="s">提示内容</param>
    /// <param name="isClear">是否把当前页面内容清空</param>
    public void AlertEnd(string s, bool isClear)
    {
        if (isClear)
        {
            Response.Clear();
        }
        Response.Write(string.Format("<script type=\"text/javascript\">alert('" + s + "');</script>", s));
        Response.End();
    }

    /// <summary>
    /// 弹出提示框 并转向新地址
    /// </summary>
    /// <param name="s">内容</param>
    /// <param name="url">转向地址</param>
    public void AlertRedirect(string s, string url)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">window.location='" + url + "';alert('" + s + "');</script>");
    }

    public void AlertAndExecScript(string s, string script)
    {
        Alert(s);
        ExecScript(script);
    }

    public void MemberCenterPageRedirect(string alertcontent, string action)
    {
        AlertAndExecScript(alertcontent.Length > 0 ? alertcontent : "您尚未登录或登录超时，请登录！", string.Format("parent.location='login.aspx?path={0}';", Server.UrlEncode(string.Format("membercenter.aspx?action={0}", action))));
    }

    /// <summary>
    /// 选中一个客户端控件
    /// </summary>
    /// <param name="id">控件ID</param>
    public void Select(string id)
    {
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">try{document.getElementById('" + id + "').select()}catch(e){alert(e.description);}</script>");
    }

    /// <summary>
    /// 选中一个服务端控件
    /// </summary>
    /// <param name="c">控件ID</param>
    public void Select(System.Web.UI.Control c)
    {
        Select(c.ClientID);
    }

    /// <summary>
    /// Url跳转
    /// </summary>
    /// <param name="url"></param>
    public void Redirect(string url)
    {
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">window.location= '" + url + "';</script>");
    }

    /// <summary>
    /// 执行客户端脚本
    /// </summary>
    /// <param name="script"></param>
    public void ExecScript(string script)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">" + script + "</script>");
    }



    /// <summary>
    /// 执行客户端刷新
    /// </summary>
    public void Reload()
    {
        ExecScript("window.location = this.location");
    }
    /// <summary>
    /// 执行页面的操作并停止执行页面的其它操作
    /// 例如this.ResponseAlert(this,"该线路可能被删除，请重试!")
    /// </summary>
    /// <param name="page">当前的页面只要传this</param>
    /// <param name="alertContent">要执行的提示内容</param>
    public void ResponseAlert(Page page, string alertContent)
    {
        page.Response.Write("<script>alert('" + alertContent + "');</script>");
        page.Response.End();
    }
    /// <summary>
    /// 执行页面的操作并停止执行页面的其它操作
    /// 例如this.ResponseExecScript（this, "parent.Ext.Win().close()"）
    /// </summary>
    /// <param name="page">当前的页面只要传this</param>
    /// <param name="script">要执行的JS操作</param>
    public void ResponseExecScript(Page page, string script)
    {
        page.Response.Write("<script>" + script + ";</script>");
        page.Response.End();
    }
    /// <summary>
    /// 执行页面的操作并停止执行页面的其它操作
    /// </summary>
    /// <param name="page">当前的页面只要传this</param>
    /// <param name="alertContent">提示内容即Alert内容</param>
    /// <param name="script">要执行的JS操作</param>
    public void ResponseAlertAndExecScript(Page page, string alertContent, string script)
    {
        page.Response.Write("<script>alert('" + alertContent + "');" + script + ";</script>");
        page.Response.End();
    }

    /// <summary>
    /// 执行客户端启动脚本
    /// </summary>
    /// <param name="script"></param>
    /// 
    public void ExecStartupScript(string script)
    {
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">" + script + "</script>");
    }

    public void AjaxResponse(string message)
    {
        Response.Clear();
        Response.Write(message);
        Response.End();
    }

    #endregion
}
