using System;
using Microsoft.AspNetCore.Components;

namespace AvalaunchDashboard.Client.Shared
{
    public partial class MainLayout
    {
        bool _drawerOpen = true;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

    }
}
