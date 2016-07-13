using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Mango_Cards.Web.Models;

namespace Mango_Cards.Web.Infrastructure
{
    public static class MailHelp
    {
        public static void SendMailForRegister(ApplyForDeveloperModel model)
        {
            var username = ConfigurationManager.AppSettings["MailUser"];
            var pwd = ConfigurationManager.AppSettings["MailPWD"];
            var host = ConfigurationManager.AppSettings["Mailhost"];
            var mailaddress= ConfigurationManager.AppSettings["MailAddress"]; 
            var sub = string.Format("{0}注册了开发者", model.Name);
            var body = string.Format("姓名：{0}<br>电话:{1}<br>Email:{2}<br>介绍:{3}", model.Name, model.PhoneNum, model.Email, model.Introduce);
            SendMail(username, pwd, mailaddress, host, sub, body, string.Empty);
        }
        /// <summary>
        /// 发送邮件,返回true表示发送成功
        /// </summary>
        /// <param name="userName">发件人邮箱地址；发件人用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="mailaddress">接受者邮箱地址</param>
        /// <param name="host">SMTP服务器的主机名</param>
        /// <param name="sub">邮件主题行</param>
        /// <param name="body">邮件主体正文</param>
        /// <param name="filePath">附件路径</param>
        /// <param name="ccMailAddresses">CC的地址</param>
        public static bool SendMail(string userName, string pwd, string mailaddress, string host, string sub, string body, string filePath, MailAddress[] ccMailAddresses = null)
        {
            var client = new SmtpClient
            {
                Host = host,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(userName, pwd),
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };


            try
            {
                var message = new MailMessage(userName, mailaddress)
                {
                    Subject = sub,
                    Body = body,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true
                };
                if (ccMailAddresses != null)
                {
                    foreach (var ccMailAddress in ccMailAddresses)
                    {
                        message.CC.Add(ccMailAddress);
                    }
                }

                if (!string.IsNullOrEmpty(filePath))
                {
                    var attachment = new Attachment(filePath) { Name = filePath.Split('/').LastOrDefault() };
                    message.Attachments.Add(attachment);
                }
                client.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}