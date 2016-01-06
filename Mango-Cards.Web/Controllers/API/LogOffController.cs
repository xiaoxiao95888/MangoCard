using System;
using System.Web;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure;
using Mango_Cards.Web.Models;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Mango_Cards.Web.Controllers.API
{
    public class LogOffController : AsyncController
    {
        public void LogoutAsync()
        {
            AsyncManager.OutstandingOperations.Decrement();
        }
        public ActionResult LogoutCompleted(Guid id)
        {

            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return Json(true,JsonRequestBehavior.AllowGet);
        }

    }
}
