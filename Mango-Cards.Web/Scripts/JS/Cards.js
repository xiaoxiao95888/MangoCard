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
        isotopeOptions: { itemSelector: ".portfolio-item" },
        file: {
            Name: ko.observable(),
            Verify: ko.observable(false),
        },
        uploadprogress: {
            Valuenow: ko.observable("0%")
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
      id:this.Id()  
    };
    $.get("/api/MyCards/",model, function (card) {
        ko.mapping.fromJS(card, {}, Cards.viewModel.selectedcard);
    });
};
Cards.viewModel.filters = function (data, event) {
    var dom = $(event.target);
    var filterValue = dom.attr("data-filter");
    Cards.viewModel.typetoshow(filterValue);
    //// use filterFn if matches value    

    $("#container").isotope({ filter: filterValue });
    
};
//上传素材
Cards.viewModel.openfileselect = function () {
    $("#file").click();
};
Cards.viewModel.fileselected = function () {
    var file = document.getElementById("file").files[0];
    if (file != null) {
        Cards.viewModel.file.Name(file.name);
        if (file.size > 5120000) {
            Cards.viewModel.file.Verify(false);
        } else {
            Cards.viewModel.file.Verify(true);
            
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