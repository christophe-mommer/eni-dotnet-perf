using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Profi.Business.Models;
using Profi.Controls.Personnes.ViewModels;

namespace Profi.Controls.Personnes
{
    public partial class ListePersonne
    {
        [Inject] private IConfiguration Configuration { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private List<PersonneListItemViewModel>? personnes = null;

        protected override async Task OnInitializedAsync()
        {
            var client = new HttpClient();
            var data = await client.GetStringAsync(new Uri(new Uri(Configuration["Api:BaseUrl"], UriKind.Absolute), "api/personnes"));
            personnes = (JsonSerializer.Deserialize<List<Personne>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })).Select(p => new PersonneListItemViewModel(p)).ToList();
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