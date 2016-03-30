using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.MapperHelper
{
   
    public class MangoCardMapper : IMangoCardMapper
    {
        public void Create()
        {
            Mapper.CreateMap<Field, FieldModel>().ForMember(n => n.FieldType, opt => opt.MapFrom(src => src.FieldType));
            Mapper.CreateMap<MangoCard, MangoCardModel>()
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id))
                .ForMember(n => n.FieldModels, opt => opt.MapFrom(src => src.Fields))
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id));
        }
    }
}