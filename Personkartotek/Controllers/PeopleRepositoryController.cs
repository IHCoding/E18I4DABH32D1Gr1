using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personkartotek.DAL;
using Personkartotek.Models;
using Personkartotek.Persistence;


namespace Personkartotek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleRepositoryController : ControllerBase
    {
        private readonly IUnitOfWork _uoWork;

        public PeopleRepositoryController(IUnitOfWork uoWork)
        {
            _uoWork = uoWork;
        }

        // GET: api/PeopleRepository
        [HttpGet]
        public IEnumerable<Person> GetPersons()
        {
            return _uoWork._People.GetAll();
        }

        // GET: api/PeopleRepository/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = _uoWork._People.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        //GET: api/PeopleRepository/
        [HttpGet("AtAdr/{id}")]
        public IActionResult GetPersonsResidingAtAddress([FromRoute] int AddressId)
        {
            var ResidingPersons = _uoWork._People.GetAllPersonsById(AddressId);

            return Ok(ResidingPersons);
        }
        
        // PUT: api/PeopleRepository/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.PersonId)
            {
                return BadRequest();
            }

            if (!PersonExists(id))
            {
                return NotFound();
            }

            _uoWork._People.Put(person);
            
            return NoContent();
        }

        // POST: api/PeopleRepository
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uoWork._People.Add(person);
            _uoWork.Complete();

            return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
        }

        // DELETE: api/PeopleRepository/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var person =  _uoWork._People.Get(id);
            if (person == null) {
                return NotFound();
            }

            _uoWork._People.Remove(person);
            _uoWork.Complete();

            return Ok(person);
        }

        private bool PersonExists(int id)
        {
            return _uoWork.Exist(id);
        }
    }
}