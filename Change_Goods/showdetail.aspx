<%@ Page Language="C#" AutoEventWireup="true" Inherits="showdetail" CodeFile="showdetail.aspx.cs"
    EnableEventValidation="false" EnableViewState="false" ValidateRequest="false"
    EnableViewStateMac="false" %>

<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看换品详细信息</title>
</head>
<body>
    <form id="form1" runat="server">
    <%if (GoodDetail != null)
      {%>
    <div>
        <uc1:Top ID="Top1" runat="server" />
        <div class="toplinebar">
        </div>
        <div class="cakeheadlink">
            当前位置：<a href="<%=SrcRootPath %>">首页</a>>><a href="<%=SrcRootPath %>searchlist.aspx?typeid=<%=GoodDetail.TypeId %>"><%=BusinessFacade.XiHuan_UserGoodsFacade.GetTypeNameById(GoodDetail.TypeId.ToString()) %></a>>><%=GoodDetail.Name %>
        </div>
        <div class="main">
            <div class="mainleft">
                <table style="width: 98%; margin-top: 10px; margin-bottom: 10px;">
                    <colgroup>
                        <col width="220px" />
                        <col width="80px" />
                        <col />
                        <col width="230px" />
                    </colgroup>
                    <tr>
                        <td colspan="6" style="height: 25px">
                            <h2>
                                <%=GoodDetail.Name %>
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" rowspan="6" valign="middle" style="height: 240px">
                            <%if (GoodDetail.DefaultPhoto != null && GoodDetail.DefaultPhoto.Trim().Length > 0 && !GoodDetail.DefaultPhoto.Trim().Equals("images/none.jpg"))
                              {%>
                            <div class="highslide-gallery">
                                <a id="defaultphoto" class="highslide" onclick="<%=GoodDetail.DefaultPhoto.Contains("none.jpg")? "":"return hs.expand(this);" %>"
                                    href="<%=SrcRootPath+GoodDetail.DefaultPhoto.Replace(BusinessFacade.GlobalVar.DefaultPhotoSize,"") %>"
                                    target="_blank">
                                    <img id="defaultimg" title="点击查看大图" alt="点击查看大图" src="<%=SrcRootPath+GoodDetail.DefaultPhoto.Replace(BusinessFacade.GlobalVar.DefaultPhotoSize,BusinessFacade.GlobalVar.BigPhotoSize) %>" />
                                </a>
                                <div class="highslide-heading">
                                    换品默认图片
                                </div>
                                <asp:Repeater ID="rptGoodsImage" runat="server">
                                    <ItemTemplate>
                                        <a style="display: none;" class="highslide" onclick="return hs.expand(this)" href="<%#SrcRootPath+DataBinder.Eval(Container.DataItem,"ImgSrc") %>">
                                            <img title="点击查看大图" alt="点击查看大图">
                                        </a>
                                        <div class="highslide-heading">
                                            浏览换品图片
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <%}
                              else
                              { %>
                            <img style="border: 0px; width: 200px; height: 220px;" title="换主未传图片" alt="换主未传图片" src="<%=SrcRootPath+GoodDetail.DefaultPhoto.Replace(BusinessFacade.GlobalVar.DefaultPhotoSize,BusinessFacade.GlobalVar.BigPhotoSize) %>" />
                            <%} %>
                        </td>
                        <td>
                            登记时间：
                        </td>
                        <td align="left">
                            <%=GoodDetail.CreateDate %>
                        </td>
                        <td align="left" colspan="3" rowspan="7" valign="middle">
                            <div class="rtitle">
                                换主小档案</div>
                            <table class="dangan">
                                <tr>
                                    <td style="margin-left: 15px; height: 30px;" colspan="2">
                                        &nbsp;&nbsp; <a href="<%=SrcRootPath %>xh.aspx?id=<%=GoodDetail.OwnerId %>" target="_blank">
                                            <%=GoodDetail.OwnerName %>
                                        </a>
                                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                                        <img style="cursor: pointer;" title="添加<%=GoodDetail.OwnerName %>为好友" src="<%=SrcRootPath %>images/addFriend.gif"
                                            onclick="addFriend('<%=GoodDetail.OwnerId %>','<%=GoodDetail.OwnerName %>');" />&nbsp;
                                        <img style="cursor: pointer;" title="给<%=GoodDetail.OwnerName %>发站内信" src="<%=SrcRootPath %>images/email.gif"
                                            onclick="SendMessage('<%=GoodDetail.OwnerName %>');" />
                                        <asp:Label ID="lblQQ" runat="server" EnableViewState="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="margin: 5px 0px 0px 0px;" valign="middle">
                                        &nbsp;&nbsp;
                                        <%=BusinessFacade.XiHuan_UserFacade.IsCertNoChecked(GoodDetail.OwnerId) ? "<img title=\"通过实名认证\" src=\" " + SrcRootPath + "images/user-name.gif \" />" : "<img title=\"未通过实名认证\" src=\" " + SrcRootPath + "images/user-name01.gif\" />"%>
                                        <%=BusinessFacade.XiHuan_UserFacade.IsStartUser(GoodDetail.OwnerId) ? "<img title=\"明星换客\" src=\" "+SrcRootPath+"images/user-star.gif\" />" : "<img title=\"不是明星换客\" src=\""+SrcRootPath+"images/user-star01.gif\" />"%>
                                        <asp:Label ID="lblWW" runat="server" EnableViewState="false"></asp:Label>
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
                                        &nbsp;&nbsp; 学 &nbsp; 校：<%= GoodDetail.SchoolName%>
                                    </td>
                                </tr>
                                <%} %>
                                <tr>
                                    <td style="margin-left: 15px;" align="left" colspan="2">
                                        &nbsp;&nbsp; 积&nbsp; 分：<asp:Label ID="lblScore" runat="server" Text="" EnableViewState="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="margin-left: 15px;" align="left" colspan="2">
                                        &nbsp;&nbsp; 换 币：<asp:Label ID="lblHB" runat="server" Text="" EnableViewState="false"></asp:Label>
                                        <img title="换币" src="<%=SrcRootPath %>images/qb.gif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="margin-left: 15px">
                                        &nbsp;&nbsp; 信 誉：<asp:Label ID="lblXY" runat="server" Text="" EnableViewState="false"
                                            Visible="false"></asp:Label>&nbsp;
                                        <img title="信誉" src="<%=SrcRootPath %>images/s-Diamond-1.gif" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="margin-left: 15px;" align="left" colspan="2">
                                        &nbsp;&nbsp; 好 评：<asp:Label ID="lblGoodFeed" runat="server" Text="" EnableViewState="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="margin-left: 15px;" align="left" colspan="2">
                                        &nbsp;&nbsp; 注册时间：<asp:Label ID="lblRegisterDate" runat="server" Text="" EnableViewState="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="margin-left: 15px">
                                        &nbsp;&nbsp; 上次登录：<asp:Label ID="lblLastLoginTime" runat="server" Text="" EnableViewState="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="margin-left: 15px">
                                        &nbsp;&nbsp; <a href="<%=SrcRootPath %>xh.aspx?id=<%=GoodDetail.OwnerId %>" target="_blank"
                                            title="查看换主的其他换品">
                                            <img style="border: 0px" title="查看换主的其他换品" src="<%=SrcRootPath %>images/button_other.gif" /></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            所在地：
                        </td>
                        <td align="left">
                            <%=GoodDetail.ProvinceName %>
                            ·<%=GoodDetail.CityName %>·<%=GoodDetail.AreaName %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            学&nbsp; 校：
                        </td>
                        <td align="left">
                            <%= GoodDetail.SchoolName %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            新旧程度：
                        </td>
                        <td align="left">
                            <%=BusinessFacade.XiHuan_UserGoodsFacade.FormatNewDeep(GoodDetail.NewDeep.ToString()) %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            浏览次数：
                        </td>
                        <td align="left">
                            <span id="viewcount">
                                <img title="正在读取数据，请稍候" src="<%=SrcRootPath%>images/loding.gif" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            换品状态：
                        </td>
                        <td align="left">
                            <%=BusinessFacade.XiHuan_UserGoodsFacade.FormatGoodsState(GoodDetail.GoodState.ToString(),GoodDetail.IsChecked.ToString())%>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle" style="height: 30px">
                            <a id="morephoto" target="_blank" style="cursor: pointer;">
                                <img title="查看换品图片" src="<%=SrcRootPath %>images/zoomin.gif" border="0" />
                                更多图片</a> &nbsp; &nbsp; <a href="###" onclick="addFavorite('<%=GoodDetail.Id %>','<%=GoodDetail.Name %>');">
                                    <img title="收藏此换品" src="<%=SrcRootPath %>images/Favorite.jpg" style="border: 0px;"
                                        onclick="addFavorite('<%=GoodDetail.Id %>','<%=GoodDetail.Name %>');" />收藏此换品</a>
                        </td>
                        <td colspan="2" align="left">
                            <%if (GoodDetail.GoodState != (byte)BusinessFacade.XiHuan_UserGoodsFacade.GoodsState.交换中 && GoodDetail.GoodState != (byte)BusinessFacade.XiHuan_UserGoodsFacade.GoodsState.交换成功)
                              {%>
                            <a href="###" onclick="GoodsExchange('<%=GoodDetail.OwnerId %>','<%=GoodDetail.OwnerName %>','<%=GoodDetail.Id %>','<%=GoodDetail.Name %>','<%=GoodDetail.DetailUrl %>');">
                                <img title="进行交换" src="<%=SrcRootPath %>images/button_change.gif" style="border: 0px;
                                    cursor: pointer;" /></a>
                            <%} %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <span id="YAD"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <span class="title_menu">交换要求</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="line_menu">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="6" style="height: 35px;" valign="middle">
                            &nbsp;&nbsp; <span class="inittext">类别要求：</span><%=BusinessFacade.XiHuan_UserGoodsFacade.GetTypeNameById(GoodDetail.HopeToChangeTypeId.ToString()) %>&nbsp;
                            &nbsp;
                            <%=BusinessFacade.XiHuan_UserGoodsFacade.GetSecondTypeNameById(GoodDetail.HopeToChangeTypeId.ToString(), GoodDetail.HopeToChangeChildTypeId.ToString())%>
                            <br />
                            &nbsp;&nbsp; <span class="inittext">交换条件：</span><%=GoodDetail.OnlyCityChange==1?"限同城交换":"" %>&nbsp;&nbsp;<%=GoodDetail.OnlySchoolChange==1?"限同校交换":"" %>
                            <br />
                            &nbsp;&nbsp; <span class="inittext">其他交换要求：</span><%=GoodDetail.HopeToChangeDesc %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <span class="title_menu">换品简介</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="line_menu">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="6" style="height: 35px;" valign="middle">
                            &nbsp;&nbsp;
                            <%=GoodDetail.Description %>
                            <br />
                            &nbsp;&nbsp;<span class="bluetext">联系我时请注明是从喜换网上看到的信息,以便于尽快达成交换。</span>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <span class="title_menu">收到的交换请求</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="line_menu">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <table class="searchlisttable" style="margin-bottom: 5px;">
                                <colgroup>
                                    <col width="100" />
                                    <col />
                                    <col width="120" />
                                    <col width="80" />
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
                        <td colspan="6">
                            <span class="title_menu">联系换主</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="line_menu">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="left" style="height: 55px;">
                            <asp:Label ID="linkMethod" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <span class="title_menu">留言板</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="line_menu">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center" valign="middle">
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
                                                    <%#BusinessFacade.XiHuan_UserNotesFacade.GetFinalNotesToShow(DataBinder.Eval(Container.DataItem, "FromId", null), DataBinder.Eval(Container.DataItem, "ToId", null), CurrentUserId.ToString(), DataBinder.Eval(Container.DataItem, "IsScerect", null),DataBinder.Eval(Container.DataItem, "Content", null),DataBinder.Eval(Container.DataItem, "ReplyContent", null),DataBinder.Eval(Container.DataItem, "IsChecked", null))%>
                                                </td>
                                                <td>
                                                    <%#DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-M-d H:m}")%>
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
                            <span class="highlight">给<%=GoodDetail.OwnerName %>留言</span>
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
                            <asp:TextBox ID="txtNoteContent" MaxLength="200" runat="server" Rows="8" TextMode="MultiLine"
                                Style="width: 98%;"></asp:TextBox>
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
                            <input id="btnAddNotes" type="button" accesskey="S" value="提交留言" onclick="check('<%=GoodDetail.Id %>','<%=GoodDetail.Name %>','<%=GoodDetail.OwnerId %>','<%=GoodDetail.OwnerName %>','<%=GoodDetail.DetailUrl %>');" />
                            (提示：可使用<span class="highlight">Alt+S</span>快捷键提交留言！)
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: center;">
                            <br />
                            查看上一换品：
                            <asp:Label ID="lblPre" runat="server"></asp:Label>
                            &nbsp; &nbsp;&nbsp;&nbsp; 查看下一换品：
                            <asp:Label ID="lblNext" runat="server"></asp:Label>
                            <br />
                            <br />
                            您当前正在查看换品：<span class="inittext"><%=GoodDetail.Name %>
                            </span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="ritem">
                <%=GoodDetail.OwnerName %>
                的热门换品</div>
            <div class="mainright">
                <div class="content">
                    <ul>
                        <asp:Repeater ID="rptHotGoods" runat="server">
                            <ItemTemplate>
                                <li><a title="<%#DataBinder.Eval(Container.DataItem,"Name",null)%>" href="<%#SrcRootPath+DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>">
                                    <img style="border: solid 1px #cccccc;" title="<%#DataBinder.Eval(Container.DataItem,"Name",null)%>"
                                        width="83" height="83" src="<%#SrcRootPath+DataBinder.Eval(Container.DataItem,"DefaultPhoto",null)%>" />
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="ritem" style="margin-top: 5px;">
                其他换客的相关换品</div>
            <div class="mainright">
                <div class="content">
                    <ul>
                        <asp:Repeater ID="rtpRelateGoods" runat="server">
                            <ItemTemplate>
                                <li><a title="<%#DataBinder.Eval(Container.DataItem,"Name",null)%>" href="<%#SrcRootPath+DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>">
                                    <img style="border: solid 1px #cccccc;" title="<%#DataBinder.Eval(Container.DataItem,"Name",null)%>"
                                        width="83" height="83" src="<%#SrcRootPath+DataBinder.Eval(Container.DataItem,"DefaultPhoto",null)%>" />
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="ritem" style="margin-top: 5px;">
                最近浏览过该换品的人
            </div>
            <div class="mainright">
                <div id="ViewContent" class="content">
                    <img title="正在读取数据，请稍候" src="<%=SrcRootPath%>images/loding.gif" />正在读取数据，请稍候...
                </div>
                <div class="content">
                    <ul>
                        <li id="YAD2"></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <%} %>
    </form>
    <%if (GoodDetail.DefaultPhoto != null && GoodDetail.DefaultPhoto.Trim().Length > 0 && !GoodDetail.DefaultPhoto.Trim().Equals("images/none.jpg"))
      {%>

    <script src="<%=SrcRootPath + "Js/highslide/highslide-with-gallery.js?v=2011-3-17" %>" type="text/javascript"></script>

    <script type="text/javascript">
        hs.graphicsDir = '<%=SrcRootPath %>Js/highslide/graphics/';
        hs.align = 'center';
        hs.transitions = ['expand', 'crossfade'];
        hs.outlineType = 'rounded-white';
        hs.wrapperClassName = 'controls-in-heading';
        hs.fadeInOut = true;
        hs.dimmingOpacity = 0.75;
        if (hs.addSlideshow) hs.addSlideshow({
            interval: 3000,
            repeat: false,
            useControls: true,
            fixedControls: false,
            overlayOptions: {
            opacity: 1,
            position: 'top right',
            hideOnMouseOut: false
            }
        });
    </script>

    <%} %>

    <script type="text/javascript">
        $(function() {
            $("#morephoto").bind("click", function() {
                $("#defaultphoto").trigger("click");
            });
        });
        LoadDetail(<%=GoodDetail.Id %>);
         $("#YAD2").html("<iframe src=\"http://u.1133.cc/showpage.php?pid=142220\" MARGINWIDTH=0 MARGINHEIGHT=0  HSPACE=0 VSPACE=0 FRAMEBORDER=0 SCROLLING=no HEIGHT=250 WIDTH=160></iframe>");
        ////<a href=\"http://shop61802477.taobao.com/\" target=\"_blank\"><img src=\"http://www.tsc8.com/taobao/760X90.gif\" style=\"border:0px;\" /></a>
         $("#YAD").html("<iframe src=\"http://u.1133.cc/showpage.php?pid=142226\" MARGINWIDTH=0 MARGINHEIGHT=0  HSPACE=0 VSPACE=0 FRAMEBORDER=0 SCROLLING=no HEIGHT=100 WIDTH=760></iframe>");
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
    </script>

    <script type="text/javascript" src="<%=SrcRootPath + "Js/scrolltopcontrol.js" %>"></script>

    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />

    <script type="text/javascript" src="http://china-addthis.googlecode.com/svn/trunk/addthis.js"
        charset="UTF-8"></script>

    <span class="addthis_org_cn" style="display: none;"><a href="http://addthis.org.cn/share/"
        i="0|1|31|22|23|28|42|48|49|21|36|44|46|2|4|43|39|45|37|40" side="left" abordercolor="#eda240"
        aheadbgcolor="#f7f5f2">
        <img src="http://addthis.org.cn/images/as1.gif" align="absmiddle" /></a></span>
</body>
</html>
