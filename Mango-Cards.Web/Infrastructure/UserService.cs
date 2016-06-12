using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Mango_Cards.Library.Models;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.Infrastructure
{
    public class UserService
    {
        public static ClaimsIdentity CreateIdentity(WeChatUser user, string authenticationType)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            _identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            _identity.AddClaim(new Claim("DisplayName", user.NickName));
            return _identity;
        }
    }
}