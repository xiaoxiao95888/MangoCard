using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.ObjectBuilder2;

namespace Mango_Cards.Web.Controllers.API
{
    [Authorize]
    public class ApplyForDeveloperController : BaseApiController
    {
        private readonly IApplyForDeveloperService _applyForDeveloperService;
        private readonly IWeChatUserService _weChatUserService;
        public ApplyForDeveloperController(IApplyForDeveloperService applyForDeveloperService, IWeChatUserService weChatUserService)
        {
            _applyForDeveloperService = applyForDeveloperService;
            _weChatUserService = weChatUserService;
        }

        public object Post(ApplyForDeveloperModel model)
        {
            var user = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            if (user.IsDeveloper)
            {
                return Failed("请勿重复注册");
            }
            if (user.ApplyForDevelopers.Any(n => !n.IsDeleted))
            {
                return Failed("正在审核中");
            }
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Name) ||
            string.IsNullOrEmpty(model.PhoneNum))
            {
                return Failed("请填写完整");
            }
            model.Email = model.Email.Trim();
            model.Name = model.Name.Trim();
            model.PhoneNum = model.PhoneNum.Trim();
            if (System.Text.RegularExpressions.Regex.IsMatch(model.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$") == false)
            {
                return Failed("邮箱格式错误");
            }
            if (_applyForDeveloperService.GetApplyForDevelopers().Any(n => n.PhoneNum == model.PhoneNum) || _weChatUserService.GetWeChatUsers().Any(n => n.PhoneNum == model.PhoneNum))
            {
                return Failed("手机已被注册");
            }
            if (_applyForDeveloperService.GetApplyForDevelopers().Any(n => n.Email == model.Email) || _weChatUserService.GetWeChatUsers().Any(n => n.Email == model.Email))
            {
                return Failed("邮箱已被注册");
            }
            try
            {
                user.ApplyForDevelopers.Add(new ApplyForDeveloper
                {
                    Id = Guid.NewGuid(),
                    Email = model.Email.Trim(),
                    PhoneNum = model.PhoneNum.Trim(),
                    Name = model.Name.Trim(),
                    Introduce = model.Introduce
                });
                _weChatUserService.Update();
                MailHelp.SendMailForRegister(model);
            }
            catch (Exception)
            {

                return Failed();
            }
            return Success();
        }

        public object Get()
        {
            return _applyForDeveloperService.GetApplyForDevelopers().Select(n => new ApplyForDeveloperModel
            {
                CreatedTime = n.CreatedTime,
                Email = n.Email,
                Id = n.Id,
                IsDeleted = n.IsDeleted,
                Name = n.Name,
                PhoneNum = n.PhoneNum,
                UpdateTime = n.UpdateTime,
                WeChatUserId = n.WeChatUserId
            }).ToArray();
        }
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Put(Guid id)
        {
            var model = _applyForDeveloperService.GetApplyForDeveloper(id);
            if (model != null && model.IsDeleted == false)
            {
                model.WeChatUser.PhoneNum = model.PhoneNum;
                model.WeChatUser.Email = model.Email;
                model.WeChatUser.Name = model.Name;
                model.WeChatUser.ApplyForDevelopers.ForEach(n => n.IsDeleted = true);
                model.WeChatUser.IsDeveloper = true;
                _applyForDeveloperService.Update();
                return Success();
            }
            return Failed();
        }
    }
}
