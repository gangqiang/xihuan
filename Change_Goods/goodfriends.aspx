<%@ Page Language="C#" AutoEventWireup="true" CodeFile="goodfriends.aspx.cs" Inherits="goodfriends" %>

<%@ Register Src="UC/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的好友管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <fieldset>
                    <legend>
                        <img title="查看和管理我的好友" src="images/notice.gif" />&nbsp;功能提示&nbsp;</legend>在这里可以查看和管理您的好友，喜欢网提醒您：在这里你不仅能换到你想要的物品，重要的是可以结识换友哦！
                </fieldset>
            </center>
            <center>
                <fieldset>
                    <legend>
                        <img title="我的好友列表" src="images/notice.gif" />我的好友列表&nbsp;</legend>
                    <table class="centerlisttable">
                        <colgroup>
                            <col width="100px" />
                            <col />
                            <col width="120" />
                            <col width="150" />
                        </colgroup>
                        <tr class="thead" style="text-align: center;">
                            <td>
                                好友名称
                            </td>
                            <td>
                                好友描述
                            </td>
                            <td>
                                添加时间
                            </td>
                            <td>
                                操作
                            </td>
                        </tr>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptGoodsList">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <a title="<%# DataBinder.Eval(Container.DataItem,"FriendName",null)%>" href="xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"FriendId",null)%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "FriendName", null)%>
                                            </a>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem,"FriendDesc",null)%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem,"AddDate",null)%>
                                        </td>
                                        <td>
                                            <a title="查看好友信息" href="xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"FriendId",null)%>"
                                                target="_blank">查看好友信息</a><a title="解除好友关系" href="###" onclick="DelFriends(<%# DataBinder.Eval(Container.DataItem,"Id",null)%>);">
                                                    解除好友关系</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tr class="highlightrow">
                            <td style="text-align: right;" colspan="5">
                                <uc1:PageControl ID="PageControl1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </center>
        </div>
        <asp:HiddenField ID="hidId" runat="server" Value="0" />
        <asp:LinkButton ID="lnkDelFriend" runat="server" OnClick="lnkDelFriend_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript" language="javascript">
     function DelFriends(id)
     {
        if(confirm("您确定要解除好友关系吗？"))
        {
            $("#hidId").val(id);
            __doPostBack('lnkDelFriend','');
        }
     }
    
    </script>

</body>
</html>
