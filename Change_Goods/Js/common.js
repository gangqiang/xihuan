var srcroot = window.location.href.indexOf("www") > 0 ? "http://www.tsc8.com/": "http://tsc8.com/";
function CheckImageHz(validhz, filename) {
    var array = validhz.split(",");
    var hf = false;
    for (var i = 0; i < array.length; i++) {
        if (filename.toLowerCase().indexOf(array[i]) > -1) {
            hf = true
        }
    }
    return hf
}
function LoadLoginInfo(isdefault) {
    $.get(srcroot + "ajaxcall.aspx", {
        action: "LoadDefaultInfo",
        s: Math.random()
    },
    function(a) {
        if (a == "notlogin") {
            $("#needlogin").show();
            $("#welcomemember").hide();
            $("#alreadylogin").hide();
            $("#notlogin").show()
        } else {
            a = a.split(";");
            $("#welcomemember").show();
            $("#needlogin").hide();
            $("#lblUserName").html("欢迎您，<a href=\"" + srcroot + "membercenter.aspx?action=membermessage.aspx?type=receive\">" + unescape(a[1]) + "，您有(<span class='highlight'>" + a[2] + "</span>)条新的短消息。</a>");
            $("#alreadylogin").show();
            $("#notlogin").hide();
            if (!isdefault) $("#username").html(unescape(a[1]));
            if (isdefault) {
                $("#Welcome").html("您好，<span class=\"highlight\" >" + unescape(a[1]) + "</span><br/><br/>欢迎来到喜换网<br/><br/><a href=\"xh.aspx?id=" + a[0] + "\" title=\"进入个人主页\"><span class=\"highlight\">个人主页</span></a>&nbsp;&nbsp;<a href=\"membercenter.aspx\" title=\"进入个人管理中心\"><span class=\"highlight\">管理中心</span></a>");
                if ($.trim(a[2]) != "0") {
                    ymPrompt.alert({
                        title: '喜换网温馨提示：',
                        message: '<span class=\"highlight\">' + unescape(a[1]) + '</span>,欢迎光临喜换网<br/><a href=\"membercenter.aspx?action=membermessage.aspx?type=receive\"><img src=\"images/UnRead.gif\" style=\"border:0px;cursor:pointer;vertical-align:middle;\" />&nbsp;您有<span class=\"highlight\">' + a[2] + '</span>条消息未读！',
                        fixPosition: true,
                        winPos: 'rb',
                        showMask: false
                    })
                }
            }
        }
    })
}
function checkusername(username) {
    var uname = unescape(username);
    if (uname.length < 4 || uname.length > 20) {
        alert('换客名应为4-20个字符！');
        return
    };
    $("#reg_msg").show();
    $.get("ajaxcall.aspx", {
        action: "chkusername",
        username: username,
        s: Math.random()
    },
    function(b) {
        if (b != "0" && b != "1") {
            $('#reg_msg').html("<img src='images/already.gif' />&nbsp;" + b + "！");
            alert(b + "！")
        }
        if (b == "0") {
            $('#reg_msg').html("<img src='images/already.gif' />&nbsp;抱歉：此换客名已经被注册！");
            alert("此换客名已经被注册，请您选择别的换客名重试！")
        }
        if (b == "1") {
            $('#reg_msg').html("<img src='images/ok.gif' />&nbsp;恭喜:您可以使用这个换客名！")
        }
    })
}
function addFavorite(id, name) {
    $.get(srcroot + "ajaxcall.aspx", {
        action: "addFavorite",
        gid: $.trim(id),
        gname: escape($.trim(name)),
        s: Math.random()
    },
    function(a) {
        if (a == "needlogin") {
            ShowLogin()
        }
        if (a == "already") {
            alert("您已经收藏过此换品了，不能重复收藏 ^_^！");
            return
        }
        if (a == "ok") {
            alert("恭喜：此换品已成功添加到您的收藏 ^_^！")
        }
    })
}
function addFriend(friendid, friendname) {
    $.get(srcroot + "ajaxcall.aspx", {
        action: "addFriend",
        fid: $.trim(friendid),
        fname: escape($.trim(friendname)),
        s: Math.random()
    },
    function(a) {
        if (a == "needlogin") {
            ShowLogin()
        }
        if (a == "self") {
            alert("您总不能添加自己为好友吧 ^_^！");
            return
        }
        if (a == "already") {
            alert(friendname + "已经是你的好友了，不能重复添加 ^_^！");
            return
        }
        if (a == "ok") {
            alert("恭喜：您已成功添加" + friendname + "为您的好友 ^_^！")
        }
    })
}
function ShowLogin() {
    ymPrompt.win({
        message: srcroot + 'loginiframe.aspx',
        width: 400,
        height: 180,
        title: '喜换网-换客登陆',
        iframe: true,
        fixPosition: true
    })
}
function GoodsExchange(oid, oname, gid, gname, detailurl) {
    $.get(srcroot + "ajaxcall.aspx", {
        action: "checkLogin",
        s: Math.random()
    },
    function(data) {
        if (data == "already") {
            ymPrompt.win({
                message: srcroot + 'exchange.aspx?goodsid=' + $.trim(gid) + '&goodsname=' + escape($.trim(gname)) + '&ownerid=' + $.trim(oid) + "&ownername=" + escape($.trim(oname)) + "&detailurl=" + escape($.trim(detailurl)),
                width: 500,
                height: 300,
                title: '喜换网-发送物品交换请求',
                iframe: true,
                fixPosition: true
            })
        } else {
            ShowLogin()
        }
    })
}
function SendMessage(name) {
    $.get(srcroot + "ajaxcall.aspx", {
        action: "checkLogin",
        s: Math.random()
    },
    function(data) {
        if (data == "already") {
            ymPrompt.win(srcroot + 'messagereply.aspx?type=other&toname=' + escape($.trim(name)), 500, 300, '喜换网-给' + $.trim(name) + '发送短消息', null, null, null, {
                id: 'a'
            })
        } else {
            ShowLogin()
        }
    })
}
function AddNotes(gid, gname, oid, oname, username, pwd, content, issceret, detailurl) {
    $("#btnAddNotes").attr("disabled", true).attr("value", "正在提交,请稍候...");
    $.post(srcroot + "ajaxcall.aspx", {
        action: "addNotes",
        gid: gid,
        gname: escape($.trim(gname)),
        oid: oid,
        oname: escape($.trim(oname)),
        type: (gid > 0 ? "1": ""),
        username: escape($.trim(username)),
        pwd: escape($.trim(pwd)),
        content: escape($.trim(content)),
        issceret: issceret,
        url: escape(detailurl),
        s: Math.random()
    },
    function(a) {
        if (a == "ok") {
            alert("恭喜：您的留言已成功提交，我们会尽快进行审核 ^_^！");
            window.location.href = srcroot + detailurl + (gid > 0 ? "?": "&") + "s=" + Math.random()
        }
        $("#btnAddNotes").attr("disabled", false).attr("value", "提交留言");
        if (a == "notvaliduser") {
            alert("换客名或密码错误！");
            return
        }
    })
}
function LoadDetail(gid) {
    $.get(srcroot + "ajaxcall.aspx", {
        action: "loadDetail",
        gid: gid,
        s: Math.random()
    },
    function(a) {
        var a = a.split('$');
        $("#ViewContent").html(a[0]);
        $("#viewcount").html(a[1])
    })
}
function LoadNewsCount(newsid) {
    $.get(srcroot + "ajaxcall.aspx", {
        action: "GetViewCount",
        nid: $.trim(newsid),
        s: Math.random()
    },
    function(data) {
        $("#lblViewCount").text(data)
    })
}
today = new Date();
var d = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
window.status = "欢迎光临喜换网，今天是" + (today.getYear() > 1900 ? today.getYear() : today.getYear() + 1900) + "年" + (today.getMonth() + 1) + "月" + today.getDate() + "日  " + d[today.getDay()];
function KillError() {
    return true
}
window.onerror = KillError;