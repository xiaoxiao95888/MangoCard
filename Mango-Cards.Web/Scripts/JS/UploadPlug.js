var UploadPlug = {
    uploadprocess: function (data, event) {
        var file = $(event.target).get(0).files[0]; if (file != null) {            
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
            } 
        }

    }   
}
