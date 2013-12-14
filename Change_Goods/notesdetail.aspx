<%@ Page Language="C#" AutoEventWireup="true" CodeFile="notesdetail.aspx.cs" Inherits="notesdetail"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看留言</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 20px;">
            <table class="serachtable">
                <colgroup>
                    <col width="60" />
                    <col width="80" />
                    <col width="60" />
                    <col width="80" />
                </colgroup>
                <tr>
                    <td align="right">
                        发送人：
                    </td>
                    <td align="left">
                        <a title="查看他的主页" href="xh.aspx?id=<%=Note.FromId %>" target="_blank">
                            <%=Note.FromName %>
                        </a>
                    </td>
                    <td align="right">
                        接收人：
                    </td>
                    <td align="left">
                        <a title="查看他的主页" href="xh.aspx?id=<%=Note.ToId %>" target="_blank">
                            <%=Note.ToName%>
                        </a>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        留言时间：</td>
                    <td colspan="3" align="left">
                        <%=Note.CreateDate%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        留言内容：</td>
                    <td colspan="3" align="left">
                        <%=CommonMethod.CodingToHtml(Note.Content)%>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        回复：
                    </td>
                    <td align="left" colspan="3">
                        <%=CommonMethod.CodingToHtml(Note.ReplyContent)%>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
