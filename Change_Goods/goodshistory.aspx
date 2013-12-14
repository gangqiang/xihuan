<%@ Page Language="C#" AutoEventWireup="true" CodeFile="goodshistory.aspx.cs" Inherits="goodshistory" %>

<%@ Register Src="UC/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的搜藏</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <fieldset>
                    <legend>
                        <img alt="查看我搜藏的换品" src="images/notice.gif" />&nbsp;功能提示&nbsp;</legend>在这里可以查看和管理您搜藏的换品，喜欢网提醒您：请时刻关注您感兴趣的换品，把握交换机会！
                </fieldset>
            </center>
            <center>
                <fieldset>
                    <legend>
                        <img alt="查看我搜藏的换品" src="images/notice.gif" />我搜藏的换品&nbsp;</legend>
                    <table class="centerlisttable">
                        <colgroup>
                            <col />
                            <col width="250" />
                            <col width="120" />
                            <col width="150" />
                        </colgroup>
                        <tr class="thead" style="text-align: center;">
                            <td>
                                换品名称
                            </td>
                            <td>
                                收藏备注
                            </td>
                            <td>
                                收藏时间
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
                                            <a title="<%# DataBinder.Eval(Container.DataItem,"GoodsName",null)%>" href="showdetail.aspx?id=<%# DataBinder.Eval(Container.DataItem,"GoodsId",null)%>"
                                                target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem,"GoodsName",null)%>
                                            </a>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem,"FacRemark",null)%>
                                        </td>
                                        <td>
                                            <%# DataBinder.Eval(Container.DataItem,"FavDate",null)%>
                                        </td>
                                        <td>
                                            <a title="查看换品信息" href="showdetail.aspx?id=<%# DataBinder.Eval(Container.DataItem,"GoodsId",null)%>"
                                                target="_blank">查看换品信息</a><a title="删除该收藏" href="###" onclick="DelFavorite(<%# DataBinder.Eval(Container.DataItem,"Id",null)%>);">
                                                    删除该收藏</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tr class="highlightrow">
                            <td style="text-align: right;" colspan="5">
                                <uc1:PageControl ID="PageControl1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </center>
        </div>
        <asp:HiddenField ID="hidId" runat="server" Value="0" />
        <asp:LinkButton ID="lnkDelFavorite" runat="server" OnClick="lnkDelFavorite_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript" language="javascript">
     function DelFavorite(id)
     {
        if(confirm("您确定要删除此搜藏吗？"))
        {
            $("#hidId").val(id);
            __doPostBack('lnkDelFavorite','');
        }
     }
    
    </script>

</body>
</html>
