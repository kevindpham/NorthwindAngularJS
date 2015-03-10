using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NorthwindCustomerService.Web.App_Start;

namespace NorthwindCustomerService.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Config AutoMapper
            AutoMapperConfig.CreateMapping();

            //Configure Unity IoC
            DIUnityResolverConfig.Config(GlobalConfiguration.Configuration);

            //Configure Json Formatter
            JsonFormatterConfig.Config(GlobalConfiguration.Configuration);
        }
    }
}
