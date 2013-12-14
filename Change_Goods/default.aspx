<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default"
    EnableViewState="false" EnableEventValidation="false" EnableViewStateMac="false" %>

<%@ Register Src="UC/OnLineQQ.ascx" TagName="OnLineQQ" TagPrefix="uc1" %>
<%@ Register Src="UC/Butomm.ascx" TagName="Buttom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="author" content="ϲ����������,xihuan@tsc8.com" />
    <meta name="copyright" content="ϲ����������,xihuan@tsc8.com" />
    <title>
        <%=BusinessFacade.SystemConfigFacade.Instance().WebSiteTitle %>
    </title>

    <script type="text/javascript">
        function changetab(index) {
            for (var i = 1; i <= 3; i++) {
                if (i == index) {
                    $("#UC_Tab" + i).addClass("selected");
                    $("#UC_TabContent" + i).show();
                }
                else {
                    $("#UC_Tab" + i).removeClass("selected");
                    $("#UC_TabContent" + i).hide();
                }
            }
        }
        $(document).ready(function() {
            $("#UC_Tab1").css("cursor", "pointer"); $("#UC_Tab2").css("cursor", "pointer"); $("#UC_Tab3").css("cursor", "pointer");
            $("#sNew").css("cursor", "pointer").attr("title", "����ע��Ļ���"); $("#sStar").css("cursor", "pointer").attr("title", "���ǻ���");
            $("#UC_Tab1").bind("mouseover", function() { changetab(1) });
            $("#UC_Tab2").bind("mouseover", function() { changetab(2) });
            $("#UC_Tab3").bind("mouseover", function() { changetab(3) });
            $("#sNew").bind("mouseover", function() { $("#divNewUser").show(); $("#divStarUser").hide(); $(this).addClass("inittext"); $("#sStar").removeClass("inittext"); });
            $("#sStar").bind("mouseover", function() { $("#divNewUser").hide(); $("#divStarUser").show(); $(this).addClass("inittext"); $("#sNew").removeClass("inittext"); });
            $("#DoSearch").bind("click", function() {
                var keyword = $.trim($("#keyword").val());
                if (keyword.length == 0) { alert("�����������ؼ���!"); }
                else {
                    var url = ($("#searchtype").val() == "0" ? "searchlist.aspx" : "userlist.aspx") + "?keyword=" + escape(keyword);
                    window.location = url;
                }
            });
        });
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="site-nav">
        <div class="bd">
            <p class="login-info">
                <span id="needlogin">���ã���ӭ����ϲ������<a href="register.aspx" title="���ע���Ϊϲ������Ա">[���ע��]</a>
                    <a href="login.aspx" title="��¼ϲ����">[��¼]</a></span> <span id="welcomemember" style="display: none;">
                        <span id="lblUserName"></span></span>&nbsp;&nbsp;&nbsp;&nbsp; <span style="font-weight: bold;">
                            ϲ�������ͽ���Ⱥ��<span class="highlight">26282365,17848131,32023365</span>�ڴ����ļ��룡</span>
            </p>
            <ul class="quick-link">
                <li><a title="ϲ����-�ҵ��ղ�" id="J_NavFavorite" href="membercenter.aspx?action=goodshistory.aspx"
                    target="_blank">�ҵ��ղؼ�</a> </li>
                <li><a title="������Ʒ" href="searchlist.aspx" target="_blank">��Ʒ����</a> </li>
                <li><a title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                    href="news/2009/6/2/newsshow35.html" target="_blank">������ȫ</a> </li>
                <li class="help"><a title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                    href="news/2009/6/3/newsshow36.html" target="_blank">����</a> </li>
            </ul>
        </div>
    </div>
    <div id="page">
        <div id="header">
            <div id="logo">
                <a title="ϲ����" href="http://www.tsc8.com/" target="_top">
                    <img title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                        src="images/logo.png" style="border: 0px;" /></a>
            </div>
            <div class="legend-logo" style="border: 0px; margin: -50px 3px 7px 480px; position: relative;
                text-align: left">
                <a title="ϲ����" href="http://www.tsc8.com/" target="_top">
                    <img title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                        src="images/areyouchange.jpg" style="border: 0px;" /></a>
            </div>
            <div class="quick-menu skin-yellow">
                <div class="about-deal box">
                    <span class="rc-tp"><span></span></span>
                    <div class="bd">
                        <ul>
                            <li><a href="searchlist.aspx" target="_top">��Ҫ��</a> </li>
                            <li><a href="membercenter.aspx?action=goodsadd.aspx" target="_top">��Ҫ�Ǽ�</a> </li>
                            <li><a href="membercenter.aspx" target="_top">�ҵ�ϲ��</a> </li>
                        </ul>
                    </div>
                    <span class="rc-bt"><span></span></span>
                </div>
                <div class="about-social box">
                    <span id="TOPYAD"></span>
                </div>
            </div>
            <!--���ٰ�ť-->
            <div class="search-box">
                <fieldset style="border-top-style: none; border-right-style: none; border-left-style: none;
                    border-bottom-style: none; width: 100%; padding: 0px;">
                    <legend>����</legend>
                    <ul class="select-search" id="J_SiteSearchTab">
                        <li id="good" class="selected"><a title="������Ʒ" onclick="$('searchtype').value='0';$('good').className='selected';$('user').className='';"
                            href="###" target="_self"><strong>�ѻ�Ʒ</strong></a> </li>
                        <li id="user" style="margin-left: 5px;"><a title="��������" onclick="$('searchtype').value='1';$('good').className='';$('user').className='selected';"
                            href="###" target="_self"><strong>�ѻ���</strong></a> </li>
                    </ul>
                    <div class="search-form">
                        <span class="search-q">
                            <input id="keyword" />
                        </span>
                        <input id="searchtype" type="hidden" value="0" />
                        <button id="DoSearch" type="button">
                            �� ��</button>
                    </div>
                    <ul class="select-more">
                        <li><a title="������Ʒ" href="searchlist.aspx" target="_top">�߼�����</a></li>
                        <li><a title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                            href="news/2009/6/3/newsshow36.html" target="_blank">ʹ�ð���</a> </li>
                    </ul>
                </fieldset>
            </div>
            <!--���������-->
            <div class="nav-box box">
                <span class="rc-tp"><span></span></span><span style="font-size: 0px; z-index: 99;
                    background: url(images/hotnew.gif) no-repeat 100% 0px; left: 460px; overflow: hidden;
                    width: 19px; line-height: 0px; position: absolute; top: -10px; height: 23px">��
                </span>
                <div class="bd">
                    <dl class="chanel">
                        <dd>
                            <ul>
                                <li id="MenuHome"><a href="<%=SrcRootPath %>" target="_top"><span>��ҳ</span></a>
                                </li>
                                <li id="MenuMall"><a href="citychange.html"><span>ͬ�ǽ���</span></a> </li>
                                <li id="MenuGlobal"><a href="schoolchange.html" target="_top"><span>ͬУ����</span></a>
                                </li>
                                <li id="MenuSecondHand" style="color:Red;"><a href="taobao.htm" target="_blank"><span>
                                    �Ա�����</span></a> </li>
                                <li id="MenuGift"><a href="searchlist.aspx" target="_top"><span>ȫ������</span></a>
                                </li>
                                <li id="MenuSale"><a href="userlist.aspx" target="_top"><span>������</span></a> </li>
                            </ul>
                        </dd>
                    </dl>
                    <dl class="news">
                        <dt>����</dt>
                        <dd>
                            <ul>
                                <li id="MenuMan"><a href="searchlist.aspx?province=110000" target="_top"><span>����</span></a>
                                </li>
                                <li id="MenuLady"><a href="searchlist.aspx?province=310000" target="_top"><span>�Ϻ�</span></a>
                                </li>
                                <li id="MenuBaby"><a href="searchlist.aspx?province=120000" target="_top"><span>���</span></a>
                                </li>
                                <li id="MenuFashion"><a href="searchlist.aspx?city=440300" target="_top"><span>����</span></a>
                                </li>
                                <li id="MenuBeauty"><a href="searchlist.aspx?city=440100" target="_top"><span>����</span></a>
                                </li>
                                <li id="MenuShishang"><a href="searchlist.aspx?city=420100" target="_top"><span>�人</span></a>
                                </li>
                                <li id="MenuLife"><a href="searchlist.aspx?city=320500" target="_top"><span>����</span></a>
                                </li>
                                <li id="MenuDigital"><a href="searchlist.aspx?city=210200" target="_top"><span>����</span></a>
                                </li>
                                <li id="MenuSport"><a href="searchlist.aspx?city=410100" target="_top"><span>֣��</span></a>
                                </li>
                                <li id="Li3"><a href="searchlist.aspx?city=370200" target="_top"><span>�ൺ</span></a>
                                </li>
                                <li id="Li1"><a href="citychange.html" target="_top"><span>>����</span></a> </li>
                            </ul>
                        </dd>
                    </dl>
                </div>
                <span class="rc-bt"><span></span></span>
            </div>
            <!--tab�˵�-->
        </div>
        <div id="content">
            <div class="grid-c3-s5e7">
                <div class="col-main">
                    <div class="main-wrap">
                        <div class="main-promotion-banner box">
                            <div class="slide-player" id="J_Slide">
                                <ul>
                                    <li>
                                        <!--AdForward Begin:-->
                                        <a target="_self" href="javascript:goUrl()">

                                            <script type="text/javascript"> 
<%=BusinessFacade.SystemConfigFacade.Instance().HomeRoundPics%>
var focus_width=355;
var focus_height=175;
var text_height=20;
var swf_height = focus_height+text_height;

document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase=http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6?0?0?0 width="'+ focus_width +'" height="'+ swf_height +'">');
document.write('<param name="allowScriptAccess" value="sameDomain"><param name="movie" value="images/focus.swf"><param name="quality" value="high"> <param name="bgcolor" value="#F0F0F0">');
document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
document.write('<param name="FlashVars" value="pics='+pics+'&links='+links+'&texts='+texts+'&borderwidth='+focus_width+'&borderheight=' +focus_height+'&textheight='+text_height+'">');
document.write('<embed src="pixviewer.swf" wmode="opaque" FlashVars="pics='+pics+'&links='+links+'&texts='+texts+'&borderwidth='+focus_width+'&borderheight= '+focus_height+'&textheight='+text_height+'" menu="false" bgcolor="#F0F0F0" quality="high" width ="'+ focus_width +'" height="'+ focus_height +'" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'); document.write('</object>');
                                            </script>

                                        </a>
                                        <!--AdForward End-->
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <!--����ֲ�-->
                        <div class="box site-promotion">
                            <span class="rc-tp"><span></span></span>
                            <div class="bd">
                                <div class="pic">
                                    <a href="http://www.tsc8.com">
                                        <img title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                            src="XiHuan_AD/rightattrans_firstad.gif" /></a>
                                </div>
                                <ul>
                                    <li><a href="http://news.163.com/09/1118/11/5ODB4G3C0001121M.html" title="�ڿ�������������328��"
                                        target="_blank">�ڿ���"��������"��328��</a></li>
                                    <li><a href="http://news.163.com/09/1118/21/5OED9LSR000120GU.html" title="�°���ϣ�����й�ϵ��Խ��������"
                                        target="_blank">�°���ϣ�����й�ϵ��Խ��������</a></li>
                                </ul>
                                <div class="pic">
                                    <a href="http://www.tsc8.com">
                                        <img title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                            src="XiHuan_AD/rightattrans_firstad.gif" /></a>
                                </div>
                            </div>
                            <span class="rc-bt"><span></span></span>
                        </div>
                        <div style="background-color: #fff; font-size: 12px; font-weight: bold;">
                            <ul style="width: 470px;">
                                <li style="width: 65px; float: left;"><a href="news/2009/5/31/newsshow2.html" style="color: #ff5500"
                                    target="_blank">������</a> </li>
                                <li style="width: 65px; float: left;"><a href="news/2009/6/4/newsshow45.html" target="_blank">
                                    ��ϵ����</a> </li>
                                <li style="width: 65px; float: left;"><a href="news/2009/6/3/newsshow36.html" target="_blank">
                                    ��������</a> </li>
                                <li style="width: 65px; float: left;"><a style="color: #ff5500" href="news/2009/6/3/newsshow38.html"
                                    target="_blank">��������</a> </li>
                                <li style="width: 65px; float: left;"><a href="news/2009/6/1/newsshow11.html">����ƽ̨</a>
                                </li>
                                <li style="width: 65px; float: left;"><a href="news/2009/6/2/newsshow35.html" style="color: #ff5500"
                                    target="_blank">������ȫ</a> </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!--����������-->
                <div class="col-sub">
                    <div class="box today-news">
                        <span class="rc-tp"><span></span></span>
                        <div class="announce">
                            <div class="hd">
                                <h3>
                                    ������</h3>
                            </div>
                            <div class="bd">
                                <div class="pic">
                                    <a href="http://www.tsc8.com">
                                        <img title="ϲ����" src="XiHuan_AD/gg_underad.gif" border="0" /></a>
                                </div>
                                <ul>
                                    <asp:Repeater ID="rptNotic" runat="server">
                                        <ItemTemplate>
                                            <li><a title="<%#DataBinder.Eval(Container.DataItem,"Title",null) %>" target="_blank"
                                                href="<%#DataBinder.Eval(Container.DataItem,"NewsUrl",null) %>">
                                                <%#DataBinder.Eval(Container.DataItem,"Title",null) %>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <div class="ft">
                                <ul class="act">
                                    <li><a class="more" href="http://www.tsc8.com">����</a> </li>
                                </ul>
                            </div>
                        </div>
                        <!--������-->
                        <div class="new-product box" style="margin-bottom: 0px">
                            <div class="hd">
                                <h3>
                                    ���·���</h3>
                            </div>
                            <div class="bd">
                                <ul class="servers icon">
                                    <asp:Repeater ID="rptNewService" runat="server">
                                        <ItemTemplate>
                                            <li><a title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                                href="<%#DataBinder.Eval(Container.DataItem,"NewsUrl",null) %>" target="_blank">
                                                <span></span>
                                                <%#CommonMethod.GetSubString( DataBinder.Eval(Container.DataItem,"Title",null),8,"") %>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <div class="ft">
                                <ul class="act">
                                    <li><a title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                        class="more" href="http://www.tsc8.com/">����</a> </li>
                                </ul>
                            </div>
                        </div>
                        <div class="hot-event">
                            <div class="bd">
                                <ul class="text-list" id="J_textbox">
                                    <li><a title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                        href="register.aspx"><span></span>ע��</a> </li>
                                    <li><a title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                        href="membercenter.aspx?action=goodsadd.aspx"><span></span>�Ǽǻ�Ʒ</a> </li>
                                    <li><a title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                        style="color: #ff5500" href="userlist.aspx"><span></span>������</a> </li>
                                    <li><a title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                        href="register.aspx"><span></span>��Ǯ����</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-extra skin-orange">
                    <div class="promotion box">
                        <span class="rc-tp"><span></span></span>
                        <div class="bd">
                            <div class="pic first J_DirectionalBox">
                                <a href="http://www.tsc8.com">
                                    <img src="XiHuan_AD/right_firstad.jpg" title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                        style="border: 0px;" /></a>
                            </div>
                            <div class="pic J_DirectionalBox">
                                <a href="http://www.tsc8.com">
                                    <img src="XiHuan_AD/right_secondad.jpg" title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                        style="border: 0px;" /></a>
                            </div>
                        </div>
                        <span class="rc-bt"><span></span></span>
                    </div>
                    <div class="expressway-frontpage select-box">
                        <div id="notlogin" class="loginbox">
                            <div class="logintop">
                                ���͵�¼</div>
                            <div class="logincontent">
                                ��������<asp:TextBox ID="txtUserName" runat="server" Height="15" EnableViewState="true"></asp:TextBox><br />
                                <span style="margin-top: 20px;">��&nbsp;&nbsp;&nbsp;�룺<asp:TextBox ID="txtPwd" runat="server"
                                    Height="15" TextMode="password" EnableViewState="true"></asp:TextBox></span><br />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:CheckBox runat="server" ID="chkAutoLogin" EnableViewState="true" Text="�������Զ���¼" />
                                <a href="###" title="ϲ����-�һ�����" onclick="ymPrompt.win('findpass.aspx',500,300,'ϲ����-�һ�����',null,null,null,{id:'a'});">
                                    �������룿</a>
                                <asp:ImageButton EnableViewState="true" ID="btnSubmit" ImageUrl="images/denglu.gif"
                                    runat="server" OnClientClick="return checkLogin();" OnClick="btnSubmit_Click" />
                                <a href="register.aspx">
                                    <img title="ע���Ϊ��Ա" style="border: 0px; margin-bottom: 3px;" src="images/reg.png" /></a>
                            </div>
                        </div>
                        <div id="alreadylogin" class="loginbox" style="text-align: center; display: none;">
                            <div class="logintop">
                                ��Ա����</div>
                            <div class="logincontent" style="margin-left: 0px;">
                                <span id="Welcome"></span>&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="lnkQuit" EnableViewState="true" ToolTip="�˳���¼"
                                    OnClientClick="return confirm('��ȷ��Ҫ�˳���¼��');" OnClick="lnkQuit_Click">
                                   <span class="highlight">�˳���½</span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!--��������-->
                </div>
                <!--����������-->
            </div>
            <!--��������-->
            <div class="grid-c2-s7f">
                <div class="col-main">
                    <div class="main-wrap">
                        <div class="category box">
                            <span class="rc-tp"><span></span></span>
                            <div class="hd">
                                <h3>
                                    ��Ʒ����</h3>
                            </div>
                            <div class="categorycontentbox">
                                <div class="category-list" id="J_CategoryList">
                                    <div class="virtual">
                                        <h4>
                                            ����</h4>
                                        <ul>
                                            <%= FixOne %>
                                        </ul>
                                    </div>
                                    <div class="digital">
                                        <h4>
                                            ����</h4>
                                        <ul>
                                            <%=FixTwo %>
                                        </ul>
                                    </div>
                                    <div class="beauty">
                                        <h4>
                                            ����</h4>
                                        <ul>
                                            <%=FixThree %>
                                        </ul>
                                    </div>
                                    <div class="fashion">
                                        <h4>
                                            ����</h4>
                                        <ul>
                                            <%=FixFour %>
                                        </ul>
                                    </div>
                                    <div class="life">
                                        <h4>
                                            �Ҿ�</h4>
                                        <ul>
                                            <%=FixFive %>
                                        </ul>
                                    </div>
                                    <div class="car">
                                        <h4>
                                            ����</h4>
                                        <ul>
                                            <%=FixSix %>
                                        </ul>
                                    </div>
                                    <div class="collection">
                                        <h4>
                                            ����</h4>
                                        <ul>
                                            <%=FixNine %>
                                        </ul>
                                    </div>
                                    <div class="collection">
                                        <h4>
                                            �ղ�</h4>
                                        <ul>
                                            <%=FixServen %>
                                        </ul>
                                    </div>
                                    <div class="other">
                                        <h4>
                                            ����</h4>
                                        <ul>
                                            <%=FixEight %>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <!--��Ŀ�б����-->
                        </div>
                        <!--��Ŀ����-->
                        <div class="banner-670x90">
                            <span id="CENTERYAD"></span>
                        </div>
                    <%--    <div class="box hot-info about-info">
                            <span class="rc-tp"></span>
                            <div class="hd">
                                <h3>
                                    ������Ѷ</h3>
                            </div>
                            <div class="bd">
                                <ul class="hot-key">
                                    <li><a title="ϲ����" href="">����</a></li>
                                    <li><a title="ϲ����" href="">����</a></li>
                                    <li><a title="ϲ����" href="">�����ֻ�</a></li>
                                    <li><a title="ϲ����" href="">����</a></li>
                                    <li><a title="ϲ����" href="">�������</a></li>
                                    <li><a title="ϲ����" href="">����</a> </li>
                                </ul>
                                <div class="pic">
                                    <a title="���л����г̽���" href="http://cqbbs.soufun.com/news~-1~1205/23670052_23670052.htm"
                                        target="_blank">
                                        <img title="���л����г̽���" src="XiHuan_AD/zixun_ad.gif" /></a>
                                </div>
                                <ul class="info-list">
                                    <asp:Repeater ID="rptZiXun" runat="server">
                                        <ItemTemplate>
                                            <li><a href="<%#DataBinder.Eval(Container.DataItem,"NewsUrl",null) %>" title="<%#DataBinder.Eval(Container.DataItem,"Title",null) %>"
                                                target="_blank">
                                                <%#DataBinder.Eval(Container.DataItem,"Title",null) %>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                        <div class="box bbs-info about-info">
                            <span class="rc-tp"><span></span></span>
                            <div class="hd">
                                <h3>
                                    վ������</h3>
                            </div>
                            <div class="bd">
                                <ul class="hot-key">
                                    <li><a title="ϲ����" href="">����</a></li>
                                    <li><a title="ϲ����" href=""><span>����</span></a></li>
                                    <li><a title="ϲ����" href="">����У��</a></li>
                                    <li><a title="ϲ����" href=" "><span>��Ԫ��н</span></a></li>
                                    <li><a title="ϲ����" href="">Ů�ɣ�</a></li>
                                    <li><a title="ϲ����" href="">����</a> </li>
                                </ul>
                                <div class="pic">
                                    <a href="http://cqbbs.soufun.com/news~-1~1205/23670052_23670052.htm">
                                        <img title="ϲ����-ͬ�ǽ���_ͬУ����_������Ʒ����_���ֽ���_רҵ����Ʒ����ƽ̨_ȫ��ḻ�Ļ�Ʒ��Ϣ_���ｻ��_ϲ����_����ϲ��www.tsc8.com"
                                            src="XiHuan_AD/shequ_ad.gif" /></a>
                                </div>
                                <ul class="info-list">
                                    <asp:Repeater ID="rptSiteNews" runat="server">
                                        <ItemTemplate>
                                            <li><a href="<%#DataBinder.Eval(Container.DataItem,"NewsUrl",null) %>" title="<%#DataBinder.Eval(Container.DataItem,"Title",null) %>"
                                                target="_blank">
                                                <%#DataBinder.Eval(Container.DataItem,"Title",null) %>
                                            </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>--%>
                    </div>
                </div>
                <div class="col-sub">
                    <div class="hot-product select-box">
                        <div class="bd" id="J_HotProductTab">
                            <ul class="hd">
                                <li id="UC_Tab1" class="selected">���»�Ʒ</li>
                                <li id="UC_Tab2">����ɽ�</li>
                                <li id="UC_Tab3">��Ʒ�Ƽ�</li>
                            </ul>
                            <div id="UC_TabContent1" class="select-bd new-product">
                                <ul class="item">
                                    <asp:Repeater ID="rptNewGoods" runat="server">
                                        <ItemTemplate>
                                            <li><a title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %>" href="<%#DataBinder.Eval(Container.DataItem,"DetailUrl",null) %>"
                                                target="_blank">
                                                <img class="tabgoodimg" title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %> "
                                                    src="<%#DataBinder.Eval(Container.DataItem,"DefaultPhoto",null) %>"></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <div id="UC_TabContent2" class="select-bd new-product">
                                <ul class="item">
                                    <asp:Repeater ID="rptSuccess" runat="server">
                                        <ItemTemplate>
                                            <li><a title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %>" href="<%#DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>"
                                                target="_blank">
                                                <img class="tabgoodimg" title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %> "
                                                    src="<%#DataBinder.Eval(Container.DataItem,"DefaultPhoto",null) %>"></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <div id="UC_TabContent3" class="select-bd new-product">
                                <ul class="item">
                                    <asp:Repeater ID="rptTJ" runat="server">
                                        <ItemTemplate>
                                            <li><a title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %>" href="<%#DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>"
                                                target="_blank">
                                                <img class="tabgoodimg" title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %> "
                                                    src="<%#DataBinder.Eval(Container.DataItem,"DefaultPhoto",null) %>"></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <!--��TAB-->
                    <div class="banner-270x90">
                        <span id="SIDEYAD">
                            <img src="XiHuan_AD/centerright_firstdad.gif" /></span>
                    </div>
                    <!--�������-->
                    <div class="guess-u-like box">
                        <div class="hd">
                            <h3>
                                <span class="inittext" id="sNew">ϲ������</span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="sStar">���ǻ���</span></h3>
                        </div>
                        <div class="bd" id="J_GuessingLayer">
                            <div id="divNewUser" style="width: 170; text-align: center; margin: 5px 0px 5px 5px;">
                                <ul>
                                    <asp:Repeater ID="rptNewUser" runat="server">
                                        <ItemTemplate>
                                            <li style="float: left; width: 80px; margin-bottom: 6px;"><a href="xh.aspx?id=<%#DataBinder.Eval(Container.DataItem,"Id",null) %>"
                                                target="_blank" title="<%#DataBinder.Eval(Container.DataItem,"UserName",null) %>">
                                                <img class="tabgoodimg" title="<%#DataBinder.Eval(Container.DataItem,"UserName",null) %>"
                                                    src="<%#DataBinder.Eval(Container.DataItem,"HeadImage",null) %>" /></a><br />
                                                <a href="xh.aspx?id=<%#DataBinder.Eval(Container.DataItem,"Id",null) %>" target="_blank"
                                                    title="<%#DataBinder.Eval(Container.DataItem,"UserName",null) %>">
                                                    <%# CommonMethod.GetSubString(DataBinder.Eval(Container.DataItem,"UserName",null),10,"..") %>
                                                </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <div id="divStarUser" style="width: 170; text-align: center; margin: 5px 0px 5px 5px;
                                display: none;">
                                <ul>
                                    <asp:Repeater ID="rptStartUser" runat="server">
                                        <ItemTemplate>
                                            <li style="float: left; width: 80px; margin-bottom: 6px;"><a href="xh.aspx?id=<%#DataBinder.Eval(Container.DataItem,"Id",null) %>"
                                                target="_blank" title="<%#DataBinder.Eval(Container.DataItem,"UserName",null) %>">
                                                <img class="tabgoodimg" title="���ǻ��ͣ�<%#DataBinder.Eval(Container.DataItem,"UserName",null) %>"
                                                    src="<%#DataBinder.Eval(Container.DataItem,"HeadImage",null) %>" /></a><br />
                                                <a href="xh.aspx?id=<%#DataBinder.Eval(Container.DataItem,"Id",null) %>" target="_blank"
                                                    title="<%#DataBinder.Eval(Container.DataItem,"UserName",null) %>">
                                                    <%# CommonMethod.GetSubString(DataBinder.Eval(Container.DataItem,"UserName",null),10,"..") %>
                                                </a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                        <div class="ft">
                            <ul class="act">
                                <li><a title="�鿴���໻��" class="more" href="userlist.aspx" target="_blank">����</a> </li>
                            </ul>
                        </div>
                    </div>
                 <%--   <div class="banner-270x90">
                        <span id="YAD2">
                            <img src="XiHuan_AD/centerright_secondad.gif" /></span>
                    </div>
                    <!--�������-->
                    <div class="banner-270x90">
                        <span id="YAD">
                            <img src="XiHuan_AD/before_starad.gif" /></span>
                    </div>
                    <!--�������-->
                    <div class="safe-and-univ select-box">
                        <div class="bd" id="J_SafeAndUnivTab">
                            <div class="select-bd safe" style="border: 0px;">
                                <span id="RIGHTYAD">
                                    <img src="XiHuan_AD/centerright_secondad.gif" /></span>.
                            </div>
                        </div>
                    </div>--%>
                    <!--��������-->
                </div>
                <!--������������-->
            </div>
            <!--��������-->
            <div class="box p4p" style="position: static; float: left;">
                <div class="hd">
                    <h3>
                        ��Ʒ���а�</h3>
                </div>
                <div class="bd" title="��Ʒ���а�" style="text-align: center;">
                    <asp:DataList runat="server" ID="dlsHotGoods" RepeatColumns="9" RepeatDirection="Horizontal">
                        <ItemTemplate>
                            <table class="searchtable" style="margin: 3px 15px 0px 0px;">
                                <tr>
                                    <td>
                                        <a title="�������<%#DataBinder.Eval(Container.DataItem,"ViewCount",null) %>" href="<%#DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>"
                                            target="_blank">
                                            <img class="smallgoodimg" title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %>,�������<%#DataBinder.Eval(Container.DataItem,"ViewCount",null) %>"
                                                src="<%#DataBinder.Eval(Container.DataItem,"DefaultPhoto",null) %>" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 35px;">
                                        <a title="<%#DataBinder.Eval(Container.DataItem,"Name",null) %>" href="<%#DataBinder.Eval(Container.DataItem,"DetailUrl",null)%>"
                                            target="_blank">
                                            <%#CommonMethod.GetSubString(DataBinder.Eval(Container.DataItem,"Name",null),22,"..") %>
                                        </a>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div class="ft">
                    <ul class="act">
                        <li><a title="�鿴���໻Ʒ" class="more" href="searchlist.aspx" target="_blank">����</a>
                        </li>
                    </ul>
                </div>
                <div class="hd" style="margin-top: 10px;">
                    <h3>
                        ��������</h3>
                </div>
                <div class="bd">
                    <div title="��������">
                        <asp:Repeater ID="rptLinks" runat="server">
                            <ItemTemplate>
                                <a href="<%#DataBinder.Eval(Container.DataItem,"Url",null) %>" target="_blank" title="<%#DataBinder.Eval(Container.DataItem,"Alt",null) %>">
                                    <%#DataBinder.Eval(Container.DataItem,"Name",null) %>
                                </a>|
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
        <uc1:OnLineQQ ID="OnLineQQ1" runat="server" />
        <asp:HiddenField ID="hidUserName" runat="server" />
        <asp:HiddenField ID="hidPwd" runat="server" />
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        LoadLoginInfo(true);
        function checkLogin() {
            if ($.trim($("#txtUserName").val()).length == 0) {
                alert("���������Ļ�������");
                $("#txtUserName").focus();
                return false;
            }

            if ($.trim($("#txtPwd").val()).length == 0) {
                alert("�������������룡");
                $("#txtPwd").focus();
                return false;
            }
            $("#hidUserName").val(escape($.trim($("#txtUserName").val())));
            $("#hidPwd").val(escape($.trim($("#txtPwd").val())));
        }
        //var adcontent = "<iframe src=\"http://u.1133.cc/showpage.php?pid=142225\" MARGINWIDTH=0 MARGINHEIGHT=0  HSPACE=0 VSPACE=0 FRAMEBORDER=0 SCROLLING=no HEIGHT=60 WIDTH=250></iframe>";
        //$("#TOPYAD").html(adcontent); $("#YAD2").html(adcontent); $("#SIDEYAD").html(adcontent); //$("#YAD").html(adcontent);
        $("#CENTERYAD").html("<iframe src=\"http://u.1133.cc/showpage.php?pid=141729\" MARGINWIDTH=0 MARGINHEIGHT=0  HSPACE=0 VSPACE=0 FRAMEBORDER=0 SCROLLING=no HEIGHT=100 WIDTH=730></iframe>");
        // $("#RIGHTYAD").html("<iframe src=\"http://u.1133.cc/showpage.php?pid=142224\" MARGINWIDTH=0 MARGINHEIGHT=0  HSPACE=0 VSPACE=0 FRAMEBORDER=0 SCROLLING=no HEIGHT=150 WIDTH=250></iframe>");
        $.get("ajaxcall.aspx", { action: "UpdateIndex", s: Math.random() }, function(data) { $("#updatetime").attr("title", data); });
    </script>
    <uc2:Buttom ID="Buttom" runat="server" EnableViewState="false" />
</body>
</html>
