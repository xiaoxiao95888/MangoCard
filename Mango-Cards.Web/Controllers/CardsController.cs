using System;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;
using Mango_Cards.Web.Infrastructure;
using RazorEngine;
using RazorEngine.Templating;

namespace Mango_Cards.Web.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public ActionResult View(Guid id)
        {
            if (HttpContext.Request.UserAgent != null && HttpContext.Request.UserAgent.ToLower().Contains("micromessenger"))
            {
                //微信浏览器
                if (Session["CardId"] == null || Session["CardId"].ToString() != id.ToString())
                {
                    return RedirectToAction("RedirectCardView", new { id = id });
                }
            }
            var cardIdCookie = new HttpCookie("CardId") {Value = id.ToString()};
            Response.Cookies.Add(cardIdCookie);
            var card = _mangoCardService.GetMangoCard(id);
            var model = new MangoCardAttributeModel
            {
                Title = card.Title,
                Description = card.Description,
                HtmlCode = card.HtmlCode,
                MangoCardId = card.Id
            };
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult CardTemplateView(Guid id)
        {
            var model = Mapper.Map<CardTemplate, CardTemplateDetailModel>(_cardTemplateService.GetCardTemplate(id));
            return View(model);
        }
        [AllowAnonymous]
        //用来缩短预览二维码长度并且跳转到正常的微信网址
        public ActionResult RedirectCardView(Guid id)
        {
            Session["CardId"] = id;
            var state = Helper.GenerateId();
            var backUrl = "http://" + HttpContext.Request.Url.Host + Url.Action("View", "Cards", new { id = id });
            var url =
                string.Format(
                    "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect",
                    ConfigurationManager.AppSettings["AppId"], backUrl, state);
            return Redirect(url);
        }
        [AllowAnonymous]
        //用来缩短预览二维码长度并且跳转到正常的微信网址
        public ActionResult RedirectCardTemplateView(Guid id)
        {
            Session["CardId"] = id;
            var state = Helper.GenerateId();
            var backUrl = "http://" + HttpContext.Request.Url.Host + Url.Action("CardTemplateView", "Cards", new { id = id });
            var url =
                string.Format(
                    "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect",
                    ConfigurationManager.AppSettings["AppId"], backUrl, state);
            return Redirect(url);
        }
    }
}