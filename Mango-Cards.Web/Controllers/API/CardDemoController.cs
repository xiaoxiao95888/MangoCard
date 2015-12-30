using System;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Models;
using System.Linq;

namespace Mango_Cards.Web.Controllers.API
{
    public class CardDemoController : BaseApiController
    {
        private readonly ICardDemoService _cardDemoService;
        public CardDemoController(ICardDemoService cardDemoService)
        {
            _cardDemoService = cardDemoService;
        }
        public object Get()
        {
            Mapper.Reset();
            Mapper.CreateMap<CardType, CardTypeModel>();
            Mapper.CreateMap<CardDemo, CardDemoModel>().ForMember(n => n.CardTypeModel, opt => opt.MapFrom(src => src.CardType)).ForMember(n => n.HtmlCode, opt => opt.Ignore());
            return _cardDemoService.GetCardDemos().Select(Mapper.Map<CardDemo, CardDemoModel>);
        }
        public object Get(Guid id)
        {
            Mapper.Reset();
            Mapper.CreateMap<CardType, CardTypeModel>();
            Mapper.CreateMap<CardDemo, CardDemoModel>()
                .ForMember(n => n.CardTypeModel, opt => opt.MapFrom(src => src.CardType));
            return Mapper.Map<CardDemo, CardDemoModel>(_cardDemoService.GetCardDemo(id));
        }
    }
}
