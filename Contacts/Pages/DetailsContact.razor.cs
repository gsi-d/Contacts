using Contacts.Dados;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Contacts.Pages
{
    public partial class DetailsContact
    {
        [Parameter]
        public string IdContact { get; set; }
        public Contact Contact { get; set; }
        public bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await GetContact();
        }

        public async Task GetContact()
        {
            var httpClient = HttpClientFactory.CreateClient("api");
            var response = await httpClient.GetAsync($"/contact/{IdContact}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Contact = JsonSerializer.Deserialize<Contact>(content);
                isLoading = false;
            }
        }

        public async Task Delete()
        {
            var httpClient = HttpClientFactory.CreateClient("api");
            var response = await httpClient.DeleteAsync($"/contact/{IdContact}");

            if (response.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("/");
            }
        }
    }
}
