using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mango_Cards.Web.Controllers
{
    public class ResultNotifyPageController : Controller
    {
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }
    }
}