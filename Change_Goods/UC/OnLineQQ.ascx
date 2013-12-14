<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnLineQQ.ascx.cs" Inherits="OnLineQQ"
    Debug="false" EnableViewState="false" %>
<%@ OutputCache Duration="360000" VaryByParam="none" %>
<%--<script type="text/javascript" src="Js/qq.js"></script>

<script type="text/javascript">
Event.observe(window,'load',function(){
     var QQ = document.getElementById("OnLineFrame");
     if(QQ == null){
        QQ = document.createElement("IFRAME");
        QQ.name = "OnLineFrame";
        QQ.id = "OnLineFrame";
       
        QQ.style.cssText="position:absolute;left:0px;top:5px;";
        QQ.style.zIndex=-100;                                    
        QQ.setAttribute("frameborder", "0", 0);
        QQ.width = document.getElementById("tracqfloater").offsetWidth;
        QQ.height = document.getElementById("tracqfloater").offsetHeight-10;
     }
     document.getElementById("tracqfloater").appendChild(QQ);
});
</script>

<div class="dragAble" id="tracqfloater">
    <div id="OL_Top">
        <div id="OL_CLose" title="点击此处关闭本窗口!" style="" onclick="document.getElementById('tracqfloater').style.visibility='hidden';">
        </div>
        <div title="拖动本窗口" style="float: right; width: 15px; height: 9px;">
        </div>
    </div>
    <div id="OL_Middle" runat="server" class="OnLineQQ_Class">
        <ul>
            <li><a href="http://wpa.qq.com/msgrd?V=1&Uin=418921050&Menu=yes" target="_blank">
                <img alt="喜换网技术支持" src="http://wpa.qq.com/pa?p=1:418921050:4" />技术支持</a></li>
            <li><a href="http://wpa.qq.com/msgrd?V=1&Uin=86386740&Menu=yes" target="_blank">
                <img alt="喜换网QQ客服" src="http://wpa.qq.com/pa?p=1:86386740:4" />QQ客服</a></li>
            <li>
                <img alt="喜换网QQ交流群" src="http://wpa.qq.com/pa?p=1:26282365:4\" />
                <a href="http://wpa.qq.com/msgrd?V=1&Uin=26282365&Menu=yes">喜换网换客群</a></li>
            <li>
                <img alt="MSN客服" src="images/msn.gif" /><a href="msnim:chat?contact=gangqiang9861@hotmail.com">MSN客服</a></li>
            <li>
                <img alt="MSN客服" src="images/msn.gif" /><a href="msnim:chat?contact=sdwlyb@msn.com">MSN客服</a></li>
        </ul>
    </div>
    <div id="OL_Bottom">
    </div>
</div>--%>
<!--QQ在线聊天end-->

<script type="text/javascript">document.write("<scr"+"ipt language=\"javascript\" src=\"http://chat.53kf.com/kf.php?arg=tsc8&style=1&keyword="+escape(document.referrer)+"\"></scr"+"ipt>");</script>

