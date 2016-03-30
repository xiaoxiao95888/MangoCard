using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.Models;
using RazorEngine;
using RazorEngine.Templating;

namespace Mango_Cards.Web.Controllers.API
{
    public class FieldUpdateController : BaseApiController
    {

        private readonly IWeChatUserService _weChatUserService;

        public FieldUpdateController(IWeChatUserService weChatUserService)
        {
            _weChatUserService = weChatUserService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">card id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public object Put(Guid id, MangoCardModel model)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(HttpContext.Current.User.Identity.GetUser().Id);
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.IsDeleted == false && n.Id == id);
            if (card != null)
            {
                foreach (var field in card.Fields)
                {
                    foreach (var fieldmodel in model.FieldModels.Where(fieldmodel => field.Id == fieldmodel.Id))
                    {
                        field.FieldValue = fieldmodel.FieldValue;
                    }
                }
                var html = Engine.Razor.RunCompile(card.FromMangoCard.HtmlCode, card.Id.ToString(),
                    model.GetType(), model);
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