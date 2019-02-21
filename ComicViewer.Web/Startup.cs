namespace ComicViewer.Web
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ComicViewer.Core;
    using ComicViewer.Core.Configuration;
    using ComicViewer.Core.Indexers;
    using Microsoft.Extensions.Caching.Memory;

    public static class AppConfiguration
	{
		public static T Load<T>(this IConfiguration Configuration, string section) where T : new()
		{
			var config = new T();
			Configuration.Bind(section, config);
			return config;
		}
	}

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            var config = Configuration.Load<ComicViewerConfiguration>("ComicViewer");
            var connectionString = Configuration.GetSection("ConnectionString").ToString();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
                
            services.AddSingleton(config);
            services.AddDbContext<ComicBookContext>(options => 
                    options.UseSqlite(connectionString, builder => 
                            builder.MigrationsAssembly(typeof(Startup).Assembly.FullName)));
            
			services.AddTransient<IImageProcessor, ImageSharpProcessor>();

            //services.AddTransient<IComicBookFactory, ComicBookFactory>();            
            services.AddTransient<IComicBookFactory, CacheComcicBookFactory>();

            if (string.IsNullOrEmpty(config.DatabasePath))
            {
                services.AddSingleton(p =>
                {
                    var factory = p.GetService<IComicBookFactory>();
                    return new InMemoryIndexer(config, factory).Run() as InMemoryIndexer;
                });
                services.AddTransient<IComicBookResolver, MemoryComicBookResolver>();
            }
            else
            {                
                services.AddTransient<IComicBookResolver, StoreComicBookResolver>();
            }
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IHostingEnvironment env)
		{
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});			
		}
	}
}