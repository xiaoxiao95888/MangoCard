using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Models;
using Mango_Cards.Web.Models.Enum;
using Microsoft.AspNet.Identity;

namespace Mango_Cards.Web.Infrastructure.Filters
{
    public static class Extensions
    {
        public static WeChatUserModel GetUser(this IIdentity identity)
        {
            var userId = identity.GetUserId();
            try
            {
                using (var weChatServie = DependencyResolver.Current.GetService<IWeChatUserService>())
                {
                    var wechatuser = weChatServie.GetWeChatUser(new Guid(userId));
                    if (wechatuser != null)
                    {
                        return new WeChatUserModel
                        {
                            Id = wechatuser.Id,
                            City = wechatuser.City,
                            Country = wechatuser.Country,
                            Province = wechatuser.Province,
                            Gender = (Gender)wechatuser.Gender,
                            Headimgurl = wechatuser.Headimgurl,
                            NickName = wechatuser.NickName,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                //this.GetLogger().Error(ex.Message);
            }
            return null;
        }
    }
}
