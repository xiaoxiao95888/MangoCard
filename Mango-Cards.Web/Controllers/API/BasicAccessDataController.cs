using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure.Filters;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Controllers.API
{
    public class BasicAccessDataController : BaseApiController
    {
        private readonly IWeChatUserService _weChatUserService;
        private readonly IMangoCardService _mangoCardService;
        public BasicAccessDataController(IMangoCardService mangoCardService, IWeChatUserService weChatUserService)
        {
            _mangoCardService = mangoCardService;
            _weChatUserService = weChatUserService;
        }
        /// <summary>
        /// 获取基础访问数据
        /// </summary>
        /// <param name="id">mango card id</param>
        /// <returns></returns>
        [Authorize]
        public object Get(Guid id)
        {
            var wechatuser = _weChatUserService.GetWeChatUser(HttpContext.Current.User.Identity.GetUser().Id);
            var card = wechatuser.MangoCards.FirstOrDefault(n => n.Id == id);
            if (card != null)
            {
                return new
                {
                    CardTitle = card.Title,
                    CardType = card.CardTemplate.CardType.Name,
                    PvDataCount = card.PvDatas.Count == 0 ? "-" : card.PvDatas.Count.ToString(),
                    ShareTimeCount = card.ShareTimes.Count == 0 ? "-" : card.ShareTimes.Count.ToString()
                };

            }
            return null;
        }
    }
}
