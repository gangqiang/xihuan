<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sendnotes.aspx.cs" Inherits="sendnotes" %>

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
                    <col width="80" />
                    <col width="60" />
                    <col width="80" />
                </colgroup>
                <tr>
                    <td align="right">
                        发送人：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtSender" Enabled="false" runat="server" Height="18px"></asp:TextBox></td>
                    <td align="right">
                        接收人：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtReceiver" runat="server" Height="18px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">
                        留言内容：</td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtContent" runat="server" Rows="6" TextMode="MultiLine" Width="75%"></asp:TextBox><br />
                        <br />
                        &nbsp;
                        <asp:CheckBox ID="chkSecret" Text="悄悄话" runat="server" /></td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="center" colspan="3">
                        <asp:Button ID="btnSend" runat="server" Text="发送留言" OnClientClick="return checkSubmit();"
                            OnClick="btnSend_Click" /></td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
     function checkSubmit()
     {
        if($.trim($("#txtReceiver").val()).length==0)
        {
            alert("请填写接收人！");
            $("#txtReceiver").focus();
            return false;
        }
        if($.trim($("#txtContent").val()).length==0)
        {
            alert("请填写留言内容！");
            $("#txtContent").focus();
            return false;
        }
        
        return true;
     }
    </script>

</body>
</html>
