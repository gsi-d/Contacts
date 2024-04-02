using Api.Data;
using Contacts.Dados;
using Microsoft.EntityFrameworkCore;

namespace Api.Service
{
    public class ContactService : IContactService
    {
        public List<Contact> Contacts { get; set; }

        public ContactService()
        {
            // DataBase mock
            Contacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "Alice", ContactNumber = 12345678, Email = "alice@example.com" },
                new Contact { Id = 2, Name = "Bob", ContactNumber = 98765432, Email = "bob@example.com" },
                new Contact { Id = 3, Name = "Charlie", ContactNumber = 12312312, Email = "charlie@example.com" },
                new Contact { Id = 4, Name = "David", ContactNumber = 45645645, Email = "david@example.com" },
                new Contact { Id = 5, Name = "Eve", ContactNumber = 78978978, Email = "eve@example.com" },
                new Contact { Id = 6, Name = "Frank", ContactNumber = 11122233, Email = "frank@example.com" },
                new Contact { Id = 7, Name = "Grace", ContactNumber = 44455566, Email = "grace@example.com" },
                new Contact { Id = 8, Name = "Hannah", ContactNumber = 77788899, Email = "hannah@example.com" },
                new Contact { Id = 9, Name = "Ian", ContactNumber = 33344455, Email = "ian@example.com" },
                new Contact { Id = 10, Name = "Jane", ContactNumber = 66677788, Email = "jane@example.com" },
                new Contact { Id = 11, Name = "Kyle", ContactNumber = 22233344, Email = "kyle@example.com" },
                new Contact { Id = 12, Name = "Laura", ContactNumber = 55566677, Email = "laura@example.com" }
            };
        }

        public List<Contact> OnGet()
        {
            return Contacts;
            //return _dbContext.Contact.ToList();
        }

        public async Task OnPost(ContactRequest request)
        {
            if (VerifyContactNumberLenght(request.ContactNumber))
                return;
            if (VerifyUniqueContactNumber(request.ContactNumber) || VerifyUniqueEmail(request.Email))
                return;
            Contact contact = new Contact
            {
                ContactNumber = request.ContactNumber,
                Name = request.Name,
                Email = request.Email,
            };
            await Task.Factory.StartNew(() =>
            {
                Contacts.Add(contact);
            });
        }

        public async Task Update(Contact contact)
        {
            if (VerifyUniqueContactNumber(contact.ContactNumber) || VerifyUniqueEmail(contact.Email))
                return;
            await Task.Factory.StartNew(() =>
            {
                Contact contactUpdate = Contacts.FirstOrDefault(x => x.Id == contact.Id);
                Contacts.Remove(contactUpdate);
                Contacts.Add(contact);
            });
        }

        public Contact GetById(int id)
        {
            var contact = Contacts.FirstOrDefault(x => x.Id == id);
            return contact;
        }

        public int Delete(int id)
        {
            Contact contactToDelete = Contacts.FirstOrDefault(x => x.Id == id);
            Contacts.Remove(contactToDelete);
            return contactToDelete.Id;
        }

        public bool VerifyUniqueContactNumber(int contactNumber)
        {
            return Contacts.Any(x => x.ContactNumber == contactNumber);
        }

        public bool VerifyUniqueEmail(string email)
        {
            return Contacts.Any(x => x.Email == email);
        }

        public bool VerifyContactNumberLenght(int contactNumber)
        {
            return contactNumber.ToString().Length != 9;
        }
    }
}
