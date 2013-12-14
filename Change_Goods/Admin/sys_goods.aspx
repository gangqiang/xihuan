<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sys_goods.aspx.cs" Inherits="Admin_sys_goods" %>

<%@ Register Src="../UC/PageBar.ascx" TagName="PageBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript">
     $(document).ready(function(){
     $("a").each(function(){
     $(this).hover(function(){
     $("#goodsimage"+this.id).show();},function(){$("#goodsimage"+this.id).hide();});
     })
     $("#btnSearch").bind("click",DoSearch);
     $("#chkOnlyShow").bind("click",DoSearch);
     ;});
     
    function DoSearch()
    {
      window.location="sys_goods.aspx?goodsname="+escape($.trim($("#txtGoodName").val()))+"&onlyshow="+$("#chkOnlyShow")[0].checked;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="centerlisttable">
                <tr>
                    <td class="head">
                        换品搜索：</td>
                    <td>
                        &nbsp;名称：<asp:TextBox ID="txtGoodName" runat="server" Height="18px" Width="289px"></asp:TextBox>&nbsp;
                        <input type="checkbox" id="chkOnlyShow" runat="server" />只显示待审核的换品 &nbsp;<input type="button"
                            id="btnSearch" value="搜索换品" />
                    </td>
                </tr>
            </table>
            <table class="centerlisttable">
                <colgroup>
                    <col />
                    <col width="80" />
                    <col width="70" />
                    <col width="80" />
                    <col width="130" />
                    <col width="150" />
                </colgroup>
                <tr class="thead" style="text-align: center;">
                    <td>
                        换品名称
                    </td>
                    <td>
                        登记人
                    </td>
                    <td>
                        状态
                    </td>
                    <td>
                        浏览次数
                    </td>
                    <td>
                        登记时间
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
                                    <a id="<%# DataBinder.Eval(Container.DataItem,"Id",null)%>" title="<%# DataBinder.Eval(Container.DataItem,"Name",null)%>"
                                        href="../<%#DataBinder.Eval(Container.DataItem,"IsChecked",null).Equals("1")? DataBinder.Eval(Container.DataItem,"DetailUrl",null):"showdetail.aspx?action=isadmin&id="+DataBinder.Eval(Container.DataItem,"Id",null)%>"
                                        target="_blank">
                                        <%#DataBinder.Eval(Container.DataItem,"Name",null) %>
                                    </a><span style="display: none; position: absolute; padding: 1px; background-color: #F99219;"
                                        id="goodsimage<%# DataBinder.Eval(Container.DataItem,"Id",null)%>">
                                        <img width="110" height="100" style="border: 0px;" title="换品<%# DataBinder.Eval(Container.DataItem,"Name",null)%>的默认图片"
                                            src="../<%# DataBinder.Eval(Container.DataItem,"DefaultPhoto",null)%>" /></span>
                                </td>
                                <td>
                                    <a href="../xh.aspx?id=<%# DataBinder.Eval(Container.DataItem,"OwnerId",null)%>"
                                        target="_blank">
                                        <%# DataBinder.Eval(Container.DataItem,"OwnerName",null)%>
                                    </a>
                                </td>
                                <td style="text-align: center;">
                                    <%#  BusinessFacade.XiHuan_UserGoodsFacade.FormatGoodsState(DataBinder.Eval(Container.DataItem, "GoodState", null), DataBinder.Eval(Container.DataItem, "IsChecked", null))%>
                                </td>
                                <td style="text-align: center;">
                                    <%# DataBinder.Eval(Container.DataItem,"ViewCount",null)%>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem,"CreateDate",null)%>
                                </td>
                                <td>
                                    <%#DataBinder.Eval(Container.DataItem,"IsTJ",null).Equals("1")?"":"<a title=\"推荐到首页\" href=\"###\" onclick=\"TJGoods("+DataBinder.Eval(Container.DataItem,"Id",null)+");\">推荐到首页</a>" %>
                                    <a href="###" onclick="GenerateDetail('<%#DataBinder.Eval(Container.DataItem,"Id",null) %>','<%#DataBinder.Eval(Container.DataItem,"DetailUrl",null) %>')">
                                        生成换品页</a><br />
                                    <a title="删除换品" href="###" onclick="DelGoods(<%# DataBinder.Eval(Container.DataItem,"Id",null)%>);">
                                        删除此换品</a>
                                    <%#DataBinder.Eval(Container.DataItem, "IsChecked", null).Equals("1") ? "" : "<a title=\"通过审核\" href=\"###\" onclick=\"CheckGoods('" + DataBinder.Eval(Container.DataItem, "Id", null) + "','" + DataBinder.Eval(Container.DataItem, "OwnerId", null) + "');\">通过审核</a>"%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tr>
                    <td style="text-align: right;" colspan="6">
                        <uc1:PageBar ID="PageBar1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
     
    
    function DelGoods(id)
    {
        if(confirm("您确定要删除此换品吗？"))
        {   
           window.location="sys_goods.aspx?action=del&id="+id;
        }
     }
     
     function TJGoods(id)
     {
         if(confirm("您确定要将此换品推荐到首页吗？"))
        {
           window.location="sys_goods.aspx?action=tj&id="+id;
        }
     }
    
     function GenerateDetail(id,detailurl)
     {   
         window.location="sys_goods.aspx?action=generate&id="+id+"&detailurl="+escape(detailurl);
     }
     
     function CheckGoods(id,ownerid)
     {
         if(confirm("您确定要将此换品通过审核吗？"))
        {
           window.location="sys_goods.aspx?action=check&id="+id+"&ownerid="+ownerid;            
        }
     }
    </script>

</body>
</html>
