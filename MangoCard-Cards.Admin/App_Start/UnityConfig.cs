using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using Unity.Mvc5;
using MangoCard_Cards.Admin.Controllers;
using Mango_Cards.Library.Services;
using Mango_Cards.Service.Services;

namespace MangoCard_Cards.Admin
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
         
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IMangoCardService, MangoCardService>();
            container.RegisterType<ICardTemplateService, CardTemplateService>();
            container.RegisterType<IApplyForDeveloperService, ApplyForDeveloperService>();
            container.RegisterType<ICardApprovedService, CardApprovedService>();
            container.RegisterType<ICardTemplateService, CardTemplateService>();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

        }
    }
}