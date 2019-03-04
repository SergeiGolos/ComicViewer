using System;

namespace ComicViewer.Web.Injection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceInstallerAttribute : Attribute {

        public string Name { get; set; }

        public int Priority { get; set; }

    }    
}
