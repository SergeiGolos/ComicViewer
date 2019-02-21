namespace ComicViewer.Core
{
    using Microsoft.EntityFrameworkCore;
    using ComicViewer.Core.Configuration;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    
    public class ComicBookContext : DbContext
	{
		private readonly ComicViewerConfiguration config;		

		public ComicBookContext(ComicViewerConfiguration config, DbContextOptions<ComicBookContext> options) : base(options)
		{
			this.config = config;
            this.Database.EnsureCreated();
        }

        public T WithFiles<T>(Func<DbSet<ComicBookFile>, T> fn)
        {
            return fn(this.Files);
        }

        public DbSet<ComicBookFile> Files { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = {config.DatabasePath}");
		}
	}
}