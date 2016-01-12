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
            var state = GenerateId();
            var weChartloginUrl = "http://" + HttpContext.Current.Request.Url.Host + "/Account" + "/LoginUrl?state=" + state;
            return new { weChartloginUrl, state };
        }
        
        private string GenerateId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);

        }
    }
}
