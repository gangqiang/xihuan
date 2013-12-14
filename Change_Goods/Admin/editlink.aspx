<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editlink.aspx.cs" Inherits="editlink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 20px;">
            <table class="serachtable">
                <colgroup>
                    <col width="80" />
                    <col width="80" />
                    <col width="80" />
                    <col width="80" />
                </colgroup>
                <tr>
                    <td align="right">
                        链接名称：</td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtLinkName" runat="server" Width="227px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">
                        链接地址：</td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtLinkUrl" runat="server" Width="227px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">
                        鼠标提示：</td>
                    <td align="center" colspan="3">
                        <asp:TextBox ID="txtAlt" runat="server" Width="223px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">
                        排 序 值：</td>
                    <td align="center" colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtSort" runat="server" Width="50px"></asp:TextBox>(输入数字，越小越靠前)</td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="center" colspan="3" >
                        <asp:Button ID="btnSave" runat="server" Text=" 保 存 " OnClick="btnSave_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
