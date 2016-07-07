'use strict';

jQuery.cookie = function (name, value, options) {
    if (typeof value != 'undefined') { // name and value given, set cookie
        options = options || {};
        if (value === null) {
            value = '';
            options = $.extend({}, options); // clone object since it's unexpected behavior if the expired property were changed
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
        }
        // NOTE Needed to parenthesize options.path and options.domain
        // in the following expressions, otherwise they evaluate to undefined
        // in the packed version for some reason...
        var path = options.path ? '; path=' + (options.path) : '';
        var domain = options.domain ? '; domain=' + (options.domain) : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else { // only name given, get cookie
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                // Does this cookie string begin with the name we want?
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};

var mc = {};
(function () {
    var signature;
    var wechartuser;
    var lib = {
        Tool: {
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
                            debug: false,
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
            if ($.cookie('WeChartUser') === "" || $.cookie('WeChartUser') === null) {
                $.ajax({
                    type: "get",
                    url: "http://WeChatService.mangoeasy.com:3000/api/WeChartUserInfo?code=" + lib.Tool.getQueryStringByName("code") + "&state=" + lib.Tool.getQueryStringByName("state"),
                    data: { url: location.href },
                    beforeSend: function(xhr) {
                        xhr.setRequestHeader("Authorization", signature);
                    },
                    success: function(xmlDoc, textStatus, xhr) {
                        if (xhr.status === 200) {
                            if (xhr.responseJSON.openid == undefined) {
                                dtd.reject();
                            } else {
                                $.cookie('WeChartUser', JSON.stringify(xhr.responseJSON));
                                $(mc).trigger("ready", [$.cookie('WeChartUser'), wx]);
                                dtd.resolve();
                            }

                        }
                    }
                });
            } else {
                $(mc).trigger("ready", [JSON.parse($.cookie('WeChartUser')), wx]);
                dtd.resolve();
            }
            
            return dtd.promise();
        },
        record: function (dtd) {
            var dtd = $.Deferred();
            var mangocardid = lib.Tool.getCookie("CardId");
            var model = {
                PvUser: wechartuser,
                MangoCardId: mangocardid
            };
            $.post("/api/Pv", model, function() {
                dtd.resolve();
            });
            return dtd.promise();
        },
        init: function () {
            lib.loadsignature()
                .then(lib.loadwechartuser)
                .then(lib.loadjssdk);
        }
    };
    lib.init();
})(jQuery);
//test
//$(mc).on("ready", function(event, user) {
//    alert(user.openid);
//});

