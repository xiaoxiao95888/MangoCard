var Home = {
    viewModel: {
        cardTypes: ko.observableArray(),
        employees: ko.observableArray(),
        typetoshow: ko.observable('*'),
        selectdemo: ko.observable(),
        wechatuser: {
            Id: ko.observable(),
            NickName: ko.observable(),
            Gender: ko.observable(),
            Language: ko.observable(),
            City: ko.observable(),
            Province: ko.observable(),
            Country: ko.observable(),
            Headimgurl: ko.observable()
        }
    }
};
ko.bindingHandlers.isotope = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        var $el = $(element);
        var value = ko.utils.unwrapObservable(valueAccessor());
        var $container = $(value.container);
        $container.isotope({
            itemSelector: value.itemSelector,
            layoutMode: "masonry"
        });
        if ($el != null) {
            $container.isotope('appended', $el);
            $container.imagesLoaded().progress(function () {
                $container.isotope('reloadItems');
                $container.isotope({ filter: Home.viewModel.typetoshow() });
            });
            $('.grid-item').hover(
                function() {
                    $(this).find('.caption').fadeIn(250);
                },
                function() {
                    $(this).find('.caption').fadeOut(205);
                });
        }
    }
};
Home.viewModel.carddemos = ko.computed(function () {
    var demos = [];
    var all = ko.toJS(Home.viewModel.cardTypes);
    ko.utils.arrayForEach(all, function (type) {
        ko.utils.arrayForEach(type.SubCardTypeModels, function (sub) {
            ko.utils.arrayForEach(sub.MangoCardModels, function (demo) {
                demos.push(demo);
            });
        });
    });

    return demos;
});

Home.viewModel.filters = function (data, event) {
    var dom = $(event.target);
    var filterValue = dom.attr('data-filter');
    Home.viewModel.typetoshow(filterValue);
    //// use filterFn if matches value    

    $('#container').isotope({ filter: filterValue });
};
Home.viewModel.showqrcode = function () {
   
    Home.viewModel.selectdemo(ko.toJS(this));
    $('#Dialog').modal({
        show: true,
        backdrop: 'static'
    });
};
Home.viewModel.closedialog = function () {
    $('#Dialog').modal('hide');
    
};
ko.bindingHandlers.qrbind = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        // This will be called when the binding is first applied to an element
        // Set up any initial state, event handlers, etc. here
    },
    update: function (element, valueAccessor) {
        var data = ko.toJS(Home.viewModel.selectdemo);
        if (data != null) {
            $(element).empty();
            $(element).qrcode(data.Url);
        }

    }
};
$(function () {
    ko.applyBindings(Home);
    $.get("/api/CardType/", function (data) {
        ko.mapping.fromJS(data, {}, Home.viewModel.cardTypes);
        $.get("/api/Employee/", function (employees) {
            ko.mapping.fromJS(employees, {}, Home.viewModel.employees);
            $.get("/api/WeChatUser/", function (wechatuser) {
                if (wechatuser != null) {
                    ko.mapping.fromJS(wechatuser, {}, Home.viewModel.wechatuser);
                }

            });
        });
    });
});

