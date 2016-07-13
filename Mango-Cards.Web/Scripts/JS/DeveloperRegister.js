var DeveloperRegister = {
    viewModel: {
        ApplyForDeveloper: {
            PhoneNum: ko.observable(),
            Email: ko.observable(),
            Name: ko.observable(),
            Introduce: ko.observable()
        },
        Submit: function () {
            var model = ko.toJS(DeveloperRegister.viewModel.ApplyForDeveloper);
            $.post("/api/ApplyForDeveloper/", model, function (result) {
                if (result.Error) {
                    Helper.ShowErrorDialog(result.Message);
                } else {
                    $("#register").hide(0,function () {
                        $("#tips").show();
                    });
                }
            });
        }
    }
};
$(function () {
    ko.applyBindings(DeveloperRegister);
});