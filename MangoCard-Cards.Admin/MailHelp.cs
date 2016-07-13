using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using MangoCard_Cards.Admin.Models;

namespace MangoCard_Cards.Admin
{
    public static class MailHelp
    {
        public static void SendMailForRegisterPass(RegisterUserModel model)
        {
            var username = ConfigurationManager.AppSettings["MailUser"];
            var pwd = ConfigurationManager.AppSettings["MailPWD"];
            var host = ConfigurationManager.AppSettings["Mailhost"];
            var sub = string.Format("MangoCard开发者申请已通过");
            var body = string.Format("Hi {0}<br>您已经通过了开发者申请。", model.Name);
            SendMail(username, pwd, model.Email, host, sub, body, string.Empty);
        }
        public static void SendMailForRegisterReject(RegisterUserModel model)
        {
            var username = ConfigurationManager.AppSettings["MailUser"];
            var pwd = ConfigurationManager.AppSettings["MailPWD"];
            var host = ConfigurationManager.AppSettings["Mailhost"];
            var sub = string.Format("MangoCard开发者申请被驳回");
            var body = string.Format("Hi {0}<br>抱歉的通知您，您的申请被驳回，原因是：<br>{0}", model.RejectMessage);
            SendMail(username, pwd, model.Email, host, sub, body, string.Empty);
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