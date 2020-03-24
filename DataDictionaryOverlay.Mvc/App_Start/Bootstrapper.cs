using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using DataDictionaryOverlay.DI;

namespace DataDictionaryOverlay.App_Start
{
    public static class Bootstrapper
    {
        public static IContainer Container { get; set; }

        public static void Initialise()
        {

            var container = BuildContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("utc_date", typeof(UtcDateRenderer));
            //ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition("web_variables", typeof(WebVariablesRenderer));

            var tasks = container.Resolve<IEnumerable<IStartupTask>>();
            foreach (var t in tasks) t.Configure();

            Container = container;


            //MvcSiteMapProvider.SiteMaps.Loader = container.Resolve<ISiteMapLoader>();

            // Check all configured .sitemap files to ensure they follow the XSD for MvcSiteMapProvider (optional)
            //var validator = container.Resolve<ISiteMapXmlValidator>();
            //validator.ValidateXml(HostingEnvironment.MapPath("~/Mvc.sitemap"));

        }

        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterModule(new MvcSiteMapProviderModule());

            var assemblies = BuildManager.GetReferencedAssemblies()
                .Cast<Assembly>()
                .Where(a => a.FullName.Contains("DataDictionaryOverlay.Mvc"))
                .ToArray();

            //Controllers and other
            builder.RegisterModule(new MvcModule());

            //builder.RegisterModule(new MvcSiteMapProviderModule());

            //TODO: DO WE NEED THIS SINCE THE BM's ARE REGISTERED ABOVE?
            //builder.RegisterAssemblyTypes(assemblies)
            //    .Where(t => t.Name == "Boeing.Applications.Braidss.Business").InstancePerHttpRequest();

            var startupType = typeof(IStartupTask);

            //Assembly Startup Tasks
            //TODO: Put this back in place when ReadOnly Repository for Greenplum is refactored
            //*********************************************************************************
            //var pregenType = typeof(PregenerateEfViewsStartupTask);

            ////TODO:  This path may require privledges for the App Pool Svc Account
            //builder.RegisterType<PregenerateEfViewsStartupTask>()
            //        .WithParameter("path", Path.Combine(HostingEnvironment.MapPath("~"), "EntityFramework", "EFViewCache.xml"))
            //        .As<IStartupTask>();

            //builder.RegisterAssemblyTypes(assemblies).Where(t => startupType.IsAssignableFrom(t) && t != pregenType).As<IStartupTask>();
            //*********************************************************************************

            builder.RegisterAssemblyTypes(assemblies).Where(t => startupType.IsAssignableFrom(t)).As<IStartupTask>();

            return builder.Build();
        }
    }
}