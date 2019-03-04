namespace ComicViewer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ComicViewer.Core.Configuration;
    using ComicViewer.Core.Model;
    using Microsoft.EntityFrameworkCore;

    public class StoreComicBookResolver : IComicBookResolver
    {
        private readonly ComicViewerConfiguration config;
        private readonly ComicBookContext context;

        public StoreComicBookResolver(ComicViewerConfiguration config, ComicBookContext context)
        {
            this.config = config;
            this.context = context;
        }

        public ComicBookFile FindById(string id)
        {
            return this.context.WithFiles(files => files.FirstOrDefault(n => n.Id == id));
        }

        public IEnumerable<ComicBookFile> FindByName(string search)
        {
            return this.context.WithFiles(files => files.FromSql("select * from search_files Where originalname match @p0", new [] { search }));
        }        

        public ComicBookFile FindByPath(string path)
        {
            return this.context.WithFiles(files => files.Where(n => n.Path == path).FirstOrDefault());
        }

        public IEnumerable<ComicBookFile> FindByPublisher(string publisher)
        {
            return this.context.WithFiles(files => files.Where(n => n.Publisher == publisher));
        }

        public IEnumerable<ComicPageFile> FindPagesById(string id)
        {
            return this.context.Pages.Where(n => n.ComicId == id).OrderBy(n=>n.Page);
        }

        public IEnumerable<ComicBookFile> FindPublishers()
        {            
            var publishers = new[] { "Archie Comics", "Asterix & Obelix", "Calvin and Hobbes", "Dark Horse Comics", "DC Chronology", "Dynamite", "Futurama", "Ghost in the Shell", "IDW Comics", "Image Comics", "Marvel Chronology", "Top Cow", "Valiant Comics", "Vertigo", "Zenescope" };
            var comics = new List<ComicBookFile>();
            foreach (var publisher in publishers)
            {
                comics.AddRange(this.context.WithFiles(files =>
                    files.FromSql("SELECT * FROM files WHERE Id IN (SELECT id FROM files WHERE publisher=@p0 ORDER BY RANDOM() LIMIT 1)", new[] { publisher })));
            }
            return comics;
        }

        public IEnumerable<ComicBookFile> Search(string[] terms)
        {
            throw new System.NotImplementedException();
        }

        public void Store(ComicBookFile file)
        {
            this.context.Add(file);
            this.context.SaveChanges();
        }
    }
}