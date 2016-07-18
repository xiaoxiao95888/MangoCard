var CardAdmin = {
    viewModel: {
        MangoCards: ko.observableArray()
    }
}
ko.bindingHandlers.date = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Date formats: http://momentjs.com/docs/#/displaying/format/
        var pattern = allBindings.format || "YYYY/MM/DD";

        var output = "-";
        if (valueUnwrapped !== null && valueUnwrapped !== undefined && valueUnwrapped.length > 0) {
            output = moment(valueUnwrapped).format(pattern);
        }

        if ($(element).is("input") === true) {
            $(element).val(output);
        } else {
            $(element).text(output);
        }
    }
};
ko.bindingHandlers.qrbind = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        // This will be called when the binding is first applied to an element
        // Set up any initial state, event handlers, etc. here
    },
    update: function (element, valueAccessor) {
        $(element).empty();
        var value = valueAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);
        if (valueUnwrapped != null) {
            //$(element).qrcode(valueUnwrapped);
            $(element).qrcode(
              {
                  width: 150,
                  height: 150,
                  text: valueUnwrapped
              }
          );
        }
    }
};
$(function () {
    ko.applyBindings(CardAdmin);
    $.get("/api/CardApproved/", function (data) {
        ko.mapping.fromJS(data, {}, CardAdmin.viewModel.MangoCards);
    });
});