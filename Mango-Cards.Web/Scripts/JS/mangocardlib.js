'use strict';
var wechartuser = null;
(function () {
    var signature;
    var mc = {
        Tool: {
            getCookie: function (cName) {
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
            },
            getQueryStringByName: function (name) {
                var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
                if (result == null || result.length < 1) {
                    return "";
                }
                return result[1];
            }
        },
        loadsignature: function (dtd) {
            var dtd = $.Deferred();
            $.get("/api/HeaderSetting/", function (result) {
                signature = result;
                dtd.resolve();
            });
            return dtd.promise();
        },
        loadjssdk: function (dtd) {
            var dtd = $.Deferred();
            console.log("2");
            $.ajax({
                type: "get",
                url: "http://WeChatService.mangoeasy.com:3000/api/JsApiConfig/",
                data: { url: location.href },
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", signature);
                },
                success: function (xmlDoc, textStatus, xhr) {
                    if (xhr.status === 200) {
                        wx.config({
                            debug: true,
                            appId: xhr.responseJSON.AppId,
                            timestamp: xhr.responseJSON.Timestamp,
                            nonceStr: xhr.responseJSON.NonceStr,
                            signature: xhr.responseJSON.Signature,
                            jsApiList: ["onMenuShareTimeline", "onMenuShareAppMessage", "onMenuShareQQ", "onMenuShareWeibo", "onMenuShareQZone", "startRecord", "stopRecord", "onVoiceRecordEnd", "playVoice", "pauseVoice", "stopVoice", "onVoicePlayEnd", "uploadVoice", "downloadVoice", "chooseImage", "previewImage", "uploadImage", "downloadImage", "translateVoice", "getNetworkType", "openLocation", "getLocation", "hideOptionMenu", "showOptionMenu", "hideMenuItems", "showMenuItems", "hideAllNonBaseMenuItem", "showAllNonBaseMenuItem", "closeWindow", "scanQRCode", "chooseWXPay", "openProductSpecificView", "addCard", "chooseCard", "openCard"]
                        });
                        wx.ready(function () {

                        });
                        dtd.resolve();
                    }
                }
            });

            return dtd.promise();
        },
        loadwechartuser: function (dtd) {
            var dtd = $.Deferred();
            $.ajax({
                type: "get",
                url: "http://WeChatService.mangoeasy.com:3000/api/WeChartUserInfo?code=" + mc.Tool.getQueryStringByName("code") + "&state=" + mc.Tool.getQueryStringByName("state"),
                data: { url: location.href },
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", signature);
                },
                success: function (xmlDoc, textStatus, xhr) {
                    if (xhr.status === 200) {
                        wechartuser = xhr.responseJSON;
                        if (wechartuser.openid == undefined) {
                            dtd.reject();

                        } else {
                            dtd.resolve();
                        }

                    }
                }
            });
            return dtd.promise();
        },
        Ready: function (callback) {
            mc.loadsignature().then(mc.loadwechartuser).done(mc.loadjssdk).fail(function () { location.href = mc.Tool.getCookie("RedirecUrl") }).then(callback);
        }
    };
    mc.Ready();
})(jQuery);

