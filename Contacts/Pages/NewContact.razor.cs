using Contacts.Dados;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace Contacts.Pages
{
    public partial class NewContact
    {
        [Parameter] public Contact Contact { get; set; } = new();

        public async Task OnPostAsync()
        {
            var httpClient = HttpClientFactory.CreateClient("api");

            ContactRequest request = new ContactRequest
            {
                Name = Contact.Name,
                ContactNumber = Contact.ContactNumber,
                Email = Contact.Email
            };
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/contact", content);

            if (response.IsSuccessStatusCode)
            {

                await _jsRunTime.InvokeVoidAsync("alert", "Registro cadastrado com sucesso!");
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
