﻿using System;
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

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class MyCardsController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;

        public MyCardsController(IWeChatUserService weChatUserService, IMapperFactory mapperFactory)
        {
            _weChatUserService = weChatUserService;
            mapperFactory.GetMangoCardMapper().Create();
        }
        public object Get()
        {
            var wechatuser = _weChatUserService.GetWeChatUser(HttpContext.Current.User.Identity.GetUser().Id);
            return
                wechatuser.MangoCards.Where(n => !n.IsDeleted)
                    .GroupBy(n => n.CardTemplate.CardType)
                    .Select(n => new MangoCardTypeModel
                    {
                        Id = n.Key.Id,
                        Name = n.Key.Name,
                        MangoCardModels = n.Select(Mapper.Map<MangoCard, MangoCardModel>).ToArray()
                    });
        }

        public object Get(Guid id)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(HttpContext.Current.User.Identity.GetUser().Id);
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (card != null)
            {
                var model = Mapper.Map<MangoCard, MangoCardModel>(card);
                model.PageHtmlCode = card.HtmlCode;
                model.Instructions = card.CardTemplate.Instructions;
                model.Url = "http://" + HttpContext.Current.Request.Url.Host + "/Cards/RedirectCardView/" + model.Id;
                var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                var timestamp = (int)(DateTime.Now - startTime).TotalSeconds;
                var nonceStr = Helper.CreateNonceStr();
                var appId = "wx990c1d301ce34a46";
                var mchId = "1259992201";
                var productId = card.Id;
                SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
                sParams.Add("appid", appId);
                sParams.Add("body", "test");
                sParams.Add("mch_id", mchId);
                sParams.Add("nonce_str", nonceStr);
             
                //var sign = Helper.Getsign(sParams, key);


                model.PayUrl =
                    string.Format(
                        "weixin：//wxpay/bizpayurl?sign={0}&appid={1}&mch_id={2}&product_id={3}&time_stamp={4}&nonce_str={5}",
                        "", appId, mchId, productId, timestamp, nonceStr);
                return model;
            }
            return null;

        }

        public object Put(Guid id, MangoCardModel model)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(HttpContext.Current.User.Identity.GetUser().Id);
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (card != null)
            {
                card.HtmlCode = model.PageHtmlCode;
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
    }
}
