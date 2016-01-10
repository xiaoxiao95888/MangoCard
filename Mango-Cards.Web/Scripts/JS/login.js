var Login = {
    viewModel: {
        wchatLoginQrCode: ko.observable(),
        wechatuser: {
            Id: ko.observable(),
            NickName: ko.observable(),
            Gender: ko.observable(),
            Language: ko.observable(),
            City: ko.observable(),
            Province: ko.observable(),
            Country: ko.observable(),
            Headimgurl: ko.observable(),
        }
    }
};
Login.viewModel.longPolling = function (result) {
    $.ajax({
        cache: false,
        type: 'get',
        url: '/comet/LongPolling/',
        data: { state: result.state },
        success: function (data) {
            if (data.State == result.state) {
                $.get("/api/WeChatUser/", function (wechatuser) {
                    if (wechatuser != null) {
                        ko.mapping.fromJS(wechatuser, {}, Login.viewModel.wechatuser);
                        //登录成功
                        location.href = "..";
                    }
                });

            } else {
                setTimeout(Login.viewModel.longPolling(result), 0);
            }
        }
    });

};
ko.bindingHandlers.qrbind = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        // This will be called when the binding is first applied to an element
        // Set up any initial state, event handlers, etc. here
    },
    update: function (element, valueAccessor) {
        var data = ko.toJS(Login.viewModel.wchatLoginQrCode);
        if (data != null) {
            $(element).empty();
            $(element).qrcode(data);
        }

    }
};
$(function () {
    ko.applyBindings(Login);
    $.get('/api/GetWechatLoginQrCode/', function (result) {
        Login.viewModel.wchatLoginQrCode(result.weChartloginUrl)
        Login.viewModel.longPolling(result);
    });
});