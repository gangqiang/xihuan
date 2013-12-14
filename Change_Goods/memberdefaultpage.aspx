<%@ Page Language="C#" AutoEventWireup="true" CodeFile="memberdefaultpage.aspx.cs"
    Inherits="memberdefaultpage" EnableViewState="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>个人管理中心--管理中心首页</title>
    <style type="text/css">
    .td_new {
	border-top-width: 1px;
	border-right-width: 1px;
	border-bottom-width: 1px;
	border-left-width: 1px;
	border-bottom-style: dashed;
	border-top-color: #C5C5C5;
	border-right-color: #C5C5C5;
	border-bottom-color: #C5C5C5;
	border-left-color: #C5C5C5;
}
    .td_nav {
	border-top-width: 1px;
	border-right-width: 1px;
	border-bottom-width: 1px;
	border-left-width: 1px;
	border-bottom-style: dashed;
	border-top-color: #C5C5C5;
	border-right-color: #C5C5C5;
	border-bottom-color: #C5C5C5;
	border-left-color: #C5C5C5;
	border-top-style: dashed;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%if (UserInfoEntity != null)
              { %>
            <table width="98%" border="0" cellspacing="5" cellpadding="0" align="center">
                <tr>
                    <td align="left" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="height: 29;">
                                    <b>您好，<%=UserInfoEntity.UserName%></b></td>
                            </tr>
                        </table>
                        <table width="80%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="center" style="width: 90px;">
                                    <img id="headImage" style="width: 100px; height: 120px;" alt="<%=UserInfoEntity.UserName %>的头像"
                                        src="<%=UserInfoEntity.HeadImage.Length>0 ? UserInfoEntity.HeadImage:"images/nophoto.gif" %>" />
                                </td>
                                <td>
                                    <table width="100%" border="0" cellspacing="3" cellpadding="3">
                                        <tr>
                                            <td align="right">
                                                用 户 名：</td>
                                            <td>
                                                <%=UserInfoEntity.UserName%>
                                            </td>
                                            <td align="right">
                                                性 别：</td>
                                            <td>
                                                <%=BusinessFacade.CommonMethodFacade.FormatGender(UserInfoEntity.Gender,SrcRootPath)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                E - mail：</td>
                                            <td>
                                                <%=UserInfoEntity.Email%>
                                            </td>
                                            <td align="right">
                                                Q Q：</td>
                                            <td>
                                                <%=UserInfoEntity.QQ%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                MSN：</td>
                                            <td>
                                                <%=UserInfoEntity.Msn%>
                                            </td>
                                            <td align="right">
                                                电 话：</td>
                                            <td>
                                                <%=UserInfoEntity.TelePhone%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                换 币：</td>
                                            <td>
                                                <%=UserInfoEntity.HuanBi%>
                                            </td>
                                            <td align="right">
                                                积 分：</td>
                                            <td>
                                                <%=UserInfoEntity.Score%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                所在地区：</td>
                                            <td>
                                                <%=UserInfoEntity.ProvinceName%>
                                                ·<%=UserInfoEntity.CityName%>·<%=UserInfoEntity.AreaName%></td>
                                            <td align="right">
                                                学 校：</td>
                                            <td>
                                                <%=UserInfoEntity.SchoolName%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                实名认证：</td>
                                            <td>
                                                <a title="申请实名认证" href="###" onclick="alert('功能开发中！');">未认证，点此申请实名认证</a>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 90px">
                                </td>
                                <td style="height: 36px;">
                                    我的待处理事项：短消息 <a href="membermessage.aspx?type=receive"><span class="highlight">(<%=BusinessFacade.XiHuan_MessageFacade.GetNewMessageCount(UserInfoEntity.ID) %>)</span></a>&nbsp;
                                    留言 <a href="membernotes.aspx?type=receive"><span class="highlight">(<%=BusinessFacade.XiHuan_UserNotesFacade.GetNewNotesCount(UserInfoEntity.ID)%>)</span></a>&nbsp;
                                    交换请求 &nbsp;<a href="userequst.aspx?type=receive"><span class="highlight">(<%=BusinessFacade.XiHuan_ChangeRequireFacade.GetNewChangeRequireCount(UserInfoEntity.ID)%>)</span></a></td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 90px">
                                </td>
                                <td>
                                    我的喜换主页：<input id="homepage" style="width: 256px" type="text" value="<%=SrcRootPath %>xh.aspx?id=<%=UserInfoEntity.ID %>" />
                                    <input id="btnCopy" type="button" value="复制地址给好友" onclick="copydz();" /></td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 90px">
                                </td>
                                <td style="height: 36px; color: #3366cc;">
                                    &nbsp;您可以将主页地址复制到博客，常去的论坛等，让更多的人知道你的换品，更好的完成交换。</td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td style="width: 180px">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <a title="我的同城换" href="searchlist.aspx?province=<%=UserInfoEntity.ProvinceId %>&city=<%=UserInfoEntity.CityId %>"
                                                    target="_blank">
                                                    <img alt="我的同城换" src="images/button_town.gif" style="border: 0px;" width="117" height="30" /></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <a title="我的同校换" href="searchlist.aspx?province=<%=UserInfoEntity.ProvinceId %>&city=<%=UserInfoEntity.CityId %>&school=<%=UserInfoEntity.SchoolId %>"
                                                    target="_blank">
                                                    <img alt="我的同校换" src="images/button_school.gif" style="border: 0px;" width="117"
                                                        height="30" /></a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left">
                                    <br />
                                    <fieldset>
                                        <legend style="font-weight: bold;">
                                            <%if (UserInfoEntity.IsStarUser == 0)
                                              {%>
                                            <img alt="喜换网明星换客" src="images/user-star01.gif" width="41" height="24" />您还不是明星换客.请继续加油....
                                            <%} %>
                                            <% else
                                                { %>
                                            <img alt="喜换网明星换客" src="images/user-star.gif" width="41" height="24" />您已经成为明星换客.望再接再励....
                                            <%} %>
                                        </legend>
                                        <p>
                                            成为明星换客的条件：
                                            <br />
                                            <span class="highlight">1.登记10个以上的换品<br />
                                                2.成功交换达到5次以上</span><br />
                                            成为明星换客的优势：<br />
                                            <span class="highlight">1.明星换客将有机会在首页明星换客里出现<br />
                                                2.明星换客可以推荐换品到首页显示。</span>
                                        </p>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="td_nav">
                            <tr>
                                <td style="height: 36px;" align="center">
                                    <img alt="喜换网-修改个人信息" src="images/button_01.jpg" width="18" height="18" />
                                    <a title="修改个人信息" href="membermanageindex.aspx">修改个人信息</a></td>
                                <td align="center">
                                    <img alt="喜换网-换物管理" src="images/button_02.jpg" width="18" height="18" />
                                    <a title="换物管理" href="goodlist.aspx">换物管理</a></td>
                                <td align="center">
                                    <img alt="喜换网-我的收藏" src="images/button_03.jpg" width="18" height="18" />
                                    <a title="我的收藏" href="goodshistory.aspx">我的收藏</a></td>
                                <td align="center">
                                    <img alt="喜换网-诚信评价" src="images/button_04.jpg" width="18" height="18" />
                                    <a title="诚信评价" href="">诚信评价</a></td>
                                <td align="center">
                                    <img src="images/button_05.jpg" alt="喜换网-站内消息" width="18" height="18" /><a title="站内消息"
                                        href="membermessage.aspx?type=receive">站内消息</a></td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="right_title_bule01">
                            <tr>
                                <td style="height: 22px; background-color: #EBF5FF;" class="td_new">
                                    <img alt="喜换网寄语" src="images/notice.gif" width="18" height="16" />&nbsp;喜换网寄语</td>
                            </tr>
                            <tr>
                                <td style="height: 35px;">
                                    &nbsp;&nbsp;用心去换，易物交友，喜换网，喜欢就去换吧...物品交换，物尽其用，您没用的物品可能是别人的宝贝哦！</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <%} %>
        </div>
    </form>

    <script type="text/javascript" language="javascript"> 
function copydz() 
{ 
  textRange = $("#homepage")[0].createTextRange(); 
  textRange.execCommand("Copy");
  alert("地址已成功复制，您可以粘贴发给好友！"); 
} 
    </script>

</body>
</html>
