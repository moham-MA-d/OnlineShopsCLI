using System.Text.Json;
using System.Threading.Tasks;
using CLI.Models;
using CLI.Output;
using CLI.Services;
using AutoFixture;
using CLI;
using NSubstitute;
using OneOf;
using Xunit;

namespace API.Tests
{
    public class OnlineShopSearchApplicationTests
    {
        private readonly OnlineShopSearchApplication _sut;
        private readonly IConsoleWriter _consoleWriter = Substitute.For<IConsoleWriter>();
        private readonly IOnlineShopSearchService _onlineShopSearchService = Substitute.For<IOnlineShopSearchService>();
        private readonly IFixture _fixture = new Fixture();

        public OnlineShopSearchApplicationTests()
        {
            _sut = new OnlineShopSearchApplication(_consoleWriter, _onlineShopSearchService);
        }

        [Fact]
        public async Task RunAsync_ShouldReturnOnlineShops_WhenOutcodeIsValid()
        {
            // Arrange
            const string outcode = "E2";
            // we have arguments in an array in a similar fashion
            var arguments = new[] {"--o", outcode};

            var onlineShopResult = _fixture.Create<OnlineShopSearchResult>();

            OneOf<OnlineShopSearchResult, OnlineShopSearchError> result = onlineShopResult;
            var searchResult = new OnlineShopSearchRequest(outcode);
            _onlineShopSearchService.SearchByCodeAsync(searchResult).Returns(result);
            var expectedSerializedText = JsonSerializer.Serialize(onlineShopResult, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Act
            await _sut.RunAsync(arguments);

            // Assert
            _consoleWriter.Received(1).WriteLine(Arg.Is(expectedSerializedText));
        }


        [Fact]
        public async Task RunAsync_ShouldReturnErrorMessage_WhenOutcodeIsInvalid()
        {
            // Arrange
            const string outcode = " E2 1AA ";
            var arguments = new[] {"--o", outcode};
            const string invalidoutcodeError = "Please provide a valid UK Outcode";
            var errorResult = new OnlineShopSearchError(new[] {invalidoutcodeError});
            OneOf<OnlineShopSearchResult, OnlineShopSearchError> result = errorResult;
            var searchResult = new OnlineShopSearchRequest(outcode);
            _onlineShopSearchService.SearchByCodeAsync(searchResult).Returns(result);

            // Act
            await _sut.RunAsync(arguments);

            // Assert
            _consoleWriter.Received(1).WriteLine(Arg.Is(invalidoutcodeError));
        }
    }
}