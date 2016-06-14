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
using Mango_Cards.Web.Infrastructure;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;
using Microsoft.AspNet.Identity;
using RazorEngine;
using RazorEngine.Templating;

namespace Mango_Cards.Web.Controllers.API
{
    [UserLogin]
    public class MyCardsController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;
        private readonly ICardTemplateService _cardTemplateService;
        private readonly IMangoCardService _mangoCardService;
        public MyCardsController(IWeChatUserService weChatUserService, ICardTemplateService cardTemplateService, IMapperFactory mapperFactory, IMangoCardService mangoCardService)
        {
            _weChatUserService = weChatUserService;
            _cardTemplateService = cardTemplateService;
            _mangoCardService = mangoCardService;
            mapperFactory.GetMangoCardMapper().Create();
            mapperFactory.GetCardTemplateMapper().Detail();
        }
        public object Get()
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            return
                wechatuser.MangoCards.Where(n => !n.IsDeleted).OrderByDescending(n => n.UpdateTime)
                    .Select(Mapper.Map<MangoCard, MangoCardModel>);
        }

        public object Get(Guid id)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (card != null)
            {
                var model = Mapper.Map<MangoCard, MangoCardModel>(card);
                return model;
            }
            return null;

        }
        public object Put(Guid id, MangoCardAttributeModel model)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (card != null)
            {
                card.HtmlCode = model.HtmlCode;
                try
                {
                    _weChatUserService.Update();
                    return Success();
                }
                catch (Exception ex)
                {
                    return Failed();
                }

            }
            return Failed("Can't find card!");
        }
        /// <summary>
        /// 选择card
        /// </summary>
        /// <returns></returns>
        public object Post(Guid id)
        {
            var card = _cardTemplateService.GetCardTemplate(id);
            if (card != null)
            {
                var model = Mapper.Map<CardTemplate, CardTemplateDetailModel>(card);
                var html = Engine.Razor.RunCompile(model.HtmlCode, Guid.NewGuid().ToString(), model.GetType(), model);

                var mangocard = new MangoCard
                {
                    Id = Guid.NewGuid(),
                    Fields = card.Fields.Select(p => new Field
                    {
                        Id = Guid.NewGuid(),
                        Name = p.Name,
                        FieldValue = p.FieldValue,
                        FieldType = p.FieldType,
                        MediaId = p.MediaId
                    }).ToArray(),
                    WeChatUserId = new Guid(User.Identity.GetUserId()),
                    ThumbnailUrl = card.ThumbnailUrl,
                    Title = card.Title,
                    HtmlCode = html
                };
                while (true)
                {
                    var code = Helper.CreateNonceCode();
                    if (!_mangoCardService.GetAllMangoCards().Any(p => p.Code == code))
                    {
                        mangocard.Code = code;
                        break;
                    }
                }
                card.MangoCards.Add(mangocard);
                _cardTemplateService.Update();
                return Success();
            }
            return Failed();
        }
    }
}
