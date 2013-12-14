<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sys_news.aspx.cs" Inherits="Admin_sys_news" %>

<%@ Register Src="../UC/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="centerlisttable">
                <colgroup>
                    <col />
                    <col width="200" />
                    <col width="70" />
                    <col width="40" />
                    <col width="80" />
                </colgroup>
                <tr class="thead" style="text-align: center;">
                    <td>
                        标题</td>
                    <td>
                        内容</td>
                    <td>
                        所属分类</td>
                    <td>
                        排序</td>
                    <td>
                        操作</td>
                </tr>
                <tbody>
                    <asp:Repeater ID="rptNews" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem,"Title",null) %>
                                </td>
                                <td>
                                    <a href="../<%#DataBinder.Eval(Container.DataItem,"NewsUrl",null) %>" target="_blank">
                                        <%#CommonMethod.GetSubString(DataBinder.Eval(Container.DataItem, "NewsUrl", null),20,"...")%>
                                    </a>
                                </td>
                                <td>
                                    <%#Enum.GetName(typeof(BusinessFacade.XiHuan_NewsFacade.NewsType),DataBinder.Eval(Container.DataItem,"Type")) %>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem,"SortNumber",null) %>
                                </td>
                                <td>
                                    <a href="###" onclick="ymPrompt.win('editnews.aspx?id=<%# DataBinder.Eval(Container.DataItem,"Id",null)%>',680,550,'修改新闻信息',null,null,null,{id:'c'});"">
                                        修改</a> <a href="###" onclick="DelNews('<%#DataBinder.Eval(Container.DataItem,"Id",null) %>');">
                                            删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="5">
                            <span style="float: left;">
                                <input id="Button2" type="button" value="新增新闻" onclick="ymPrompt.win('editnews.aspx',680,550,'新增新闻',null,null,null,{id:'b'});" /></span>
                            <span style="float: right;">
                                <uc1:PageControl ID="PageControl2" runat="server" />
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <asp:HiddenField ID="hidNewsId" runat="server" />
        <asp:LinkButton ID="lnkDelNews" runat="server" OnClick="lnkDelNews_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript">
    
     function DelNews(id)
    {
        if(confirm("确定要删除此新闻吗？"))
        {
            $("#hidNewsId").val(id);
            __doPostBack('lnkDelNews','');
        }
    }
    </script>

</body>
</html>
