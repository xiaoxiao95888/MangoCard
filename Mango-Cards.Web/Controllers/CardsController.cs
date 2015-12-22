using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mango_Cards.Web.Controllers
{
    public class CardsController : Controller
    {
         [Authorize]
        // GET: Cards
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMangoCard()
        {
            return View();
        }
    }
}