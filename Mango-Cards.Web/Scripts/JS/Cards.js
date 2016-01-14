var Cards = {
    viewModel: {
        mycardtypes: ko.observableArray(),
        typetoshow: ko.observable('*'),
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
        isotopeOptions: { itemSelector: ".portfolio-item" }
    }
};

Cards.viewModel.mycards = ko.computed(function () {
    var demos = [];
    var all = ko.toJS(Cards.viewModel.mycardtypes);
    ko.utils.arrayForEach(all, function (type) {
        ko.utils.arrayForEach(type.MangoCardModels, function (card) {
            demos.push(card);
        });
    });

    return demos;
});
Cards.viewModel.SelectCard = function () {
    var model = {        
      id:this.Id()  
    };
    $.get('/api/MyCards/',model, function (card) {
        ko.mapping.fromJS(card, {}, Cards.viewModel.selectedcard);
    });
};
Cards.viewModel.filters = function (data, event) {
    var dom = $(event.target);
    var filterValue = dom.attr('data-filter');
    Cards.viewModel.typetoshow(filterValue);
    //// use filterFn if matches value    

    $('#container').isotope({ filter: filterValue });
    
};
$(function () {
    ko.applyBindings(Cards);
    $.get("/api/WeChatUser/", function (wechatuser) {
        if (wechatuser != null) {
            ko.mapping.fromJS(wechatuser, {}, Cards.viewModel.wechatuser);
            $.get('/api/MyCards/', function (cards) {
                ko.mapping.fromJS(cards, {}, Cards.viewModel.mycardtypes);
            });
        }
    });
    
});