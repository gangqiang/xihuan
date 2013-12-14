<%@ Page Language="C#" AutoEventWireup="true" CodeFile="modifypic.aspx.cs" Inherits="modifypic" %>

<%@ Register Src="UC/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>喜换网--修改换品图片</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <fieldset>
                <legend>
                    <img alt="查看和管理换品图片" src="images/notice.gif" />&nbsp;功能提示&nbsp;</legend>在这里可以查看和管理您的换品图片，喜欢网提醒您：相信有了图片，您的交换机会会更多！
            </fieldset>
        </center>
        <center>
            <fieldset>
                <legend>
                    <img alt="查看和管理换品图片" src="images/notice.gif" />查看和管理换品<span class="highlight"><%=Server.UrlDecode(CommonMethod.FinalString(Request["name"])) %></span>的图片信息&nbsp;</legend>
                <table id="receive" class="centerlisttable">
                    <colgroup>
                        <col width="20" />
                        <col width="90" />
                        <col />
                        <col width="60" />
                        <col width="120" />
                        <col width="110" />
                    </colgroup>
                    <tr class="thead" style="text-align: center;">
                        <td>
                            <input title="全选" type="checkbox" id="chkAllReceive" onclick="SelectAll('Receive');" />
                        </td>
                        <td>
                            图片
                        </td>
                        <td>
                            图片描述
                        </td>
                        <td>
                            默认图片
                        </td>
                        <td>
                            上传时间
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
                                            value="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>" />
                                    </td>
                                    <td style="text-align: center;">
                                        <a href="goodsimage.aspx?id=<%# DataBinder.Eval(Container.DataItem,"GoodsId",null)%>&name=<%#Server.UrlEncode(DataBinder.Eval(Container.DataItem,"GoodsName",null))%>"
                                            target="_blank">
                                            <img src="<%# DataBinder.Eval(Container.DataItem, "ImgSrc", null)%>" alt="换品<%# DataBinder.Eval(Container.DataItem, "GoodsName", null)%>的图片"
                                                class="smallgoodimg" /></a>
                                    </td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem, "ImgDesc", null)%>
                                    </td>
                                    <td style="text-align: center;">
                                        <%#DataBinder.Eval(Container.DataItem, "IsDefaultPhoto", null) == "1" ? "<span class=\"bluetext\">是</span>" : "<span class=\"highlight\">否</span>"%>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "CreateDate", null)%>
                                    </td>
                                    <td style="text-align: center;">
                                        <a href="###" title="修改图片信息" onclick="ModPic('ModifyPic','<%#DataBinder.Eval(Container.DataItem,"Id",null)%>','<%#DataBinder.Eval(Container.DataItem,"GoodsId",null)%>','<%# DataBinder.Eval(Container.DataItem, "GoodsName", null)%>','<%#DataBinder.Eval(Container.DataItem,"ImgSrc",null)%>','<%#DataBinder.Eval(Container.DataItem,"ImgDesc",null)%>','<%#DataBinder.Eval(Container.DataItem, "IsDefaultPhoto", null) %>');">
                                            修改图片信息</a><br />
                                        <%# ActionString(DataBinder.Eval(Container.DataItem, "IsDefaultPhoto", null), DataBinder.Eval(Container.DataItem, "Id", null))%>
                                        <br />
                                        <a title="删除此图片" href="###" onclick="DelMessage(<%# DataBinder.Eval(Container.DataItem,"Id",null)%>);">
                                            删除这张图片</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    <tr class="highlightrow">
                        <td colspan="6">
                            <span style="float: left;">
                                <input id="btnDelMultiReceive" type="button" value="删除选中的图片" onclick="DelMulti('Receive');" />
                                <input type="button" id="btnAddPic" onclick="ModPic('addNewPic','<%=CommonMethod.FinalString(Request["id"]) %>','<%=CommonMethod.FinalString(Request["id"]) %>','<%=Server.UrlDecode(CommonMethod.FinalString(Request["name"])) %>','','','0');"
                                    value="上传新的换品图片" />
                                <input type="button" value="返回换品管理" onclick="history.back(-1);" />
                            </span><span style="float: right;">
                                <uc1:PageControl ID="PageControl1" runat="server" />
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
    <asp:LinkButton ID="lnkCancleDefault" runat="server" OnClick="lnkCancleDefault_Click"></asp:LinkButton>
    <asp:LinkButton ID="lnkSetDefault" runat="server" OnClick="lnkSetDefault_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript" language="javascript">
     
     function DelMessage(id)
     {
         if(confirm("您确定要删除该图片吗？"))
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
            alert("请选中你要删除的图片！");
            return;
        }
        else
        {   
            if(confirm("您确定要删除选中的图片吗？"))
            {
                __doPostBack('lnkDelMultiMessage','');
            }    
        }
     }
      function ModPic(action,id,gid,gname,src,desc,isdefault)
     {
        ymPrompt.win('uploadgoodspic.aspx?action='+action+'&id='+id+'&gid='+gid+'&gname='+escape(gname)+'&src='+escape(src)+'&desc='+escape(desc)+'&isdefault='+isdefault,500,300,'喜换网-'+(action=="addNewPic"? "上传换品图片":"修改换品图片信息"),null,null,null,{id:'a'});
     }
     
     function CancleDefaultPhoto(id)
     {
        if(confirm("您确定要取消此图片作为默认图片吗？"))
        {
            $("#hidId").val(id);
            __doPostBack('lnkCancleDefault','');
        }
     }
     function SetDefaultPhoto(id)
     { 
         $("#hidId").val(id);
          __doPostBack('lnkSetDefault','');
     }
    </script>

</body>
</html>
