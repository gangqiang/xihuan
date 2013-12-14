<%@ Page Language="C#" AutoEventWireup="true" CodeFile="schoolchange.aspx.cs" Inherits="schoolchange"
    EnableViewState="false" EnableEventValidation="false" %>

<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>同校交换，校园易物，换客易物--校园换区</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:Top ID="Top1" runat="server" EnableViewState="false" />
            <div class="toplinebar">
            </div>
            <div class="cakeheadlink">
                当前位置：<a href="index.html">首页</a>>><a href="schoolchange.html">同校交换</a>>>学校列表&nbsp;&nbsp;<span
                    class="highlight">(使用Ctrl+F查找学校)</span>
            </div>
            <div class="main">
                <div class="mainleft" style="width: 95%;">
                    <asp:Repeater ID="rptProvince" runat="server" OnItemDataBound="rptProvince_ItemDataBound1">
                        <ItemTemplate>
                            <div class="title_menu" style="width: 100px; margin-left: 10px; margin-top: 5px;
                                clear: left;">
                                <a style="color: Black;" href="searchlist.aspx?changearea=school&province=<%#DataBinder.Eval(Container.DataItem,"provinceId",null) %>">
                                    <%#DataBinder.Eval(Container.DataItem,"province",null) %>
                                </a>
                            </div>
                            <div style="border-top: solid 1px #CCCCCC; margin-left: 10px; width: 98%;">
                            </div>
                            <div style="width: 98%; clear: left;">
                                <ul>
                                    <asp:Repeater ID="rptSchool" runat="server">
                                        <ItemTemplate>
                                            <li style="width: 130px; float: left;"><a title="<%#DataBinder.Eval(Container.DataItem,"SchoolName",null) %>"
                                                href="searchlist.aspx?changearea=school&province=<%#DataBinder.Eval(Container.DataItem,"ProvinceId",null) %>&city=<%#DataBinder.Eval(Container.DataItem,"CityId",null) %>&school=<%#DataBinder.Eval(Container.DataItem,"Id",null) %>">
                                                <%#DataBinder.Eval(Container.DataItem,"SchoolName",null) %>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </form>
    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />
</body>
</html>
