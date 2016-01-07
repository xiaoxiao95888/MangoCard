var Cards = {
    viewModel: {
        mycards: ko.observableArray(),
        selectedcard: {
            Id: ko.observable(),
            HtmlCode: ko.observable(),
            PvCount: ko.observable(),
            ShareTimeCount: ko.observable(),
            IsPublish: ko.observable()
        },
        wechatuser: {
            Id: ko.observable(),
            NickName: ko.observable(),
            Gender: ko.observable(),
            Language: ko.observable(),
            City: ko.observable(),
            Province: ko.observable(),
            Country: ko.observable(),
            Headimgurl: ko.observable(),
        },
    }
};
Cards.viewModel.SelectCard = function () {
    var model = {        
      id:this.Id()  
    };
    $.get('/api/MyCards/',model, function (card) {
        ko.mapping.fromJS(card, {}, Cards.viewModel.selectedcard);
    });
};
$(function () {
    ko.applyBindings(Cards);
    $.get("/api/WeChatUser/", function (wechatuser) {
        if (wechatuser != null) {
            ko.mapping.fromJS(wechatuser, {}, Home.viewModel.wechatuser);
            $.get('/api/MyCards/', function (cards) {
                ko.mapping.fromJS(cards, {}, Cards.viewModel.mycards);

            });
        }
    });
    
});