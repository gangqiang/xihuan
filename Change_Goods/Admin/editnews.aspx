<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editnews.aspx.cs" Inherits="editnews" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 20px;">
            <table class="serachtable" style="width: 98%">
                <colgroup>
                    <col width="60" />
                    <col />
                </colgroup>
                <tr>
                    <td align="right" style="width: 60px; text-align: right">
                        标题：</td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtLinkName" runat="server" Width="227px" MaxLength="50"></asp:TextBox>
                        &nbsp; &nbsp; 所属分类：<asp:DropDownList ID="ddlNewsType" runat="server">
                        </asp:DropDownList>
                        &nbsp; &nbsp;&nbsp;
                        <input id="rbtContent" runat="server" type="radio" onclick="$('#link').hide();$('#content').show();"
                            checked="true" />内容型&nbsp;
                        <input id="rbtLink" runat="server" type="radio" onclick="$('#link').show();$('#content').hide();" />&nbsp;
                        外链型</td>
                </tr>
                <tr id="link" style="display: none;">
                    <td align="right" style="width: 60px; text-align: right">
                        链接地址：</td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtLinkUrl" runat="server" Width="227px"></asp:TextBox></td>
                </tr>
                <tr id="content">
                    <td align="right" style="width: 60px; text-align: right">
                        新闻内容：</td>
                    <td align="center" colspan="3" style="text-align: left">
                        <FCKeditorV2:FCKeditor ID="NewContent" runat="server" BasePath="../fckeditor/" Height="380px"
                            ToolbarSet="LeagueBasic">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 60px; text-align: right">
                        排 序 值：</td>
                    <td align="center" colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtSort" runat="server" Width="50px"></asp:TextBox>(输入数字，越小越靠前)</td>
                </tr>
                <tr>
                    <td align="right" style="width: 60px">
                    </td>
                    <td align="center" colspan="3">
                        <asp:Button ID="btnSave" runat="server" Text=" 保 存 " OnClick="btnSave_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
