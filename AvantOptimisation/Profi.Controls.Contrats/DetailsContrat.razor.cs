using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Profi.Business.Models;
using System.Text.Json;

namespace Profi.Controls.Contrats
{
    public partial class DetailsContrat
    {
        [Inject] private IConfiguration Configuration { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

        [Parameter] public string? Uid { get; set; }

        private Contrat? contrat = null;

        protected override async Task OnInitializedAsync()
        {
            var client = new HttpClient();
            string? data;
            if (!string.IsNullOrWhiteSpace(Uid))
            {
                data = await client.GetStringAsync(new Uri(new Uri(Configuration["Api:BaseUrl"], UriKind.Absolute), $"api/contrats/byId/{Uid}"));
                contrat = JsonSerializer.Deserialize<Contrat>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
    }
}