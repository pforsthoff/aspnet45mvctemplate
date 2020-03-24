using System.Data.Entity;
using System.Web.Security;
using Autofac;
using Autofac.Extras.Attributed;
using Autofac.Integration.Mvc;
using AutoMapper;
using DataDictionary.Business.Managers;
using DataDictionary.Common.Logging;
using DataDictionary.Data;
using DataDictionary.Data.Repositories;
using ServiceModel.Business;

namespace DataDictionaryOverlay.DI
{
    public class MvcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register controllers all at once using assembly scanning...
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //NLog
            builder.RegisterType<NLogLogger>().As<ILogger>().SingleInstance();
            //AutoMapper
            builder.Register(c => Mapper.Engine).As<IMappingEngine>();

            //builder.Register(c => Roles.Provider).As<RoleProvider>();
            builder.RegisterType<SQLiteDBContext>().As<DbContext>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MetadataManager>().As<IMetadataManager>().WithAttributeFilter().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MetadataManager>().As<IMetadataManager>().WithAttributeFilter().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<Repository>().As<IRepository>().WithAttributeFilter().AsImplementedInterfaces().InstancePerLifetimeScope();
           
        }
    }
}