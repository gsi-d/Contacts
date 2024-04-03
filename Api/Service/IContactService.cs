﻿
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
        bool VerifyUniqueContactNumber(string contactNumber, int id = 0);
        bool VerifyUniqueEmail(string email, int id = 0);
        bool VerifyContactNumberLenght(string contactNumber);
    }
}
