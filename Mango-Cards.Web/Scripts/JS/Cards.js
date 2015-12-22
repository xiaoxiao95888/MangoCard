var Cards = {
    viewModel: {
        mycards: ko.observableArray(),
        selectedcard: {
            Id: ko.observable(),
            HtmlCode: ko.observable(),
            PvCount: ko.observable(),
            ShareTimeCount: ko.observable(),
            IsPublish: ko.observable()
        }
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
    $.get('/api/MyCards/', function(cards) {
        ko.mapping.fromJS(cards, {}, Cards.viewModel.mycards);
    });
});