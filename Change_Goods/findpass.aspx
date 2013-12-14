<%@ Page Language="C#" AutoEventWireup="true" CodeFile="findpass.aspx.cs" Inherits="findpass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="text-align: center; margin-top: 20px;">
                <table class="serachtable">
                    <tr>
                        <td align="right">
                            &nbsp;输入用户名：</td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            安全提问问题：</td>
                        <td colspan="3" align="left">
                            <asp:TextBox ID="txtQuestion" runat="server" Width="227px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            问题答案：</td>
                        <td align="center" colspan="3">
                            <asp:TextBox ID="txtAnswer" runat="server" Width="223px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            您的邮箱：</td>
                        <td align="center" colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtEmail" runat="server" Width="158px"></asp:TextBox><br />
                            （密码将会发送至您输入的邮箱）</td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td align="center" colspan="3">
                            <asp:Button ID="btnSave" runat="server" Text="找回密码" OnClick="btnSave_Click" OnClientClick="return check();" /></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>

    <script type="text/javascript">
     function check()
     {
        if($.trim($("#txtName").val()).length==0)
        {
            alert("请输入您的换客名！");
            $("#txtName").focus();
            return false;
        }
        if($.trim($("#txtQuestion").val()).length==0)
        {
            alert("请输入安全提问问题！");
            $("#txtQuestion").focus();
            return false;
        }
        if($.trim($("#txtAnswer").val()).length==0)
        {
            alert("请输入问题答案！");
            $("#txtAnswer").focus();
            return false;
        }
        if($.trim($("#txtEmail").val()).length==0)
        {
            alert("请输入您用于找回密码的邮箱！");
            $("#txtEmail").focus();
            return false;
        }
     }
    </script>

</body>
</html>
