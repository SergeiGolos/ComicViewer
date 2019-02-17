namespace ComicViewer.Indexer
{
    using CommandLine;

    public class IndexerArguments
    {
        [Option('p', "path", Required = true, HelpText = "Root path to be indexed.")]
        public string IndexPath { get; set; }

        [Option('d', "database", Required = true, HelpText = "The sqlite database file to populated with indexed data.")]
        public string Database { get; set; }

        [Option('h', "height", Default = 100)]
        public int Height { get; set; }

        [Option('w', "width", Default = 70)]
        public int Width { get; set; }        
    }
}
