<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenericErrorPage.aspx.cs"
    Inherits="GenericErrorPage" EnableViewState="false" %>

<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <title>出错了....</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:Top ID="Top1" runat="server" EnableViewState="false" />
            <div class="toplinebar">
            </div>
            <div class="cakeheadlink">
                当前位置：<a href="index.html">首页</a>>>访问出错...</div>
            <div class="mainleft" style="width: 95%; margin-top: 10px;">
                <img title="您要访问的页面出错了..." src="images/error.gif" style="vertical-align: middle; margin: 10px 0px 10px 0px;" />
                <span class="highlight">抱歉：网站访问出错，错误信息已自动发送给网站管理员，我们会尽快解决。。给您造成的不便，敬请谅解！</span>
            </div>
        </div>
    </form>
    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />
    <script src="http://u.1133.cc/showpage.php?pid=140327&show_t=2" language="javascript"></script>
</body>
</html>
