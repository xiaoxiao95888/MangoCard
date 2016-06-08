using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Mango_Cards.Web.Models;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Mango_Cards.Web.Infrastructure.Filters
{
    public class UserLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (HttpContext.Current.User.Identity.GetUser() == null)
            {
                var response = new ResponseModel
                {
                    Error = true,
                    ErrorCode = 2000
                };
                var errorResponse = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, response);//InternalServerError
                filterContext.Response = errorResponse;
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}