using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.MapperHelper;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class UnpublishController : BaseApiController
    {
       
        private readonly IWeChatUserService _weChatUserService;
        public UnpublishController(IWeChatUserService weChatUserService)
        {
            _weChatUserService = weChatUserService;
        }
        public object Put(Guid id)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (card != null)
            {
                card.IsPublish = false;
                card.IsReview = false;
                card.CardApproveds.ForEach(n => { n.IsDeleted = true;
                });
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
