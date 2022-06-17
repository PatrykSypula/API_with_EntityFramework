using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PersonController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]//Show all people
        public async Task<ActionResult<List<Person>>> Get()
        {
            return Ok(await _dataContext.People_.ToListAsync());
        }

        [HttpGet("{id}")]//Show particular person by id
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await _dataContext.People_.FindAsync(id);
            if (person == null)
            {
                return BadRequest("Person not found.");
            }
            else
            {
                return Ok(person);
            }
        }

        [HttpPost]//Add person
        public async Task<ActionResult<List<Person>>> AddPerson(Person person)
        {
            _dataContext.People_.Add(person);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.People_.ToListAsync());
        }

        [HttpPut]//Update person
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person request)
        {
            var dbPerson = await _dataContext.People_.FindAsync(request.Id);
            if (dbPerson == null)
            {
                return BadRequest("Person not found.");
            }
            else
            {
                dbPerson.FirstName = request.FirstName;
                dbPerson.LastName = request.LastName;
                dbPerson.Place = request.Place;

                await _dataContext.SaveChangesAsync();

                return Ok(await _dataContext.People_.ToListAsync());
            }
            
        }

        [HttpDelete("{id}")]//Delete particular person by id
        public async Task<ActionResult<List<Person>>> Delete(int id)
        {
            var dbPerson = await _dataContext.People_.FindAsync(id);
            if (dbPerson == null)
            {
                return BadRequest("Person not found.");
            }
            else
            {
                _dataContext.People_.Remove(dbPerson);

                await _dataContext.SaveChangesAsync();

                return Ok(await _dataContext.People_.ToListAsync());
            }
        }
    }
}
