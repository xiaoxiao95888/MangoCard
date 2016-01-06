using Mango_Cards.Library.Models;
using Mango_Cards.Library.Models.Enum;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mango_Cards.Web.Controllers.API
{
    public class LoginConfirmationController : BaseApiController
    {
        private readonly ILoginLogService _loginLogService;
        private readonly IWeChatUserService _weChatUserService;
        public LoginConfirmationController(ILoginLogService loginLogService, IWeChatUserService weChatUserService)
        {
            _loginLogService = loginLogService;
            _weChatUserService = weChatUserService;
        }
        public object Post(LoginLogModel model)
        {
            if (model.WeChatUserModel.OpenId != null)
            {
                var wecharuser = _weChatUserService.GetWeChatUser(model.WeChatUserModel.OpenId);
                if (wecharuser == null)
                {
                    wecharuser = new WeChatUser
                    {
                        Id = Guid.NewGuid(),
                        OpenId = model.WeChatUserModel.OpenId,
                        NickName = model.WeChatUserModel.NickName,
                        Gender = (Gender)model.WeChatUserModel.Gender,
                        Language = model.WeChatUserModel.Language,
                        City = model.WeChatUserModel.City,
                        Province = model.WeChatUserModel.Province,
                        Headimgurl = model.WeChatUserModel.Headimgurl
                    };
                    _weChatUserService.Insert(wecharuser);
                }
                else
                {
                    wecharuser.OpenId = model.WeChatUserModel.OpenId;
                    wecharuser.NickName = model.WeChatUserModel.NickName;
                    wecharuser.Gender = (Gender)model.WeChatUserModel.Gender;
                    wecharuser.Language = model.WeChatUserModel.Language;
                    wecharuser.City = model.WeChatUserModel.City;
                    wecharuser.Province = model.WeChatUserModel.Province;
                    wecharuser.Headimgurl = model.WeChatUserModel.Headimgurl;
                    _weChatUserService.Update();
                }
                var loginLogId = Guid.NewGuid();
                if (_loginLogService.GetLoginLog(model.State) == null)
                {
                    _loginLogService.Insert(new LoginLog
                    {
                        Id = loginLogId,
                        State = model.State,
                        CreateTime = DateTime.Now,
                        WeChatUser_Id = wecharuser.Id
                    });
                    model.CreateTime = DateTime.Now;
                    model.Id = loginLogId;
                }

                return model;
            }
            return null;
        }
    }
}
