using Contacts.Model;
using Contacts.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

namespace Contacts.Pages
{
    public partial class NewContact
    {
        [Parameter] public Contact Contact { get; set; } = new();
        [Inject] private IContactService? _contactService { get; set; }

        public async Task OnPostAsync()
        {
            Contact request = new Contact
            {
                Name = Contact.Name,
                ContactNumber = Contact.ContactNumber,
                Email = Contact.Email
            };

            var response = await _contactService.OnPost(request);

            if (response == null)
            {
                await _jsRunTime.InvokeVoidAsync("alert", "Contact saved successfully!");
                _navigationManager.NavigateTo("/");
            }
            else
            {
                await _jsRunTime.InvokeVoidAsync("alert", $"Error: " + response);
            }
        }
    }
}
