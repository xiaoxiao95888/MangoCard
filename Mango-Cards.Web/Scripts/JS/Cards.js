var Cards = {
    viewModel: {
        mycardtypes: ko.observableArray(),
        mediaetypes: ko.observableArray(),
        medias: ko.observableArray(),
        mediatypetoshow: ko.observable("*"),
        typetoshow: ko.observable("*"),
        MangoCardAttribute: {
            MangoCardId: ko.observable(),
            MangoCardTitle: ko.observable(),
            MangoCardUrl: ko.observable(),
            HtmlCode: ko.observable(),
            Instructions: ko.observable(),
            FieldModels: ko.observableArray()
        },
        //预览URL
        PreviewCard: {
            MangoCardUrl: ko.observable(),
            MangoCardTitle: ko.observable()
        },
        SaveAndPreview: ko.observable(false),
        MediaeType: ko.observable(),
        Mediae: ko.observable(),
        //查看的素材
        ViewMediae: {
            ExtensionName: ko.observable(),
            FileName: ko.observable(),
            Id: ko.observable(),
            MediaTypeId: ko.observable(),
            Name: ko.observable(),
            ThumbnailUrl: ko.observable(),
            Url: ko.observable(),
        },
        FieldModel: ko.observable(),
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
        },
        pagedata: {
            Fields: ko.observableArray(),
            Rows: ko.observableArray()
        },
        selectcard: ko.observable(),
        baseaccessdata: {
            CardTitle: ko.observable(),
            CardType: ko.observable(),
            PvDataCount: ko.observable(),
            ShareTimeCount: ko.observable()
        },
        codeMirror: ko.observable()
    }
};
Cards.viewModel.CurrentMediaIds = ko.computed({
    read: function () {
        var ids = []
        ko.utils.arrayForEach(Cards.viewModel.MangoCardAttribute.FieldModels(), function (item) {
            var model = ko.mapping.toJS(item.MediaModel);
            if (model != null) {
                ids.push(model.Id)
            }
        });
        return ids;
    }
});
ko.bindingHandlers.isotopetype = {
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
                $container.isotope({ filter: Cards.viewModel.typetoshow() });
            });
        }
        $('.grid-item').hover(
               function () {
                   $(this).find('.caption').fadeIn(250);
               },
               function () {
                   $(this).find('.caption').fadeOut(205);
               });
    }
};
ko.bindingHandlers.nailthumb = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var $el = $(element);
        var value = ko.utils.unwrapObservable(valueAccessor());
        $el.attr("src", value.url).imagesLoaded($el, function () { $el.nailthumb({ width: 155, height: 116 }); });
        //$el.attr("src", value.url);
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
function buildHtmlTable(result) {
    var columns = addAllColumnHeaders(result);
    var rows = [];
    for (var i = 0 ; i < result.length ; i++) {
        var cellValues = [];
        for (var colIndex = 0 ; colIndex < columns.length ; colIndex++) {
            var cellValue = result[i][columns[colIndex]];

            if (cellValue == null) { cellValue = ""; }

            cellValues.push({ value: cellValue });
        }
        rows.push({ tdvalues: cellValues });
    }
    ko.mapping.fromJS(rows, {}, Cards.viewModel.pagedata.Rows);
}

// Adds a header row to the table and returns the set of columns.
// Need to do union of keys from all records as some records may not contain
// all records
function addAllColumnHeaders(myList) {
    var columnSet = [];
    for (var i = 0 ; i < myList.length ; i++) {
        var rowHash = myList[i];
        for (var key in rowHash) {
            if ($.inArray(key, columnSet) === -1) {
                columnSet.push(key);

            }
        }
    }
    ko.mapping.fromJS(columnSet, {}, Cards.viewModel.pagedata.Fields);
    return columnSet;
}
//点击编辑
Cards.viewModel.edit = function () {
    $("#normallink").click();
    var model = ko.toJS(this);
    $.get("/api/MangoCardAttribute/" + model.Id, function (card) {
        ko.mapping.fromJS(card, {}, Cards.viewModel.MangoCardAttribute);
        ko.mapping.fromJS(card, {}, Cards.viewModel.PreviewCard);
        //定位
        $("html, body").stop().animate({
            scrollTop: $("#dataedit").offset().top - 25
        }, 600);
    });
};
//显示素材库
Cards.viewModel.showlibrary = function () {
    Cards.viewModel.FieldModel = this;
    Cards.viewModel.Mediae(null);
    Cards.viewModel.MediaeType(null);
    $("#library-dialog").modal({ show: true, backdrop: "static" });
    $.get("/api/Media/", function (data) {
        ko.mapping.fromJS(data, {}, Cards.viewModel.medias);
    });
    $.get("/api/MediaeType/", function (data) {
        ko.mapping.fromJS(data, {}, Cards.viewModel.mediaetypes);
    });
};
//过滤素材
Cards.viewModel.libraryFilter = function () {
    var model = ko.mapping.toJS(this);
    if (model.Id == null) {
        Cards.viewModel.MediaeType(null);
    } else {
        Cards.viewModel.MediaeType(model);
    }
    $.get("/api/Media", { MediaTypeId: model.Id }, function (data) {
        ko.mapping.fromJS(data, {}, Cards.viewModel.medias);

    });
}
//选择素材
Cards.viewModel.SelectedMediae = function () {
    var mediae = ko.mapping.toJS(this);
    var current = ko.mapping.toJS(Cards.viewModel.Mediae);
    if (current != null && mediae.Id === current.Id) {
        Cards.viewModel.Mediae(null);
    } else {
        Cards.viewModel.Mediae(mediae);
    }

}
//确定选择的素材
Cards.SelectMediaeDetermine = function () {
    var mediae = ko.mapping.toJS(Cards.viewModel.Mediae);
    ko.mapping.fromJS(mediae, {}, Cards.viewModel.FieldModel.MediaModel);
    $("#library-dialog").modal("hide");
}
//高级编辑保存
Cards.viewModel.advancedsave = function (data, event) {
    var dom = $(event.target);
    dom.button("loading");
    var model = ko.mapping.toJS(Cards.viewModel.MangoCardAttribute);
    model.HtmlCode = Cards.viewModel.codeMirror().codemirror.doc.cm.getValue();
    $.ajax({
        type: "put",
        url: "/api/MyCards/" + model.MangoCardId,
        contentType: "application/json",
        data: JSON.stringify(model),
        dataType: "json",
        success: function (result) {
            if (result.Error) {
                Helper.ShowErrorDialog(result.Message);
            } else {
                if (Cards.viewModel.SaveAndPreview()) {
                    //弹出预览二维码
                    var dialog = $("#preview-dialog");
                    dialog.modal({
                        keyboard: false,
                        show: true,
                        backdrop: "static"
                    });
                }
            }
            dom.button("reset");
        }
    });
};
//预览二维码
Cards.viewModel.preview = function () {
    var model = ko.toJS(this);
    $.get("/api/MangoCardAttribute/" + model.Id, function (card) {
        ko.mapping.fromJS(card, {}, Cards.viewModel.PreviewCard);
        var dialog = $("#preview-dialog");
        dialog.modal({
            keyboard: false,
            show: true,
            backdrop: "static"
        });
    });
}
//普通编辑保存
Cards.viewModel.normalsave = function (data, event) {
    var dom = $(event.target);
    dom.button('loading');
    var model = ko.mapping.toJS(Cards.viewModel.MangoCardAttribute);
    $.ajax({
        type: "put",
        url: "/api/MangoCardAttribute/" + model.MangoCardId,
        contentType: "application/json",
        data: JSON.stringify(model),
        dataType: "json",
        success: function (result) {
            if (result.Error) {
                Helper.ShowErrorDialog(result.Message);
            } else {
                if (Cards.viewModel.SaveAndPreview()) {
                    //弹出预览二维码
                    var dialog = $("#preview-dialog");
                    dialog.modal({
                        keyboard: false,
                        show: true,
                        backdrop: "static"
                    });
                } else {
                    dom.button('success');
                    //$(".OperationTips").addClass("alert-success");
                    //$(".OperationTips h4").text("success");
                    //$(".OperationTips").show();
                }

            }
            dom.button("reset");
        }
    });
};
//发布
Cards.viewModel.submitaudit = function () {
    var dialog = $("#publish-dialog");
    dialog.modal({
        keyboard: false,
        show: true,
        backdrop: "static"
    });
}
//点击显示数据
Cards.viewModel.data = function () {
    var model = ko.toJS(this);
    Cards.viewModel.selectcard(model);
    $.get("/api/PageValue/" + model.Id, function (result) {
        buildHtmlTable(result);
        //定位
        $("html, body").stop().animate({
            scrollTop: $("#dataview").offset().top - 25
        }, 600);
    });
    $.get("/api/BasicAccessData/" + model.Id, function (result) {
        ko.mapping.fromJS(result, {}, Cards.viewModel.baseaccessdata);
    });
};
//刷新表单数据
Cards.viewModel.refreshformpagevalue = function (data, event) {
    var dom = $(event.target);
    dom.removeClass("fa-refresh");
    dom.addClass("fa-spinner fa-pulse");
    var model = ko.toJS(Cards.viewModel.selectcard);
    $.get("/api/PageValue/" + model.Id, function (result) {
        buildHtmlTable(result);
        dom.removeClass("fa-spinner fa-pulse");
        dom.addClass("fa-refresh");
        dom.tooltip({
            title: "Completed!"
        });
        dom.tooltip("show");
        dom.on("hidden.bs.tooltip", function () {
            dom.tooltip("destroy");
        });
    });
};
//刷新基础数据
Cards.viewModel.refreshbasepagevalue = function (data, event) {
    //var dom = $(event.target);
    //dom.removeClass("fa-refresh");
    //dom.addClass("fa-spinner fa-pulse");
    //var model = ko.toJS(Cards.viewModel.selectcard);
    //$.get("/api/PageValue/" + model.Id, function (result) {
    //    buildHtmlTable(result);
    //    dom.removeClass("fa-spinner fa-pulse");
    //    dom.addClass("fa-refresh");
    //});
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
Cards.viewModel.filters = function (data, event) {
    var dom = $(event.target);
    var filterValue = dom.attr("data-filter");
    Cards.viewModel.typetoshow(filterValue);
    //// use filterFn if matches value    

    $("#container").isotope({ filter: filterValue });

};
Cards.viewModel.mediafilters = function (data, event) {
    var dom = $(event.target);
    var filterValue = dom.attr("data-filter");
    Cards.viewModel.mediatypetoshow(filterValue);
    Cards.viewModel.mediafiltersshow();
};
Cards.viewModel.mediafiltersshow = function () {
    var filterValue = Cards.viewModel.mediatypetoshow();
    if (filterValue === "*") {
        $(".imggrid").animate({
            opacity: 'show'
        }, 600);
    } else {
        $(".imggrid").each(function () {
            if ($(this).hasClass(filterValue.replace(".", ""))) {
                $(this).animate({
                    opacity: 'show'
                }, 600);
            } else {
                $(this).hide();
            }
        });
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
            $(element).qrcode(valueUnwrapped);
        }

    }
};

//上传素材
Cards.viewModel.openfileselect = function () {
    $("#file").click();
};
//删除素材
Cards.viewModel.delete = function () {
    var selectedmedia = ko.mapping.toJS(this);
    var ids = Cards.viewModel.CurrentMediaIds();
    if (ids.indexOf(selectedmedia.Id) != -1) {
        Helper.ShowErrorDialog("素材正在使用，禁止删除。");
    } else {
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
                            //刷新素材列表
                            $.get("/api/Media/", function (media) {
                                ko.mapping.fromJS(media, {}, Cards.viewModel.medias);
                            });
                        }
                    }
                });
            }
        });
    }
};
//拷贝素材URL
Cards.viewModel.copylink = function (data, event) {
    var dom = $(event.target);
    var btn = dom.parents(".action-group").find(".hide")[0];
    var clipboard = new Clipboard(btn);
    clipboard.on("success", function (e) {
        console.log("copy success");
    });
    clipboard.on("error", function (e) {
        //console.log(e);
        console.log("copy error");
    });
    btn.click();
    dom.tooltip({
        title: "Copied!"
    });
    dom.tooltip("show");
    dom.on("hidden.bs.tooltip", function () {
        dom.tooltip("destroy");
    });
    clipboard.destroy();
};
//tab
Cards.viewModel.tab = function (data, event) {
    var dom = $(event.target);
    event.preventDefault();
    dom.tab('show');
    if (dom.attr("id") === "advancedlink") {
        $("#editnormal").hide();
        $.get("/api/MangoCardAttribute/" + Cards.viewModel.MangoCardAttribute.MangoCardId(), function (card) {
            ko.mapping.fromJS(card, {}, Cards.viewModel.MangoCardAttribute);
            $("#editadvanced").show(function () {
                var area = document.getElementById("editadvancedarea");

                if (Cards.viewModel.codeMirror() == null) {
                    var codemirror = CodeMirror(area, {
                        value: Cards.viewModel.MangoCardAttribute.HtmlCode(),
                        lineNumbers: true,
                        lineWrapping: true,
                        mode: "htmlmixed",
                        theme: "ambiance"
                    });
                    var model = {
                        codemirror: codemirror,
                        cardId: Cards.viewModel.MangoCardAttribute.MangoCardId()
                    }
                    Cards.viewModel.codeMirror(model);
                } else {
                    Cards.viewModel.codeMirror().codemirror.doc.cm.clearHistory();
                    Cards.viewModel.codeMirror().codemirror.doc.cm.setValue(Cards.viewModel.MangoCardAttribute.HtmlCode());
                    Cards.viewModel.codeMirror().cardId = Cards.viewModel.MangoCardAttribute.MangoCardId();
                }

                //定位
                $("html, body").stop().animate({
                    scrollTop: $("#dataedit").offset().top - 25
                }, 600);

            });

        });

    } else {
        $("#editadvanced").hide();
        $("#editnormal").show();
    }
};
//HTML5上传
Cards.viewModel.fileselected = function () {
    var file = document.getElementById("file").files[0];
    if (file != null) {
        Cards.viewModel.file.Name(file.name);
        if (file.size > 5242880) {
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
Cards.viewModel.UploadMaterial = function (data, event) {
    var dom = $(event.target);
    dom.next().click();
}
//查看上传的素材
Cards.viewModel.ViewUpload = function (data, event) {
    var dom = $(event.target);
    //img-dialog
    var img = ko.mapping.toJS(data);
    ko.mapping.fromJS(img, {}, Cards.viewModel.ViewMediae);
    $("#img-dialog").modal({ show: true, backdrop: "static" });
}
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
            ko.mapping.fromJS(media, {}, Cards.viewModel.medias);
            ko.mapping.fromJS({
                Name: null,
                Verify: false,
                UploadProgress: false
            }, {}, Cards.viewModel.file);
            Cards.viewModel.mediafiltersshow();
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
            });
        }
    });
});