using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Mango_Cards.Library.Models;

namespace Mango_Cards.Web.Infrastructure
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(WeChatUser weChatUser)
        {
            WeChatUser = weChatUser;
        }

        public WeChatUser WeChatUser { get; }

        public string Name => WeChatUser == null ? "" : WeChatUser.NickName;

        public string AuthenticationType => "Custom";

        public bool IsAuthenticated => WeChatUser != null;
    }
}