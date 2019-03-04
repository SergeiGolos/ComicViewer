using ComicViewer.Core.Configuration;
using ComicViewer.Web.Injection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComicViewer.Web.Installers
{
    [ServiceInstaller(Name = "Configuration", Priority = 5)]
    public class ConfigurationInstall : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.Load<ComicViewerConfiguration>("ComicViewer");
            services.AddSingleton(config);
        }
    }
}
