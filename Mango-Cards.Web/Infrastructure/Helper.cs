using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.Owin.Security.Twitter.Messages;
using Newtonsoft.Json;

namespace Mango_Cards.Web.Infrastructure
{
    public static class Helper
    {
        public static string GenerateId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);

        }
        public static string SHA1_Hash(string strSha1In)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesSha1In = System.Text.Encoding.Default.GetBytes(strSha1In);
            var bytesSha1Out = sha1.ComputeHash(bytesSha1In);
            var strSha1Out = BitConverter.ToString(bytesSha1Out);
            strSha1Out = strSha1Out.Replace("-", "").ToLower();
            return strSha1Out;
        }
        //创建随机字符串  
        public static string CreateNonceStr()
        {
            const int length = 16;
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var str = string.Empty;
            var rad = new Random();
            for (var i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }

        /** 获取大写的MD5签名结果 */
        public static string GetMd5(string encypStr, string charset)
        {
            var m5 = new MD5CryptoServiceProvider();
            //创建md5对象
            byte[] inputBye;
            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            var outputBye = m5.ComputeHash(inputBye);
            var retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        /// <summary>
        /// 获取微信签名
        /// </summary>
        /// <param name="sParams"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Getsign(SortedDictionary<string, string> sParams, string key)
        {
            var sb = new StringBuilder();
            foreach (var temp in sParams.Where(temp => !string.IsNullOrEmpty(temp.Value) && temp.Key.ToLower() != "sign"))
            {
                sb.Append(temp.Key.Trim() + "=" + temp.Value.Trim() + "&");
            }
            sb.Append("key=" + key.Trim() + "");
            var signkey = sb.ToString();
            var sign = GetMd5(signkey, "utf-8");
            return sign;
        }
        // <summary>
        /// post数据到指定接口并返回数据
        /// </summary>
        public static string PostXmlToUrl(string url, string postData)
        {
            string returnmsg = "";
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;
                returnmsg = wc.UploadString(url, "POST", postData);
            }
            return returnmsg;
        }
    }
}