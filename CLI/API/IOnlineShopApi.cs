using System.Threading.Tasks;
using CLI.Responses;

namespace CLI;

public interface IOnlineShopApi
{
    Task<OnlineShopSearchResponse>SearchByPostcodeAsync(string postcode);
}