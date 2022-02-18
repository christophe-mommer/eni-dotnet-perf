using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Profi.Business.Models;
using Profi.Controls.Personnes.ViewModels;
using System.Text.Json;

namespace Profi.Controls.Personnes
{
    public partial class ListePersonne
    {
        [Inject] private IConfiguration Configuration { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private ISessionStorageService Session { get; set; } = default!;

        private List<PersonneListItemViewModel>? personnes = null;

        protected override async Task OnInitializedAsync()
        {
            var client = new HttpClient();
            var data = await client.GetStringAsync(new Uri(new Uri(Configuration["Api:BaseUrl"], UriKind.Absolute), "api/personnes"));
            var list = JsonSerializer.Deserialize<List<Personne>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (list is not null)
            {
                await Session.SetItemAsync("personnes", list);
                personnes = list.ConvertAll(p => new PersonneListItemViewModel(p));
            }
        }

        private void AfficherDetails(PersonneListItemViewModel personne)
        {
            if (personne is not null)
            {
                Navigation.NavigateTo($"/personne/{personne.Payload.Uid}");
            }
        }
    }
}