<%@ Page Language="C#" AutoEventWireup="true" CodeFile="exchange.aspx.cs" Inherits="exchange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发起物品交换请求</title>

    <script type="text/javascript">
      function reconfirm()
     {
        if(confirm("您目前还没有换品，要现在去登记换品吗？"))
        {
            parent.location="membercenter.aspx?action=goodsadd.aspx";
        }
     }
     $(document).ready(function(){
     $("a").each(function(){
     $(this).hover(function(){$("#goodsimage"+this.id).show();},function(){$("#goodsimage"+this.id).hide();});
     });});
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="vertical-align: middle; margin-top: 10px;">
            <center>
                <table id="FirstStep" class="serachtable" style="height: 180px;">
                    <tr>
                        <td style="text-align: center;" colspan="2">
                            发送交换请求：<span id="step1" class="highlight">选择交换方式--&gt;</span><span id="step2" class="highlight"
                                style="height: 35px">选择换品或输入Money金额--&gt;发送交换请求</span>
                        </td>
                    </tr>
                    <tr id="trFirst">
                        <td style="text-align: center; height: 25px;" colspan="2">
                            <asp:RadioButton ID="rbtMethodGoods" runat="server" Checked="true" GroupName="Method"
                                Text="用我的换品交换" />
                            <asp:RadioButton ID="rbtMethodMoney" runat="server" Checked="false" GroupName="Method"
                                Text="用Money交换" />
                        </td>
                    </tr>
                    <tr id="trFirst2">
                        <td colspan="2" style="text-align: center; height: 30px;">
                            <input id="btnShowNext" type="button" value="下一步" onclick="next();" />
                            &nbsp; &nbsp;&nbsp;
                            <input id="btnCancleChange" type="button" value="取消交换" onclick="parent.ymPrompt.close();" /></td>
                    </tr>
                    <tr id="trMoney" style="display: none;">
                        <td colspan="2" style="text-align: center; height: 50px;">
                            <span class="highlight">*</span>输入用于交换的Money金额：<asp:TextBox ID="txtMoney" runat="server"
                                Height="18px" MaxLength="8" Width="69px"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr id="trSelectGoods" style="display: none;">
                        <td colspan="2" style="text-align: left">
                            <span class="highlight">*</span>选择用于交换的换品：
                        </td>
                    </tr>
                    <tr id="trGoodsList" style="display: none;">
                        <td colspan="2" style="text-align: center; vertical-align: top;">
                            <table class="searchlisttable" style="width: 98%;">
                                <colgroup>
                                    <col width="2px;" />
                                    <col />
                                    <col width="60px" />
                                    <col width="60px" />
                                </colgroup>
                                <tr class="searchthead">
                                    <td>
                                    </td>
                                    <td>
                                        换品名称</td>
                                    <td>
                                        换品状态
                                    </td>
                                    <td>
                                        登记日期</td>
                                </tr>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptGoodsList">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <input type="checkbox" name="usergoods" id="goods<%# DataBinder.Eval(Container.DataItem,"Id",null)%>"
                                                        value="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>,<%# DataBinder.Eval(Container.DataItem,"Name",null).Replace(',',' ').Replace(';',' ')%>,<%# DataBinder.Eval(Container.DataItem,"DetailUrl",null).Replace(',',' ').Replace(';',' ')%>" /></td>
                                                <td>
                                                    <a id="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>" title="<%# DataBinder.Eval(Container.DataItem,"Name",null)%>"
                                                        href="<%# DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>" target="_blank">
                                                        <%#DataBinder.Eval(Container.DataItem,"Name",null) %>
                                                    </a><span style="display: none; position: absolute; padding: 1px; background-color: #F99219;"
                                                        id="goodsimage<%# DataBinder.Eval(Container.DataItem,"Id",null)%>">
                                                        <img width="110" height="100" style="border: 0px;" title="换品<%# DataBinder.Eval(Container.DataItem,"Name",null)%>的默认图片"
                                                            src="<%# DataBinder.Eval(Container.DataItem,"DefaultPhoto",null)%>" /></span>
                                                </td>
                                                <td style="text-align: center;">
                                                    <span class="highlight">
                                                        <%# System.Enum.GetName(typeof(BusinessFacade.XiHuan_UserGoodsFacade.GoodsState),CommonMethod.ConvertToInt(DataBinder.Eval(Container.DataItem,"GoodState",null),0))%>
                                                    </span>
                                                </td>
                                                <td>
                                                    <%# DataBinder.Eval(Container.DataItem,"CreateDate","{0:yyyy-M-d}")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr id="trSecond" style="display: none;">
                        <td style="text-align: center" colspan="2">
                            &nbsp;&nbsp;&nbsp;
                            <input id="btnSendRequire" type="button" value="发送交换请求" onclick="return checkSendRequire();" />
                            <span class="highlight">
                                <asp:CheckBox ID="chkSecret" runat="server" Text="发送私有请求" ToolTip="私有请求只有你和换主可以看到" /></span>
                            &nbsp;
                            <input id="btnShowPre" type="button" value="返回上一步" onclick="pre();" /></td>
                    </tr>
                </table>
            </center>
        </div>
        <asp:HiddenField runat="server" ID="hidGoodsId" Value="" />
        <asp:LinkButton ID="lnkSend" runat="server" OnClick="lnkSend_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript" language="javascript">
     function next()
     {
        $("#trFirst").hide();
        $("#trFirst2").hide();
        $("#step2").addClass("highlight");
        $("#trSecond").show();
        if($("#rbtMethodGoods")[0].checked)
        {
             $("#trSelectGoods").show();
             $("#trGoodsList").show();
             $("#trMoney").hide();
        }
        else
        {
             $("#trSelectGoods").hide();
             $("#trGoodsList").hide();
             $("#trMoney").show();
        }
     }
     
     function pre()
     {
        $("#trFirst").show();
        $("#trFirst2").show();
        $("#step2").removeClass("highlight");
        $("#trSecond").hide();
        $("#trSelectGoods").hide();
        $("#trGoodsList").hide();
        $("#trMoney").hide();
     }   
      function checkSendRequire()
     {  
       
        if($("#rbtMethodGoods")[0].checked)
        {
             $("#hidGoodsId").val("");
             $('[name=usergoods]:checked').each(function(){
              if(this.checked)
              { 
                $("#hidGoodsId").val($("#hidGoodsId").val()+$.trim($(this).val())+";"); 
              }
            });
            
            if($.trim($("#hidGoodsId").val()).length==0)
            {
                alert("您还没有选择用哪个换品去交换呐 ^_^！");
                return false;
            }
        }
        
        else
        {
            if($.trim($("#txtMoney").val()).length==0)
            {
               alert("请输入您要用多少Money去交换 ^_^？");
               $("#txtMoney").focus();
               return false;
            }
        }
        
        __doPostBack('lnkSend','');
     }
    </script>

</body>
</html>
