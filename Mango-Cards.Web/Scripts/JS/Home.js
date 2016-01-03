var Home = {
    viewModel: {
        cardTypes: ko.observableArray(),
        employees: ko.observableArray(),
        typetoshow: ko.observable('*'),
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

Home.viewModel.carddemos = ko.computed({
    read: function () {
        var demos = [];
        var all = ko.toJS(Home.viewModel.cardTypes);
        ko.utils.arrayForEach(all, function (type) {
            ko.utils.arrayForEach(type.SubCardTypeModels, function (sub) {
                ko.utils.arrayForEach(sub.CardDemoModels, function (demo) {
                    demos.push(demo)
                });
            });
        });
        return demos;
    }
});
var $container;
function initialize() {
    $container = $('#isotope').isotope({
        layoutMode: 'fitRows',
    });
    $(' #isotope > li ').each(function () { $(this).hoverdir(); });
}
Home.viewModel.filters = function (data, event) {
    var dom = $(event.target);
    var filterValue = dom.attr('data-filter')
    Home.viewModel.typetoshow(filterValue);
    // use filterFn if matches value    
    $container.isotope({ filter: filterValue });
};


ko.bindingHandlers.qrbind = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        // This will be called when the binding is first applied to an element
        // Set up any initial state, event handlers, etc. here
    },
    update: function (element, valueAccessor) {
        var data = ko.unwrap(valueAccessor());
        var url = window.location.href + "demo.html?id=" + data;
        $(element).qrcode(url);
    }
};
$(function () {
    ko.applyBindings(Home);
    $.get("http://api.card.mangoeasy.com/api/CardType/", function (data) {
        ko.mapping.fromJS(data, {}, Home.viewModel.cardTypes);
        initialize();
        $.get("http://api.card.mangoeasy.com/api/Employee/", function (employees) {
            ko.mapping.fromJS(employees, {}, Home.viewModel.employees);
            $.get("http://api.card.mangoeasy.com/api/WeChatUser/", function (wechatuser) {
                if (wechatuser != null) {
                    ko.mapping.fromJS(wechatuser, {}, Home.viewModel.wechatuser);
                }

            });
        });
    });
})

