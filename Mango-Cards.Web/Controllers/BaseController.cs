using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers
{
    public class BaseController : Controller
    {
        protected ResponseModel Success()
        {
            return new ResponseModel
            {
                ErrorCode = 0,
                Message = "success",
                Error = false
            };
        }
        protected ResponseModel Failed(string message)
        {
            return new ResponseModel
            {
                ErrorCode = 0,
                Message = message,
                Error = true
            };
        }
    }
}