using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.MapperHelper.Implementation
{
    public class CardTypeMapper : ICardTypeMapper
    {
        public void Create()
        {
            Mapper.CreateMap<CardType, CardTypeModel>()
                .ForMember(n => n.CardTemplateModels, opt => opt.MapFrom(src => src.CardTemplates));
        }
    }
}