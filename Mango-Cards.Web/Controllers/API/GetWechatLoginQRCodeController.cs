using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Mango_Cards.Web.Controllers.API
{
    public class GetWechatLoginQrCodeController : BaseApiController
    {
        public object Get()
        {
            var state = Guid.NewGuid().ToString();
            var backUrl = HttpUtility.UrlEncode("http://card.mangoeasy.com/home/loginconfirmation");
            var weChartloginUrl =
                string.Format(
                    "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect",
                    ConfigurationManager.AppSettings["AppId"], backUrl, state);

            return new { weChartloginUrl, state };
        }
    }
}
