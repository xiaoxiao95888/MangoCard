using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.MapperHelper.Implementation
{
    public class MangoCardAttributeMapper : IMangoCardAttributeMapper
    {
        public void Create()
        {
            var mediaMapper = new MediaMapper();
            mediaMapper.Create();
            Mapper.CreateMap<Field, FieldModel>()
                .ForMember(n => n.FieldType, opt => opt.MapFrom(src => src.FieldType))
                .ForMember(n => n.MediaModel, opt => opt.MapFrom(src => Mapper.Map<Media, MediaModel>(src.Media)));
            Mapper.CreateMap<MangoCard, MangoCardAttributeModel>()
                .ForMember(n => n.FieldModels,
                    opt =>
                        opt.MapFrom(
                            src => src.Fields.OrderBy(p => p.Index).Select(Mapper.Map<Field, FieldModel>)))
                .ForMember(n => n.MangoCardId, opt => opt.MapFrom(src => src.Id))
                .ForMember(n => n.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(n => n.MangoCardUrl,
                    opt =>
                        opt.MapFrom(
                            src => $"http://{HttpContext.Current.Request.Url.Host}/Cards/RedirectCardView/{src.Id}"))
                .ForMember(n => n.Instructions, opt => opt.MapFrom(src => src.CardTemplate.Instructions));
        }
    }
}