using System;
using System.Web.Mvc;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers
{
    //[Authorize]
    public class CardsController : Controller
    {
        private readonly IMangoCardService _mangoCardService;
        public CardsController(IMangoCardService mangoCardService)
        {
            _mangoCardService = mangoCardService;
        }
        // GET: Cards
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View(Guid id)
        {
            var card = _mangoCardService.GetMangoCard(id);
            Mapper.CreateMap<PageValue, PageValueModel>();
            Mapper.CreateMap<Field, FieldModel>().ForMember(n=>n.FieldType,opt=>opt.MapFrom(src=>src.FieldType));
            Mapper.CreateMap<MangoCard, MangoCardModel>()
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id))
                .ForMember(n => n.FieldModels, opt => opt.MapFrom(src => src.Fields))
                .ForMember(n => n.PageValueModels, opt => opt.MapFrom(src => src.PageValues));
            var model = Mapper.Map<MangoCard, MangoCardModel>(card);
            return View(model);
        }
    }
}