using Microsoft.AspNetCore.Components;
using Profi.Client.Service;
using Profi.Dtos;
using System.Net.Http.Json;

namespace Profi.Client.Pages
{
    public partial class Index
    {
        [Inject] private IConfiguration Configuration { get; set; } = default!;
        [Inject] private CookieService Cookie { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private readonly LoginDto login = new();
        private bool? isError;


        private async Task Login()
        {
            isError = null;
            var client = new HttpClient();
            var data = await client.PostAsJsonAsync(new Uri(new Uri(Configuration["Api:BaseUrl"], UriKind.Absolute), "login"), login);
            if (data.IsSuccessStatusCode)
            {
                await Cookie.CreateCookie("Authenticated", "true");
                Navigation.NavigateTo("/personnes", true);
            }
            else
            {
                isError = true;
            }
        }
    }
}