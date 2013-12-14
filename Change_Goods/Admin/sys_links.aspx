<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sys_links.aspx.cs" Inherits="Admin_sys_links" %>

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
                    <col width="150" />
                    <col width="50" />
                    <col width="80" />
                </colgroup>
                <tr class="thead" style="text-align: center;">
                    <td>
                        链接名称</td>
                    <td>
                        链接地址</td>
                    <td>
                        鼠标提示</td>
                    <td>
                        排序</td>
                    <td>
                        操作</td>
                </tr>
                <tbody>
                    <asp:Repeater ID="rptLinks" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem,"Name",null) %>
                                </td>
                                <td>
                                    <a href="<%#DataBinder.Eval(Container.DataItem,"Url",null) %>" target="_blank">
                                        <%#DataBinder.Eval(Container.DataItem,"Url",null) %>
                                    </a>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem,"Alt",null) %>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem,"Sort",null) %>
                                </td>
                                <td>
                                    <a href="###" onclick="ymPrompt.win('editlink.aspx?id=<%# DataBinder.Eval(Container.DataItem,"Id",null)%>',500,300,'修改链接信息',null,null,null,{id:'a'});"">
                                        修改</a> <a href="###" onclick="DelLink('<%#DataBinder.Eval(Container.DataItem,"Id",null) %>');">
                                            删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="5">
                            <span style="float: left;">
                                <input id="btnNewLink" type="button" value="新增友情链接" onclick="ymPrompt.win('editlink.aspx',500,300,'修改链接信息',null,null,null,{id:'a'});" />
                            </span><span style="float: right;">
                                <uc1:PageControl ID="PageControl1" runat="server" />
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <asp:HiddenField ID="hidLinkId" runat="server" />
        <asp:LinkButton ID="lnkDelLink" runat="server" OnClick="lnkDelLink_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript">
    function DelLink(id)
    {
        if(confirm("确定要删除此链接吗？"))
        {
            $("#hidLinkId").val(id);
            __doPostBack('lnkDelLink','');
        }
    }
    </script>

</body>
</html>
