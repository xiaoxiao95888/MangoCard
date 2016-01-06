var Home = {
    viewModel: {
        cardTypes: ko.observableArray(),
        employees: ko.observableArray(),
        typetoshow: ko.observable('*'),
        showdialog: ko.observable(false),
        isshowqrcode: ko.observable(true),
        selectdemo: ko.observable(),
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

Home.viewModel.carddemos = ko.computed(function () {
    var demos = [];
    var all = ko.toJS(Home.viewModel.cardTypes);
    ko.utils.arrayForEach(all, function (type) {
        ko.utils.arrayForEach(type.SubCardTypeModels, function (sub) {
            ko.utils.arrayForEach(sub.CardDemoModels, function (demo) {
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
    Home.viewModel.isshowqrcode(true);
    Home.viewModel.selectdemo(ko.toJS(this));
    $('#Dialog').modal({
        show: true,
        backdrop: 'static'
    });
};
Home.viewModel.closedialog = function () {
    Home.viewModel.showdialog(false);
    $('#Dialog').modal('hide');
    
};
Home.viewModel.longPolling = function (result) {
    $.ajax({
        cache: false,
        type: 'get',
        url: '/comet/LongPolling/',
        data: { state: result.state },
        success:function(data) {
            if (data.State == result.state) {
                $.get("/api/WeChatUser/", function (wechatuser) {
                    if (wechatuser != null) {
                        ko.mapping.fromJS(wechatuser, {}, Home.viewModel.wechatuser);
                        Home.viewModel.isshowqrcode(false);
                    }
                });

            } else if (Home.viewModel.showdialog() == true) {
                setTimeout(Home.viewModel.longPolling(result), 0);
            }
        }
    });
    
};
Home.viewModel.mycards = function () {
    $.get('/api/GetWechatLoginQrCode/', function (result) {
        var model = {
            Name: result.Name,
            Url: result.weChartloginUrl
        };
        Home.viewModel.selectdemo(model);
        $('#Dialog').modal({
            show: true,
            backdrop: 'static'
        });
        Home.viewModel.showdialog(true);
        Home.viewModel.longPolling(result);
    });
};
//Çå¿Õwechatuser
Home.viewModel.clearwechatuser = function () {
    for (var index in Home.viewModel.wechatuser) {
        if (ko.isObservable(Home.viewModel.wechatuser[index])) {
            Home.viewModel.wechatuser[index](null);
        }
    }
};
Home.viewModel.logout = function() {
    $.get('/LogOff/Logout/', function (result) {
        Home.viewModel.clearwechatuser();
        Home.viewModel.isshowqrcode(true);
    });
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

