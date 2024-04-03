using Contacts.Dados;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace Contacts.Pages
{
    public partial class EditContact
    {
        [Parameter]
        public string IdContact { get; set; } = string.Empty;
        public Contact Contact { get; set; } = new();
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

        public async Task UpdateAsync()
        {
            var httpClient = HttpClientFactory.CreateClient("api");

            var json = JsonSerializer.Serialize(Contact);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync("/contact/", content);

            if (response.IsSuccessStatusCode)
            {

                await _jsRunTime.InvokeVoidAsync("alert", "Registro alterado com sucesso!");
                _navigationManager.NavigateTo("/");
            }
            else
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                await _jsRunTime.InvokeVoidAsync("alert", $"Erro: " + responseMessage);
            }
        }
    }
}
