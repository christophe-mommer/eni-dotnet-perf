using Blazored.SessionStorage;
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
        [Inject] private ISessionStorageService SessionStorageService { get; set; } = default!;
        [Parameter] public string? Uid { get; set; }

        private Personne? personne;

        protected override async Task OnInitializedAsync()
        {
            var personnes = await SessionStorageService.GetItemAsync<List<Personne>>("personnes");
            if (personnes is not null)
            {
                personne = personnes.Find(p => p.Uid == Uid);
            }
            if (personne is null)
            {
                var client = new HttpClient();
                var data = await client.GetStringAsync(new Uri(new Uri(Configuration["Api:BaseUrl"], UriKind.Absolute), $"api/personnes/{Uid}"));
                personne = JsonSerializer.Deserialize<Personne>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (personne is not null && personnes is not null)
                {
                    personnes.Add(personne);
                    await SessionStorageService.SetItemAsync("personnes", personnes);
                }
            }
        }
    }
}