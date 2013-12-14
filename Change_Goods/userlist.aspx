<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userlist.aspx.cs" Inherits="userlist"
    EnableViewState="true" EnableEventValidation="false" %>

<%@ Register Src="UC/PageBar.ascx" TagName="PageBar" TagPrefix="uc3" %>
<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>换客秀</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Top ID="Top1" runat="server" />
        <div class="toplinebar">
        </div>
        <div class="cakeheadlink">
            当前位置：<a href="index.html">首页</a>>><a href="userlist.aspx">换客秀</a><asp:Label ID="lblLinkName"
                runat="server" Text=""></asp:Label>
            <table class="serachtable" id="GoodsSearch_Table" style="margin-top: 10px;">
                <tr>
                    <td>
                        换客名：<asp:TextBox ID="txtGoodName" runat="server" Height="18px"></asp:TextBox>&nbsp;&nbsp;性别：<asp:DropDownList
                            ID="ddlGender" runat="server">
                            <asp:ListItem Value="">不限</asp:ListItem>
                            <asp:ListItem Value="1">帅哥</asp:ListItem>
                            <asp:ListItem Value="0">美女</asp:ListItem>
                        </asp:DropDownList>
                        所在地：
                        <ajax:AjaxPanel ID="AjaxPanel" runat="server">
                            <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged1">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged1">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlArea" runat="server">
                            </asp:DropDownList>
                            学校：<asp:DropDownList ID="ddlSchool" runat="server">
                            </asp:DropDownList>
                            &nbsp;
                        </ajax:AjaxPanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        注册时间：<asp:DropDownList ID="ddlTime" runat="server" onchange="DoUserSearch();">
                            <asp:ListItem Value="">不限</asp:ListItem>
                            <asp:ListItem Value="0">近一周</asp:ListItem>
                            <asp:ListItem Value="1">近一月</asp:ListItem>
                            <asp:ListItem Value="2">近三个月</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; 排序方式：<asp:DropDownList ID="ddlOrderBy" runat="server" onchange="DoUserSearch();">
                            <asp:ListItem Value=" RegisterDate " Text="按注册时间降序"></asp:ListItem>
                            <asp:ListItem Value=" LastLoginTime " Text="按最后登录时间降序"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkHavePhoto" runat="server" Text="有头像" onclick="DoUserSearch();" />&nbsp;<asp:CheckBox
                            ID="chkStarUser" runat="server" Text="明星换客" onclick="DoUserSearch();" />&nbsp;
                        <input type="button" value="搜索换客" onclick="DoUserSearch();" />
                    </td>
                </tr>
            </table>
            <table class="searchlisttable">
                <colgroup>
                    <col width="70px" />
                    <col width="200px" />
                    <col />
                    <col width="150px" />
                    <col width="65px" />
                    <col width="100px" />
                </colgroup>
                <tr class="searchthead">
                    <td>
                        头像
                    </td>
                    <td>
                        换客名
                    </td>
                    <td>
                        所在地
                    </td>
                    <td>
                        注册信息
                    </td>
                    <td>
                        换品数量
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
                                    <a title="<%# DataBinder.Eval(Container.DataItem,"UserName",null)%>" href="xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"Id",null)%>"
                                        target="_blank">
                                        <img class="smallgoodimg" title="<%# DataBinder.Eval(Container.DataItem,"UserName",null)%>的头像"
                                            src="<%# DataBinder.Eval(Container.DataItem,"HeadImage",null)%>" />
                                    </a>
                                </td>
                                <td>
                                    <a title="<%# DataBinder.Eval(Container.DataItem,"UserName",null)%>" href="xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"Id",null)%>"
                                        target="_blank">
                                        <%# DataBinder.Eval(Container.DataItem,"UserName",null)%>
                                    </a>
                                    <%#BusinessFacade.XiHuan_UserFacade.IsCertNoChecked(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Id", null))) ? "<img title=\"通过实名认证\"  src=\" " + SrcRootPath + "images/user-name.gif \" />" : "<img title=\"未通过实名认证\" src=\" " + SrcRootPath + "images/user-name01.gif\" />"%>
                                    <%#BusinessFacade.XiHuan_UserFacade.IsStartUser(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Id", null))) ? "<img title=\"明星换客\" src=\" " + SrcRootPath + "images/user-star.gif\" />" : "<img title=\"不是明星换客\" src=\"" + SrcRootPath + "images/user-star01.gif\" />"%>
                                </td>
                                <td style="text-align: center;">
                                    <%# DataBinder.Eval(Container.DataItem,"ProvinceName",null)%>
                                    ·
                                    <%# DataBinder.Eval(Container.DataItem,"CityName",null)%>
                                    ·
                                    <%# DataBinder.Eval(Container.DataItem,"AreaName",null)%>
                                    <%# DataBinder.Eval(Container.DataItem,"SchoolName",null).Length>0? "<br/>学校："+DataBinder.Eval(Container.DataItem,"SchoolName",null):"" %>
                                </td>
                                <td>
                                    <span class="graytext">注册日期：<%# DataBinder.Eval(Container.DataItem,"RegisterDate","{0:yyyy-MM-dd}")%></span><br />
                                    <span class="graytext">最近登录：<%# DataBinder.Eval(Container.DataItem,"LastLoginTime","{0:yyyy-MM-dd}")%></span>
                                </td>
                                <td>
                                    <a title="点击查看他的换品" href="searchlist.aspx?ownername=<%#Microsoft.JScript.GlobalObject.escape( DataBinder.Eval(Container.DataItem,"UserName",null))%>&ownerid=<%#DataBinder.Eval(Container.DataItem,"Id",null) %>">
                                        <img title="拥有换品数量" src="images/click.gif" />
                                        <span class="bluetext">
                                            <%# DataBinder.Eval(Container.DataItem,"GoodsNumber",null)%>
                                        </span></a>
                                </td>
                                <td style="text-align: center;">
                                    <img style="cursor: pointer;" title="添加<%# DataBinder.Eval(Container.DataItem,"UserName",null)%>为好友"
                                        src="<%=SrcRootPath %>images/addFriend.gif" onclick="addFriend('<%#DataBinder.Eval(Container.DataItem,"Id",null) %>','<%# DataBinder.Eval(Container.DataItem,"UserName",null)%>');" />
                                    <img style="cursor: pointer;" title="给<%#DataBinder.Eval(Container.DataItem,"UserName",null) %>发站内信"
                                        src="<%=SrcRootPath %>images/email.gif" onclick="SendMessage('<%#DataBinder.Eval(Container.DataItem,"UserName",null) %>');" />
                                    <%#CommonMethod.FinalString(DataBinder.Eval(Container.DataItem, "WangWang", null)).Length > 0 ? string.Format(BusinessFacade.GlobalVar.SMALLSTRWW,Server.UrlEncode(DataBinder.Eval(Container.DataItem, "WangWang", null))):string.Empty%>
                                    <%#CommonMethod.FinalString(DataBinder.Eval(Container.DataItem, "QQ", null)).Length > 0 ?  string.Format(BusinessFacade.GlobalVar.QQSTR, DataBinder.Eval(Container.DataItem, "QQ", null)) : string.Empty%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tr class="highlightrow">
                    <td style="text-align: right;" colspan="8">
                        <uc3:PageBar ID="PageBar1" runat="server" OnPageChange="PageBar1_PageChanged" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />

    <script type="text/javascript">
     function DoUserSearch()
     {
        var url="userlist.aspx?keyword="+escape($("#txtGoodName").val())+"&gender="+$("#ddlGender").val()+"&province="+$("#ddlProvince").val()+"&city="+$("#ddlCity").val()+
                "&area="+$("#ddlArea").val()+"&school="+$("#ddlSchool").val()+"&time="+$("#ddlTime").val()+"&sort="+$("#ddlOrderBy").val()+"&ph="+$("#chkHavePhoto")[0].checked+"&type="+($("#chkStarUser")[0].checked?"star":"");
        window.location=url;
     }
    </script>

</body>
</html>
