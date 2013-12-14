<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadgoodspic.aspx.cs" Inherits="uploadgoodspic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传换品图片</title>

    <script type="text/javascript">
     function checkImage(src)
     {
        var hf=CheckImageHz(".jpg,.gif,.jpeg,.png",src);
        if(!hf)
        {
           alert("图片格式不正确！");
           return;
        } 
     }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="vertical-align: middle; margin-top: 5px;">
            <center>
                <table class="serachtable">
                <colgroup>
                <col width="80" />
                <col />
                </colgroup>
                    <tr id="trOldPic" runat="server" enableviewstate="false">
                        <td style="text-align: right">
                            旧图片：</td>
                        <td style="text-align: center">
                            <asp:Image ID="OldImage" runat="server" EnableViewState="false" Height="85px" Width="85px" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 52px;">
                            <span class="highlight">*</span>上传新图片：</td>
                        <td style="text-align: left; height: 52px;">
                            <span class="highlight">(图片大小不超过500k,格式.jpg,.jpeg,.gif,.png)<br />
                            </span>
                            <asp:FileUpload ID="flpImage" runat="server" onchange="checkImage(this.value);" Height="25px" />
                            <asp:Label ID="notice" EnableViewState="false" runat="server" Text="不修改请留空"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            图片描述：</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtImgDesc" runat="server" TextMode="MultiLine" Width="85%" Rows="3"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left;">
                            <input id="chkIsDefault" runat="server" type="checkbox" />设为默认图片
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align:left;">
                            &nbsp;
                            <asp:Button ID="btnUpLoad" runat="server" Text=" 上传图片 " OnClick="btnUpLoad_Click" />
                            &nbsp; &nbsp;&nbsp;
                            <input id="btnClose" type="button" value=" 取消上传 " onclick="parent.ymPrompt.close();" /></td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
