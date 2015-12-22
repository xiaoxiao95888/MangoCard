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
            Mapper.CreateMap<CardDemo, CardDemoModel>().ForMember(n => n.CardTypeModel, opt => opt.MapFrom(src => src.CardType));
            return _cardDemoService.GetCardDemos().Select(Mapper.Map<CardDemo, CardDemoModel>);
        }
    }
}
