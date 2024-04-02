using Contacts.Dados;
using Microsoft.AspNetCore.Components;
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
                _navigationManager.NavigateTo("/");
            }
        }
    }
}
