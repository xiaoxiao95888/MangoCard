using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class MyCardTypeController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;
        public MyCardTypeController(IWeChatUserService weChatUserService, IMapperFactory mapperFactory)
        {
            _weChatUserService = weChatUserService;
            //mapperFactory.GetCardTypeMapper().Create();
         
        }
        public object Get()
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            return
                wechatuser.MangoCards.Select(n => n.CardTemplate.CardType)
                    .Distinct()
                    .Select(n => new CardTypeModel
                    {
                        Id = n.Id,
                        Name = n.Name
                    });
        }
    }
}
