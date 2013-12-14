<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usermanage.aspx.cs" Inherits="usermanage" %>

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
                    <td style="text-align: right;">
                        换客名：</td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtUserName" runat="server" Width="227px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="left" colspan="3">
                        <asp:Button ID="btnQuery" runat="server" Text="查询密码" OnClick="btnQuery_Click" /></td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="left" colspan="3">
                        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidId" runat="server" Value="0" />
        <asp:LinkButton ID="lnkLockUser" runat="server" OnClick="lnkLockUser_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkUnLockUser" runat="server" OnClick="lnkUnLockUser_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript">
     function LockUser(id)
     {
         if(confirm("确定要锁定此账号吗？"))
         {
            $("#hidId").val(id);
            __doPostBack('lnkLockUser','');
         }
     }
     function UnLockUser(id)
     {
         if(confirm("确定要解除锁定此账号吗？"))
         {
            $("#hidId").val(id);
            __doPostBack('lnkUnLockUser','');
         }
     }
     
    </script>

</body>
</html>
