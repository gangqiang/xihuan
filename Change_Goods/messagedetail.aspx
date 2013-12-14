<%@ Page Language="C#" AutoEventWireup="true" CodeFile="messagedetail.aspx.cs" Inherits="messagedetail"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看短消息</title>
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
                        <a title="查看他的主页" href="xh.aspx?id=<%=Message.FromId %>" target="_blank">
                            <%=Message.FromName %>
                        </a>
                    </td>
                    <td align="right">
                        接收人：
                    </td>
                    <td align="left">
                        <a title="查看他的主页" href="xh.aspx?id=<%=Message.ToId %>" target="_blank">
                            <%=Message.ToName %>
                        </a>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        发送时间：</td>
                    <td colspan="3" align="left">
                        <%=Message.CreateDate %>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        消息内容：</td>
                    <td colspan="3" align="left">
                        <%=CommonMethod.CodingToHtml(Message.Content)%>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
