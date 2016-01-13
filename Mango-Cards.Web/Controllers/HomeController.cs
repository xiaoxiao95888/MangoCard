using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Mango_Cards.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeChatUserService _weChatUserService;
        public HomeController( IWeChatUserService weChatUserService)
        {
            _weChatUserService = weChatUserService;
        }
        public ActionResult Index()
        {
            //test
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var identity = UserService.CreateIdentity(_weChatUserService.GetWeChatUsers().FirstOrDefault(), DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties(), identity);
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult LoginConfirmation()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}