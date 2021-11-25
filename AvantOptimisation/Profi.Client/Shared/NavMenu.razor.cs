using Microsoft.AspNetCore.Components;
using Profi.Client.Service;

namespace Profi.Client.Shared
{
    public partial class NavMenu
    {
        [Inject] private CookieService Cookie { get; set; } = default!;

        private bool collapseNavMenu = true;
        private bool isAuthenticated;
        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override async Task OnInitializedAsync()
        {
            isAuthenticated = (await Cookie.GetCookie("Authenticated")) == "true";
        }
    }
}