using ComicViewer.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicViewer.Core
{
	public class ComicBookContext : DbContext
	{
		private readonly ComicViewerConfiguration config;

		public DbSet<ComicBookListing> Listing { get; set; }

		public ComicBookContext(ComicViewerConfiguration config, DbContextOptions<ComicBookContext> options) : base(options)
		{
			this.config = config;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source = {config.DatabasePath}");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity();
			//modelBuilder.Entity();
			//modelBuilder.Entity();
			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges()
		{
			AddAuitInfo();
			return base.SaveChanges();
		}

		public async Task<int> SaveChangesAsync()
		{
			AddAuitInfo();
			return await base.SaveChangesAsync();
		}

		private void AddAuitInfo()
		{
			var entries = ChangeTracker.Entries().Where(x => x.Entity is ComicBookListing && (x.State == EntityState.Added || x.State == EntityState.Modified));
			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					((ComicBookListing)entry.Entity).Created = DateTime.UtcNow;
				}
			((ComicBookListing)entry.Entity).Updated = DateTime.UtcNow;
			}
		}
	}
}