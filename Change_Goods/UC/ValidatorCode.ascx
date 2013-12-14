<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ValidatorCode.ascx.cs"
    Inherits="UC_ValidatorCode" %>
<img id="validateimg" style="cursor: hand; border: 0px;" src="<%=ParentPage.SrcRootPath%>ValidateCode.aspx"
    title="换个验证码" onclick="$(this).attr('src','<%=ParentPage.SrcRootPath%>ValidateCode.aspx?'+Math.random());" />
（不区分大小写） <a title="换个验证码" href="###" onclick="$('#validateimg').attr('src','<%=ParentPage.SrcRootPath%>ValidateCode.aspx?'+Math.random());">
    看不清,换一个</a>