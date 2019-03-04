using ComicViewer.Core;
using ComicViewer.Core.Configuration;
using ComicViewer.Web.Injection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComicViewer.Web.Installers
{
    public class StoredComicbookResolverInstall : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.Load<ComicViewerConfiguration>("ComicViewer");
            if (!string.IsNullOrEmpty(config.DatabasePath))
            {
                services.AddTransient<IComicBookResolver, StoreComicBookResolver>();
            }
        }
    }
}
