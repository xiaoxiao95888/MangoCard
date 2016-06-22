var wechartuser = null;
$(function () {
    function getQueryStringByName(name) {
        var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    }
    function getCookie(cName) {
        if (document.cookie.length > 0) {
            var cStart = document.cookie.indexOf(cName + "=");
            if (cStart !== -1) {
                cStart = cStart + cName.length + 1;
                var cEnd = document.cookie.indexOf(";", cStart);
                if (cEnd === -1) cEnd = document.cookie.length;
                return unescape(document.cookie.substring(cStart, cEnd));
            }
        }
        return "";
    }
    var model = {
        url: location.href
    };
    $.get("/api/HeaderSetting/", function (base64) {
        $.ajax({
            type: "get",
            url: "http://WeChatService.mangoeasy.com:3000/api/JsApiConfig/",
            data: model,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", base64);
            },
            success: function (xmlDoc, textStatus, xhr) {
                if (xhr.status === 200) {
                    wx.config({
                        debug: false,
                        appId: xhr.responseJSON.AppId,
                        timestamp: xhr.responseJSON.Timestamp,
                        nonceStr: xhr.responseJSON.NonceStr,
                        signature: xhr.responseJSON.Signature,
                        jsApiList: ["onMenuShareTimeline", "onMenuShareAppMessage", "onMenuShareQQ", "onMenuShareWeibo", "onMenuShareQZone", "startRecord", "stopRecord", "onVoiceRecordEnd", "playVoice", "pauseVoice", "stopVoice", "onVoicePlayEnd", "uploadVoice", "downloadVoice", "chooseImage", "previewImage", "uploadImage", "downloadImage", "translateVoice", "getNetworkType", "openLocation", "getLocation", "hideOptionMenu", "showOptionMenu", "hideMenuItems", "showMenuItems", "hideAllNonBaseMenuItem", "showAllNonBaseMenuItem", "closeWindow", "scanQRCode", "chooseWXPay", "openProductSpecificView", "addCard", "chooseCard", "openCard"]
                    });
                    wx.ready(function () {

                    });
                }
            }
        });
        $.ajax({
            type: "get",
            url: "http://WeChatService.mangoeasy.com:3000/api/WeChartUserInfo?code=" + getQueryStringByName("code") + "&state=" + getQueryStringByName("state"),
            data: model,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", base64);
            },
            success: function (xmlDoc, textStatus, xhr) {
                if (xhr.status === 200) {
                    wechartuser = xhr.responseJSON;
                    if (wechartuser.openid == undefined) {
                        location.href = getCookie("RedirecUrl");
                    } else {
                        //alert(wechartuser.openid)
                    }
                }
            }
        });
    });
});