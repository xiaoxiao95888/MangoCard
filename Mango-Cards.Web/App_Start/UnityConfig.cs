using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Mango_Cards.Library.Services;
using Mango_Cards.Service.Services;
using Unity.Mvc5;
using Mango_Cards.Web.Controllers;

namespace Mango_Cards.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);

            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICardTypeService, CardTypeService>();
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<ILoginLogService, LoginLogService>();
            container.RegisterType<IWeChatUserService, WeChatUserService>();
            container.RegisterType<IMangoCardService, MangoCardService>();

            container.RegisterType<AccountController>(new InjectionConstructor());
        }
    }
}