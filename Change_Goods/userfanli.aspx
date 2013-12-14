<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userfanli.aspx.cs" Inherits="userfanli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>淘宝返利--申请提现</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <fieldset style="padding: 10px; width: 90%; font-size: 12px; color: Black;">
                <legend style="font-weight: bold;">
                    <img src="images/pos.jpg" />&nbsp;申请提现&nbsp;</legend>
                <table width="100%" cellpadding="5" cellspacing="0" style="background-color: #efefef;">
                    <tr>
                        <td style="background-color: #ffffff;">
                            <br />
                            <table width="100%" border="0" style="text-align: center;" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right;">
                                        <span class="highlight">*</span>淘宝账号：
                                    </td>
                                    <td style="text-align: left; height: 25px;">
                                        <asp:TextBox ID="txtTaoBaoAccount" runat="server" Height="20px" Width="204px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right">
                                        <span class="highlight">*</span>提现金额：
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        <asp:TextBox ID="txtAmount" runat="server" Height="20px" Width="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right">
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Button ID="btnChange" runat="server" Text="申请提现" OnClientClick="return checkSubmit();"
                                            OnClick="btnChange_Click" />
                                    </td>
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
        function checkSubmit() {
            if ($.trim($("#txtTaoBaoAccount").val()).length == 0) {
                alert("请您填写申请提现的淘宝账号！");
                $("#txtTaoBaoAccount").focus();
                return false;
            }

            if ($.trim($("#txtAmount").val()).length == 0) {
                alert("请您输入提现金额！");
                $("#txtAmount").focus();
                return false;
            }

        }
    </script>

</body>
</html>
