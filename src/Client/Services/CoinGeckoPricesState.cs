using System.Net.Http.Json;

namespace AvalaunchDashboard.Client.Services
{
    public class CoinGeckoPricesState : State
    {
        public const string AvaxContractAddress = "0xb31f66aa3c1e785363f0875a1b74e27b85fd66c7";
        public Dictionary<string, decimal> Data { get; set; } = new();
        public DateTime Last { get; set; } = new();
        public const string baseUrlFormat = "https://api.coingecko.com/api/v3/simple/token_price/avalanche?contract_addresses={0}&vs_currencies=usd";

        internal decimal GetAvaxPrice()
        {
            if (Data.ContainsKey(AvaxContractAddress))
            {
                return Data[AvaxContractAddress];
            }
            return 0;
        }

        public SaleInfoState SaleInfo { get; }
        public CoinGeckoPricesState(HttpClient http, SaleInfoState saleInfo) : base(http)
        {
            this.SaleInfo = saleInfo;
            this.SaleInfo.StateChanged += async () => await Load();
        }
        public async Task Load()
        {
            Console.WriteLine("coingecko load");
            var addresses = SaleInfo.Data.Items.Items.Values.Select(x => x.TokenAddress);
            if (addresses == null || !addresses.Any())
            {
                Console.WriteLine("coingecko load canceled - no addresses");
                return;
            }
            if ((DateTime.Now - Last).TotalMinutes < 5)
            {
                return;
            }
            addresses = addresses.Append(AvaxContractAddress);
            var url = string.Format(baseUrlFormat, string.Join(",", addresses));
            var baseResult = await Http.GetFromJsonAsync<Dictionary<string, Dictionary<string, decimal>>>(url);
            if (baseResult != null)
            {
                Last = DateTime.Now;
                Data = baseResult.ToDictionary(x => x.Key, x => x.Value.Values.First());
                StateHasChanged("Coingecko prices loaded");
            }
        }
    }
}