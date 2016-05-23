using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers.API
{
    public class CardTemplateController : BaseApiController
    {
        private readonly ICardTemplateService _cardTemplateService;
        public CardTemplateController(ICardTemplateService cardTemplateService, IMapperFactory mapperFactory)
        {
            _cardTemplateService = cardTemplateService;
            mapperFactory.GetCardTemplateMapper().Normal();
        }
        public object Get()
        {
            var model =
                _cardTemplateService.GetCardTemplates()
                    .Where(n => n.IsPublish)
                    .Select(Mapper.Map<CardTemplate, CardTemplateModel>)
                    .ToArray();
            return model;
        }
    }
    public class CardTemplateDetailController : BaseApiController
    {
        private readonly ICardTemplateService _cardTemplateService;
        public CardTemplateDetailController(ICardTemplateService cardTemplateService, IMapperFactory mapperFactory)
        {
            _cardTemplateService = cardTemplateService;
            mapperFactory.GetCardTemplateMapper().Detail();
        }

        public object Get()
        {
            var model =
                _cardTemplateService.GetCardTemplates()
                    .Where(n => n.IsPublish)
                    .Select(Mapper.Map<CardTemplate, CardTemplateDetailModel>)
                    .ToArray();
            return model;
        }
    }
}
