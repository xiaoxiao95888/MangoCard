﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Mango_Cards.Web.Controllers.API
{
    public class TestLoginController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;
        public TestLoginController(IWeChatUserService weChatUserService)
        {
            _weChatUserService = weChatUserService;
          
        }
        
        public object Get()
        {
            var wechartuser = _weChatUserService.GetWeChatUsers().FirstOrDefault();
            //var identity = new CustomIdentity(wechartuser);
            //var principal = new CustomPrincipal(identity);
            //HttpContext.Current.User = principal;
            FormsAuthentication.SetAuthCookie(wechartuser.Id.ToString(), true);
            return Success();
        }
    }
}