var GetMangoCard = {
    viewModel: {
        cardTypes: ko.observableArray(),
        employees: ko.observableArray(),
    }
};

$(function () {
    ko.applyBindings(GetMangoCard);
    $.get("/api/GetWechatLoginQRCode/", function (result) {
        $('#qrcode').empty();
        $('#qrcode').qrcode(result.weChartloginUrl);
        function longPolling() {
            $.get('/comet/LongPolling/', { state: result.state }, function (data) {
                if (data.State == result.state) {
                    location.href = "http://card.mangoeasy.com/"; //location.href实现客户端页面的跳转  
                }
                longPolling();
            });
        }
        longPolling();
    });

});