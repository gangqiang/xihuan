<%@ Page Language="C#" AutoEventWireup="true" Inherits="shownews" CodeFile="shownews.aspx.cs"
    EnableEventValidation="false" EnableViewState="false" ValidateRequest="false"
    EnableViewStateMac="false" %>

<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Top ID="Top1" runat="server" />
        <div class="toplinebar">
        </div>
        <div class="cakeheadlink">
            当前位置：<a href="<%=SrcRootPath %>">首页</a>>><asp:Label ID="lblType" runat="server"></asp:Label>>><asp:Label
                ID="lblTitle2" runat="server"></asp:Label>
        </div>
        <div class="main">
            <div class="mainleft">
                <table style="width: 85%; margin-top: 10px; margin-bottom: 10px;">
                    <tr>
                        <td style="height: 30px">
                            <h2>
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px; text-align: center;">
                            <span class="graytext">
                                <asp:Label ID="lblTime" runat="server"></asp:Label></span> 字体：【<a href="javascript:void(0);"
                                    onclick="SetSize(16);">大</a> <a href="javascript:void(0);" onclick="SetSize(14);">中</a>
                            <a href="javascript:void(0);" onclick="SetSize(12);">小</a>】 浏览：<span class="highlight"
                                id="lblViewCount"></span>&nbsp;次
                        </td>
                    </tr>
                    <tr>
                        <td id="tdContent" style="font-size: 14px; text-align: left;">
                            &nbsp;&nbsp;
                            <asp:Label ID="lblContent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; height: 30px; background-color: #fffeed;">
                            <br />
                            上一篇：<asp:Label ID="lblPre" runat="server" Text=""></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; 下一篇：<asp:Label ID="lblNext" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 85%; margin-top: 10px; margin-bottom: 10px;">
                    <tr>
                        <td>
                            <span id="YAD2"></span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="ritem">
                浏览排行</div>
            <div class="mainright">
                <div class="content">
                    <ul>
                        <asp:Repeater ID="rptHotNews" runat="server">
                            <ItemTemplate>
                                <li style="width: 170px; text-align: left;"><a title="<%#DataBinder.Eval(Container.DataItem,"Title",null)%>"
                                    target="<%#DataBinder.Eval(Container.DataItem,"NewsUrl",null).ToLower().IndexOf("http")>-1 ? "_blank":"_self" %>"
                                    href="<%# DataBinder.Eval(Container.DataItem,"NewsUrl",null).ToLower().IndexOf("http")>-1 ? DataBinder.Eval(Container.DataItem,"NewsUrl",null):SrcRootPath+DataBinder.Eval(Container.DataItem,"NewsUrl",null)%>">
                                    <%#CommonMethod.GetSubString(DataBinder.Eval(Container.DataItem,"Title",null),25,"...")%>
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li><span id="YAD"></span></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hidId" runat="server" />
    </form>

    <script type="text/javascript">
        LoadNewsCount($("#hidId").val());
        function SetSize(fixsize) {
            $("#tdContent").css("fontSize", fixsize + "px");
        }
        $("#YAD2").html("<iframe src=\"http://u.1133.cc/showpage.php?pid=141729\" MARGINWIDTH=0 MARGINHEIGHT=0  HSPACE=0 VSPACE=0 FRAMEBORDER=0 SCROLLING=no HEIGHT=100 WIDTH=730></iframe>");
        $("#YAD").html("<iframe src=\"http://u.1133.cc/showpage.php?pid=142220\" MARGINWIDTH=0 MARGINHEIGHT=0  HSPACE=0 VSPACE=0 FRAMEBORDER=0 SCROLLING=no HEIGHT=250 WIDTH=160></iframe>");
    </script>

    <script type="text/javascript" src="<%=SrcRootPath + "Js/scrolltopcontrol.js" %>"></script>

    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />

    <script src="http://u.1133.cc/showpage.php?pid=128913&show_t=2" language="javascript"></script>

</body>
</html>
