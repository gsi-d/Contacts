using System.ComponentModel.DataAnnotations;

namespace Contacts.Dados
{
    public class Contact
    {
        public int Id { get; set; }

        [MinLength(5, ErrorMessage = "Name should be a string of any size greater than 5")]
        public string Name { get; set; }

        public int ContactNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
    }
}
