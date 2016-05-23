using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Models.Enum;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.MapperHelper.IMapperInterfaces;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers.API
{
    public class CardTypeController : BaseApiController
    {
        private readonly ICardTypeService _cardTypeService;
        public CardTypeController(ICardTypeService cardTypeService, IMapperFactory mapperFactory)
        {
            _cardTypeService = cardTypeService;
            mapperFactory.GetCardTemplateMapper().Normal();
            mapperFactory.GetCardTypeMapper().Create();
        }
        public object Get()
        {
            return _cardTypeService.GetCardTypes().Select(Mapper.Map<CardType, CardTypeModel>);
        }

        public object Get(Guid id)
        {
            var model = Mapper.Map<CardType, CardTypeModel>(_cardTypeService.GetCardType(id));
            return model;
        }
    }
}
