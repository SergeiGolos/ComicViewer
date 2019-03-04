using ComicViewer.Core;
using ComicViewer.Core.Configuration;
using ComicViewer.Core.Indexers;
using ComicViewer.Web.Injection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComicViewer.Web.Installers
{
    public class ComicBookResolverInstall : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.Load<ComicViewerConfiguration>("ComicViewer");
            if (string.IsNullOrEmpty(config.DatabasePath))
            {
                services.AddSingleton(p =>
                {
                    var factory = p.GetService<IComicBookFactory>();
                    return new InMemoryIndexer(config, factory).Run() as InMemoryIndexer;
                });
                services.AddTransient<IComicBookResolver, MemoryComicBookResolver>();
            }
        }
    }
}
