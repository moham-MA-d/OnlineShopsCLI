using System.Linq;
using System.Threading.Tasks;
using CLI.Mapping;
using CLI.Models;
using CLI.Responses;
using CLI.Services;
using CLI.Validators;
using AutoFixture;
using CLI;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace API.Tests
{
    public class OnlineShopSearchServiceTests
    {
        private readonly OnlineShopSearchService _sut;
        private readonly IOnlineShopApi _onlineShopApi = Substitute.For<IOnlineShopApi>();
        private readonly IValidator<OnlineShopSearchRequest> _validator = new OnlineShopSearchRequestValidator();
        private readonly IFixture _fixture = new Fixture();

        public OnlineShopSearchServiceTests()
        {
            _sut = new OnlineShopSearchService(_onlineShopApi, _validator);
        }


        [Fact]
        public async Task SearchByOutcodeAsync_ShouldReturnResults_WhenOutcodeIsValid()
        {
            // Arrange
            const string outcode = "E2";
            var request = new OnlineShopSearchRequest(outcode);
            var apiResponse = _fixture.Create<OnlineShopSearchResponse>();
            _onlineShopApi.SearchByPostcodeAsync(outcode).Returns(apiResponse);
            var expectedResult = new OnlineShopSearchResult
            {
                OnlineShops = apiResponse.Products.Select(x => x.ToOnlineShopResult()).ToList()
            };

            // Act
            var result = await _sut.SearchByCodeAsync(request);

            // Assert
            // AsT0 get the first parameter of OneOf
            // Because we are using records we have to add options 
            result.AsT0.Should().BeEquivalentTo(expectedResult, options =>
                options.ComparingByMembers<OnlineShopSearchResult>()
                    .ComparingByMembers<OnlineShopResult>());
        }
        
        
        [Fact]
        public async Task SearchByOutcodeAsync_ShouldReturnErrors_WhenOutcodeIsInValid()
        {
            // Arrange
            const string outcode = "E2AAA";
            var request = new OnlineShopSearchRequest(outcode);
           
            // apiResponse should be remove because there will be no api response 

            var errorMessages = new string[] {"Please provide a valid UK Outcode"};
            var expectedResult = new OnlineShopSearchError(errorMessages);

            // Act
            var result = await _sut.SearchByCodeAsync(request);

            // Assert
            // AsT1 is the bad response in OneOf
            result.AsT1.Should().BeEquivalentTo(expectedResult, options =>
                options.ComparingByMembers<OnlineShopSearchError>()
            );
        }
    }
}