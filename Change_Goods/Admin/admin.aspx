<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="addschool"
    EnableEventValidation="false" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>喜换网-后台管理中心</title>

    <script type="text/javascript">
     $(document).ready(function(){
     $("div.SideItem").each(function(){$(this).next("ul").css("margin-left","36px");
     $(this).next("ul").children("li").each(function(){$(this).bind("click",function(){$("#main").attr("src",this.id);});});
     $(this).attr("title","点击收缩展开菜单").bind("click",function(){
     $(this).next("ul").toggle();
     });});$("div.centerleft").each(function(){$(this).css("margin-top","5px");});
     });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img alt="喜换网--后台管理中心" src="../images/title.jpg" style="width: 980px; margin-left: 5px;" />
            <div style="margin-top: 5px;">
                <div class=" centerright">
                    <iframe id="main" src="sys_set.aspx" frameborder="0" height="800px" width="100%"
                        scrolling="auto"></iframe>
                </div>
                <div class="centerleft">
                    <div class="SideItem">
                        系统管理</div>
                    <ul id="UlSystem">
                        <li><a href="###" title="退出登陆" onclick="__doPostBack('lnkQuit','');">退出登陆</a></li>
                        <li id="sys_set.aspx"><a href="###" title="系统设置">系统设置</a></li>
                        <li id="sys_links.aspx"><a href="###" title="友情链接管理">友情链接管理</a></li>
                        <li id="sys_news.aspx"><a href="###" title="新闻公告管理">新闻公告管理</a></li>
                        <li id="sys_goods.aspx"><a href="###" title="换品管理">换品管理</a></li>
                        <li id="sys_notes.aspx"><a href="###" title="留言审核">留言审核</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <asp:LinkButton runat="server" ID="lnkQuit" OnClick="lnkQuit_Click"></asp:LinkButton>
    </form>
</body>
</html>
