namespace AvalaunchDashboard.Client.Services
{
    public class AppState : State
    {
        public SaleInfoState SaleInfo { get; }
        public UserInfoState UserInfoState { get; }
        internal AppState(HttpClient http) : base(http)
        {
            SaleInfo = new SaleInfoState(http);
            ChildStates.Add(SaleInfo);
            UserInfoState = new UserInfoState(http);
            ChildStates.Add(UserInfoState);
            CoinGeckoPrices = new CoinGeckoPricesState(http,SaleInfo);
            ChildStates.Add(CoinGeckoPrices);
        }
        public bool IsDark{get;set;}
        public CoinGeckoPricesState CoinGeckoPrices { get; }
    }
}