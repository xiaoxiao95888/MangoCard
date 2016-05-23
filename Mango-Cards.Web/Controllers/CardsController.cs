using System;
using System.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers
{
    //[Authorize]
    public class CardsController : Controller
    {
        private readonly ICardTemplateService _cardTemplateService;

        private readonly IMangoCardService _mangoCardService;
        public CardsController(IMangoCardService mangoCardService, IMapperFactory mapperFactory, ICardTemplateService cardTemplateService)
        {
            _mangoCardService = mangoCardService;
            _cardTemplateService = cardTemplateService;
            mapperFactory.GetCardTemplateMapper().Detail();
        }
        // GET: Cards
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View(Guid id)
        {
            var card = _mangoCardService.GetMangoCard(id);
            Mapper.CreateMap<Field, FieldModel>().ForMember(n => n.FieldType, opt => opt.MapFrom(src => src.FieldType));
            Mapper.CreateMap<MangoCard, MangoCardModel>()
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardTemplate.Id))
                .ForMember(n => n.FieldModels, opt => opt.MapFrom(src => src.Fields));
            var model = Mapper.Map<MangoCard, MangoCardModel>(card);
            model.PageHtmlCode = card.HtmlCode;
            return View(model);
        }
        public ActionResult CardTemplateView(Guid id)
        {
            var model = Mapper.Map<CardTemplate, CardTemplateDetailModel>(_cardTemplateService.GetCardTemplate(id));
            return View(model);
        }

        public ActionResult RedirectView()
        {
            var backUrl = "http://romaingauthier.mangoeasy.com/home/filedownload/";
            var state = Guid.NewGuid();
            var fileDownloadloginUrl =
                string.Format(
                    "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect",
                    ConfigurationManager.AppSettings["AppId"], backUrl, state);
            return Redirect(fileDownloadloginUrl);
        }
    }
}