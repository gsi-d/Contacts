
using Contacts.Dados;

namespace Api.Service
{
    public interface IContactService
    {
        List<Contact> OnGet();
        Task OnPost(ContactRequest request);
        Task Update(Contact contact);
        Contact GetById(int id);
        int Delete(int id);
        bool VerifyUniqueContactNumber(string contactNumber);
        bool VerifyUniqueEmail(string email);
        bool VerifyContactNumberLenght(string contactNumber);
    }
}
