using Mango_Cards.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MangoCard_Cards.Admin.Models;
using MangoCard_Cards.Admin.Models.Enum;
using Microsoft.Practices.ObjectBuilder2;

namespace MangoCard_Cards.Admin.Controllers.API
{
    public class RegisterAdminController : BaseApiController
    {
        private readonly ICardTemplateService _cardTemplateService;
        private readonly IMangoCardService _mangoCardService;
        private readonly IApplyForDeveloperService _applyForDeveloperService;
        public RegisterAdminController(IMangoCardService mangoCardService, ICardTemplateService cardTemplateService, IApplyForDeveloperService applyForDeveloperService)
        {
            _mangoCardService = mangoCardService;
            _cardTemplateService = cardTemplateService;
            _applyForDeveloperService = applyForDeveloperService;
        }
        [Authorize]
        public object Get()
        {
            var model =
                _applyForDeveloperService.GetApplyForDevelopers().Where(n => !n.WeChatUser.IsDeveloper).OrderBy(n => n.CreatedTime).Select(n => new RegisterUserModel
                {
                    ApplyForDeveloperId = n.Id,
                    WeChatUserId = n.WeChatUserId,
                    NickName = n.WeChatUser.NickName,
                    Gender = (Gender)n.WeChatUser.Gender,
                    Language = n.WeChatUser.Language,
                    City = n.WeChatUser.City,
                    Province = n.WeChatUser.Province,
                    Country = n.WeChatUser.Country,
                    Headimgurl = n.WeChatUser.Headimgurl,
                    PhoneNum = n.PhoneNum,
                    Email = n.Email,
                    Name = n.Name,
                    Introduce = n.Introduce,
                    UpdateTime = n.UpdateTime,
                    CreatedTime = n.CreatedTime
                }).ToArray();
            return model;
        }

        public object Put(RegisterUserModel model)
        {
            var item = _applyForDeveloperService.GetApplyForDeveloper(model.ApplyForDeveloperId);
            if (item != null)
            {
                if (model.Pass)
                {
                    item.WeChatUser.PhoneNum = model.PhoneNum;
                    item.WeChatUser.Email = model.Email;
                    item.WeChatUser.Name = model.Name;
                    item.WeChatUser.ApplyForDevelopers.ForEach(n => n.IsDeleted = true);
                    item.WeChatUser.IsDeveloper = true;
                    _applyForDeveloperService.Update();
                    MailHelp.SendMailForRegisterPass(model);
                }
                if (model.Reject)
                {
                    item.WeChatUser.ApplyForDevelopers.ForEach(n => n.IsDeleted = true);
                    _applyForDeveloperService.Update();
                    MailHelp.SendMailForRegisterReject(model);
                }
                return Success();
            }
            return Failed();
        }
    }
}
