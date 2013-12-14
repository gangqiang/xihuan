<%@ Page Language="C#" AutoEventWireup="true" CodeFile="membercenter.aspx.cs" Inherits="membercenter"
    EnableViewState="false" EnableEventValidation="false" %>

<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>换客个人管理中心--</title>

    <script type="text/javascript">
        $(document).ready(function() {
            $("div.SideItem").each(function() {
                $(this).next("ul").css("margin-left", "36px");
                $(this).next("ul").children("li").each(function() { $(this).bind("click", function() { $("#main").attr("src", this.id); }); });
                $(this).attr("title", "点击收缩展开菜单").bind("click", function() {
                    $(this).next("ul").toggle();
                });
            }); $("div.centerleft").each(function() { $(this).css("margin-top", "5px"); });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Top ID="Top1" runat="server" EnableViewState="false" />
        <img alt="喜换网--后台管理中心" src="images/title.jpg" style="width: 980px; margin-left: 5px;" />
        <div style="margin-top: 5px;">
            <div class="centerright">
                <iframe id="main" src="memberdefaultpage.aspx" frameborder="0" height="800px" width="100%"
                    scrolling="auto"></iframe>
            </div>
            <div class="centerleft">
                <div class="SideItem">
                    系统管理</div>
                <ul id="UlSystem">
                    <li><a href="###" title="退出登陆" onclick="__doPostBack('lnkQuit','');">退出登陆</a></li>
                    <li><a href="membercenter.aspx" title="回到个人管理中心首页">管理首页</a></li>
                    <li><a href="index.html" title="回到喜换网首页">网站首页</a></li>
                </ul>
            </div>
            <div class="centerleft">
                <div class="SideItem">
                    换品管理</div>
                <ul id="UlGoodds">
                    <li id="goodsadd.aspx"><a href="###" title="登记新的换品">登记新换品</a> </li>
                    <li id="goodlist.aspx"><a href="###" title="查看已有的换品">已有换品</a></li>
                    <li id="goodshistory.aspx"><a href="###" title="查看我收藏的换品">收藏的换品</a> </li>
                </ul>
            </div>
            <div class="centerleft">
                <div class="SideItem">
                    交换请求管理</div>
                <ul id="UlRequest">
                    <li id="userequst.aspx?type=receive"><a href="###" title="查看我收到的交换请求">我收到的请求</a></li>
                    <li id="userequst.aspx?type=send"><a href="###" title="查看我发起的交换请求">我发起的请求</a></li>
                </ul>
            </div>
            <div class="centerleft">
                <div class="SideItem">
                    个人资料管理</div>
                <ul id="UlPerson">
                    <li id="membermanageindex.aspx"><a title="修改注册资料" href="###">修改注册资料</a></li>
                    <li id="modifypwd.aspx"><a title="修改登录密码" href="###">修改登录密码</a></li>
                    <li id="goodfriends.aspx"><a title="查看和管理我的好友" href="###">我的好友</a></li>
                </ul>
            </div>
            <div class="centerleft">
                <div class="SideItem">
                    短消息和留言管理</div>
                <ul id="UlMessage">
                    <li id="membermessage.aspx?type=receive"><a title="查看我收到的短消息" href="###">我收到的短消息</a></li>
                    <li id="membermessage.aspx?type=send"><a title="查看我发出的短消息" href="###">我发出的短消息</a></li>
                    <li id="membernotes.aspx?type=receive"><a title="查看我收到的留言" href="###">我收到的留言</a></li>
                    <li id="membernotes.aspx?type=send"><a title="查看我发出的留言" href="###">我发出的留言</a></li>
                </ul>
            </div>
            <div class="centerleft">
                <div class="SideItem">
                    淘宝返利</div>
                <ul id="UlFanli">
                    <li id="userfanli.aspx"><a title="申请淘宝返利提现" href="###">申请提现</a></li>
                </ul>
            </div>
            <div class="centerleft">
                <div class="SideItem">
                    评价管理</div>
                <ul id="UlPJ">
                    <li id="usereply.aspx?state=notreply"><a title="未评价的交换" href="###">未评价的交换</a></li>
                    <li id="usereply.aspx?state=tomine"><a title="别人对我的评价" href="###">别人对我的评价</a></li>
                    <li id="usereply.aspx?state=toother"><a title="我对别人的评价" href="###">我对别人的评价</a></li>
                </ul>
            </div>
        </div>
    </div>
    <asp:LinkButton runat="server" ID="lnkQuit" OnClick="lnkQuit_Click"></asp:LinkButton>
    </form>
    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />
</body>
</html>
