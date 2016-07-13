using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MangoCard_Cards.Admin.Models;

namespace MangoCard_Cards.Admin.Controllers.API
{
    public class BaseApiController : ApiController
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
        protected ResponseModel Failed(string message = null)
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
