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
        //创建随机字符串
        public static string CreateNonceCode()
        {
            var letter = new[]
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U",
                "V", "W", "X", "Y", "Z"
            };
            var number = new[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            };
       
            var rand = new Random();
            var str = letter[rand.Next(0, 24)] + letter[rand.Next(0, 24)] +
                             letter[rand.Next(0, 24)] + number[rand.Next(0, 8)] + number[rand.Next(0, 8)] + number[rand.Next(0, 8)];
            return str;
        }
    }
}