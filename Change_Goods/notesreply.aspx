<%@ Page Language="C#" AutoEventWireup="true" CodeFile="notesreply.aspx.cs" Inherits="notesreply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>回复短消息</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 20px;">
            <table class="serachtable">
                <colgroup>
                    <col width="60" />
                    <col />
                </colgroup>
                <tr>
                    <td align="right">
                        回复内容：</td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtContent" runat="server" Rows="8" TextMode="MultiLine" Width="85%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="center" colspan="3">
                        <asp:Button ID="btnSend" runat="server" Text="立即回复" OnClientClick="if($F('txtContent').length==0) {alert('请输入回复内容！');return false;}return true;" OnClick="btnSend_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
