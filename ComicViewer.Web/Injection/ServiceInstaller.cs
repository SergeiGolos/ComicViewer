using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ComicViewer.Web.Injection
{
    public static class ServiceInstaller
    {
        public static void Install(this IServiceCollection services, IConfiguration configuration)
        {
            services.Install(configuration, new[] { Assembly.GetExecutingAssembly() });
        }

        public static void Install(this IServiceCollection services, IConfiguration configuration, IEnumerable<Assembly> assemblies)
        {
            var installers = assemblies.SelectMany(asm => asm.DefinedTypes)
                .Where(type => type.ImplementedInterfaces.Any(t => t == typeof(IServiceInstaller)))
                .OrderByDescending(type =>
                {
                    var attribute = type.GetCustomAttributes(typeof(ServiceInstallerAttribute), true).FirstOrDefault() as ServiceInstallerAttribute;
                    return attribute != null
                        ? attribute.Priority
                        : 0;
                })
                .Select(x => Activator.CreateInstance(x) as IServiceInstaller);

            foreach(var installer in installers)
            {
                installer.Install(services, configuration);
            }
        }
    }
}
