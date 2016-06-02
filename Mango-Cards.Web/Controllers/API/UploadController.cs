using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class UploadController : BaseApiController
    {
        private readonly IMediaService _mediaService;
        private readonly IMediaTypeService _mediaTypeService;
        public UploadController(IMediaService mediaService, IMediaTypeService mediaTypeService)
        {
            _mediaService = mediaService;
            _mediaTypeService = mediaTypeService;
        }
        public object Post()
        {
            var fileFullPath = string.Empty;
            var weChatUserId = HttpContext.Current.User.Identity.GetUser().Id;
            var newFileName = string.Empty;
            try
            {
               
                var file = HttpContext.Current.Request.Files[0];
                var uploadFilePath = ConfigurationManager.AppSettings["UploadFilePath"] + weChatUserId + @"\";
                if (!Directory.Exists(uploadFilePath))
                {
                    Directory.CreateDirectory(uploadFilePath);
                }
                var fileNamePrefix = Guid.NewGuid().ToString();
                var id = Guid.NewGuid();
                if (file.ContentLength > 0)
                {
                  
                    var fileName = Path.GetFileName(file.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    var extension = string.IsNullOrEmpty(fileExtension) == false
                        ? fileExtension.ToUpper().Replace(".", string.Empty)
                        : string.Empty;
                    var typeId =
                        _mediaTypeService.GetMediaTypes()
                            .Where(n => n.Extension.Contains(extension))
                            .Select(n => n.Id)
                            .FirstOrDefault();
                    if (typeId == Guid.Empty)
                    {
                        typeId = _mediaTypeService.GetMediaTypes().Where(n => n.Extension == null).Select(n => n.Id)
                            .FirstOrDefault();
                    }
                    newFileName = fileNamePrefix + fileExtension;
                    fileFullPath = uploadFilePath + newFileName;
                    file.SaveAs(uploadFilePath + newFileName);
                    
                    var media = new Media
                    {
                        Id = id,
                        FileName = fileName,
                        ExtensionName = extension,
                        MediaTypeId = typeId,
                        Name = newFileName,
                        WeChatUserId = weChatUserId
                    };
                    _mediaService.Insert(media);
                }
              
                var uploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"] + weChatUserId + "/";
                return new UploadResponseModel
                {
                    ErrorCode = 0,
                    Message = "success",
                    Error = false,
                    FileId = id,
                    OriginalFileName = file.FileName,
                    Url = uploadFileUrl + newFileName
                };
            }
            catch (Exception ex)
            {
                File.Delete(fileFullPath);
                return Failed(ex.Message);
            }
        }
    }
}
