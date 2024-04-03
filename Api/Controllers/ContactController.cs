using Api.Service;
using Contacts.Dados;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult<List<Contact>> GetAll()
        {
            return _contactService.OnGet();
        }

        [HttpGet("{id}")]
        public ActionResult<Contact?> GetById(int id)
        {
            return _contactService.GetById(id);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Create([FromBody] ContactRequest request)
        {
            if (_contactService.VerifyContactNumberLenght(request.ContactNumber))
                return BadRequest("The contact number is invalid!");
            if (_contactService.VerifyUniqueContactNumber(request.ContactNumber))
                return BadRequest("This contact number is already registered!");
            if (_contactService.VerifyUniqueEmail(request.Email))
                return BadRequest("\r\nThis email is already registered!");
            _contactService.OnPost(request);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Contact contact)
        {
            if (_contactService.GetById(contact.Id) == null)
                return NotFound();
            if (_contactService.VerifyUniqueContactNumber(contact.ContactNumber))
                return BadRequest("This contact number is already registered!");
            if (_contactService.VerifyUniqueEmail(contact.Email))
                return BadRequest("\r\nThis email is already registered!");
            _contactService.Update(contact);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            int idDelete = Convert.ToInt32(id);
            if (_contactService.GetById(idDelete) == null)
                return NotFound();

            _contactService.Delete(idDelete);
            return NoContent();
        }
    }
}
