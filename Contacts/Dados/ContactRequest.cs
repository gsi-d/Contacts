using System.ComponentModel.DataAnnotations;


namespace Contacts.Dados
{
    public class ContactRequest
    {
        public string Name { get; set; }
        public int ContactNumber { get; set; }
        [EmailAddress(ErrorMessage = "O endereço de email não é válido.")]
        public string Email { get; set; }
    }
}
