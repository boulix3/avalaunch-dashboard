namespace AvalaunchDashboard.Client.Shared
{
    public partial class MainLayout
    {
        public string YellowFill = $"color:{MudBlazor.Colors.Yellow.Lighten1};";
        public bool IsDarkMode { get; set; }
        bool _drawerOpen = true;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;

        }

        protected override Task OnInitializedAsync()
        {
            _snackbar.Configuration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomLeft;
            _state.StateChangedMessage += ShowMessage;
            return base.OnInitializedAsync();
        }

        protected void ShowMessage(string message)
        {
            message = $"{DateTime.Now:T} - {message}";
            _snackbar.Add(message, MudBlazor.Severity.Info);
        }
public const string coinGeckoWhite = """
<svg version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
	 viewBox="0 0 683.1 680.74" style="enable-background:new 0 0 683.1 680.74;" xml:space="preserve">
<style type="text/css">
	.st0{fill:#FFFFFF;}
</style>
<g id="Coin_Gecko_Vector_Text">
</g>
<g id="Coin_Gecko_AI">
	<g>
		<g id="XMLID_230_">
			<path class="st0" d="M428.65,207.87c-0.73-2.13-1.56-4.16-2.53-6.08C427.4,203.97,428.23,206.01,428.65,207.87z"/>
		</g>
		<g id="XMLID_200_">
			<g id="XMLID_202_">
				<g>
					<path class="st0" d="M340.78,11.76C158.55,12.59,11.49,160.97,12.3,343.22c0.83,182.23,149.23,329.29,331.46,328.48
						c182.23-0.83,329.29-149.21,328.48-331.44C671.41,158.01,523.03,10.96,340.78,11.76z M299.01,325.55
						c-32.6,0-59.02-26.4-59.02-58.99c0-32.57,26.42-58.99,59.02-58.99c32.57,0,58.99,26.42,58.99,58.99
						C358,299.16,331.58,325.55,299.01,325.55z M346.05,389.32l0.31-0.38l0.33-0.38c16.84,10.86,36.05,14.69,55.26,15.26
						c19.23,0.5,38.82-0.95,58.09-4.83c19.25-4,38.18-9.91,56.46-17.46c10.27-4.23,20.46-8.85,30.18-14.29
						c0.07-0.05,0.12-0.07,0.19-0.12c6.06-3.64,11.97-7.47,17.76-11.42c1.25-0.9,2.48-1.84,3.69-2.81l0.14,0.17l0.5,0.59
						c-14.83,13.51-32.5,23.54-50.48,32.38c-18.17,8.56-37.11,15.56-56.67,20.63c-19.49,5.04-39.86,8.85-60.46,6.98
						C381.09,411.84,359.7,404.75,346.05,389.32z M497.46,325.55c-0.02-7.07,5.68-12.8,12.73-12.84c7.07-0.02,12.8,5.65,12.84,12.73
						c0.02,7.05-5.68,12.82-12.73,12.84C503.26,338.3,497.51,332.6,497.46,325.55z M396.39,642.04
						c-12.75-89.29,65.24-176.74,109.21-221.5c9.98-10.15,25.4-24.13,39.76-38.98c57.08-53.36,68.43-117.13-48.37-149.02
						c-22.14-6.41-45.08-15.49-68.34-24.67c-0.73-2.13-1.56-4.16-2.53-6.08c-2.67-4.59-7.33-9.89-14.41-15.9
						c-15.19-13.18-43.71-12.82-68.34-7c-27.23-6.41-54.1-8.68-79.9-2.51c-112.97,31.13-131.14,86.24-135.54,152.62
						c-5.98,71.74-9.49,125.2-36.64,186.3c-33.9-48.89-53.88-108.19-54.14-172.2C36.4,174.59,172.39,37.38,340.9,36.62
						s305.73,135.23,306.48,303.74C648.07,490.87,539.61,616.45,396.39,642.04z"/>
				</g>
			</g>
			<path id="XMLID_201_" class="st0" d="M340.5,267.1c0,22.92-18.59,41.49-41.49,41.49c-22.92,0-41.51-18.57-41.51-41.49
				s18.59-41.49,41.51-41.49C321.9,225.62,340.5,244.18,340.5,267.1z"/>
		</g>
	</g>
</g>
</svg>
""";
    }
}
