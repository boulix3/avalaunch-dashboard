namespace AvalaunchDashboard.Client.Services
{
    public class AppState : State
    {
        public SaleInfoState SaleInfo { get; }
        internal AppState(HttpClient http) : base(http)
        {
            SaleInfo = new SaleInfoState(http);
            ChildStates.Add(SaleInfo);
        }
        public bool IsDark{get;set;}
    }
}