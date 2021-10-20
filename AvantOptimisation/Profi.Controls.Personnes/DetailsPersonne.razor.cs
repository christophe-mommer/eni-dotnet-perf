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

        private async Task SavePersonne()
        {

        }
    }
}