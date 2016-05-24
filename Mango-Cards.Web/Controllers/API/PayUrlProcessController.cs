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
            var apikey = "OSX0EDRMNI9Y7635BAVCUGKFTZHQL2J1WP84";
            var payRequired = new PayRequiredModel
            {
                appId = "wx990c1d301ce34a46",//公众账号ID
                mch_id = "1259992201",//商户号
                nonce_str = Helper.CreateNonceStr(),//随机字符串
                body = "test",//商品描述
                out_trade_no = "3200755816",//商户订单号
                total_fee = "1",//总金额
                spbill_create_ip = "127.0.0.1",////终端IP
                notify_url = "http://card.mangoeasy.com/",//通知地址
                trade_type = "NATIVE",//交易类型
            };
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
            sParams.Add("appid", payRequired.appId);
            sParams.Add("mch_id", payRequired.mch_id);
            sParams.Add("nonce_str", payRequired.nonce_str);
            sParams.Add("body", payRequired.body);
            sParams.Add("out_trade_no", payRequired.out_trade_no);
            sParams.Add("total_fee", payRequired.total_fee);
            sParams.Add("spbill_create_ip", payRequired.spbill_create_ip);
            sParams.Add("notify_url", payRequired.notify_url);
            sParams.Add("trade_type", payRequired.trade_type);
            var sign = Helper.Getsign(sParams, apikey);
            payRequired.sign = sign;
            var xmlstr = XmlUtil.getQueryOrderXml(payRequired);
            //post数据到微信获取Code_Url
            var code_url = Helper.PostXmlToUrl("https://api.mch.weixin.qq.com/pay/unifiedorder", xmlstr);
            return code_url;
        }
       
    }
}
