
var Helper = {
    ShowErrorDialog: function (message) {
        var dialog = $("#Dialog");
        dialog.find(".modal-title").text("错误");
        dialog.find(".modal-body p").empty();
        dialog.find(".modal-body p").append(message);
        dialog.modal({
            keyboard: false,
            show: true
        });
    },
    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },
    ShowSuccessDialog: function (message) {
        var dialog = $("#Dialog");
        dialog.find(".modal-title").text("成功");
        dialog.find(".modal-body p").empty();
        dialog.find(".modal-body p").append(message);
        dialog.modal({
            keyboard: false,
            show: true
        });
    },
    ShowMessageDialog: function (message, title) {
        var dialog = $("#Dialog");
        dialog.find(".modal-title").text(title);
        dialog.find(".modal-body p").empty();
        dialog.find(".modal-body p").append(message);
        dialog.modal({
            keyboard: false,
            show: true
        });
    },
    ShowConfirmationDialog: function (parm) {
        var dialog = $("#Confirmation");
        dialog.find(".modal-title").text("提示");
        dialog.find(".modal-body p").empty();
        dialog.find(".modal-body p").append(parm.message);
        dialog.modal("show");
        callback = parm.confirmFunction;
    },
    getGuid: function () {
        function s4() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        }
        return (s4() + s4() + "-" + s4() + "-" + s4() + "-" + s4() + "-" + s4() + s4() + s4());
    },
    ClearObject: function (obj) {
        for (var attribute in obj) {
            if (obj.hasOwnProperty(attribute)) {
                if (typeof (obj[attribute]) == "object") {
                    this.ClearObject(obj[attribute]); //递归遍历
                } else {
                    obj[attribute] = null;
                }
            }
        }
    }
};



var callback;
var Messages = {
    UploadFileError: "文件类型错误，仅支持xls、xlsx",
    Success: "操作成功"
};
$(function () {
    var dialog = $("#Confirmation");
    var confirmbtn = dialog.find(".btn-primary");
    confirmbtn.click(function () {
        dialog.modal("hide");
        if (callback != null) {
            callback();
        }
    });
});


//var $container = $("#container");
//ko.bindingHandlers.isotope = {
//    init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {},
//    update: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
//        var $el = $(element);
//        var value = ko.utils.unwrapObservable(valueAccessor());
//        var $container = $(value.container);
//        $container.isotope({ itemSelector: value.itemSelector });
//        $container.isotope('appended', $el);
//    }
//};
//var box = function(input) {
//    var _color = ko.observable(input);
//    return { color: _color };
//};
//var layout = function() {
//    var _items = ko.observableArray([new box('red'), new box('red'), new box('cyan'), new box('cyan'), new box('green'), new box('cyan'), new box('red'), new box('green')]);
//    return { items: _items, addItem: add }

//    function add(item) {
//        console.log('item: ' + item);
//        _items.push(new box(item));
//    }
//};
//var vm = new layout();
//ko.applyBindings(vm);
//$('#filters a').click(function() {
//    var selector = $(this).attr('data-filter');
//    $container.isotope({ filter: selector });
//    return false;
//});
//$('#commands a').click(function () {
//    var data = $(this).attr('data');
//    vm.addItem(data);
//    return false;
//});