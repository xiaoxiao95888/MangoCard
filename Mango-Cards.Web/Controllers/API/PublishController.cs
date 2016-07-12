using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class PublishController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;
        public PublishController(IWeChatUserService weChatUserService)
        {
            _weChatUserService = weChatUserService;
        }
        public object Put(Guid id)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (card != null)
            {
                card.CardApproveds.Add(new CardApproved {Id = Guid.NewGuid()});
                card.IsPublish = false;
                card.IsReview = true;
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
