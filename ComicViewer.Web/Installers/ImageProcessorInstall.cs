using ComicViewer.Core;
using ComicViewer.Web.Injection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicViewer.Web.Installers
{
    public class ImageProcessorInstall : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IImageProcessor, ImageSharpProcessor>();
        }
    }
}
