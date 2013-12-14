<%@ Page Language="C#" AutoEventWireup="true" CodeFile="membernotes.aspx.cs" Inherits="membernotes" %>

<%@ Register Src="UC/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>喜换网--站内消息</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <fieldset>
                    <legend>
                        <img alt="查看和管理我的留言" src="images/notice.gif" />&nbsp;功能提示&nbsp;</legend>在这里可以查看和管理您的留言，喜欢网提醒您：请及时关注您的留言，把握交换机会！
                </fieldset>
            </center>
            <center>
                <fieldset>
                    <legend>
                        <img alt="我的留言" src="images/notice.gif" />我<asp:Label ID="lblType" runat="server"></asp:Label>的留言&nbsp;</legend>
                    <table id="receive" class="centerlisttable">
                        <colgroup>
                            <col width="20" />
                            <col width="90" />
                            <col />
                            <col width="40" />
                            <col width="60" />
                            <col width="120" />
                            <col width="110" />
                        </colgroup>
                        <tr class="thead" style="text-align: center;">
                            <td>
                                <input title="全选" type="checkbox" id="chkAllReceive" onclick="SelectAll('Receive');" />
                            </td>
                            <td>
                                留言人
                            </td>
                            <td>
                                内容和回复
                            </td>
                            <td>
                                悄悄话
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
                            <asp:Repeater runat="server" ID="rptGoodsList">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>" name="chkReceive"
                                                value="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>" /></td>
                                        <td style="text-align: center;">
                                            <a title="留言人：<%# DataBinder.Eval(Container.DataItem,"FromName",null)%>" href="xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"FromId",null)%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "FromName", null)%>
                                            </a>
                                        </td>
                                        <td>
                                            <%#showReceiveNoteContent(DataBinder.Eval(Container.DataItem, "FromName", null), DataBinder.Eval(Container.DataItem, "GoodsName", null), DataBinder.Eval(Container.DataItem, "GoodsId", null), DataBinder.Eval(Container.DataItem, "Content", null))%>
                                            <br />
                                            <span class="bluetext">我的回复：<%#DataBinder.Eval(Container.DataItem, "ReplyContent", null).Length > 0 ? CommonMethod.GetSubString(DataBinder.Eval(Container.DataItem, "ReplyContent", null), 30, "...") : "<span class=\"inittext\">还没回复</span>"%></span>
                                        </td>
                                        <td style="text-align: center;">
                                            <%#DataBinder.Eval(Container.DataItem, "IsScerect", null) == "1" ? "<span class=\"bluetext\">是</span>" : "<span class=\"highlight\">否</span>"%>
                                        </td>
                                        <td style="text-align: center;">
                                            <%#BusinessFacade.XiHuan_UserNotesFacade.FormatReiveNotesFlag(DataBinder.Eval(Container.DataItem,"Flag",null)) %>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem, "CreateDate", null)%>
                                        </td>
                                        <td>
                                            <a title="查看留言" href="###" onclick="ymPrompt.win('notesdetail.aspx?type=receive&id=<%# DataBinder.Eval(Container.DataItem,"Id",null)%>',500,300,'喜换网-查看留言',handler2,null,null,{id:'c'});">
                                                查看</a> <a href="###" onclick="ymPrompt.win('notesreply.aspx?type=receive&id=<%# DataBinder.Eval(Container.DataItem,"Id",null)%>',500,300,'喜换网-回复留言',handler2,null,null,{id:'d'});">
                                                    回复</a> <a title="删除" href="###" onclick="DelMessage(<%# DataBinder.Eval(Container.DataItem,"Id",null)%>);">
                                                        删除</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tr class="highlightrow">
                            <td colspan="7">
                                <span style="float: left;">
                                    <input id="btnDelMultiReceive" type="button" value="删除选中的留言" onclick="DelMulti('Receive');" />
                                    <input id="btnSendNewMessage" type="button" value="发送新的留言" onclick="ymPrompt.win('sendnotes.aspx?type=send',500,300,'喜换网-发送留言',handler2,null,null,{id:'a'});" />
                                </span><span style="float: right;">
                                    <uc1:PageControl ID="PageControl1" runat="server" />
                                </span>
                            </td>
                        </tr>
                    </table>
                    <table id="send" style="display: none;" class="centerlisttable">
                        <colgroup>
                            <col width="20" />
                            <col width="90" />
                            <col />
                            <col width="40" />
                            <col width="60" />
                            <col width="120" />
                            <col width="60" />
                        </colgroup>
                        <tr class="thead" style="text-align: center;">
                            <td>
                                <input title="全选" type="checkbox" id="chkAllSend" onclick="SelectAll('Send');" />
                            </td>
                            <td>
                                接收人
                            </td>
                            <td>
                                内容和回复
                            </td>
                            <td>
                                悄悄话
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
                            <asp:Repeater runat="server" ID="rptSend">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>" name="chkSend"
                                                value="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>" /></td>
                                        <td>
                                            <a title="接收人：<%# DataBinder.Eval(Container.DataItem,"ToName",null)%>" href="xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"ToId",null)%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "ToName", null)%>
                                            </a>
                                        </td>
                                        <td>
                                            <%#showSendNoteContent(DataBinder.Eval(Container.DataItem, "ToName", null), DataBinder.Eval(Container.DataItem, "GoodsName", null), DataBinder.Eval(Container.DataItem, "GoodsId", null), DataBinder.Eval(Container.DataItem, "Content", null))%>
                                            <br />
                                            <span class="bluetext">对方回复：<%#DataBinder.Eval(Container.DataItem, "ReplyContent", null).Length > 0 ? CommonMethod.GetSubString(DataBinder.Eval(Container.DataItem, "ReplyContent", null), 30, "...") : "<span class=\"inittext\">还没回复</span>"%></span>
                                        </td>
                                        <td style="text-align: center;">
                                            <%#DataBinder.Eval(Container.DataItem, "IsScerect", null) == "1" ? "<span class=\"bluetext\">是</span>" : "<span class=\"highlight\">否</span>"%>
                                        </td>
                                        <td style="text-align: center;">
                                            <%#BusinessFacade.XiHuan_UserNotesFacade.FormatSendNotesFlag(DataBinder.Eval(Container.DataItem, "Flag", null), DataBinder.Eval(Container.DataItem, "IsChecked", null))%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem,"CreateDate",null)%>
                                        </td>
                                        <td>
                                            <a title="查看留言" href="###" onclick="ymPrompt.win('notesdetail.aspx?type=send&id=<%# DataBinder.Eval(Container.DataItem,"Id",null)%>',500,300,'喜换网-查看留言',null,null,null,{id:'a'});">
                                                查看</a> <a title="删除留言" href="###" onclick="DelMessage(<%# DataBinder.Eval(Container.DataItem,"Id",null)%>);">
                                                    删除</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tr class="highlightrow">
                            <td style="text-align: right;" colspan="7">
                                <span style="float: left;">
                                    <input id="btnDelMultiSend" type="button" value="删除选中的留言" onclick="DelMulti('Send');" />
                                    <input id="btnSendNewMessage2" type="button" value="发送新的留言" onclick="ymPrompt.win('sendnotes.aspx?type=send',500,300,'喜换网-发送留言',handler2,null,null,{id:'a'});" />
                                </span><span style="float: right;">
                                    <uc1:PageControl ID="PageControl2" runat="server" />
                                </span>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </center>
        </div>
        <asp:HiddenField ID="hidId" runat="server" Value="" />
        <asp:LinkButton ID="lnkDelMessage" runat="server" OnClick="lnkDelMessage_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkDelMultiMessage" runat="server" OnClick="lnkDelMultiMessage_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript" language="javascript">
     
     function DelMessage(id)
     {
         if(confirm("您确定要删除该留言吗？"))
         {
            $("#hidId").val(id);
            __doPostBack('lnkDelMessage','');
         }
     }
    
     function SelectAll(type)
     {
        $("[name=chk"+type+"]").each(function(){this.checked=$("#chkAll"+type)[0].checked;});
     }
     function DelMulti(type)
     {   
         $("#hidId").val("");  
        $("[name=chk"+type+"]").each(function(){
        if(this.checked){$("#hidId").val($("#hidId").val()+$(this).val()+",");}});
        if($("#hidId").val().length==0)
        {
            alert("请选中你要删除的留言！");
            return;
        }
        else
        {   
            if(confirm("您确定要删除选中的留言吗？"))
            {
                __doPostBack('lnkDelMultiMessage','');
            }    
        }
     }
    </script>

</body>
</html>
