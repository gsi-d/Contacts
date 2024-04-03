using Api.Data;
using Contacts.Dados;

namespace Api.Service
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;

        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Contact> OnGet()
        {
            return _context.Contact.ToList();
        }

        public async Task OnPost(ContactRequest request)
        {
            Contact contact = new Contact
            {
                ContactNumber = request.ContactNumber,
                Name = request.Name,
                Email = request.Email,
            };
            _context.Contact.Add(contact);
            _context.SaveChanges();
        }

        public async Task Update(Contact contact)
        {
            try
            {
                var existingContact = await _context.Contact.FindAsync(contact.Id);
                if (existingContact != null)
                {
                    _context.Entry(existingContact).CurrentValues.SetValues(contact);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Contact.Update(contact);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public Contact GetById(int id)
        {
            return _context.Contact.FirstOrDefault(x => x.Id == id);
        }

        public int Delete(int id)
        {
            Contact contactToDelete = _context.Contact.FirstOrDefault(x => x.Id == id);
            _context.Remove(contactToDelete);
            _context.SaveChanges();
            return contactToDelete.Id;
        }

        public bool VerifyUniqueContactNumber(string contactNumber, int id = 0)
        {
            if (id != 0)
                return _context.Contact.Where(x => x.Id != id).Any(x => x.ContactNumber == contactNumber);
            return false;
        }

        public bool VerifyUniqueEmail(string email, int id = 0)
        {
            if (id != 0)
                return _context.Contact.Where(x => x.Id != id).Any(x => x.Email == email);
            return false;
        }

        public bool VerifyContactNumberLenght(string contactNumber)
        {
            return contactNumber.ToString().Length != 9;
        }
    }
}