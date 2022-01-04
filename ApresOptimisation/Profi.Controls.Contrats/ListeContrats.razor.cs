using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Profi.Business.Models;
using Profi.Controls.Contrats.ViewModels;
using System.Text.Json;

namespace Profi.Controls.Contrats
{
    public partial class ListeContrats
    {
        [Inject] private IConfiguration Configuration { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        [Parameter] public string? PersonneId { get; set; }

        private List<ContratListItemViewModel>? contrats = null;

        protected override async Task OnInitializedAsync()
        {
            var client = new HttpClient();
            string? data;
            if (!string.IsNullOrWhiteSpace(PersonneId))
            {
                data = await client.GetStringAsync(new Uri(new Uri(Configuration["Api:BaseUrl"], UriKind.Absolute), $"api/contrats/{PersonneId}"));
                var list = JsonSerializer.Deserialize<List<Contrat>>(data,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (list is not null)
                {
                    contrats = list.ConvertAll(c => new ContratListItemViewModel(c, false));
                }
            }
        }

        private void AfficherDetails(ContratListItemViewModel contrat)
        {
            if (contrat is not null)
            {
                Navigation.NavigateTo($"/contrat/{contrat.Payload.Uid}");
            }
        }
    }
}