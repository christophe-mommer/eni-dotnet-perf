using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Profi.Business.Models;
using System.Text.Json;

namespace Profi.Controls.Personnes
{
    public partial class DetailsPersonne
    {
        [Inject] private IConfiguration Configuration { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Parameter] public string? Uid { get; set; }

        private Personne? personne;

        protected override async Task OnInitializedAsync()
        {
            var client = new HttpClient();
            var data = await client.GetStringAsync(new Uri(new Uri(Configuration["Api:BaseUrl"], UriKind.Absolute), $"api/personnes/{Uid}"));
            personne = JsonSerializer.Deserialize<Personne>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}