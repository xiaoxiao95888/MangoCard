using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mango_Cards.Library.Services;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.Controllers
{
    [Authorize]
    public class DeveloperController : Controller
    {
        private readonly IWeChatUserService _weChatUserService;
        public DeveloperController(IWeChatUserService weChatUserService)
        {
            _weChatUserService = weChatUserService;
        }
        // GET: Developer
        public ActionResult Index()
        {
            var user= _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            if (!user.IsDeveloper)
            {
                return RedirectToAction("Register");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}