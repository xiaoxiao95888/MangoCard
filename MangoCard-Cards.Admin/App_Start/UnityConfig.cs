using Microsoft.Practices.Unity;
using System.Web.Http;
using MangoCard_Cards.Admin.Controllers;
using Mango_Cards.Library.Services;
using Mango_Cards.Service.Services;
using Unity.WebApi;

namespace MangoCard_Cards.Admin
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IMangoCardService, MangoCardService>();
            container.RegisterType<ICardTemplateService, CardTemplateService>();
            container.RegisterType<IApplyForDeveloperService, ApplyForDeveloperService>();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}