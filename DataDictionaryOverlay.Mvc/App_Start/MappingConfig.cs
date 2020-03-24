using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;

namespace DataDictionaryOverlay.App_Start
{
    public class MappingConfig : IStartupTask
    {
        public void Configure()
        {
            var profileType = typeof(Profile);
            var profiles =
                BuildManager.GetReferencedAssemblies()
                    .Cast<Assembly>()
                    .Where(a => a.FullName.Contains("DataDictionaryOverlay.Mvc")).ToArray()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => profileType.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null)
                    .Select(Activator.CreateInstance)
                    .Cast<Profile>();


            Mapper.Initialize(a => { foreach (var profile in profiles) a.AddProfile(profile); });
        }
    }
}