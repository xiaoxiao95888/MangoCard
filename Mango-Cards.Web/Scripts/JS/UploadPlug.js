var UploadPlug = {
    dom: "",
    progressbar: "",
    p: "",
    obj: "",
    uploadprocess: function (data, event) {
        UploadPlug.obj = data;
        var target = $(event.target).get(0);
        UploadPlug.dom = $(target);
        UploadPlug.progressbar = $(target).prevAll(".progress").first();
        UploadPlug.p = $(target).prevAll("p").first();
        UploadPlug.progressbar.find(".progress-bar").first().css("width", "0%");
        UploadPlug.progressbar.find("span").first().html("0%");
        var file = target.files[0];
        if (file != null) {
            if (file.size <= 5120000) {
                var fd = new FormData();
                fd.append("file", file);
                var xhr = new XMLHttpRequest();
                xhr.upload.addEventListener("progress", uploadProgress, false);
                xhr.addEventListener("load", uploadComplete, false);
                xhr.addEventListener("error", uploadFailed, false);
                xhr.addEventListener("abort", uploadCanceled, false);
                xhr.open("POST", "/Api/Upload");
                xhr.send(fd);
            } else {
                Helper.ShowErrorDialog("文件大小超出5M的限制");
            }
        }
    }
}
function uploadProgress(evt) {
    if (evt.lengthComputable) {
        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
        //document.getElementById("progressNumber").innerHTML = percentComplete.toString() + "%";
        UploadPlug.p.hide();
        UploadPlug.progressbar.show();
        UploadPlug.progressbar.find(".progress-bar").first().css("width", percentComplete + "%");
        UploadPlug.progressbar.find("span").first().html(percentComplete + "%");
    }
    else {
        //document.getElementById("progressNumber").innerHTML = "unable to compute";
    }
}
function uploadComplete(evt) {
    /* This event is raised when the server send back a response */
    var result = jQuery.parseJSON(evt.target.response);
    if (!result.Error) {
        UploadPlug.p.show();
        UploadPlug.progressbar.hide();
        var currentfield = ko.mapping.toJS(UploadPlug.obj);
        Cards.viewModel.MangoCardAttribute.FieldModels.remove(UploadPlug.obj);
        var newfield = {
            Description: ko.observable(currentfield.Description),
            FieldType: ko.observable(currentfield.FieldType),
            FieldTypeName: ko.observable(currentfield.FieldTypeName),
            FieldValue: ko.observable(currentfield.FieldValue),
            Id: ko.observable(currentfield.Id),
            Index: ko.observable(currentfield.Index),
            Name: ko.observable(currentfield.Name),
            MediaModel: {
                ExtensionName: ko.observable(result.MediaModel.ExtensionName),
                FileName: ko.observable(result.MediaModel.FileName),
                Id: ko.observable(result.MediaModel.Id),
                MediaTypeId: ko.observable(result.MediaModel.MediaTypeId),
                Name: ko.observable(result.MediaModel.Name),
                ThumbnailUrl: ko.observable(result.MediaModel.ThumbnailUrl),
                Url: ko.observable(result.MediaModel.Url),
                UpdateTime: ko.observable(result.MediaModel.UpdateTime)
            }
        }
        Cards.viewModel.MangoCardAttribute.FieldModels.splice(currentfield.Index, 0, newfield);

    } else {

    }

}
function uploadFailed(evt) {
    alert("There was an error attempting to upload the file.");
}
function uploadCanceled(evt) {
    alert("The upload has been canceled by the user or the browser dropped the connection.");
}