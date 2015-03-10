using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using NorthwindCustomerService.DataAccess;
using NorthwindCustomerService.Model;
using NorthwindCustomerService.Web.DIResolver;

namespace NorthwindCustomerService.Web.App_Start
{
    public class DIUnityResolverConfig
    {
        public static void Config(HttpConfiguration config)
        {
            //Dependency Injection Resolver Init
            var container = new UnityContainer();
            container.RegisterType<UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<NorthwindEntities>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}