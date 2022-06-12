using System.Text.Json;
using System.Threading.Tasks;
using CLI.Models;
using CLI.Output;
using CLI.Services;
using CommandLine;
using OneOf;

namespace CLI
{
    //Entry point for our application through DI Container
    // So any service that cascading out of that will also have DI Enabled
    // and we don't have to be worry about testability because we just get for free if we use interfaces
    //and dependency injection
    public class OnlineShopSearchApplication
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IOnlineShopSearchService _onlineShopSearchService;

        public OnlineShopSearchApplication(IConsoleWriter consoleWriter,
            IOnlineShopSearchService onlineShopSearchService)
        {
            _consoleWriter = consoleWriter;
            _onlineShopSearchService = onlineShopSearchService;
        }

        
        // Where we get argument from the command line and to through actual obligation flow.
        public async Task RunAsync(string[] args)
        {
            await Parser.Default
                .ParseArguments<OnlineShopSearchApplicationOption>(args)
                .WithParsedAsync(async option =>
                {
                    var searchRequest = new OnlineShopSearchRequest(option.Outcode);
                    var result = await _onlineShopSearchService.SearchByCodeAsync(searchRequest);
                 
                    HandleSearchResult(result);
                });
        }

        private void HandleSearchResult(OneOf<OnlineShopSearchResult, OnlineShopSearchError> result)
        {
            result.Switch(searchResult =>
                {
                    var formattedTextResult = JsonSerializer.Serialize(searchResult, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    _consoleWriter.WriteLine(formattedTextResult);
                },
                error =>
                {
                    var formattedErrors = string.Join(" , ", error.ErrorMessages);
                    _consoleWriter.WriteLine(formattedErrors);
                });
        }
    }
}