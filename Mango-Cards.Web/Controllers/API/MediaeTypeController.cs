using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.Controllers.API
{
    [UserLogin]
    public class MediaeTypeController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;
        public MediaeTypeController(IWeChatUserService weChatUserService, IMapperFactory mapperFactory)
        {
            _weChatUserService = weChatUserService;
        }

        public object Get()
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            return
                wechatuser?.Mediae.GroupBy(n => n.MediaType)
                    .Select(n => new MediaTypeModel {Id = n.Key.Id, Name = n.Key.Name})
                    .ToArray();
        }
    }
}
