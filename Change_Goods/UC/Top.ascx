<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Top.ascx.cs" Inherits="UC_Top"
    EnableViewState="false" %>
<%@ OutputCache Duration="360000" VaryByParam="none" %>

<script type="text/javascript">
function doSearch()
{
    var type=$("#search_type").val();
    var key= $("#keyword").val();
    var typeid=$("#<%=ddlGoodType.ClientID %>").val();
    if(type=="0")
    {
        window.location="<%=ParentPage.SrcRootPath%>searchlist.aspx?keyword="+escape(key)+"&typeid="+typeid;
    }
    if(type=="1")
    {
       window.location="<%=ParentPage.SrcRootPath%>userlist.aspx?keyword="+escape(key); 
    }
}
</script>

<div id="Head">
    <div class="logo" style="float: left; margin: 5px 10px 5px 10px;">
        <a href="<%=ParentPage.SrcRootPath%>" title="喜换网-物品交换,闲置物品互换,二手物品调剂">
            <img src="<%=ParentPage.SrcRootPath%>images/logo.png" title="喜换网-物品交换,闲置物品互换,二手物品调剂" /></a>
        <a href="###" class="legend"><span title="喜换网-换我喜欢">换我喜欢</span></a><br />
        <span id="needlogin">您好，欢迎来到喜换网！<a href="<%=ParentPage.SrcRootPath%>register.aspx"
            title="免费注册成为喜换网会员">[免费注册]</a> <a href="<%=ParentPage.SrcRootPath%>login.aspx" title="登录喜换网">
                [登录]</a></span> <span id="welcomemember"><span id="lblUserName"></span></span>
    </div>
    <div style="float: right; margin: 20px 60px 5px 10px;">
        <div>
            <select name="search_type" id="search_type">
                <option value="0" selected="selected">搜索换品</option>
                <option value="1">搜索换客</option>
            </select>
            <input type="text" name="q" id="keyword" style="height: 17px; width: 180px;" class="keyword"
                value="" />
            <asp:DropDownList ID="ddlGoodType" runat="server">
            </asp:DropDownList>
            <input type="button" value="搜  索" id="btnSearch" onclick="doSearch();" />
            <a href="<%=ParentPage.SrcRootPath%>searchlist.aspx">高级搜索</a> <a href="<%=ParentPage.SrcRootPath%>news/2009/6/3/newsshow36.html"
                target="_blank">使用帮助</a>
        </div>
    </div>
    <div class="nav-box box" style="clear: both; margin-top: 5px; margin-left: 3px; width: 98%;">
        <span class="rc-tp"><span></span></span><span style="font-size: 0px; z-index: 99;
            background: url(http://www.tsc8.com/images/hotnew.gif) no-repeat 100% 0px; left: 250px; overflow: hidden;
            width: 19px; line-height: 0px; position: absolute; top: -10px; height: 23px">热 </span>
        <div class="bd">
            <dl class="chanel">
                <dd>
                    <ul>
                        <li id="MenuHome"><a title="回到喜换网首页" href="<% =ParentPage.SrcRootPath%>" target="_top">
                            <span>首页</span></a> </li>
                        <li id="MenuMall"><a title="喜换网-同城交换频道" href="<%=ParentPage.SrcRootPath%>citychange.html"
                            target="_top"><span>同城交换</span></a> </li>
                        <li id="MenuGlobal"><a title="喜换网-同校交换频道" href="<%=ParentPage.SrcRootPath%>schoolchange.html"
                            target="_top"><span>同校交换</span></a> </li>
                     <%--   <li id="MenuSecondHand"><a href="<%=ParentPage.SrcRootPath%>taobao.htm"
                            target="_blank"><span>淘宝返利</span></a> </li>--%>
                        <li id="MenuGift"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx" target="_top">
                            <span>全部交换</span></a> </li>
                        <li id="MenuSale"><a href="<%=ParentPage.SrcRootPath%>userlist.aspx" target="_top"><span>
                            换客秀</span></a> </li>
                    </ul>
                </dd>
            </dl>
            <!--热门城市-->
            <dl class="news">
                <dt>城市</dt>
                <dd>
                    <ul>
                        <li id="MenuMan"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?province=110000"
                            target="_top"><span>北京</span></a> </li>
                        <li id="MenuLady"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?province=310000"
                            target="_top"><span>上海</span></a></li>
                        <li id="MenuBaby"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?province=120000"
                            target="_top"><span>天津</span></a> </li>
                        <li id="MenuFashion"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?city=440300"
                            target="_top"><span>深圳</span></a> </li>
                        <li id="MenuBeauty"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?city=440100"
                            target="_top"><span>广州</span></a> </li>
                        <li id="MenuShishang"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?city=420100"
                            target="_top"><span>武汉</span></a> </li>
                        <li id="Li2"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?city=320500" target="_top">
                            <span>苏州</span></a> </li>
                        <li id="MenuDigital"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?city=210200"
                            target="_top"><span>大连</span></a> </li>
                        <li id="MenuSport"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?city=410100"
                            target="_top"><span>郑州</span></a> </li>
                        <li id="Li3"><a href="<%=ParentPage.SrcRootPath%>searchlist.aspx?city=370200" target="_top">
                            <span>青岛</span></a> </li>
                        <li id="Li1"><a href="<%=ParentPage.SrcRootPath%>citychange.html" target="_top"><span>
                            >更多</span></a> </li>
                    </ul>
                </dd>
            </dl>
        </div>
    </div>
    <!-- nav-box end -->
</div>
