using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsWebAPI.Data;
using ContactsWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDBContext _db;

        public ContactsController(ApplicationDBContext db)
        {
            _db = db;
        }
        // GET: api/Contacts
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Contact>>> Get()
        {
            return await _db.Contact.ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            if (ModelState.IsValid)
            {
                var contact = await _db.Contact.FindAsync(id);
                if (contact == null)
                {
                    return NotFound();
                }
                return contact;
            }
            return NotFound();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> Post(Contact contact)
        {
            _db.Contact.Add(contact);
            await _db.SaveChangesAsync();
            return Ok("Record created successfully");
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Contact>> Put(int id,  Contact contact)
        {
            if (ModelState.IsValid)
            {
                var contactFromDb = await _db.Contact.FindAsync(id);
                if (contactFromDb == null)
                {
                    return NotFound("No record found against this Id..");  
                }
                contactFromDb.Name = contact.Name;
                contactFromDb.PhoneNumber = contact.PhoneNumber;
                contactFromDb.Email = contact.Email;
                await _db.SaveChangesAsync();
                return Ok("Record updated successfully");
            }
            return NotFound("Failure record not updated successfully");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> Delete(int id)
        {
            var contact = await _db.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound("No record found against this Id..");
            }
            _db.Contact.Remove(contact);
            await _db.SaveChangesAsync();
            return Ok("Record deleted successfully");
        }
    }
}
