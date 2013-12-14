<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register"
    EnableEventValidation="false" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc4" %>
<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>换客免费注册</title>

    <script type="text/javascript" language="javascript">
     $(document).ready(function(){
     $("#txtUserName").bind("blur",function(){
     if($.trim($("#txtUserName").val()).length==0)
     {
        alert("请输入换客名！");return false;
     }else{checkusername(escape($.trim($("#txtUserName").val())));}
     });
     });
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="lnkSubmit">
    <div>
        <center>
            <uc1:Top ID="Top1" runat="server" />
            <div class="toplinebar">
            </div>
            <fieldset style="width: 90%;">
                <legend>
                    <img src="images/signimage.gif" />当前位置--换客注册</legend>
                <table align="center" width="840" border="0" style="text-align: center; margin-top: 5px;"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: center;">
                            <table style="width: 90%;" border="0" cellspacing="0" cellpadding="6">
                                <tr>
                                    <td style="height: 35px; text-align: right;">
                                        <span><span class="highlight">*</span>换客名：</span>
                                    </td>
                                    <td>
                                        <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 32%; text-align: left; height: 37px;">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtUserName" runat="server" Height="20px" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td align="left" class="top_font_gray" style="height: 37px; width: 37%;">
                                                    <img alt="" src="images/standard_msg_warning.gif" />
                                                    5-20个字符，注册后将不能修改
                                                    <div style="color: Red; display: none;" id="reg_msg">
                                                        <img alt="" src="images/loading.gif" />
                                                        正在检测换客名......
                                                    </div>
                                                </td>
                                                <td style="width: 34%; height: 37px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 35px; text-align: right;">
                                        <span class="highlight">*</span>密码：
                                    </td>
                                    <td>
                                        <table style="width: 100%; border: 0px;" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 32%; text-align: left;">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtPassWord" TextMode="Password" runat="server" Height="20px" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td align="left" class="top_font_gray">
                                                    <img alt="" src="images/standard_msg_warning.gif" />
                                                    输入密码
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 35px; text-align: right;">
                                        <span class="highlight">*</span>再次输入密码：
                                    </td>
                                    <td style="height: 59px">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 32%;" align="left">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtPassWord2" TextMode="Password" runat="server" Height="20px" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td align="left" class="top_font_gray">
                                                    <img alt="" src="images/standard_msg_warning.gif" />
                                                    确认你上面输入的密码
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 35px; text-align: right;">
                                        电子邮件：
                                    </td>
                                    <td>
                                        <table style="width: 100%; border: 0px;" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 32%; text-align: left;">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtEmail" runat="server" Height="20px" MaxLength="50" Width="167px"></asp:TextBox>
                                                </td>
                                                <td align="left" class="top_font_gray">
                                                    <img alt="" src="images/standard_msg_warning.gif" />
                                                    为了及时与对方沟通,请填写常用邮件
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 35px; text-align: right;">
                                        <span class="highlight">*</span>性别：
                                    </td>
                                    <td colspan="2" align="left">
                                        <table id="Rad_sex" border="0">
                                            <tr>
                                                <td>
                                                    &nbsp;<input id="Rad_sex_0" checked="true" runat="server" type="radio" name="Rad_sex"
                                                        value="1" /><label for="Rad_sex_0"><img alt="" src='images/male.gif' />
                                                            帅哥</label>
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;<input id="Rad_sex_1" runat="server" type="radio" name="Rad_sex" value="2" /><label
                                                        for="Rad_sex_1"><img src='images/female.gif' alt="" />
                                                        美女</label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 35px; text-align: right;">
                                        <span class="highlight">*</span>目前所在地：
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <ajax:AjaxPanel ID="AjaxPanel" runat="server">
                                            <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlArea" runat="server">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp; &nbsp;&nbsp;<img alt="" src="images/standard_msg_warning.gif" />&nbsp;
                                            <span class="top_font_gray">必选.请认真填写</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 35px; text-align: right;">
                                        学校：
                                    </td>
                                    <td align="left" style="vertical-align: middle">
                                        &nbsp;&nbsp;<asp:DropDownList ID="ddlSchool" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                </ajax:AjaxPanel>
                                <tr>
                                    <td style="height: 35px; text-align: right;">
                                        推荐会员：
                                    </td>
                                    <td align="left">
                                        <table style="width: 100%; border: 0px;" width="100%" border="0" cellspacing="0"
                                            cellpadding="0">
                                            <tr>
                                                <td style="width: 32%; text-align: left;">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtTuiJianMember" runat="server" Height="20px" MaxLength="50"></asp:TextBox>
                                                </td>
                                                <td style="text-align: left;" class="top_font_gray">
                                                    <img alt="" src="images/standard_msg_warning.gif" />&nbsp; 如果你是由本站会员推荐注册的，在此输入推荐人的<br />
                                                    &nbsp; &nbsp; &nbsp; 换客名，他将会得到喜换网送出的积分和换币
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 35px;">
                                        <span class="highlight">*</span>安全提问：
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:TextBox ID="txtQuestion" runat="server" Height="20px" MaxLength="50"></asp:TextBox>&nbsp;
                                        <img alt="" src="images/standard_msg_warning.gif" />&nbsp;用于找回密码的提问问题
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 35px;">
                                        <span class="highlight">*</span>安全回答：
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                        <asp:TextBox ID="txtAnswer" runat="server" Height="20px" MaxLength="50"></asp:TextBox>&nbsp;&nbsp;<img
                                            alt="" src="images/standard_msg_warning.gif" />&nbsp;<span class="top_font_gray">用此答案便于找回您的密码</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align: left;">
                                        <input id="chk_apply" type="checkbox" name="chk_apply" checked="checked" />
                                        我已阅读并接受&nbsp;<a href="news/2009/6/4/newsshow44.html" target="_blank">喜换网服务条款</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center; height: 50px; vertical-align: middle;">
                                        &nbsp;<img src="images/sign_up.gif" onclick="return checkSubmit();" style="cursor: hand;"
                                            id="btnRegister" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
    <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click"></asp:LinkButton>
    </form>
    <uc4:Butomm ID="Butomm1" runat="server" EnableViewState="false" />

    <script type="text/javascript" language="javascript">
     function checkSubmit()
     {   
         var username= $.trim($("#txtUserName").val());
         if(username.length==0)
         {
            alert("请您填写换客名！");
            $("#txtUserName").focus();
            return false;
         }
          if(username.length<4 || username.length>20)
         {
            alert("换客名应为4-20个字符！");
            $("#txtUserName").focus();
            return false;
         }
          if($.trim($("#txtPassWord").val()).length==0)
         {
            alert("请您填写密码！");
            $("#txtPassWord").focus();
            return false;
         }
         
         if($.trim($("#txtPassWord2").val()).length==0)
         {
            alert("请您再次输入密码！");
            $("#txtPassWord2").focus();
            return false;
         }
         
         if($.trim($("#txtPassWord").val())!=$.trim($("#txtPassWord2").val()))
         {
            alert("两次输入密码不一致！");
            $("#txtPassWord2").focus();
            return false; 
         }
         var inputemail=$.trim($("#txtEmail").val());
         if(inputemail.length>0)
         {  
            var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
            if(!myreg.test(inputemail))
            {
                alert("邮箱格式填写不正确！");
                $("#txtEmail").focus(); 
                return false;
            }
        }
        
         if($.trim($("#ddlProvince").val()).length==0)
         {
            alert("请您选择省份！");
            $("#ddlProvince").focus();
            return false;
         }
         
         if($.trim($("#ddlCity").val()).length==0)
         {
            alert("请您选择城市！");
            $("#ddlCity").focus();
            return false;
         }
         
         if($.trim($("#txtQuestion").val()).length==0)
         {
            alert("请您填写安全提问问题！");
            $("#txtQuestion").focus();
            return false;
         }
         if($.trim($("#txtAnswer").val()).length==0)
         {
            alert("请您填写安全提问答案！");
            $("#txtAnswer").focus();
            return false;
         }
//         if($.trim($("#txtVoucerCode").val()).length==0)
//         {
//            alert("请您填写验证码！");
//            $("#txtVoucerCode").focus();
//            return false;
//         }
         
         if(!$("#chk_apply")[0].checked)
         {
            alert("您必须先同意注册条款才能提交注册信息！");
            return false;
         }
         
          __doPostBack('lnkSubmit','');
     }    
    </script>

</body>
</html>
