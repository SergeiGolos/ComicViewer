namespace ComicViewer.Core
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using ComicViewer.Core.Configuration;
    using ComicViewer.Core.Model;

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
        public DbSet<ComicPageFile> Pages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = {config.DatabasePath}");
		}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .Property(u => u.DisplayName)
        //        .HasColumnName("display_name");
        //}
    }
}