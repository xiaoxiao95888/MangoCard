using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangoCard_Cards.Admin.Controllers
{
    public class CardsController : Controller
    {
        // GET: Cards
        public ActionResult CardView()
        {
            return View();
        }
    }
}