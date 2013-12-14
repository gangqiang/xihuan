<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageControl.ascx.cs" Inherits="UC_PageControl" %>
<div runat="server" id="pageCtrl" visible="false">
    <a href="###">首页</a> <a href="###">上页</a> <a href="###">1</a> <a href="###">2</a>
    <strong class="highlight">3</strong> <a href="###">4</a> <a href="###">5</a> <a href="###">
        下页</a> <a href="###">末页</a>
</div>
<asp:TextBox ID="txtPageIndex" runat="server"></asp:TextBox>
<asp:Label ID="txtAllRecordCount" runat="server"></asp:Label>
<asp:LinkButton ID="btnChangePage" runat="server" OnClick="btnChangePage_Click"></asp:LinkButton>

<script type="text/javascript">
$(document).ready(function(){
$("a.pagelistnormal").each(function(){$(this).hover(function(){$(this).addClass("pagelistnormalover");},function(){$(this).removeClass("pagelistnormalover");$(this).addClass("pagelistnormal");});});
});
</script>

