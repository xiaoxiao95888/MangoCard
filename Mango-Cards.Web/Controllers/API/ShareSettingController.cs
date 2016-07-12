using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Models;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class ShareSettingController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;
        private readonly IMangoCardService _mangoCardService;
        public ShareSettingController(IWeChatUserService weChatUserService, ICardTemplateService cardTemplateService,IMangoCardService mangoCardService)
        {
            _weChatUserService = weChatUserService;
            _mangoCardService = mangoCardService;
        }

        public object Put(MangoCardModel model)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == model.Id);
            if (string.IsNullOrEmpty(model.Title))
            {
                return Failed("标题不能为空");
            }
            if (card != null)
            {
                if (card.IsReview || card.IsPublish)
                {
                    return Failed("禁止修改");
                }
                card.Title = model.Title;
                card.Description = model.Description;
                card.ShareThumbnailUrl = model.ShareThumbnailUrl;
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
            return Failed();
        }
    }
}
