using Mango_Cards.Library.Services;
using Mango_Cards.Web.Infrastructure;
using Mango_Cards.Web.Models;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Mango_Cards.Web.Controllers.API
{
    public class CometController : AsyncController
    {
        private readonly ILoginLogService _loginLogService;
        public CometController(ILoginLogService loginLogService)
        {
            _loginLogService = loginLogService;
        }
        //LongPolling Action 1 - 处理客户端发起的请求
        public void LongPollingAsync()
        {
            var state = Request["state"];
            //计时器，5秒种触发一次Elapsed事件
            var timer = new System.Timers.Timer(1000);
            //告诉ASP.NET接下来将进行一个异步操作
            AsyncManager.OutstandingOperations.Increment();
            //订阅计时器的Elapsed事件
            timer.Elapsed += (sender, e) =>
            {
                //保存将要传递给LongPollingCompleted的参数
                AsyncManager.Parameters["model"] = new LoginLogModel();
                if (state != null)
                {
                    var log = _loginLogService.GetLoginLog(state);
                    if (log != null && log.IsDeleted == false)
                    {
                        //保存将要传递给LongPollingCompleted的参数
                        AsyncManager.Parameters["model"] = new LoginLogModel
                        {
                            Id = log.Id,
                            CreateTime = log.CreateTime,
                            State = log.State,
                        };
                    }

                }
                //告诉ASP.NET异步操作已完成，进行LongPollingCompleted方法的调用
                AsyncManager.OutstandingOperations.Decrement();
            };
            //启动计时器
            timer.Start();
        }
        //LongPolling Action 2 - 异步处理完成，向客户端发送响应
        public ActionResult LongPollingCompleted(LoginLogModel model)
        {
            var log = _loginLogService.GetLoginLog(model.Id);
            if (log != null)
            {
                log.IsDeleted = true;
                _loginLogService.Update();

                //var identity = new CustomIdentity(wechartuser);
                //var principal = new CustomPrincipal(identity);
                //HttpContext.Current.User = principal;
                //FormsAuthentication.SetAuthCookie(log.WeChatUser.Id.ToString(), true);
                var wechartuser = log.WeChatUser;
                var identity = UserService.CreateIdentity(wechartuser, DefaultAuthenticationTypes.ApplicationCookie);
                System.Web.HttpContext.Current.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
