<%@ Page Language="C#" AutoEventWireup="true" CodeFile="membermanageindex.aspx.cs"
    Inherits="membermanageindex" EnableEventValidation="false" %>

<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>喜换网-个人管理中心-注册信息修改</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <fieldset>
                <legend>
                    <img alt="更换头像" src="images/pos.jpg" />&nbsp;更换头像</legend>
                <table width="100%" cellpadding="5" cellspacing="1" style="background-color: #efefef;">
                    <tr>
                        <td style="background-color: #ffffff; width: 90px;" align="center">
                            <asp:Image runat="server" ID="headPic" Width="100" Height="120" />
                        </td>
                        <td style="background-color: #ffffff;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" style="height: 21px">
                                        <asp:FileUpload ID="headImage" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        头像宽高100*100效果最佳，大小50k以内,<br />
                                        头像格式：.jpg,.gif,.jpeg,.png
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <span id="errmsg" style="color: Red;"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>
                    <img src="images/pos.jpg" />&nbsp;基本资料</legend>
                <table width="100%" cellpadding="5" cellspacing="0" style="background-color: #efefef;">
                    <tr>
                        <td style="background-color: #ffffff;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right;">
                                        换客名：
                                    </td>
                                    <td width="81%" style="text-align: left; height: 25px;">
                                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right;">
                                        E-mail：
                                    </td>
                                    <td style="text-align: left; height: 25px;">
                                        <asp:TextBox ID="txtEmail" runat="server" Height="20px" Width="200px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right;">
                                        性 别：
                                    </td>
                                    <td style="text-align: left; height: 25px;">
                                        <asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Value="1">男</asp:ListItem>
                                            <asp:ListItem Value="0">女</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right;">
                                        所在地区：
                                    </td>
                                    <td style="text-align: left; height: 32px;">
                                        <ajax:AjaxPanel ID="AjaxPanel" runat="server">
                                            <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlArea" runat="server">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right;">
                                        所在学校：
                                    </td>
                                    <td style="text-align: left; height: 30px;">
                                        <asp:DropDownList ID="ddlSchool" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                </ajax:AjaxPanel>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right">
                                        安全提问问题：
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        <asp:TextBox ID="txtQuestion" runat="server" Height="20px" Width="200px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; text-align: right">
                                        安全提问答案：
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        <asp:TextBox ID="txtAnswer" runat="server" Height="20px" Width="200px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>
                    <img src="images/pos.jpg" />&nbsp;联系方式</legend>
                <table width="100%" cellpadding="5" cellspacing="0" style="background-color: #efefef;">
                    <tr>
                        <td style="background-color: #ffffff;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px">
                                        联系电话：
                                    </td>
                                    <td style="text-align: left">
                                        <input name="txtMobel" type="text" maxlength="50" id="txtTel" style="width: 197px;
                                            height: 20px;" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px">
                                        旺旺：
                                    </td>
                                    <td style="text-align: left">
                                        <input name="txtWangWang" type="text" maxlength="50" id="txtWangWang" style="width: 197px;
                                            height: 20px;" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px">
                                        MSN：
                                    </td>
                                    <td style="text-align: left">
                                        <input name="txt_msn" type="text" id="txt_msn" style="width: 197px; height: 20px;"
                                            runat="server" maxlength="50" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; height: 18px">
                                        QQ：
                                    </td>
                                    <td style="text-align: left;">
                                        <input name="txt_qq" type="text" id="txt_qq" style="width: 197px; height: 20px;"
                                            runat="server" maxlength="50" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="font_item" style="width: 88px; height: 24px">
                                        其它联系方式：
                                    </td>
                                    <td style="height: 24px; text-align: left">
                                        <asp:TextBox ID="txtOtherLink" runat="server" MaxLength="50" Width="197px" Height="20px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>
                    <img src="images/pos.jpg" />&nbsp;换铺公告</legend>
                <asp:TextBox ID="txtSingnNote" runat="server" Width="633px" Height="20px" MaxLength="200"></asp:TextBox>
                <div style="text-align: left; color: Gray;">
                    <br />
                    &nbsp;&nbsp;换铺公告将在您的换铺上显示。</div>
                <br />
                <input type="submit" name="btn_submitinfo" runat="server" value="提交修改" id="btn_submitinfo"
                    onserverclick="btn_submitinfo_ServerClick" />
            </fieldset>
        </center>
    </div>
    </form>

    <script type="text/javascript">
        function ShowImg(src) {
            var hf = CheckImageHz(".jpg,.gif,.jpeg,.png", src);
            if (!hf) {
                alert("头像格式不正确！");
                return;
            }
            else {
                $("#headPic").attr("src", src);
            }
        } 
    </script>

</body>
</html>
