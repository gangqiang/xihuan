<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dealgoodsimage.aspx.cs" Inherits="Admin_dealgoodsimage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtGoodsId" runat="server"></asp:TextBox>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:Button ID="btnDealGoodsImage" runat="server" Text="更新换品默认图片" OnClick="btnDealGoodsImage_Click" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="跑图片表数据" />
        <br />
        <asp:TextBox ID="txtStartId" runat="server" Width="80px"></asp:TextBox>
        &nbsp; 到
        <asp:TextBox ID="txtEndId" runat="server" Width="85px"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="跑头像数据" onclick="Button2_Click" />
    </div>
    </form>
</body>
</html>
