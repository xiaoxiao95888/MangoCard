using System;
using System.Configuration;
using System.Security.Cryptography;

namespace MangoCard_Cards.Admin.Controllers.API
{
    public class HeaderSettingController : BaseApiController
    {
        public object Get()
        {
            var appId = ConfigurationManager.AppSettings["AppId"];
            var appSecret = ConfigurationManager.AppSettings["AppSecret"];
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var timestamp = (int)(DateTime.Now - startTime).TotalSeconds;
            var random = Guid.NewGuid().ToString();
            var signature = SHA1_Hash(string.Format("appsecret={0}&random={1}&timestamp={2}", appSecret, random, timestamp));
            var base64 =
                Convert.ToBase64String(
                    System.Text.Encoding.Default.GetBytes(
                        string.Format("appid={0}&signature={1}&random={2}&timestamp={3}", appId,
                            signature, random, timestamp)));

            return base64;
        }
        private static string SHA1_Hash(string strSha1In)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var bytesSha1In = System.Text.Encoding.Default.GetBytes(strSha1In);
            var bytesSha1Out = sha1.ComputeHash(bytesSha1In);
            var strSha1Out = BitConverter.ToString(bytesSha1Out);
            strSha1Out = strSha1Out.Replace("-", "").ToUpper();
            return strSha1Out;
        }


    }
}
