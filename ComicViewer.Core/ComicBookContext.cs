namespace ComicViewer.Core
{
    using Microsoft.EntityFrameworkCore;
    using ComicViewer.Core.Configuration;

    public class ComicBookContext : DbContext
	{
		private readonly ComicViewerConfiguration config;		

		public ComicBookContext(ComicViewerConfiguration config, DbContextOptions<ComicBookContext> options) : base(options)
		{
			this.config = config;
            this.Database.EnsureCreated();
        }

        public DbSet<ComicBookFile> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = {config.DatabasePath}");
		}
	}
}