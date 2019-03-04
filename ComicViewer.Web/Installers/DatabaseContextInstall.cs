using ComicViewer.Core;
using ComicViewer.Web.Injection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComicViewer.Web.Installers
{
    [ServiceInstaller(Name = "DatabaseContext", Priority = 10)]
    public class DatabaseContextInstall : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionString").ToString();

            services.AddDbContext<ComicBookContext>(options =>
                    options.UseSqlite(connectionString, builder =>
                            builder.MigrationsAssembly(typeof(Startup).Assembly.FullName)));

        }
    }
}
