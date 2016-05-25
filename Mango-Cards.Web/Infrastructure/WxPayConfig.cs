using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mango_Cards.Web.Infrastructure
{  /**
    * 	配置账号信息
    */
    public class WxPayConfig
    {
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * Appid：绑定支付的APPID（必须配置）
        * Mchid：商户号（必须配置）
        * Key：商户支付密钥，参考开户邮件设置（必须配置）
        * Appsecret：公众帐号secert（仅JSAPI支付的时候需要配置）
        */
        public const string Appid = "wx990c1d301ce34a46";
        public const string Mchid = "1259992201";
        public const string Key = "U1WD49KOM26TB7HPF3R8LZQXSAEVIY5J0CNG";
        public const string Appsecret = "b8bf5fc74c548a1e13d178baaf0071df";

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public const string SslcertPath = "cert/apiclient_cert.p12";
        public const string SslcertPassword = "1233410002";



        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        public const string NotifyUrl = "http://card.mangoeasy.com";

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public const string Ip = "8.8.8.8";


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public const string ProxyUrl = "http://10.152.18.220:8080";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public const int ReportLevenl = 1;

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public const int LogLevenl = 0;
    }
}