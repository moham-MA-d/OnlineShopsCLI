using System.Threading.Tasks;
using CLI.Models;
using OneOf;

namespace CLI.Services;

public interface IOnlineShopSearchService
{
    Task<OneOf<OnlineShopSearchResult, OnlineShopSearchError>> SearchByCodeAsync(OnlineShopSearchRequest request);
}