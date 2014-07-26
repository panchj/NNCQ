using Microsoft.Practices.Unity;
using NNCQ.Domain.Common;
using NNCQ.Domain.Core;
using NNCQ.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NNCQ.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //var container = new UnityContainer();

            //container.RegisterType<IEntityRepository<BusinessFile>, EntityRepository<BusinessFile>>(new HierarchicalLifetimeManager());
            //container.RegisterType<IEntityRepository<BusinessImage>, EntityRepository<BusinessImage>>(new HierarchicalLifetimeManager());
            //config.DependencyResolver = new UnityResolverForWebApi(container);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}