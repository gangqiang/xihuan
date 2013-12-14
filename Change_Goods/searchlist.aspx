<%@ Page Language="C#" AutoEventWireup="true" CodeFile="searchlist.aspx.cs" Inherits="searchllist"
    EnableEventValidation="false" %>

<%@ Register Src="UC/PageBar.ascx" TagName="PageBar" TagPrefix="uc3" %>
<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>换品列表<%= title+"-"+BusinessFacade.SystemConfigFacade.Instance().WebSiteTitle%>
    </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Top ID="Top1" runat="server" />
        <div class="toplinebar">
        </div>
        <div class="cakeheadlink">
            当前位置：<a href="index.html">首页</a>>><a href="searchlist.aspx">换品列表</a><asp:Label ID="lblLinkName"
                runat="server" Text=""></asp:Label>
            <table class="serachtable" style="margin-top: 10px;">
                <tr>
                    <td>
                        换品名称：<asp:TextBox ID="txtGoodName" runat="server" Height="18px"></asp:TextBox>&nbsp;
                        <ajax:AjaxPanel ID="AjaxPanelType" runat="server">
                            换品类别：<asp:DropDownList ID="ddlGoodType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGoodType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlGoodChildType" runat="server">
                                <asp:ListItem Text="不限子类别" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ajax:AjaxPanel>
                        &nbsp;新旧程度：
                        <asp:DropDownList ID="ddlNewOldDeep" runat="server">
                            <asp:ListItem Value="">不限</asp:ListItem>
                            <asp:ListItem Value="10">全新</asp:ListItem>
                            <asp:ListItem Value="9">九成新</asp:ListItem>
                            <asp:ListItem Value="8">八成新</asp:ListItem>
                            <asp:ListItem Value="7">七成新</asp:ListItem>
                            <asp:ListItem Value="6">六成新</asp:ListItem>
                            <asp:ListItem Value="5">五成新</asp:ListItem>
                            <asp:ListItem Value="4">四成新</asp:ListItem>
                            <asp:ListItem Value="3">三成新</asp:ListItem>
                            <asp:ListItem Value="2">三成新以下</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        换主：<asp:TextBox ID="txtOwnerName" runat="server" Height="18px"></asp:TextBox>
                        &nbsp; 换品所在地：
                        <ajax:AjaxPanel ID="AjaxPanelArea" runat="server">
                            <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                <asp:ListItem Text="不限城市" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlArea" runat="server">
                                <asp:ListItem Text="不限地区" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            学校：
                            <asp:DropDownList ID="ddlSchool" runat="server">
                                <asp:ListItem Text="不限学校" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </ajax:AjaxPanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        登记日期：<asp:DropDownList ID="ddlTime" runat="server" onchange="DoSearchGoods();">
                            <asp:ListItem Value="">不限</asp:ListItem>
                            <asp:ListItem Value="0">近一周</asp:ListItem>
                            <asp:ListItem Value="1">近一月</asp:ListItem>
                            <asp:ListItem Value="2">近三个月</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; 排序方式：<asp:DropDownList ID="ddlOrderBy" runat="server" onchange="DoSearchGoods();">
                            <asp:ListItem Value=" CreateDate " Text="按登记时间降序"></asp:ListItem>
                            <asp:ListItem Value=" ViewCount " Text="按点击量降序"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkHavePhoto" runat="server" Text="有图片" onclick="DoSearchGoods();" />
                        <asp:CheckBox ID="chkShow" runat="server" Text="只显示可交换的换品" onclick="DoSearchGoods();" />
                        <input type="button" value="搜索换品" onclick="DoSearchGoods();" />
                    </td>
                </tr>
            </table>
            <table class="searchlisttable">
                <colgroup>
                    <col width="70px" />
                    <col width="152px" />
                    <col width="80px" />
                    <col width="120px" />
                    <col width="155px" />
                    <col />
                    <col width="80px" />
                </colgroup>
                <tr class="searchthead">
                    <td>
                        换品图片
                    </td>
                    <td>
                        换品名称
                    </td>
                    <td>
                        新旧程度
                    </td>
                    <td>
                        换主
                    </td>
                    <td>
                        所在地
                    </td>
                    <td>
                        交换意向
                    </td>
                    <td>
                        操作
                    </td>
                </tr>
                <tbody>
                    <asp:Repeater runat="server" ID="rptGoodsList" EnableViewState="false">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <a title="<%# DataBinder.Eval(Container.DataItem,"Name",null)%>" href="<%# DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>"
                                        target="_blank">
                                        <img class="smallgoodimg" title="<%# DataBinder.Eval(Container.DataItem,"Name",null)%>"
                                            src="<%# DataBinder.Eval(Container.DataItem,"DefaultPhoto",null)%>" />
                                    </a>
                                </td>
                                <td>
                                    <a title="<%# DataBinder.Eval(Container.DataItem,"Name",null)%>" href="<%# DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>"
                                        target="_blank">
                                        <%# DataBinder.Eval(Container.DataItem,"Name",null)%>
                                    </a>
                                    <img title="点击次数" src="images/click.gif" />
                                    <span class="bluetext">
                                        <%# DataBinder.Eval(Container.DataItem,"ViewCount",null)%>
                                    </span>
                                    <br />
                                    <br />
                                    <img title="登记时间" src="images/adddate.gif" />
                                    <%# DataBinder.Eval(Container.DataItem,"CreateDate","{0:yyyy-MM-dd}")%>
                                    [<%#BusinessFacade.XiHuan_UserGoodsFacade.FormatGoodsState(DataBinder.Eval(Container.DataItem, "GoodState", null), DataBinder.Eval(Container.DataItem, "IsChecked", null))%>]
                                </td>
                                <td>
                                    <%#BusinessFacade.XiHuan_UserGoodsFacade.FormatNewDeep(DataBinder.Eval(Container.DataItem,"NewDeep",null))%>
                                </td>
                                <td>
                                    <a title="点击进入<%# DataBinder.Eval(Container.DataItem,"OwnerName",null)%>的喜换主页" href="xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"OwnerId",null)%>"
                                        target="_blank">
                                        <%# DataBinder.Eval(Container.DataItem,"OwnerName",null)%>
                                    </a>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem,"ProvinceName",null)%>
                                    ·
                                    <%# DataBinder.Eval(Container.DataItem,"CityName",null)%>
                                    ·
                                    <%# DataBinder.Eval(Container.DataItem,"AreaName",null)%>
                                    <%# DataBinder.Eval(Container.DataItem,"SchoolName",null).Length>0? "<br/>学校："+DataBinder.Eval(Container.DataItem,"SchoolName",null):"" %>
                                </td>
                                <td>
                                    <%#ChangeRequire(DataBinder.Eval(Container.DataItem, "HopeToChangeTypeId", null),DataBinder.Eval(Container.DataItem, "HopeToChangeChildTypeId", null),DataBinder.Eval(Container.DataItem, "OnlyCityChange", null) ,DataBinder.Eval(Container.DataItem, "OnlySchoolChange", null))%>
                                </td>
                                <td style="text-align: center;">
                                    <a href="<%# DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>" title="现在就去交换">
                                        <img title="现在就去交换" style="cursor: pointer;" src="images/button_01.jpg" /></a>
                                    &nbsp;&nbsp;
                                    <img id="imgFavorite" style="cursor: pointer;" title="添加到我的收藏" src="images/Favorite.jpg"
                                        onclick="addFavorite('<%# DataBinder.Eval(Container.DataItem,"Id",null)%>','<%# DataBinder.Eval(Container.DataItem,"Name",null)%>');" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tr class="highlightrow">
                    <td style="text-align: right;" colspan="8">
                        <uc3:PageBar ID="PageBar1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />

    <script type="text/javascript">
        function DoSearchGoods() {
            var url = "searchlist.aspx?keyword=" + escape($("#txtGoodName").val()) + "&typeid=" + $("#ddlGoodType").val() + "&childid=" + $("#ddlGoodChildType").val() +
                "&newdeep=" + $("#ddlNewOldDeep").val() + "&ownername=" + escape($("#txtOwnerName").val()) + "&province=" + $("#ddlProvince").val() + "&city=" + $("#ddlCity").val() +
                "&area=" + $("#ddlArea").val() + "&school=" + $("#ddlSchool").val() + "&time=" + $("#ddlTime").val() + "&sort=" + $("#ddlOrderBy").val() + "&ph=" + $("#chkHavePhoto")[0].checked + "&sc=" + $("#chkShow")[0].checked;
            window.location = url;
        }
    </script>

</body>
</html>
