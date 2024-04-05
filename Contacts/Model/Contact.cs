using System.ComponentModel.DataAnnotations;

namespace Contacts.Model
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name field is required.")]
        [MinLength(5, ErrorMessage = "Name should be a string of any size greater than 5")]
        public string? Name { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "The ContactNumber field must contain exactly 9 numbers.")]
        public string? ContactNumber { get; set; }

        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string? Email { get; set; }
    }
}
