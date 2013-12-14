using System.Web;
using System.Web.UI.HtmlControls;

/// <summary>
/// BaseAdminPage 的摘要说明
/// </summary>
public class BaseAdminPage : BasePage
{
    public BaseAdminPage()
    {
         
    }
    protected override void Page_PreLoad()
    {
        GenerateMeta("Content-Type", string.Empty, "text/html;charset=GB2312", 0);
        //开启IE8兼容模式
        GenerateMeta("X-UA-Compatible", string.Empty, "IE=EmulateIE7", 1);
        //后台管理关闭搜索引擎
        GenerateMeta(string.Empty, "Robots", "noindex, nofollow", 2);

        HtmlLink maincss = new HtmlLink();
        maincss.Attributes["type"] = "text/css";
        maincss.Attributes["href"] = SrcRootPath + "App_Themes/Default/style.css";
        maincss.Attributes["rel"] = "stylesheet";
        Header.Controls.AddAt(3, maincss);

        HtmlLink ymcss = new HtmlLink();
        ymcss.Attributes["type"] = "text/css";
        ymcss.Attributes["href"] = SrcRootPath + "Js/ymPromot/skin/qq/ymPrompt.css";
        ymcss.Attributes["rel"] = "stylesheet";
        Header.Controls.AddAt(4, ymcss);
        GenerateScript(SrcRootPath + "Js/ymPromot/ymPrompt.js", 5);
        GenerateScript(SrcRootPath + "Js/common.js", 6);
        GenerateScript("http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js", 7);

        HtmlLink favicon = new HtmlLink();
        favicon.Attributes["rel"] = "shortcut icon'";
        favicon.Attributes["href"] = SrcRootPath + "images/favicon.ico";
        Header.Controls.AddAt(8, favicon);

    }
    protected virtual void Page_Load()
    {
        HttpCookie XIHuan8 = HttpContext.Current.Request.Cookies["XIHuan8_Admin"];
        if (XIHuan8 == null)
        {
            Alert("您尚未登录或登录超时，请登录！");
            ExecScript("parent.parent.location='default.aspx';");
        }
    }

}
