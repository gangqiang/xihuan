<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userequst.aspx.cs" Inherits="userequst" %>

<%@ Register Src="UC/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>喜换网--管理我的交换请求</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <fieldset>
                    <legend>
                        <img title="查看和管理我的交换请求" src="images/notice.gif" />&nbsp;功能提示&nbsp;</legend>
                    在这里可以查看和管理您的交换请求，喜欢网提醒您：请及时关注您的交换请求，把握交换机会！
                </fieldset>
            </center>
            <center>
                <fieldset>
                    <legend>
                        <img title="我的请求" src="images/notice.gif" />我<asp:Label ID="lblType" runat="server"></asp:Label>的请求&nbsp;</legend>
                    <table id="receive" class="centerlisttable">
                        <colgroup>
                            <col width="20" />
                            <col width="60" />
                            <col />
                            <col width="60" />
                            <col width="80" />
                            <col width="155" />
                        </colgroup>
                        <tr class="thead" style="text-align: center;">
                            <td>
                                <input title="全选" type="checkbox" id="chkAllReceive" onclick="SelectAll('Receive');" />
                            </td>
                            <td>
                                发送人
                            </td>
                            <td>
                                请求内容
                            </td>
                            <td>
                                状态
                            </td>
                            <td>
                                发送日期
                            </td>
                            <td>
                                操作
                            </td>
                        </tr>
                        <tbody>
                            <asp:Repeater runat="server" ID="rptReceive">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>" name="chkReceive"
                                                value="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>" /></td>
                                        <td>
                                            <a title="请求人：<%# DataBinder.Eval(Container.DataItem,"SenderName",null)%>" href="xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"SenderId",null)%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "SenderName", null)%>
                                            </a>
                                        </td>
                                        <td>
                                            <%#DataBinder.Eval(Container.DataItem,
                                                "SenderName", null) + "想用" + DataBinder.Eval(Container.DataItem, "RequireDescribe",
                                                                                                                                                null) + "换您的" + string.Format("&nbsp;<a href=\"showdetail.aspx?id={0}\" title=\"{1} \" target=\"_blank\" >", DataBinder.Eval(Container.DataItem, "GoodsId", null), DataBinder.Eval(Container.DataItem, "GoodsName", null)) + DataBinder.Eval(Container.DataItem, "GoodsName", null)+"</a>"%>
                                        </td>
                                        <td style="text-align: center;">
                                            <%# BusinessFacade.XiHuan_ChangeRequireFacade.FormatState(DataBinder.Eval(Container.DataItem,"Flag",null),"receive") %>
                                        </td>
                                        <td style="text-align: center;">
                                            <%# DataBinder.Eval(Container.DataItem, "RequireDate", "{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%#CommonMethod.FinalString(DataBinder.Eval(Container.DataItem, "Flag", null)) == BusinessFacade.XiHuan_ChangeRequireFacade.ChangeRequireState.新发起.ToString("d") ? "<a title=\"接受交换请求\" href=\"###\" onclick=\"DelMessage('" + DataBinder.Eval(Container.DataItem, "Id", null) + "','Receive');\">接受交换请求</a>&nbsp;&nbsp;<a href=\"###\" onclick=\"DelMessage('" + DataBinder.Eval(Container.DataItem, "Id", null) + "','Ref')\">拒绝交换请求</a>" : ""%>
                                            <%#CommonMethod.FinalString(DataBinder.Eval(Container.DataItem, "Flag", null)) == BusinessFacade.XiHuan_ChangeRequireFacade.ChangeRequireState.交换成功.ToString("d") ? "已成功交换":"  <a title=\"更改交换状态\" href=\"###\" onclick=\"ChangeState('"+DataBinder.Eval(Container.DataItem,"Id",null)+"','receive');\">更改交换状态</a>"%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tr class="highlightrow">
                            <td colspan="6">
                                <span style="float: left;">
                                    <input id="btnDelMultiReceive" type="button" value="接受选中的请求" onclick="DelMulti('Receive');" /></span>
                                <span style="float: right;">
                                    <uc1:PageControl ID="PageControl1" runat="server" />
                                </span>
                            </td>
                        </tr>
                    </table>
                    <table id="send" style="display: none;" class="centerlisttable">
                        <colgroup>
                            <col width="20" />
                            <col width="60" />
                            <col />
                            <col width="60" />
                            <col width="80" />
                            <col width="150" />
                        </colgroup>
                        <tr class="thead" style="text-align: center;">
                            <td>
                                <input title="全选" type="checkbox" id="chkAllSend" onclick="SelectAll('Send');" />
                            </td>
                            <td>
                                接收人
                            </td>
                            <td>
                                请求内容
                            </td>
                            <td>
                                状态
                            </td>
                            <td>
                                发送日期
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
                                            <a title="接收人：<%# DataBinder.Eval(Container.DataItem,"OwnerName",null)%>" href="xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"OwnerId",null)%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "OwnerName", null)%>
                                            </a>
                                        </td>
                                        <td>
                                            <%#"您想用" + DataBinder.Eval(Container.DataItem, "RequireDescribe",null) + string.Format("换<a href=\"xh.aspx?id={0}\" target=\"_blank\" >", DataBinder.Eval(Container.DataItem,
                                                                                                                                                "OwnerId", null)) + DataBinder.Eval(Container.DataItem,
                                                                                                                                                "OwnerName", null) + "</a>的" + string.Format("<a href=\"showdetail.aspx?id={0}\" target=\"_blank\" >", DataBinder.Eval(Container.DataItem,
                                                                                                                                                "GoodsId", null)) + DataBinder.Eval(Container.DataItem, "GoodsName", null)+"</a>"%>
                                        </td>
                                        <td style="text-align: center;">
                                            <%# BusinessFacade.XiHuan_ChangeRequireFacade.FormatState(DataBinder.Eval(Container.DataItem,"Flag",null),"send") %>
                                        </td>
                                        <td style="text-align: center;">
                                            <%# DataBinder.Eval(Container.DataItem,"RequireDate","{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%#CommonMethod.FinalString(DataBinder.Eval(Container.DataItem, "Flag", null)) == BusinessFacade.XiHuan_ChangeRequireFacade.ChangeRequireState.交换成功.ToString("d") ? "已成功交换":"  <a title=\"更改交换状态\" href=\"###\" onclick=\"ChangeState('"+DataBinder.Eval(Container.DataItem,"Id",null)+"','send');\">更改交换状态</a>"%>
                                            <%#CommonMethod.FinalString(DataBinder.Eval(Container.DataItem, "Flag", null)) == BusinessFacade.XiHuan_ChangeRequireFacade.ChangeRequireState.新发起.ToString("d") ? " <a title=\"取消交换请求\" href=\"###\" onclick=\"DelMessage('"+DataBinder.Eval(Container.DataItem,"Id",null)+"','Send');\">取消交换请求</a>":""%>
                                            <%#CommonMethod.FinalString(DataBinder.Eval(Container.DataItem, "Flag", null)) == BusinessFacade.XiHuan_ChangeRequireFacade.ChangeRequireState.已取消.ToString("d") ? " <a title=\"恢复交换请求\" href=\"###\" onclick=\"DelMessage('"+DataBinder.Eval(Container.DataItem,"Id",null)+"','Recover');\">恢复交换请求</a>":""%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tr class="highlightrow">
                            <td style="text-align: right;" colspan="6">
                                <span style="float: left;">
                                    <input id="btnDelMultiSend" type="button" value="取消选中的请求" onclick="DelMulti('Send');" /></span>
                                <span style="float: right;">
                                    <uc1:PageControl ID="PageControl2" runat="server" />
                                </span>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </center>
        </div>
        <asp:HiddenField ID="hidId" runat="server" Value="" />
        <asp:HiddenField ID="hidType" runat="server" Value="" />
        <asp:LinkButton ID="lnkDelMessage" runat="server" OnClick="lnkDelMessage_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkDelMultiMessage" runat="server" OnClick="lnkDelMultiMessage_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript" language="javascript">
     
     function DelMessage(id,type)
     {
         var content="";
         if(type=="Ref")
         {
            content="拒绝";
            $("#hidType").val("Ref");
         }
         if(type=="Recover")
         {
            content="恢复";
            $("#hidType").val("Recover");
         }
         if(type!="Ref" && type!="Recover")
         {
            content=(type=="Send"?"取消":"接受");
         }
         if(confirm("您确定要"+content+"该请求吗？"))
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
         var content=(type=="Send"?"取消":"接受");
         $("#hidType").val(type);
         $("#hidId").val("");
         $("[name=chk"+type+"]").each(function(){
         if(this.checked){$("#hidId").val($("#hidId").val()+$(this).val()+",");}
         }); 
         
        if($("#hidId").val().length==0)
        {
            alert("请选中你要"+content+"的请求！");
            return;
        }
        else
        {   
            if(confirm("您确定要"+content+"选中的请求吗？"))
            {
                __doPostBack('lnkDelMultiMessage','');
            }    
        }
     }
     
     function ChangeState(id,type)
     {
        ymPrompt.win('changestate.aspx?id='+id+"&type="+type,320,120,'喜换网-更改交换状态',null,null,null,{id:'a'});
     }
    </script>

</body>
</html>
