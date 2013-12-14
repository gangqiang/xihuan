<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xh.aspx.cs" Inherits="xh"
    EnableViewState="false" %>

<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=GoodDetail!=null ? GoodDetail.UserName:string.Empty %>
        的 "喜换" 换铺--<%=BusinessFacade.SystemConfigFacade.Instance().WebSiteTitle%></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Top ID="Top1" runat="server" />
        <div class="cakeheadlink">
            当前位置：<a href="<%=SrcRootPath %>">首页</a>>><a href="<%=SrcRootPath %>xh.aspx?id=<%=CommonMethod.FinalString(Request["Id"])%>"><%=GoodDetail.UserName %>的
                "喜换" 换铺</a>
        </div>
        <center>
            <div class="toplinebar" style="background-image: url(App_Themes/Default/images/searchthead.gif);
                background-color: #EAEAEA; background-repeat: repeat-x; width: 98%; text-align: center;
                margin-top: 10px; margin-left: 10px; height: 150px; border: solid 1px #CCCCCC;">
                <h2 style="margin-top: 30px;">
                    <%=GoodDetail.UserName %>
                    的&nbsp;"喜换"&nbsp;换铺</h2>
                &nbsp;&nbsp;换铺公告：<%=CommonMethod.FinalString(GoodDetail.SignNote).Length > 0 ? "<marquee direction=\"left\" onmouseover=\"this.stop()\" onmouseout=\"this.start()\" scrollDelay=\"150\" width=\"300\"'> " + GoodDetail.SignNote + "</marquee>" : "换主最近太忙啦，还没来得及设置公告呐 ^_^ ！"%>
                <span style="float: right; margin-right: 5px;"><span class="highlight">换铺地址：</span>
                    <input id="homepage" readonly="readonly" style="width: 256px" type="text" value="<%=SrcRootPath %>xh.aspx?id=<%=CommonMethod.FinalString(Request["Id"]) %>" />
                    <input id="btnCopy" type="button" value="复制地址给好友" onclick="copydz();" /></span>
            </div>
            <div class="main">
                <div class="mainleft" style="float: right; width: 75%; height: auto;">
                    <table style="width: 98%; margin-top: 10px; margin-bottom: 10px;">
                        <tr>
                            <td>
                                <span class="title_menu">最新换品</span> <span style="float: right;"><a href="searchlist.aspx?ownername=<%=Microsoft.JScript.GlobalObject.escape(GoodDetail.UserName) %>&ownerid=<%=GoodDetail.ID %>">
                                    更多>></a></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="line_menu">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataList runat="server" ID="dlsHotGoods" RepeatColumns="6" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <table class="searchtable" style="margin: 3px 5px 0px 0px;">
                                            <tr>
                                                <td>
                                                    <a title="浏览次数<%#DataBinder.Eval(Container.DataItem,"ViewCount",null) %>" href="<%#DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>"
                                                        target="_blank">
                                                        <img class="smallgoodimg" title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %>,浏览次数<%#DataBinder.Eval(Container.DataItem,"ViewCount",null) %>"
                                                            src="<%#DataBinder.Eval(Container.DataItem,"DefaultPhoto",null) %>" /></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 35px;">
                                                    <a title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %>" href="<%#DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>"
                                                        target="_blank">
                                                        <%#CommonMethod.GetSubString(DataBinder.Eval(Container.DataItem,"Name",null),22,"..") %>
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="title_menu">收到的请求</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="line_menu">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="searchlisttable" style="margin-bottom: 5px;">
                                    <colgroup>
                                        <col width="100" />
                                        <col />
                                        <col width="120" />
                                        <col width="60" />
                                    </colgroup>
                                    <tr class="searchthead">
                                        <td>
                                            发起人
                                        </td>
                                        <td>
                                            请求内容
                                        </td>
                                        <td>
                                            发起时间
                                        </td>
                                        <td>
                                            状态
                                        </td>
                                    </tr>
                                    <tbody>
                                        <asp:Repeater ID="rptSend" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <a href="<%=SrcRootPath %>xh.aspx?id=<%#DataBinder.Eval(Container.DataItem,"SenderId",null) %>"
                                                            title="<%#DataBinder.Eval(Container.DataItem,"SenderName",null) %>" target="_blank">
                                                            <%#DataBinder.Eval(Container.DataItem,"SenderName",null) %>
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <%#BusinessFacade.XiHuan_UserNotesFacade.IsSceretNoteShow(DataBinder.Eval(Container.DataItem, "OwnerId", null), DataBinder.Eval(Container.DataItem, "SenderId", null), CurrentUserId.ToString(), DataBinder.Eval(Container.DataItem, "IsSecret", null)) ? DataBinder.Eval(Container.DataItem, "SenderName", null) + "想用" + DataBinder.Eval(Container.DataItem, "RequireDescribe", null) + " 换换主的" + DataBinder.Eval(Container.DataItem, "GoodsName", null) : "<span class=\"bluetext\">此请求为私有请求，换主和请求人可见。</span>"%>
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "RequireDate", "{0:yyyy-M-d H:m}")%>
                                                    </td>
                                                    <td>
                                                        <%#BusinessFacade.XiHuan_ChangeRequireFacade.FormatState(DataBinder.Eval(Container.DataItem,"Flag",null),"receive")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="title_menu">发起的请求</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="line_menu">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="searchlisttable" style="margin-bottom: 5px;">
                                    <colgroup>
                                        <col width="100" />
                                        <col />
                                        <col width="120" />
                                        <col width="60" />
                                    </colgroup>
                                    <tr class="searchthead">
                                        <td>
                                            换客名
                                        </td>
                                        <td>
                                            请求内容
                                        </td>
                                        <td>
                                            发起时间
                                        </td>
                                        <td>
                                            状态
                                        </td>
                                    </tr>
                                    <tbody>
                                        <asp:Repeater ID="rptRequire" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <a href="<%=SrcRootPath %>xh.aspx?id=<%#DataBinder.Eval(Container.DataItem,"SenderId",null) %>"
                                                            title="<%#DataBinder.Eval(Container.DataItem,"SenderName",null) %>" target="_blank">
                                                            <%#DataBinder.Eval(Container.DataItem,"SenderName",null) %>
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <%#BusinessFacade.XiHuan_UserNotesFacade.IsSceretNoteShow(DataBinder.Eval(Container.DataItem, "OwnerId", null), DataBinder.Eval(Container.DataItem, "SenderId", null), CurrentUserId.ToString(), DataBinder.Eval(Container.DataItem, "IsSecret", null)) ? DataBinder.Eval(Container.DataItem, "SenderName", null) + "想用" + DataBinder.Eval(Container.DataItem, "RequireDescribe", null) + " 换" + string.Format("<a href=\"xh.aspx?id={0}\" target=\"_blank\">{1}</a>", DataBinder.Eval(Container.DataItem, "OwnerId", null), DataBinder.Eval(Container.DataItem, "OwnerName", null)) + "的" + DataBinder.Eval(Container.DataItem, "GoodsName", null) : "<span class=\"bluetext\">此请求为私有请求，换主和请求人可见。</span>"%>
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "RequireDate", "{0:yyyy-M-d H:m}")%>
                                                    </td>
                                                    <td>
                                                        <%#BusinessFacade.XiHuan_ChangeRequireFacade.FormatState(DataBinder.Eval(Container.DataItem,"Flag",null),"send")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="title_menu">收到的留言</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="line_menu">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="searchlisttable">
                                    <colgroup>
                                        <col width="100" />
                                        <col />
                                        <col width="120" />
                                    </colgroup>
                                    <tr class="searchthead">
                                        <td>
                                            换客名
                                        </td>
                                        <td>
                                            留言内容
                                        </td>
                                        <td>
                                            留言时间
                                        </td>
                                    </tr>
                                    <tbody>
                                        <asp:Repeater ID="rptUserNotes" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <a href="<%=SrcRootPath %>xh.aspx?id=<%#DataBinder.Eval(Container.DataItem,"FromId",null) %>"
                                                            title="<%#DataBinder.Eval(Container.DataItem,"FromName",null) %>" target="_blank">
                                                            <%#DataBinder.Eval(Container.DataItem,"FromName",null) %>
                                                        </a>
                                                    </td>
                                                    <td>
                                                        <%#showNoteContent(BusinessFacade.XiHuan_UserNotesFacade.IsSceretNoteShow(DataBinder.Eval(Container.DataItem, "FromId", null), DataBinder.Eval(Container.DataItem, "ToId", null), CurrentUserId.ToString(), DataBinder.Eval(Container.DataItem, "IsScerect", null)), DataBinder.Eval(Container.DataItem, "FromName", null), DataBinder.Eval(Container.DataItem, "GoodsName", null), DataBinder.Eval(Container.DataItem, "GoodsId", null), DataBinder.Eval(Container.DataItem, "Content", null), DataBinder.Eval(Container.DataItem, "IsChecked", null))%>
                                                        <%#ShowReply(BusinessFacade.XiHuan_UserNotesFacade.IsSceretNoteShow(DataBinder.Eval(Container.DataItem, "FromId", null), DataBinder.Eval(Container.DataItem, "ToId", null), CurrentUserId.ToString(), DataBinder.Eval(Container.DataItem, "IsScerect", null)), DataBinder.Eval(Container.DataItem, "ReplyContent", null), DataBinder.Eval(Container.DataItem, "IsChecked", null))%>
                                                    </td>
                                                    <td>
                                                        <%#DataBinder.Eval(Container.DataItem,"CreateDate","{0:yyyy-M-d H:m}") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" style="height: 35px">
                                <span class="highlight">给<%=GoodDetail.UserName %>留言</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" style="height: 35px">
                                <span id="alreadylogin">您的换客名：<span class="highlight" id="username"></span> </span>
                                <span id="notlogin">用户名：<asp:TextBox ID="txtUserName" runat="server" Height="18px"></asp:TextBox>
                                    &nbsp; &nbsp;密码：<asp:TextBox ID="txtPwd" runat="server" Height="18px" TextMode="Password"
                                        Width="108px"></asp:TextBox></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <asp:TextBox ID="txtNoteContent" MaxLength="100" runat="server" Rows="5" TextMode="MultiLine"
                                    Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" style="height: 27px">
                                <asp:CheckBox ID="chkSecret" runat="server" ToolTip="发送悄悄话，留言只有你和换主可以看到" Text="发送悄悄话" />
                                &nbsp;&nbsp;<span class="dealingtext">请自觉遵守互联网相关的政策法规，严禁发布色情、暴力、反动的言论。</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" colspan="6">
                                <input id="btnAddNotes" type="button" value="提交留言" accesskey="S" onclick="check(0,'','<%=GoodDetail.ID %>','<%=GoodDetail.UserName %>','xh.aspx?id=<%=GoodDetail.ID %>');" />
                                (提示：可使用<span class="highlight">Alt+S</span>快捷键提交留言！)
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float: left; margin-left: 10px;">
                    <div class="ritem">
                        换主小档案</div>
                    <div class="mainright" style="clear: both;">
                        <table class="dangan" style="margin: 0 0 0 0; width: 185px;">
                            <tr>
                                <td style="text-align: center; margin-top: 5px; height: 115px; clear: both;" colspan="2">
                                    <img title="<%=GoodDetail.UserName %>的头像" src="<%=GoodDetail.HeadImage %>" width="100"
                                        height="100" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 15px; height: 30px;" colspan="2">
                                    &nbsp;&nbsp; <a href="<%=SrcRootPath %>xh.aspx?id=<%=GoodDetail.ID %>">
                                        <%=GoodDetail.UserName %>
                                    </a>
                                    <%=BusinessFacade.CommonMethodFacade.FormatGender(GoodDetail.Gender,SrcRootPath) %>
                                    <img style="cursor: pointer;" title="添加<%=GoodDetail.UserName %>为好友" src="<%=SrcRootPath %>images/addFriend.gif"
                                        onclick="addFriend('<%=GoodDetail.ID%>','<%=GoodDetail.UserName %>');" />&nbsp;
                                    <img style="cursor: pointer;" title="给<%=GoodDetail.UserName %>发站内信" src="<%=SrcRootPath %>images/email.gif"
                                        onclick="SendMessage('<%=GoodDetail.UserName %>');" />
                                    <%=CommonMethod.FinalString(GoodDetail.QQ).Length > 0 ? string.Format(BusinessFacade.GlobalVar.QQSTR,GoodDetail.QQ) : string.Empty%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="margin: 5px 0px 0px 0px;" valign="middle">
                                    &nbsp;&nbsp;
                                    <%=BusinessFacade.XiHuan_UserFacade.IsCertNoChecked(GoodDetail.ID) ? "<img title=\"通过实名认证\" src=\" " + SrcRootPath + "images/user-name.gif \" />" : "<img title=\"未通过实名认证\" src=\" " + SrcRootPath + "images/user-name01.gif\" />"%>
                                    <%=BusinessFacade.XiHuan_UserFacade.IsStartUser(GoodDetail.ID) ? "<img title=\"明星换客\" src=\" "+SrcRootPath+"images/user-star.gif\" />" : "<img title=\"不是明星换客\" src=\""+SrcRootPath+"images/user-star01.gif\" />"%>
                                    <%=CommonMethod.FinalString(GoodDetail.WangWang).Length>0 ? string.Format(BusinessFacade.GlobalVar.BIGSTRWW,GoodDetail.WangWang):string.Empty %>
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 15px;" colspan="2">
                                    &nbsp;&nbsp; 所在地：<%=GoodDetail.ProvinceName %>·<%=GoodDetail.CityName %>·<%=GoodDetail.AreaName %>
                                </td>
                            </tr>
                            <%if (CommonMethod.FinalString(GoodDetail.SchoolName).Length > 0)
                              { %>
                            <tr>
                                <td style="margin-left: 15px;" align="left" colspan="2">
                                    &nbsp;&nbsp; 学 &nbsp; 校：<%=GoodDetail.SchoolName %>
                                </td>
                            </tr>
                            <%} %>
                            <tr>
                                <td style="margin-left: 15px;" align="left" colspan="2">
                                    &nbsp;&nbsp; 换&nbsp;品：<%=GoodDetail.GoodsNumber %>
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 15px;" align="left" colspan="2">
                                    &nbsp;&nbsp; 积&nbsp; 分：<%=GoodDetail.Score %>
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 15px;" align="left" colspan="2">
                                    &nbsp;&nbsp; 换 币：<%=GoodDetail.HuanBi %>
                                    <img title="换币" src="<%=SrcRootPath %>images/qb.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="margin-left: 15px">
                                    &nbsp;&nbsp; 信 誉：<%--<%=GoodDetail.XinYu %>&nbsp;--%>
                                    <img alt="信誉" src="<%=SrcRootPath %>images/s-diamond-1.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 15px;" align="left" colspan="2">
                                    &nbsp;&nbsp; 好 评：<%=GoodDetail.GoodFeed %>
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 15px;" align="left" colspan="2">
                                    &nbsp;&nbsp; 注册时间：<%=GoodDetail.RegisterDate.ToString("yyyy-M-d H:m")%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="margin-left: 15px">
                                    &nbsp;&nbsp; 上次登录：<%=GoodDetail.LastLoginTime.ToString("yyyy-M-d H:m")%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="ritem" style="margin-top: 5px; clear: both;">
                        <%=GoodDetail.UserName %>
                        的好友
                    </div>
                    <div class="mainright" style="clear: both;">
                        <div class="content">
                            <ul>
                                <asp:Repeater runat="server" ID="rptGoodFriends">
                                    <ItemTemplate>
                                        <li style="width: 85px;"><a title="<%#DataBinder.Eval(Container.DataItem,"FriendName",null) %>"
                                            target="_blank" href="xh.aspx?id=<%#DataBinder.Eval(Container.DataItem,"FriendId",null) %>">
                                            <%#DataBinder.Eval(Container.DataItem,"FriendName",null) %>
                                        </a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                    <div class="ritem" style="margin-top: 5px; clear: both;">
                        最近访客
                    </div>
                    <div class="mainright" style="clear: both;">
                        <div id="ViewContent" class="content">
                            <ul>
                                <asp:Repeater runat="server" ID="rptVisitor">
                                    <ItemTemplate>
                                        <%#string.Format("<li style=\"clear:left;width:160px;text-align:left; margin-left:5px; margin-top:5px;\"><a title=\"来访时间{0}\" href=\"{1}\" target=\"_blank\"><img style=\"border: 0px; vertical-align: middle;\" title=\"{2}\" src=\"{3}\" width=\"50\" height=\"40\" />&nbsp;&nbsp;{4}</a></li>", DataBinder.Eval(Container.DataItem, "VisitDate", null), "xh.aspx?id=" + DataBinder.Eval(Container.DataItem, "VisitorId", null), DataBinder.Eval(Container.DataItem, "VisitorName", null),
                                                                                                                                                                                                         DataBinder.Eval(Container.DataItem, "VisitorHeadImage", null), DataBinder.Eval(Container.DataItem, "VisitorName", null)
                                        )%>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </center>
    </div>
    </form>
    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />

    <script type="text/javascript">
        function check(gid, gname, oid, oname, detailurl) {
            if ($("#notlogin").css("display") != "none") {
                if ($.trim($("#txtUserName").val()).length == 0) {
                    alert("请输入您的换客名！");
                    $("#txtUserName").focus();
                    return;
                }
                if ($.trim($("#txtPwd").val()).length == 0) {
                    alert("请输入您的密码！");
                    $("#txtPwd").focus();
                    return;
                }
            }
            if ($.trim($("#txtNoteContent").val()).length == 0) {
                alert("请您输入留言内容！");
                $("#txtNoteContent").focus();
                return;
            }
            else if ($.trim($("#txtNoteContent").val()).length > 200) {
                alert("留言内容不得超过200字！");
                $("#txtNoteContent").focus();
                return;
            }
            AddNotes(gid, gname, oid, oname, $("#txtUserName").val(), $("#txtPwd").val(), $("#txtNoteContent").val(), $("#chkSecret").checked, detailurl);
        }

        function copydz() {
            textRange = $("#homepage")[0].createTextRange();
            textRange.execCommand("Copy");
            alert("地址已成功复制，您可以粘贴发给好友！");
        }              
    </script>

</body>
</html>
