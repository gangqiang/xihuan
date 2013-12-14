<%@ Page Language="C#" AutoEventWireup="true" CodeFile="modifypwd.aspx.cs" Inherits="modifypwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>个人管理中心-修改密码</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <fieldset style="padding: 10px; width: 90%; font-size: 12px; color: Black;">
                    <legend style="font-weight: bold;">
                        <img src="images/pos.jpg" />&nbsp;修改密码&nbsp;</legend>
                    <table width="100%" cellpadding="5" cellspacing="0" style="background-color: #efefef;">
                        <tr>
                            <td style="background-color: #ffffff;">
                                <br />
                                <table width="100%" border="0" style="text-align: center;" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="right" class="font_item" style="width: 88px; text-align: right;">
                                            <span class="highlight">*</span>原密码：</td>
                                        <td style="text-align: left; height: 25px;">
                                            <asp:TextBox ID="txtOldPassWord" runat="server" Height="20px" TextMode="Password"
                                                Width="204px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="font_item" style="width: 88px; text-align: right">
                                            <span class="highlight">*</span>新密码：</td>
                                        <td style="height: 30px; text-align: left">
                                            <asp:TextBox ID="txtNewPassWord" runat="server" Height="20px" TextMode="Password"
                                                Width="204px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="font_item" style="width: 88px; text-align: right">
                                            <span class="highlight">*</span>重复新密码：</td>
                                        <td style="height: 30px; text-align: left">
                                            <asp:TextBox ID="txtNewPassWord2" runat="server" Height="20px" TextMode="Password"
                                                Width="204px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="font_item" style="width: 88px; text-align: right">
                                        </td>
                                        <td style="height: 30px; text-align: left">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:Button ID="btnConfirm" runat="server" Text="确认修改密码" OnClientClick="return checkSubmit();"
                                                OnClick="btnConfirm_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </center>
        </div>
    </form>

    <script type="text/javascript">
     function checkSubmit()
     {     
         if($.trim($("#txtOldPassWord").val()).length==0)
         {
            alert("请您填写原密码！");
            $("#txtOldPassWord").focus();
            return false;
         }
         
         if($.trim($("#txtNewPassWord").val()).length==0)
         {
            alert("请您输入新密码！");
            $("#txtNewPassWord").focus();
            return false;
         }
          if($.trim($("#txtNewPassWord2").val()).length==0)
         {
            alert("请您再次输入新密码！");
            $("#txtNewPassWord2").focus();
            return false;
         }
         if($.trim($("#txtNewPassWord").val())!=$.trim($("#txtNewPassWord2").val()))
         {
            alert("两次输入的新密码不一致！");
            $("#txtNewPassWord2").focus();
            return false; 
         }
    }
    </script>

</body>
</html>
