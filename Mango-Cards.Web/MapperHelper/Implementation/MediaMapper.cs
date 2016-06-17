using System;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using System.Web;
using System.Configuration;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.Models;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.MapperHelper.Implementation
{
    public class MediaMapper : IMediaMapper
    {
        public void Create()
        {
            //var userId = HttpContext.Current.User.Identity.GetUserId();
            var uploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"];
            const string cssThumbnailUrl = "/images/css.png";
            const string jsThumbnailUrl = "/images/js.png";
            const string fileThumbnailUrl = "/images/file.png";
            Mapper.CreateMap<Media, MediaModel>()
                .ForMember(n => n.Url, opt => opt.MapFrom(src => uploadFileUrl + "/" + src.WeChatUserId + "/" + src.Name))
                .ForMember(n => n.ThumbnailUrl,
                    opt =>
                        opt.MapFrom(
                            src =>
                                src.MediaType.Name == "图片"
                                    ? (uploadFileUrl + src.Name)
                                    : (src.MediaType.Name == "CSS"
                                        ? cssThumbnailUrl
                                        : (src.MediaType.Name == "JS" ? jsThumbnailUrl : fileThumbnailUrl))));
        }
    }
}