using Contacts.Model;
using Contacts.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Contacts.Pages
{
    public partial class DetailsContact
    {
        [Inject] private IContactService? _contactService { get; set; }
        [Parameter] public string IdContact { get; set; }
        public Contact Contact { get; set; }
        public bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await GetContact();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetContact();

                if (Contact == null)
                {
                    await _jsRunTime.InvokeVoidAsync("alert", "Error showing contact. Please check your connection and try again.");
                }
            }
        }

        public async Task GetContact()
        {
            int idContact = Convert.ToInt32(IdContact);
            Contact contact = _contactService.GetById(idContact);

            if (contact != null)
            {
                Contact = contact;
                isLoading = false;
            }
            else
            {
                Contact = new Contact();
                isLoading = false;
            }
        }

        public async Task Delete()
        {
            int response = _contactService.Delete(Contact.Id);
            if (response != 0)
            {
                await _jsRunTime.InvokeVoidAsync("alert", "Contact deleted successfully!");
                _navigationManager.NavigateTo("/");
            }
            else
            {
                await _jsRunTime.InvokeVoidAsync("alert", "Error when deleting contact.");
            }
        }
    }
}
