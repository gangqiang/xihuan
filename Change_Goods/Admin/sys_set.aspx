<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sys_set.aspx.cs" Inherits="Admin_sys_set" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset>
            <table>
                <tr>
                    <td style="height: 30px">
                        <asp:Button ID="btnRefresh" runat="server" Text="刷新省市和学校信息" OnClick="btnRefresh_Click" />&nbsp;&nbsp;<asp:Button
                            ID="btnSystemConfig" runat="server" Text="刷新系统配置信息" OnClick="btnSystemConfig_Click" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnRefreshType" runat="server" Text="刷新换品类别信息" OnClick="btnRefreshType_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                        &nbsp;<asp:Button ID="btnGenerateIndex" runat="server" Text="重新生成首页" OnClick="btnGenerateIndex_Click" />
                        <asp:Button ID="btnGenerateCity" runat="server" Text="重新生成省市信息" OnClick="btnGenerateCity_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                        <asp:TextBox ID="txtStartDate" runat="server" Width="97px"></asp:TextBox>
                        &nbsp;到
                        <asp:TextBox ID="txtEndDate" runat="server" Width="97px"></asp:TextBox>
                        &nbsp;<asp:Button ID="btnGenerateGoodsDetail" runat="server" OnClick="btnGenerateGoodsDetail_Click"
                            Text="重新生成换品详细信息" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                        <asp:Button ID="btnGenerateSchool" runat="server" Text="重新生成学校信息" OnClick="btnGenerateSchool_Click" />
                        <asp:Button ID="btnGenerateNews" runat="server" Text="重新生成新闻页面" OnClick="btnGenerateNews_Click" />
                        <input id="btnUserManage" type="button" value="会员管理" onclick="ymPrompt.win('usermanage.aspx',350,180,'会员管理',null,null,null,{id:'a'});" />
                        &nbsp;&nbsp;<asp:Button ID="btnUpDatePic" runat="server" Text="更新幻灯片" OnClick="btnUpDatePic_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                        给最近<asp:TextBox ID="txtDays" runat="server" Width="33px" Text="10"></asp:TextBox>天未登录过网站的会员发送提醒邮件&nbsp;
                        <asp:Button ID="btnSendMaiil" runat="server" Text="立即发送" OnClick="btnSendMaiil_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                        非法字符设置 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 换品登记需要审核：<asp:RadioButtonList
                            ID="rbtIsGoodsAddNeedCheck" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSetGoodsAdd" runat="server" Text="保存" OnClick="btnSetGoodsAdd_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                        &nbsp;<asp:TextBox ID="txtFFZF" TextMode="multiline" runat="server" Height="331px"
                            Width="540px"></asp:TextBox>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px; text-align: center;">
                        <asp:Button ID="btnSaveFFZF" runat="server" Text="保存设置" OnClick="btnSaveFFZF_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    </form>
</body>
</html>
