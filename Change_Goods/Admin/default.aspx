<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="adminlogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>喜换网--后台管理登录</title>
</head>
<body style="background-color: #CCE8CF; vertical-align: middle; text-align: center;"
    bgcolor="#cc8c00">
    <form id="form1" runat="server">
    <div style="background-color: #CCE8CF; height: 600px; margin-top: 200px; margin-left: 380px;
        *margin-left: auto;">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="height: 21px; text-align: right;">
                    用户名：
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td rowspan="3" style="vertical-align: middle; text-align: center">
                    <asp:Button ID="btnLogin" runat="server" Text=" 登 录 " OnClick="btnLogin_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    密 码：
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
