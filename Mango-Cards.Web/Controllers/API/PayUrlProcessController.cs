using Mango_Cards.Web.Infrastructure;
using Mango_Cards.Web.Models.WeiXinModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;

namespace Mango_Cards.Web.Controllers.API
{
    public class PayUrlProcessController : BaseApiController
    {
        /// <summary>
        /// 根据产品ID生成PayUrl
        /// </summary>
        /// <param name="id">产品Id</param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            var apikey = "JAN5I2BP4TSMUFL91ZROE7C3VXQG8DH0";
            var nativePay = new NativePay();
            //生成扫码支付模式二url
            var codeUrl = nativePay.GetPayUrl("123456789");
            return codeUrl;
        }
       
    }
}
