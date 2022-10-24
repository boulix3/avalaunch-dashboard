using System;
using Microsoft.AspNetCore.Components;

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

    }
}
