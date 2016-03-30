using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Models.Enum;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.MapperHelper
{
    public class CardTypeMapper : ICardTypeMapper
    {
        public void Create()
        {
            Mapper.CreateMap<CardType, CardTypeModel>()
                .ForMember(n => n.MangoCardModels,
                    opt => opt.MapFrom(src => src.MangoCards.Where(m => m.PageType == PageType.Demo)));
        }
    }
}