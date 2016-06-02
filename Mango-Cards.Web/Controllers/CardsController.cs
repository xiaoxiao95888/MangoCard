using System;
using System.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;
using Mango_Cards.Web.Infrastructure;

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
            //var card = _mangoCardService.GetMangoCard(id);
            //var uploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"] + card.WeChatUser.Id + "/";
            //Mapper.CreateMap<Media, MediaModel>()
            //    .ForMember(n => n.Url, opt => opt.MapFrom(src => uploadFileUrl + src.Name));
            //Mapper.CreateMap<Field, FieldModel>()
            //    .ForMember(n => n.FieldType, opt => opt.MapFrom(src => src.FieldType))
            //    .ForMember(n => n.MediaModel, opt => opt.MapFrom(src => src.Media));
            //Mapper.CreateMap<MangoCard, MangoCardModel>()
            //    .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardTemplate.Id))
            //    .ForMember(n => n.FieldModels, opt => opt.MapFrom(src => src.Fields));
            //var model = Mapper.Map<MangoCard, MangoCardModel>(card);
            //model.PageHtmlCode = card.HtmlCode;
            //return View(model);
            return null;
        }
        public ActionResult CardTemplateView(Guid id)
        {
            var model = Mapper.Map<CardTemplate, CardTemplateDetailModel>(_cardTemplateService.GetCardTemplate(id));
            return View(model);
        }
        //用来缩短预览二维码长度并且跳转到正常的微信网址
        public ActionResult RedirectCardView(Guid id)
        {
            var state = Helper.GenerateId();
            var backUrl = "http://" + HttpContext.Request.Url.Host + Url.Action("View", "Cards", new { id = id });
            var url =
                string.Format(
                    "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect",
                    ConfigurationManager.AppSettings["AppId"], backUrl, state);
            return Redirect(url);
        }
    }
}