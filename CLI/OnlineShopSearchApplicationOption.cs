using CommandLine;

namespace CLI;

public abstract class OnlineShopSearchApplicationOption
{
    [Option('o', "outcode", Required = true, HelpText = "Provides the outcode to perform the search on.")]
    public string Outcode { get; init; }
}