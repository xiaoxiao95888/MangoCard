using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.Models;
using System.Dynamic;


namespace Mango_Cards.Web.Controllers.API
{
    public class PageValueController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;
        private readonly IMangoCardService _mangoCardService;
        public PageValueController(IMangoCardService mangoCardService, IWeChatUserService weChatUserService)
        {
            _mangoCardService = mangoCardService;
            _weChatUserService = weChatUserService;
        }
        public object Post(dynamic model)
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                var cardId = HttpContext.Current.Request.UrlReferrer.Segments.LastOrDefault();
                if (cardId != null)
                {
                    var card = _mangoCardService.GetMangoCard(new Guid(cardId));
                    if (card != null)
                    {
                        card.PageValues.Add(new PageValue
                        {
                            Id = Guid.NewGuid(),
                            Value = model.ToString()
                        });
                        _mangoCardService.Update();
                        return Success();
                    }
                }
            }
            return Failed();
        }
        /// <summary>
        /// 获取页面收集的数据
        /// </summary>
        /// <param name="id">mango card id</param>
        /// <returns></returns>
        [Authorize]
        public object Get(Guid id)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(HttpContext.Current.User.Identity.GetUser().Id);
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.Id == id);
            if (card != null)
            {
                Mapper.CreateMap<PageValue, PageValueModel>();
                var pageValueModels =
                    card.PageValues.Where(n=>!n.IsDeleted).Select(Mapper.Map<PageValue, PageValueModel>)
                        .ToArray()
                        .Select(item => System.Web.Helpers.Json.Decode(item.Value))
                        .ToList();
                return pageValueModels;
            }
            return null;
        }
    }
}
