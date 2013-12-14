<%@ Page Language="C#" AutoEventWireup="true" CodeFile="goodsimage.aspx.cs" Inherits="goodsimage"
    EnableViewState="false" EnableEventValidation="false" %>

<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Butomm" TagPrefix="uc2" %>
<%@ OutputCache Duration="1800" VaryByParam="id" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看换品图片</title>

    <script language="javascript" type="text/javascript">
 var imageArray;
 var imageDesc;
 var index=0;
 function getnext()
 {
  index++;
  if(index<imageArray.length)
  {
     document.images["saint"].src=imageArray[index];
     $("#imgDesc").html(imageDesc[index]);
  }
  else
  { 
    index=0;
    document.images["saint"].src=imageArray[index];
    $("#imgDesc").html(imageDesc[index]); 
  }
   
 }
 
 function getpre()
 {
  index--;
  if(index>=0)
  {
   document.images["saint"].src=imageArray[index];
     $("#imgDesc").html(imageDesc[index]);
  }
  else
  {
   index=imageArray.length-1;
   document.images["saint"].src=imageArray[index];
   $("#imgDesc").html(imageDesc[index]);
  }
 }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:Top ID="Top1" runat="server" EnableViewState="false" />
            <div class="toplinebar">
            </div>
            <div class="cakeheadlink">
                当前位置：<a href="index.html">首页</a>>><a href="searchlist.aspx">换品列表</a>>><a href="<%=Server.UrlDecode(CommonMethod.FinalString(Request["detailurl"])) %>">
                    <%=Server.UrlDecode(CommonMethod.FinalString(Request["name"])) %>
                </a>>>查看图片</div>
            <center>
                <div class="main" style="border: solid 1px #CCCCCC;">
                    <table style="margin: 10px 0px 10px 0px; text-align: center;">
                        <tr>
                            <td style="text-align: center;">
                                <a href="javascript:getnext();"><span id="loadingimage">
                                    <img alt="正在装载图片，请稍候..." src="images/loading.gif" />&nbsp;正在装载图片，请稍候...</span>
                                    <img src="" style="display: none;" alt="点击查看下一张" id="saint" name="saint" border="0" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px; text-align: left">
                                图片描述：<span id="imgDesc"></span></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;">
                                <input name="up" type="button" id="up" onclick="getpre();" value="上一张" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input name="down" type="button" id="down" onclick="getnext();" value="下一张" />
                            </td>
                        </tr>
                    </table>
                </div>
            </center>
        </div>
    </form>
    <uc2:Butomm ID="Butomm1" runat="server" EnableViewState="false" />
</body>
</html>
