namespace CLI.Models
{
    public record OnlineShopSearchRequest
    {
        public OnlineShopSearchRequest(string outcode)
        {
            Outcode = outcode;
        }

        public string Outcode {get;}
    }
}