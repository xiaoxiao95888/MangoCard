using System;
using System.Configuration;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.MapperHelper.Implementation
{
    public class CardTemplateMapper : ICardTemplateMapper
    {
        private readonly string _cardThumbnailPath;
        public CardTemplateMapper()
        {
            _cardThumbnailPath = ConfigurationManager.AppSettings["CardThumbnailPath"];
        }
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Detail()
        {
            Mapper.CreateMap<Field, FieldModel>().ForMember(n => n.FieldType, opt => opt.MapFrom(src => src.FieldType));
            Mapper.CreateMap<CardTemplate, CardTemplateDetailModel>()
                .ForMember(n => n.EmployeeId, opt => opt.MapFrom(src => src.Employee.Id))
                .ForMember(n => n.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id))
                .ForMember(n => n.CardTypeName, opt => opt.MapFrom(src => src.CardType.Name))
                .ForMember(n => n.ThumbnailUrl, opt => opt.MapFrom(src => _cardThumbnailPath + src.ThumbnailUrl))
                .ForMember(n => n.FieldModels, opt => opt.MapFrom(src => src.Fields));
        }

        public void Normal()
        {
            Mapper.CreateMap<CardTemplate, CardTemplateModel>()
                .ForMember(n => n.EmployeeId, opt => opt.MapFrom(src => src.Employee.Id))
                .ForMember(n => n.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id))
                .ForMember(n => n.CardTypeName, opt => opt.MapFrom(src => src.CardType.Name))
                .ForMember(n => n.ThumbnailUrl, opt => opt.MapFrom(src => _cardThumbnailPath + src.ThumbnailUrl));
        }
    }
}