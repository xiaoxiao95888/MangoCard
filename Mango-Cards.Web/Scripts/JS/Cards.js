var Cards = {
    viewModel: {
        mycardtypes: ko.observableArray(),
        mymediaetypes: ko.observableArray(),
        mediatypetoshow: ko.observable("*"),
        typetoshow: ko.observable("*"),
        selectedcard: {
            Id: ko.observable(),
            HtmlCode: ko.observable(),
            PvCount: ko.observable(),
            ShareTimeCount: ko.observable(),
            IsPublish: ko.observable()
        },
        wechatuser: {
            Id: ko.observable(),
            NickName: ko.observable(),
            Gender: ko.observable(),
            Language: ko.observable(),
            City: ko.observable(),
            Province: ko.observable(),
            Country: ko.observable(),
            Headimgurl: ko.observable()
        },
        file: {
            Name: ko.observable(),
            Verify: ko.observable(false),
            UploadProgress: ko.observable(false)
        },
        uploadprogress: {
            Valuenow: ko.observable("0%")
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
                $container.isotope({ filter: Cards.viewModel.mediatypetoshow() });
            });
        }
    }
};
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
Cards.viewModel.mycards = ko.computed(function () {
    var demos = [];
    var all = ko.toJS(Cards.viewModel.mycardtypes);
    ko.utils.arrayForEach(all, function (type) {
        ko.utils.arrayForEach(type.MangoCardModels, function (card) {
            demos.push(card);
        });
    });

    return demos;
});
Cards.viewModel.SelectCard = function () {
    var model = {
        id: this.Id()
    };
    $.get("/api/MyCards/", model, function (card) {
        ko.mapping.fromJS(card, {}, Cards.viewModel.selectedcard);
    });
};
Cards.viewModel.filters = function (data, event) {
    var dom = $(event.target);
    var filterValue = dom.attr("data-filter");
    Cards.viewModel.typetoshow(filterValue);
    //// use filterFn if matches value    

    $("#mediacontainer").isotope({ filter: filterValue });

};
Cards.viewModel.mediafilters = function (data, event) {
    var dom = $(event.target);
    var filterValue = dom.attr("data-filter");
    Cards.viewModel.mediatypetoshow(filterValue);
    //// use filterFn if matches value    
    $("#mediacontainer").isotope({ filter: filterValue });

};
Cards.viewModel.mediademos = ko.computed(function () {
    var demos = [];
    var all = ko.toJS(Cards.viewModel.mymediaetypes);
    ko.utils.arrayForEach(all, function (type) {
        ko.utils.arrayForEach(type.MediaModels, function (demo) {
            demos.push(demo);
        });
    });
    return demos;
});
//上传素材
Cards.viewModel.openfileselect = function () {
    $("#file").click();
};
//删除素材
Cards.viewModel.delete = function () {
    var selectedmedia = ko.mapping.toJS(this);
    Helper.ShowConfirmationDialog({
        message: "是否确认删除?",
        confirmFunction: function () {
            $.ajax({
                type: "delete",
                url: "/api/Media/" + selectedmedia.Id,
                contentType: "application/json",
                dataType: "json",
                success: function (result) {
                    if (result.Error) {
                        Helper.ShowErrorDialog(result.Message);
                    } else {
                        //Helper.ShowSuccessDialog(Messages.Success);
                        //刷新素材列表
                        $.get("/api/Media/", function (media) {
                            ko.mapping.fromJS(media, {}, Cards.viewModel.mymediaetypes);
                        });
                    }
                }
            });
        }
    });
};
Cards.viewModel.fileselected = function () {
    var file = document.getElementById("file").files[0];
    if (file != null) {
        Cards.viewModel.file.Name(file.name);
        if (file.size > 5120000) {
            Cards.viewModel.file.Verify(false);
            Cards.viewModel.file.UploadProgress(false);
        } else {
            Cards.viewModel.file.Verify(true);
            Cards.viewModel.file.UploadProgress(true);
            var fd = new FormData();
            fd.append("file", file);
            var xhr = new XMLHttpRequest();
            xhr.upload.addEventListener("progress", uploadProgress, false);
            xhr.addEventListener("load", uploadComplete, false);
            xhr.addEventListener("error", uploadFailed, false);
            xhr.addEventListener("abort", uploadCanceled, false);
            xhr.open("POST", "/Api/Upload");
            xhr.send(fd);
        }

    }
};
function uploadProgress(evt) {
    if (evt.lengthComputable) {
        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
        Cards.viewModel.uploadprogress.Valuenow(percentComplete.toString() + "%");
        //document.getElementById("progressNumber").innerHTML = percentComplete.toString() + "%";
    }
    else {
        //document.getElementById("progressNumber").innerHTML = "unable to compute";
    }
}
function uploadComplete(evt) {
    /* This event is raised when the server send back a response */
    if (!evt.target.response.Error) {
        Cards.viewModel.file.Name("上传成功!");
        Cards.viewModel.file.UploadProgress(false);
        //刷新素材列表
        $.get("/api/Media/", function (media) {
            ko.mapping.fromJS(media, {}, Cards.viewModel.mymediaetypes);
            ko.mapping.fromJS({
                Name: null,
                Verify: false,
                UploadProgress: false
            }, {}, Cards.viewModel.file);

        });
    } else {
        Cards.viewModel.file.Name("上传失败!");
    }

}

function uploadFailed(evt) {
    alert("There was an error attempting to upload the file.");
}
function uploadCanceled(evt) {
    alert("The upload has been canceled by the user or the browser dropped the connection.");
}
$(function () {
    ko.applyBindings(Cards);
    $.get("/api/WeChatUser/", function (wechatuser) {
        if (wechatuser != null) {
            ko.mapping.fromJS(wechatuser, {}, Cards.viewModel.wechatuser);
            $.get("/api/MyCards/", function (cards) {
                ko.mapping.fromJS(cards, {}, Cards.viewModel.mycardtypes);
                $.get("/api/Media/", function (media) {
                    ko.mapping.fromJS(media, {}, Cards.viewModel.mymediaetypes);
                });
            });

        }
    });

});