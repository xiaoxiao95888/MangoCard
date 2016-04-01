using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Mango_Cards.Web.Infrastructure
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly CustomIdentity _identity;

        public CustomPrincipal(CustomIdentity identity)
        {
            this._identity = identity;
        }

        public IIdentity Identity => this._identity;

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}