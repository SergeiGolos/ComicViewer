namespace ComicViewer.Indexer
{
    using CommandLine;

    public class IndexerArguments
    {
        [Option('p', "path", Required = true, HelpText = "Root path to be indexed.")]
        public string IndexPath { get; set; }

        [Option('d', "database", Required = true, HelpText = "The sqlite database file to populated with indexed data.")]
        public string Database { get; set; }
    }
}
