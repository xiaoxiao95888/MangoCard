using System;
using System.Configuration;
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
        private readonly string _cardThumbnailPath;
        public CardDemoController(ICardDemoService cardDemoService)
        {
            _cardDemoService = cardDemoService;
            _cardThumbnailPath = ConfigurationManager.AppSettings["CardThumbnailPath"];
        }
        public object Get()
        {
            Mapper.Reset();
            Mapper.CreateMap<CardType, CardTypeModel>();
            Mapper.CreateMap<CardDemo, CardDemoModel>()
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id))
                .ForMember(n => n.ThumbnailUrl,
                    opt =>
                        opt.MapFrom(
                            src => src.Thumbnail != null ? (_cardThumbnailPath + "/" + src.Thumbnail) : string.Empty))
                .ForMember(n => n.HtmlCode, opt => opt.Ignore());
            return _cardDemoService.GetCardDemos().Select(Mapper.Map<CardDemo, CardDemoModel>);
        }
        public object Get(Guid id)
        {
            Mapper.Reset();
            Mapper.CreateMap<CardType, CardTypeModel>();
            Mapper.CreateMap<CardDemo, CardDemoModel>().ForMember(n => n.ThumbnailUrl,
                opt =>
                    opt.MapFrom(
                        src => src.Thumbnail != null ? (_cardThumbnailPath + "/" + src.Thumbnail) : string.Empty))
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id));
            return Mapper.Map<CardDemo, CardDemoModel>(_cardDemoService.GetCardDemo(id));
        }
    }
}
