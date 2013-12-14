<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageBar.ascx.cs" Inherits="UC_PageBar"
    EnableViewState="false" %>
<div runat="server" id="pageCtrl" class="pagectrl">
</div>

<script type="text/javascript">
$(document).ready(function(){
$("a.pagelistnormal").each(function(){$(this).hover(function(){$(this).addClass("pagelistnormalover");},function(){$(this).removeClass("pagelistnormalover");$(this).addClass("pagelistnormal");});});
});
</script>

