<%@ Page Language="C#" AutoEventWireup="true" CodeFile="goodsadd.aspx.cs" Inherits="goodsadd"
    EnableEventValidation="false" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登记换品</title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <center>
            <fieldset>
                <legend>
                    <img alt="登记换品" src="images/pos.jpg" />&nbsp;<asp:Label ID="TitleName" runat="server"
                        Text="登记换品"></asp:Label>&nbsp;</legend>
                <table width="100%" cellpadding="5" cellspacing="0" style="background-color: #efefef;">
                    <tr>
                        <td style="background-color: #ffffff;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="right" style="width: 100px; height: 36px; text-align: right">
                                    </td>
                                    <td style="height: 25px; text-align: left">
                                        <span class="greentext">重要通知：请自觉遵守互联网相关的政策法规，严禁任何违法、色情、暴力、反动的言论！一旦发现,我们将<%=BusinessFacade.SystemConfigFacade.Instance().IsGoodsAddNeedCheck ? "直接不予审核," : string.Empty%>直接删除，并永久禁用用户帐号！</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px; text-align: right; height: 36px;">
                                        <span class="highlight">*</span>换品名称：
                                    </td>
                                    <td width="81%" style="text-align: left; height: 25px;">
                                        <asp:TextBox ID="txtGoodName" runat="server" Height="20px" MaxLength="50" Width="358px"></asp:TextBox>
                                        &nbsp;
                                        <asp:CheckBox ID="chkTJ" CssClass="highlight" runat="server" Text="推荐到首页显示" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px; text-align: right; height: 36px;">
                                        <span class="highlight">*</span>换品类别：&nbsp;
                                    </td>
                                    <td style="text-align: left; height: 25px;">
                                        <ajax:AjaxPanel ID="AjaxPanelType" runat="server">
                                            <asp:DropDownList ID="ddlGoodType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGoodType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlGoodChildType" runat="server">
                                                <asp:ListItem Text="选择子类别" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </ajax:AjaxPanel>
                                    </td>
                                </tr>
                                <tr id="trimage" runat="server">
                                    <td align="right" style="width: 100px; text-align: right; height: 36px;">
                                        换品图片：&nbsp;
                                    </td>
                                    <td style="text-align: left; height: 25px;">
                                        <input id="rbtNo" onclick="ControlImage();" type="radio" value="0" name="gimage"
                                            runat="server" />无
                                        <input id="rbtYes" onclick="ControlImage();" type="radio" value="1" name="gimage"
                                            runat="server" checked="true" />有<span class="highlight">(为了更好的完成交换，建议您上传物品图片)</span>
                                    </td>
                                </tr>
                                <tr id="uploadimage" runat="server">
                                    <td align="right" style="width: 100px; height: 36px; text-align: right">
                                        上传换品图片：
                                    </td>
                                    <td style="height: 25px; text-align: left">
                                        <br />
                                        <span class="highlight">(每张图片大小不超过500k,图片格式.jpg,.jpeg,.gif,.png,不符合条件的将不会上传！)</span>
                                        <span id="addImage">
                                            <br />
                                            <input type="file" name="goodimage" id="goodimagefile0" style="height: 20px; width: 320px;"
                                                onchange="checkImage(this.value,this.id);" />&nbsp;<a href="###" onclick="addImage();">添加图片</a>
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px; text-align: right; height: 36px;">
                                        <span class="highlight">*</span>换品简介：
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        <FCKeditorV2:FCKeditor ID="txtGoodDesc" runat="server" BasePath="fckeditor/" ToolbarSet="Basic">
                                        </FCKeditorV2:FCKeditor>
                                        <span class="highlight">(不超过1000字！)</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px; text-align: right; height: 36px;">
                                        <span class="highlight">*</span>新旧程度：
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        <asp:DropDownList ID="ddlNewOldDeep" runat="server">
                                            <asp:ListItem Value="10">全新</asp:ListItem>
                                            <asp:ListItem Value="9">九成新</asp:ListItem>
                                            <asp:ListItem Value="8">八成新</asp:ListItem>
                                            <asp:ListItem Value="7">七成新</asp:ListItem>
                                            <asp:ListItem Value="6">六成新</asp:ListItem>
                                            <asp:ListItem Value="5">五成新</asp:ListItem>
                                            <asp:ListItem Value="4">四成新</asp:ListItem>
                                            <asp:ListItem Value="3">三成新</asp:ListItem>
                                            <asp:ListItem Value="2">三成新以下</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px; text-align: right; height: 36px;">
                                        交换条件：
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        <span class="highlight">
                                            <asp:CheckBox runat="server" ID="chkValidCity" Text="限同城交换" />
                                            <asp:CheckBox runat="server" ID="chkValidSchool" Text="限同校交换" /></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px; text-align: right; height: 36px;">
                                        想交换物品类别：
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        <ajax:AjaxPanel ID="AjaxType1" runat="server">
                                            <asp:DropDownList ID="ddlGoodType1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGoodType1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlGoodChildType1" runat="server">
                                            </asp:DropDownList>
                                        </ajax:AjaxPanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px; text-align: right; height: 36px;">
                                        其他交换要求：
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        <asp:TextBox ID="txtHopeToChangeDesc" runat="server" Height="20px" MaxLength="50"
                                            Width="303px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 100px; height: 36px; text-align: right">
                                    </td>
                                    <td style="height: 30px; text-align: left">
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                        <asp:Button ID="btnSubmit" runat="server" Text="保存换品" OnClientClick="return checkSubmit();"
                                            OnClick="btnSubmit_Click" />
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

    <script type="text/javascript" language="javascript">
        var index = 0;
        function ControlImage() {
            $("[name=goodimage]").each(function() { $(this).attr("disabled", $("#rbtYes").checked); });
            if ($("#rbtNo")[0].checked) {
                $("#uploadimage").hide();
            }
            else {
                $("#uploadimage").show();
            }
        }

        function addImage() {
            index = index + 1;
            var str = "<span id=\"s" + index + "\"><br/><input name=\"goodimage\" onchange=\"checkImage(this.value,this.id);\" id=\"goodimagefile" + index + "\" type=\"file\" style=\"height:20px; width:320px;\" /><a href=\"###\" onclick=\"$('#goodimagefile" + index + "').select(); document.selection.clear();$('#s" + index + "').hide();\"> 取 消 </a></span>";
            $("#addImage").after(str);
        }

        function checkImage(src, obj) {
            var hf = CheckImageHz(".jpg,.gif,.jpeg,.png", src);
            if (!hf) {
                alert("图片格式不正确！");
                $("#" + obj).select();
                document.selection.clear();
                return;
            }
        }

        function checkSubmit() {
            if ($.trim($("#txtGoodName").val()).length == 0) {
                alert("请您填写换品名称！");
                $("#txtGoodName").focus();
                return false;
            }
            if ($.trim($("#ddlGoodType").val()).length == 0) {
                alert("请您选择换品类别！");
                $("#ddlGoodType").focus();
                return false;
            }
            var content = $.trim(FCKeditorAPI.GetInstance('txtGoodDesc').EditorDocument.body.innerText);
            if (content.length == 0) {
                alert("请您填写换品简介！");
                return false;
            }
            if (content.length > 1000) {
                alert("换品简介不能超过1000字！");
                return false;
            }

        }
    </script>

</body>
</html>
