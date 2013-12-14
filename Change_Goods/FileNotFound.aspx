<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileNotFound.aspx.cs" Inherits="FileNotFound"  EnableViewState="false"%>

<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>您要访问的页面不存在</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:Top ID="Top1" runat="server" EnableViewState="false" />
            <div class="toplinebar">
            </div>
            <div class="cakeheadlink">
                当前位置：<a href="index.html">首页</a>>>您要访问的页面不存在...</div>
            <div class="mainleft" style="width: 95%; margin-top: 10px;">
                <img title="您要访问的页面不存在..." src="images/error.gif" style="vertical-align: middle; margin: 10px 0px 10px 0px;" />
                <span class="highlight">抱歉：您要访问的页面不存在，请确认你是否按正常的浏览方式来浏览网站。。</span>
            </div>
        </div>
    </form>
    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />
</body>
</html>
