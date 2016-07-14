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
        Reject: function (data, event) {
            var item = ko.mapping.toJS(data);
            ko.mapping.fromJS(item, {}, RegisterAdmin.viewModel.RegisterUserModel);
            $("#RejectMessage").modal({ show: true, backdrop: "static" });
        },
        RejectSubmit: function () {
            var model = ko.mapping.toJS(RegisterAdmin.viewModel.RegisterUserModel);
            model.Reject = true;
            var str = [];
                    $("[name='reason']:checked").each(function () {
                        str.push($(this).next().text());
                    });
            model.RejectMessage = str.join(",");
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
                        $("#RejectMessage").modal("hide");
                    }
                }
            });
        }
        
    }
}

$(function () {
    ko.applyBindings(RegisterAdmin);
    $.get("/api/RegisterAdmin/", function (data) {
        ko.mapping.fromJS(data, {}, RegisterAdmin.viewModel.RegisterUserModels);
    });

});