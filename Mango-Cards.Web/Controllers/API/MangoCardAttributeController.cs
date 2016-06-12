using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.MapperHelper;
using Mango_Cards.Web.Models;
using Microsoft.AspNet.Identity;
using RazorEngine;
using RazorEngine.Templating;

namespace Mango_Cards.Web.Controllers.API
{
    [UserLogin]
    public class MangoCardAttributeController : BaseApiController
    {

        private readonly IWeChatUserService _weChatUserService;
        public MangoCardAttributeController(IWeChatUserService weChatUserService, IMapperFactory mapperFactory)
        {
            _weChatUserService = weChatUserService;
            mapperFactory.GetMangoCardAttributeMapper().Create();
        }
        /// <summary>
        /// 获取Card的相关字段
        /// </summary>
        /// <param name="id">Card Id</param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            var card = wechatuser.MangoCards.Where(n => !n.IsDeleted).FirstOrDefault(n => n.Id == id);
            var model = Mapper.Map<MangoCard, MangoCardAttributeModel>(card);
            return model;
        }
        /// <summary>
        /// 更新相关字段
        /// </summary>
        /// <param name="id">card id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public object Put(Guid id, MangoCardAttributeModel model)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(User.Identity.GetUserId());
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (card != null)
            {
                foreach (var field in card.Fields)
                {
                    foreach (var fieldmodel in model.FieldModels.Where(fieldmodel => field.Id == fieldmodel.Id))
                    {
                        if (fieldmodel.MediaModel != null && fieldmodel.MediaModel.Id != Guid.Empty)
                        {
                            field.FieldValue = null;
                            field.MediaId = fieldmodel.MediaModel.Id;
                            field.FieldValue = fieldmodel.MediaModel.Url;
                        }
                        else
                        {
                            field.FieldValue = fieldmodel.FieldValue;
                            field.MediaId = null;
                        }

                    }
                }
                var html = Engine.Razor.RunCompile(card.CardTemplate.HtmlCode, Guid.NewGuid().ToString(), model.GetType(), model);
                try
                {
                    card.HtmlCode = html;
                    _weChatUserService.Update();
                    return Success();
                }
                catch (Exception ex)
                {
                    return Failed();
                }

            }
            return Failed("Can't find card!");
        }
    }
}
