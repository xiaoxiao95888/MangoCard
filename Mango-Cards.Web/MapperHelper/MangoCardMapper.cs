using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.MapperHelper
{
   
    public class MangoCardMapper : IMangoCardMapper
    {
        private readonly string _cardThumbnailPath;
        public MangoCardMapper()
        {
            _cardThumbnailPath = ConfigurationManager.AppSettings["CardThumbnailPath"];
        }
        public void Create()
        {
            var userId = HttpContext.Current.User.Identity.GetUser().Id;
            var uploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"] + userId + "/";
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
            ;
            Mapper.CreateMap<Field, FieldModel>()
                .ForMember(n => n.FieldType, opt => opt.MapFrom(src => src.FieldType))
                .ForMember(n => n.MediaModel, opt => opt.MapFrom(src => src.Media));
          
            Mapper.CreateMap<MangoCard, MangoCardModel>()
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardTemplate.CardType.Id))
                .ForMember(n => n.FieldModels, opt => opt.MapFrom(src => src.Fields))
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardTemplate.CardType.Id))
                .ForMember(n => n.ThumbnailUrl, opt => opt.MapFrom(src => _cardThumbnailPath + src.ThumbnailUrl));

        }
    }
}