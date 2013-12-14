<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changestate.aspx.cs" Inherits="changestate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="vertical-align: middle; margin-top: 5px;">
        <center>
            <table class="serachtable">
                <tr>
                    <td colspan="2" style="text-align: left">
                        更改交换状态：<asp:RadioButtonList ID="rbtFlag" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Selected="True" Value="1">考虑中</asp:ListItem>
                            <asp:ListItem Value="2">交换中</asp:ListItem>
                            <asp:ListItem Value="3">交换成功</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnChange" runat="server" Text=" 更改交换状态 " OnClick="btnLogin_Click1" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
