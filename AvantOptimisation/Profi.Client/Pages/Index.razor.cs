using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Profi.Client;
using Profi.Client.Shared;
using Profi.Controls.Contrats;
using Profi.Controls.Personnes;
using Profi.Dtos;

namespace Profi.Client.Pages
{
    public partial class Index
    {
        [Inject] private IConfiguration Configuration { get; set; } = default!;

        private LoginDto login = new();

        private async Task Login()
        {
            var client = new HttpClient();
            var data = await client.PostAsJsonAsync(new Uri(new Uri(Configuration["Api:BaseUrl"], UriKind.Absolute), "login"), login);
            if(data.IsSuccessStatusCode)
            {

            }
            else
            {

            }
        }
    }
}