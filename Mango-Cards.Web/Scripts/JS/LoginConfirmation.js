var LoginConfirmation = {
    viewModel: {
        confirmed: ko.observable(false)
    }
};
function getQueryStringByName(name) {
    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
}
LoginConfirmation.viewModel.Confirmation = function () {

    var model = {
        code: getQueryStringByName("code"),
        state: getQueryStringByName("state")
    };
    $.get('/api/HeaderSetting/', function (base64) {
        $.ajax({
            type: "get",
            data: model,
            url: "http://WeChatService.mangoeasy.com:3000/api/WeChartUserInfo/",
            beforeSend: function (xhr) { //beforeSend定义全局变量
                xhr.setRequestHeader("Authorization", base64); //Authorization 需要授权,即身体验证
            },
            success: function (xmlDoc, textStatus, xhr) {
                if (xhr.status == 200) {
                    var loginlomodel = {
                        State: model.state,
                        WeChatUserModel: {
                            OpenId: xhr.responseJSON.openid,
                            NickName: xhr.responseJSON.nickname,
                            Gender: xhr.responseJSON.sex,
                            City: xhr.responseJSON.city,
                            Province: xhr.responseJSON.province,
                            Country: xhr.responseJSON.country,
                            Headimgurl: xhr.responseJSON. headimgurl,
                        }
                    };
                    $.post('/api/LoginConfirmation/', loginlomodel, function (result) {
                        LoginConfirmation.viewModel.confirmed(true);
                    });
                }
            }
        });
    });
};
$(function () {
    ko.applyBindings(LoginConfirmation);
    LoginConfirmation.viewModel.Confirmation();
});