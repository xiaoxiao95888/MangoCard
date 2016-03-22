using Mango_Cards.Library.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class MediaController : BaseApiController
    {
        private readonly IMediaService _mediaService;
        private readonly IWeChatUserService _weChatUserService;
        public MediaController(IMediaService mediaService, IWeChatUserService weChatUserService)
        {
            _mediaService = mediaService;
            _weChatUserService = weChatUserService;
        }

        public object Get()
        {
            var wechatuser = _weChatUserService.GetWeChatUser(HttpContext.Current.User.Identity.GetUser().Id);
            var uploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"] + wechatuser.Id + "/";
            var cssThumbnailUrl = "/images/css.png";
            var jsThumbnailUrl = "/images/js.png";
            var fileThumbnailUrl = "/images/file.png";
            Mapper.CreateMap<Media, MediaModel>()
                .ForMember(n => n.Url, opt => opt.MapFrom(src => uploadFileUrl + src.Name))
                .ForMember(n => n.ThumbnailUrl,
                    opt =>
                        opt.MapFrom(
                            src =>
                                src.MediaType.Name == "图片"
                                    ? (uploadFileUrl + src.Name)
                                    : (src.MediaType.Name == "CSS"
                                        ? cssThumbnailUrl
                                        : (src.MediaType.Name == "JS" ? jsThumbnailUrl : fileThumbnailUrl))));
            var model = wechatuser.Mediae.Where(n => !n.IsDeleted).OrderByDescending(n=>n.CreatedTime).GroupBy(n => n.MediaType).Select(n => new MediaTypeModel
            {
                Id = n.Key.Id,
                MediaModels = n.Select(Mapper.Map<Media, MediaModel>).ToArray(),
                Name = n.Key.Name
            });
            return model;
        }
    }
}
