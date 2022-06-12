using System.Linq;
using System.Threading.Tasks;
using CLI.Mapping;
using CLI.Models;
using FluentValidation;
using OneOf;

namespace CLI.Services
{
    public class OnlineShopSearchService : IOnlineShopSearchService
    {
        private readonly IOnlineShopApi _onlineShopApi;
        private readonly IValidator<OnlineShopSearchRequest> _validator;

        public OnlineShopSearchService(IOnlineShopApi onlineShopApi, IValidator<OnlineShopSearchRequest> validator)
        {
            _onlineShopApi = onlineShopApi;
            _validator = validator;
        }

        public async Task<OneOf<OnlineShopSearchResult, OnlineShopSearchError>> SearchByCodeAsync(OnlineShopSearchRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new OnlineShopSearchError(errorMessages);
            }

            var response = await _onlineShopApi.SearchByPostcodeAsync(request.Outcode);
            return new OnlineShopSearchResult
            {
                OnlineShops = response.Products.Select(x => x.ToOnlineShopResult()).ToList()
            };
        }
    }
}