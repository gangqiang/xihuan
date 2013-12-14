<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loginiframe.aspx.cs" Inherits="loginiframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>换客登陆-</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="vertical-align: middle; margin-top: 5px;">
            <center>
                <table class="serachtable">
                    <tr>
                        <td style="text-align: right;">
                            <span class="highlight">*</span> 换客名：</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtUserName" runat="server" Height="20px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <span class="highlight">*</span> 密码：</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Height="20px" Width="151px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left;">
                            <input id="chkAutoLogin" runat="server" type="checkbox" />两周内自动登录
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left;">
                            <asp:Button ID="btnLogin" runat="server" Text=" 登 录 " OnClick="btnLogin_Click1" OnClientClick="return checkLogin();" />
                            &nbsp;&nbsp;&nbsp;&nbsp;<a href="###" onclick="parent.location='register.aspx';">现在就去注册</a>
                            &nbsp;&nbsp;&nbsp; <a href="###" title="喜换网-找回密码" onclick="parent.ymPrompt.win('findpass.aspx',500,300,'喜换网-找回密码',null,null,null,{id:'f'});">
                                忘记密码？</a>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>

    <script type="text/javascript" language="javascript">
    function checkLogin()
    {
        if($.trim($("#txtUserName").val()).length==0)
        {
            alert("请输入您的换客名！");
            $("#txtUserName").focus();
            return false;
        }
          if($.trim($("#txtPwd").val()).length==0)
        {
            alert("请输入您的换客名！");
            $("#txtPwd").focus();
            return false;
        }
    }
    </script>

</body>
</html>
