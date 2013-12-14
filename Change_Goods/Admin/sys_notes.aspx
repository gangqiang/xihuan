<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sys_notes.aspx.cs" Inherits="Admin_sys_notes" %>

<%@ Register Src="../UC/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>留言审核</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="centerlisttable">
                <tr>
                    <td class="head">
                        留言审核：</td>
                    <td>
                        <asp:Button ID="btnAll" runat="server" Text="显示所有留言" OnClick="btnAll_Click" />
                        <asp:Button ID="btnChecked" runat="server" Text="显示未审核的留言" OnClick="btnChecked_Click" />
                    </td>
                </tr>
            </table>
            <table class="centerlisttable">
                <colgroup>
                    <col width="80" />
                    <col width="80" />
                    <col />
                    <col width="90" />
                    <col width="120" />
                    <col width="125" />
                </colgroup>
                <tr class="thead" style="text-align: center;">
                    <td>
                        发送人
                    </td>
                    <td>
                        接收人
                    </td>
                    <td>
                        内容和回复
                    </td>
                    <td>
                        状态</td>
                    <td>
                        留言时间
                    </td>
                    <td>
                        操作
                    </td>
                </tr>
                <tbody>
                    <asp:Repeater runat="server" ID="rpNotesList">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: center;">
                                    <a title="留言人：<%# DataBinder.Eval(Container.DataItem,"FromName",null)%>" href="../xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"FromId",null)%>"
                                        target="_blank">
                                        <%# DataBinder.Eval(Container.DataItem, "FromName", null)%>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <a title="接收人：<%# DataBinder.Eval(Container.DataItem,"ToName",null)%>" href="../xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ToId",null)%>"
                                        target="_blank">
                                        <%# DataBinder.Eval(Container.DataItem, "ToName", null)%>
                                    </a>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem, "Content", null)%>
                                    <br />
                                    <span class="bluetext">回复：<%#DataBinder.Eval(Container.DataItem, "ReplyContent", null)%></span>
                                </td>
                                <td style="text-align: center;">
                                    <%#BusinessFacade.XiHuan_UserNotesFacade.FormatSendNotesFlag(DataBinder.Eval(Container.DataItem,"Flag",null),DataBinder.Eval(Container.DataItem,"IsChecked",null)) %>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "CreateDate", null)%>
                                </td>
                                <td style="text-align: center;">
                                    <a title="删除" href="###" onclick="DelMessage(<%# DataBinder.Eval(Container.DataItem,"Id",null)%>);">
                                        删除</a>
                                    <%#DataBinder.Eval(Container.DataItem, "IsChecked",null).Equals("1") ? string.Empty:"<a title=\"通过审核\" href=\"###\" onclick=\"CheckNotes('"+DataBinder.Eval(Container.DataItem,"Id",null)+"','"+DataBinder.Eval(Container.DataItem,"GoodsId",null)+"');\"> 通过审核</a>"%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tr class="highlightrow">
                    <td colspan="7">
                        <span style="float: right;">
                            <uc1:PageControl ID="PageControl1" runat="server" />
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidValue" runat="server" />
    </form>

    <script type="text/javascript">
     function DelMessage(id)
     {
         if(confirm("您确定要删除该留言吗？"))
         {  
            window.location="sys_notes.aspx?action=del&id="+id;
         }
     }
     function CheckNotes(id,gid)
     {
         if(confirm("您确定要将该留言通过审核吗？"))
         {  
           window.location="sys_notes.aspx?action=check&id="+id+"&gid="+gid;
         } 
     }
    </script>

</body>
</html>
