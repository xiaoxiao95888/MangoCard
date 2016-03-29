using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class MyCardsController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;
        public MyCardsController(IWeChatUserService weChatUserService)
        {
            _weChatUserService = weChatUserService;
        }
        public object Get()
        {
            var wechatuser = _weChatUserService.GetWeChatUser(HttpContext.Current.User.Identity.GetUser().Id);
            Mapper.CreateMap<MangoCard, MangoCardModel>()
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id));
            return
                wechatuser.MangoCards.Where(n => !n.IsDeleted)
                    .GroupBy(n => n.CardType)
                    .Select(n => new MangoCardTypeModel
                    {
                        Id = n.Key.Id,
                        Name = n.Key.Name,
                        MangoCardModels =n.Select(Mapper.Map<MangoCard, MangoCardModel>).ToArray()
                    });
        }

        public object Get(Guid id)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(HttpContext.Current.User.Identity.GetUser().Id);
            Mapper.CreateMap<MangoCard, MangoCardModel>()
                .ForMember(n => n.CardTypeId, opt => opt.MapFrom(src => src.CardType.Id));
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (card != null)
            {
                var model = Mapper.Map<MangoCard, MangoCardModel>(card);
                model.PageHtmlCode = card.HtmlCode;
                return model;
            }
            return null;
            
        }
    }
}
