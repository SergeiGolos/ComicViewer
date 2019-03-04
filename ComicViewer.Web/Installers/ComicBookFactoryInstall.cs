using ComicViewer.Core;
using ComicViewer.Web.Injection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComicViewer.Web.Installers
{
    public class ComicBookFactoryInstall : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            // TODO: pull in the type from configuration.
            services.AddTransient<IComicBookFactory, CacheComcicBookFactory>();
        }
    }
}
