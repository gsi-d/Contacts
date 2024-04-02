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
            _contactService.OnPost(request);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Contact contact)
        {
            if (_contactService.GetById(contact.Id) == null)
                return NotFound();
            _contactService.Update(contact);
            return NoContent();
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
