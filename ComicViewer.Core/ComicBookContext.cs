namespace ComicViewer.Core
{
    using Microsoft.EntityFrameworkCore;
    using ComicViewer.Core.Configuration;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;

    public class ParallelComicBookContext : IComicBookContext
    {
        private readonly ComicViewerConfiguration config;
        private readonly DbContextOptions<ComicBookContext> options;

        public ParallelComicBookContext(ComicViewerConfiguration config, DbContextOptions<ComicBookContext> options)
        {
            this.config = config;
            this.options = options;
        }

        public DbSet<ComicBookFile> Files { get {
                return new ComicBookContext(this.config, this.options).Files;
            }
        }

        public T WithFiles<T>(Func<DbSet<ComicBookFile>, T> fn)
        {
            using (var context = new ComicBookContext(this.config, this.options))
            {
                return fn(context.Files);
            }
        }

        public EntityEntry Add(object entity)
        {
            using (var context = new ComicBookContext(this.config, this.options))
            {
                var result = context.Add(entity);
                context.SaveChanges();
                return result;
            }
                
        }

        public int SaveChanges()
        {
            return 1;
        }
    }

    public class ComicBookContext : DbContext, IComicBookContext
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

    public interface IComicBookContext
    {
        T WithFiles<T>(Func<DbSet<ComicBookFile>, T> fn);
        int SaveChanges();
        EntityEntry Add(object entity);
    }
}