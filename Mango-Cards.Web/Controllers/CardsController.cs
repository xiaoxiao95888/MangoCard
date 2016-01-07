using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mango_Cards.Web.Controllers
{
    [Authorize]
    public class CardsController : Controller
    {         
        // GET: Cards
        public ActionResult Index()
        {
            return View();
        }        
    }
}