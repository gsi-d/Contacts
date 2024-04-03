using Contacts.Dados;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
            else
            {
                isLoading = false;
                await _jsRunTime.InvokeVoidAsync("alert", "Error showing contact details.");
            }
        }

        public async Task Delete()
        {
            var httpClient = HttpClientFactory.CreateClient("api");
            var response = await httpClient.DeleteAsync($"/contact/{IdContact}");

            if (response.IsSuccessStatusCode)
            {

                await _jsRunTime.InvokeVoidAsync("alert", "Contact deleted successfully!");
                _navigationManager.NavigateTo("/");
            }
            else
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                await _jsRunTime.InvokeVoidAsync("alert", $"Error: " + responseMessage);
            }
        }
    }
}
