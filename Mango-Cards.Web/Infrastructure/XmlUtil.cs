using Mango_Cards.Web.Models.WeiXinModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Mango_Cards.Web.Infrastructure
{

    public static class XmlUtil
    {
        /// <summary>
        /// 微信订单查询接口XML参数整理
        /// </summary>
        /// <param name="queryorder">微信订单查询参数实例</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string getQueryOrderXml(PayRequiredModel payRequired)
        {
            string return_string = string.Empty;
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();

            sParams.Add("appid", payRequired.appId);
            sParams.Add("mch_id", payRequired.mch_id);
            sParams.Add("nonce_str", payRequired.nonce_str);
            sParams.Add("body", payRequired.body);
            sParams.Add("out_trade_no", "3200755816");
            sParams.Add("total_fee", payRequired.total_fee);
            sParams.Add("spbill_create_ip", payRequired.spbill_create_ip);
            sParams.Add("notify_url", payRequired.notify_url);
            sParams.Add("trade_type", payRequired.trade_type);
            sParams.Add("sign", payRequired.sign);

            //拼接成XML请求数据
            StringBuilder sbPay = new StringBuilder();
            foreach (KeyValuePair<string, string> k in sParams)
            {
                if (k.Key == "attach" || k.Key == "body" || k.Key == "sign")
                {
                    sbPay.Append("<" + k.Key + "><![CDATA[" + k.Value + "]]></" + k.Key + ">");
                }
                else
                {
                    sbPay.Append("<" + k.Key + ">" + k.Value + "</" + k.Key + ">");
                }
            }
            return_string = string.Format("<xml>{0}</xml>", sbPay.ToString());
            byte[] byteArray = Encoding.UTF8.GetBytes(return_string);
            return_string = Encoding.GetEncoding("GBK").GetString(byteArray);
            return return_string;
        }
    }

}