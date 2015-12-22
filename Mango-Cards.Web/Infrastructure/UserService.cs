using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.Infrastructure
{
    public class UserService
    {
        public static ClaimsIdentity CreateIdentity(WeChatUser user, string authenticationType)
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.NickName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            identity.AddClaim(new Claim("DisplayName", user.NickName));
            return identity;
        }
    }
}
