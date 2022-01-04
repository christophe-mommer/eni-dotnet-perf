using Microsoft.JSInterop;

namespace Profi.Client.Service
{
    public class CookieService
    {
        private readonly IJSRuntime jsRuntime;

        public CookieService(
            IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public ValueTask CreateCookie(string name, string value, int days = 30)
            => jsRuntime.InvokeVoidAsync("createCookie", name, value, days);

        public ValueTask<string> GetCookie(string name)
            => jsRuntime.InvokeAsync<string>("readCookie", name);
    }
}
