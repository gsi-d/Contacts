using Contacts.Model;
using Contacts.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Contacts.Pages
{
    public partial class EditContact
    {
        [Inject] private IContactService? _contactService { get; set; }
        [Parameter] public string? IdContact { get; set; }
        public Contact Contact { get; set; } = new();
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

        public async Task UpdateAsync()
        {
            var response = await _contactService.Update(Contact);

            if (response == null)
            {
                await _jsRunTime.InvokeVoidAsync("alert", "Contact updated successfully!");
                _navigationManager.NavigateTo("/");
            }
            else
            {
                await _jsRunTime.InvokeVoidAsync("alert", $"Error: " + response);
            }
        }
    }
}
