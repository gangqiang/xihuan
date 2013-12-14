<%@ Page Language="C#" AutoEventWireup="true" CodeFile="goodlist.aspx.cs" Inherits="goodlist"
    EnableEventValidation="false" %>

<%@ Register Src="UC/PageControl.ascx" TagName="PageControl" TagPrefix="uc1" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>已登记的换品管理</title>
    <style type="text/css">
    @import url(Js/calendar/themes/system.css);
    </style>

    <script type="text/javascript" src="Js/calendar/calendar_minall.js"></script>

    <script type="text/javascript">
     $(document).ready(function(){
     $("a").each(function(){
     $(this).hover(function(){$("#goodsimage"+this.id).show();},function(){$("#goodsimage"+this.id).hide();});
     });});
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <fieldset>
                    <legend>
                        <img title="查看我已经登记的换品" src="images/notice.gif" />&nbsp;功能提示&nbsp;</legend>在这里可以查看和管理您已经登记的换品，喜欢网提醒您：请时刻关注您的换品状态，把握交换机会！
                </fieldset>
            </center>
            <center>
                <fieldset>
                    <legend>
                        <img title="查看我已经登记的换品" src="images/notice.gif" />我已登记的换品&nbsp;</legend>
                    <br />
                    <table class="centerlisttable">
                        <tr>
                            <td class="head" rowspan="2">
                                换品搜索：</td>
                            <td>
                                &nbsp;名称：<asp:TextBox ID="txtGoodName" runat="server" Height="18px"></asp:TextBox>&nbsp;
                                <ajax:AjaxPanel ID="AjaxPanelType" runat="server">
                                    所属类别：<asp:DropDownList ID="ddlGoodType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGoodType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlGoodChildType" runat="server">
                                        <asp:ListItem Text="不限子类别" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </ajax:AjaxPanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;状态：<asp:DropDownList ID="ddlGoodState" runat="server">
                                </asp:DropDownList>&nbsp; 登记日期：从<input id="txtDateBegin" style="height: 16px; width: 100px;"
                                    runat="server" size="10" type="text" class="textbox" readonly="readonly" /><img title="选择日期"
                                        style="vertical-align: middle; margin-bottom: 6px; cursor: pointer;" id="btnDateBegin"
                                        src="images/calendar.gif" />&nbsp;到
                                <input id="txtDateEnd" runat="server" style="height: 16px; width: 100px;" size="10"
                                    type="text" class="textbox" readonly="readonly" /><img title="选择日期" style="vertical-align: middle;
                                        margin-bottom: 6px; cursor: pointer;" id="btnDateEnd" src="images/calendar.gif" />&nbsp;图片：<asp:DropDownList
                                            ID="ddlImage" runat="server">
                                            <asp:ListItem Selected="True">不限</asp:ListItem>
                                            <asp:ListItem Value="1">有图片</asp:ListItem>
                                            <asp:ListItem Value="0">无图片</asp:ListItem>
                                        </asp:DropDownList><asp:Button ID="btnSearch" runat="server" Text="搜索换品" OnClick="btnSearch_Click" /></td>
                        </tr>

                        <script type="text/javascript">
                // <![CDATA[
                
                BindCalendar('txtDateBegin', 'btnDateBegin');
                BindCalendar('txtDateEnd', 'btnDateEnd');
                
                // ]]>
                        </script>

                    </table>
                    <table class="centerlisttable">
                        <colgroup>
                            <col />
                            <col width="70" />
                            <col width="55" />
                            <col width="120" />
                            <col width="200" />
                        </colgroup>
                        <tr class="thead" style="text-align: center;">
                            <td>
                                换品名称
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
                                                href="<%#DataBinder.Eval(Container.DataItem,"IsChecked",null).Equals("1")? DataBinder.Eval(Container.DataItem,"DetailUrl",null):"showdetail.aspx?id="+DataBinder.Eval(Container.DataItem,"Id",null)%>"
                                                target="_blank">
                                                <%#DataBinder.Eval(Container.DataItem,"Name",null) %>
                                            </a><span style="display: none; position: absolute; padding: 1px; background-color: #F99219;"
                                                id="goodsimage<%# DataBinder.Eval(Container.DataItem,"Id",null)%>">
                                                <img width="110" height="100" style="border: 0px;" title="换品<%# DataBinder.Eval(Container.DataItem,"Name",null)%>的默认图片"
                                                    src="<%# DataBinder.Eval(Container.DataItem,"DefaultPhoto",null)%>" /></span>
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
                                            <a title="修改换品基本信息" href="goodsadd.aspx?id=<%# DataBinder.Eval(Container.DataItem,"Id",null)%>">
                                                修改信息</a>&nbsp; <a title="管理换品图片信息" href="modifypic.aspx?id=<%# DataBinder.Eval(Container.DataItem,"Id",null)%>&name=<%#Server.UrlEncode(DataBinder.Eval(Container.DataItem,"Name",null))%>&detailurl=<%#Server.UrlEncode(DataBinder.Eval(Container.DataItem,"DetailUrl",null))%>">
                                                    换品图片管理</a>&nbsp; <a title="删除换品" href="###" onclick="DelGoods(<%# DataBinder.Eval(Container.DataItem,"Id",null)%>);">
                                                        删除此换品</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tr class="highlightrow">
                            <td style="text-align: right;" colspan="5">
                                <span style="float: left;">
                                    <input type="button" onclick="window.location='goodsadd.aspx';" value="登记新换品" /></span>
                                <span style="float: right;">
                                    <uc1:PageControl ID="PageControl1" runat="server" />
                                </span>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </center>
        </div>
        <asp:HiddenField runat="server" ID="hidGoodId" Value="0" />
        <asp:LinkButton ID="lnkDel" runat="server" OnClick="lnkDel_Click"></asp:LinkButton>
    </form>

    <script type="text/javascript">
     function DelGoods(id)
     {
        if(confirm("您确定要删除此换品吗？"))
        {
            $("#hidGoodId").val(id);
            __doPostBack('lnkDel','');
        }
     }
    </script>

</body>
</html>
