var RegisterAdmin = {
    viewModel: {
        RegisterUserModel: {
            ApplyForDeveloperId: ko.observable(),
            WeChatUserId: ko.observable(),
            NickName: ko.observable(),
            Gender: ko.observable(),
            Language: ko.observable(),
            City: ko.observable(),
            Province: ko.observable(),
            Country: ko.observable(),
            Headimgurl: ko.observable(),
            PhoneNum: ko.observable(),
            Email: ko.observable(),
            Name: ko.observable(),
            Introduce: ko.observable(),
            UpdateTime: ko.observable(),
            CreatedTime: ko.observable(),
            IsDeleted: ko.observable(),
            Pass: ko.observable(),
            Reject: ko.observable()
        },
        RegisterUserModels: ko.observableArray(),
        Refresh: function () {
            $.get("/api/RegisterAdmin/", function (data) {
                ko.mapping.fromJS(data, {}, RegisterAdmin.viewModel.RegisterUserModels);
            });
        },
        Pass: function (data, event) {
            var model = ko.toJS(data);
            model.Pass = true;
            $.ajax({
                type: "PUT",
                url: "/api/RegisterAdmin/",
                contentType: "application/json",
                data: JSON.stringify(model),
                dataType: "json",
                success: function (result) {
                    if (result.Error) {
                        alert(result.Message);
                    } else {
                        RegisterAdmin.viewModel.Refresh();
                    }
                }
            });
        },
        ChosenRejectReasons: ko.observableArray(),
        RejectReasons: ko.observableArray([
            { itemValue: "手机号码错误" },
            { itemValue: "请完善你的介绍，我们不是太了解您。" },
            { itemValue: "您和我们的技能要求不太相符。" }
        ]),
        Reject: function (data, event) {
            var item = ko.mapping.toJS(data);
            ko.mapping.fromJS(item, {}, RegisterAdmin.viewModel.RegisterUserModel);
            $("#RejectMessage").modal({ show: true, backdrop: "static" });
        },
        RejectSubmit: function () {
            var model = ko.mapping.toJS(RegisterAdmin.viewModel.RegisterUserModel);
            model.Reject = true;
            $.ajax({
                type: "PUT",
                url: "/api/RegisterAdmin/",
                contentType: "application/json",
                data: JSON.stringify(model),
                dataType: "json",
                success: function (result) {
                    if (result.Error) {
                        alert(result.Message);
                    } else {
                        RegisterAdmin.viewModel.Refresh();
                    }
                }
            });
        },
        SelectReason: function (data, event) {
            var dom = $(event.target);
            var str = RegisterAdmin.viewModel.RegisterUserModel.RejectMessage();
            str = str + dom.val();
            RegisterAdmin.viewModel.RegisterUserModel.RejectMessage(str);
        }
    }
}
RegisterAdmin.viewModel.RegisterUserModel.RejectMessage = ko.computed({
    read: function() {
        var str = "";
        ko.utils.arrayForEach(RegisterAdmin.viewModel.ChosenRejectReasons(), function(item) {
            return item.itemValue;
        });
        return str;
    }
});
$(function () {
    ko.applyBindings(RegisterAdmin);
    $.get("/api/RegisterAdmin/", function (data) {
        ko.mapping.fromJS(data, {}, RegisterAdmin.viewModel.RegisterUserModels);
    });

});