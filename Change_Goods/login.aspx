<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login"
    EnableEventValidation="false" %>

<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>换客登陆</title>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Top ID="Top" runat="server" EnableViewState="false" />
    <div style="margin-left: 98px;">
        <table border="0" cellspacing="0" cellpadding="0" align="left">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="806" align="center">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <table border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td colspan="3" align="center" valign="bottom">
                                                        <img src="images/Login_title.gif" width="324" height="36" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="22" align="right">
                                                        <img src="images/Login_left.gif" width="21" height="254" />
                                                    </td>
                                                    <td width="282" align="center" valign="middle" bgcolor="#FFFFF7">
                                                        <table border="0" cellspacing="0" cellpadding="6">
                                                            <tr>
                                                                <td align="center" valign="middle" style="height: 43px">
                                                                    换客名：
                                                                    <input type="text" id="username" runat="server" style="width: 130px; height: 20px;" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="middle" style="height: 43px">
                                                                    密 &nbsp; 码：
                                                                    <input type="password" runat="server" id="userpwd" style="width: 130px; height: 20px;" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="middle" style="height: 43px">
                                                                    <input runat="server" type="checkbox" id="autologin" title="为了您的信息安全请不要在网吧或公用电脑上使用此功能" />
                                                                    两周内自动登录
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    <table border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <input type="image" src="images/denglu.gif" width="84" height="38" onclick="return checkLogin();" />
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <img src="images/forget.gif" width="95" height="38" border="0" style="cursor: pointer;"
                                                                                    alt="喜换网-找回密码" onclick="ymPrompt.win('findpass.aspx',500,300,'喜换网-找回密码',null,null,null,{id:'a'});" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="middle" style="height: 43px">
                                                                    <a href="register.aspx">
                                                                        <img src="images/change_new.gif" width="158" height="26" border="0" /></a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="22" align="left">
                                                        <img src="images/Login_right.gif" width="21" height="254" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="center" valign="top">
                                                        <img src="images/Login_down.gif" width="322" height="30" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="15" align="left" valign="top">
                                            &nbsp;
                                        </td>
                                        <td valign="top">
                                            <table border="0" cellspacing="10" cellpadding="0">
                                                <tr>
                                                    <td align="right">
                                                        &nbsp;
                                                    </td>
                                                    <td valign="middle">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <img src="images/pic1.jpg" width="66" height="45" />
                                                    </td>
                                                    <td align="left" valign="middle">
                                                        <strong><a href="membercenter.aspx" class="orange">我的喜换</a></strong>
                                                        <br />
                                                        喜换网的个人管理中心，您可以在此登记物品信息完成物品交换。
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <img src="images/pic2.jpg" width="58" height="52" />
                                                    </td>
                                                    <td align="left" valign="middle">
                                                        <strong><a href="searchlist.aspx" class="orange">找换品</a></strong><br />
                                                        在众多换品中如何快速找到您想要的物品，喜换网的物品搜索器助您一臂之力！
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <img src="images/pic3.jpg" width="69" height="50" />
                                                    </td>
                                                    <td align="left" valign="middle">
                                                        <strong><a href="membercenter.aspx" class="orange">发送接收交换请求</a></strong>
                                                        <br />
                                                        当您找到感兴趣的物品时，可以发起交换请求给对方，<br />
                                                        别人发给你的交换请求也可以立即查看。
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <img src="images/pic4.jpg" width="52" height="50" />
                                                    </td>
                                                    <td align="left" valign="middle">
                                                        <strong><a href="membercenter.aspx" class="orange">我的收藏和短消息</a></strong>
                                                        <br />
                                                        您可以将您喜欢的物品收藏到收藏夹方便交换<br />
                                                        您还可以通过站内信和喜换网的换客们交流。
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table width="753" border="0" cellpadding="2" cellspacing="0">
                                    <tr>
                                        <td align="left">
                                            <br />
                                            <img src="images/light.gif" width="17" height="23" />
                                            <strong>喜换小贴士：</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="50" align="left" valign="middle">
                                            喜换网温馨提示：换客们在交换时要注意交易安全，以防上当受骗 ……<br />
                                            要时刻提高警惕，谨防交易骗局！尽量选择在公共场合完成物品交易！
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:LinkButton ID="lnkLogin" runat="server" OnClick="lnkLogin_Click"></asp:LinkButton>
    <uc4:Butomm ID="Buttom" runat="server" EnableViewState="false" />
    </form>

    <script language="javascript" type="text/javascript">
        function checkLogin() {
            if ($.trim($("#username").val()).length == 0) {
                alert("请输入您的换客名！");
                $("#username").focus();
                return false;
            }

            if ($.trim($("#userpwd").val()).length == 0) {
                alert("请输入您的密码！");
                $("#userpwd").focus();
                return false;
            }

            __doPostBack('lnkLogin', '');
        }
    </script>

</body>
</html>
