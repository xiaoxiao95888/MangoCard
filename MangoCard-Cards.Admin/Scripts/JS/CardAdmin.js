var CardAdmin = {
    viewModel: {
        

    }
}

$(function () {
    ko.applyBindings(RegisterAdmin);
    $.get("/api/CardAdmin/", function (data) {
        ko.mapping.fromJS(data, {}, RegisterAdmin.viewModel.RegisterUserModels);
    });

});