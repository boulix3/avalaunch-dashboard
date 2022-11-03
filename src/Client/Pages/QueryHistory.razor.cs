
namespace AvalaunchDashboard.Client.Pages
{
    public partial class QueryHistory
    {
        protected override void OnInitialized()
        {
            _dispatcher.Dispatch(new Flux.QueryHistory.Actions.Load());
            base.OnInitialized();
        }
    }
}