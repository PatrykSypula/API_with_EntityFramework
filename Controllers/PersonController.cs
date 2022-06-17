using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private static List<Person> people = new List<Person>
            {
                new Person {
                    Id = 1,
                    FirstName ="Patryk",
                    LastName ="Sypuła",
                    Place="Łódź" },
                new Person {
                    Id = 2,
                    FirstName ="Patrick",
                    LastName ="Sypula",
                    Place="Lodz" }
            };

        [HttpGet]//Show all people
        public async Task<ActionResult<List<Person>>> Get()
        {
            return Ok(people);
        }

        [HttpGet("{id}")]//Show particular person by id
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = people.Find(p => p.Id == id);
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
            people.Add(person);
            return Ok(people);
        }

        [HttpPut]//Update person
        public async Task<ActionResult<List<Person>>> UpdatePerson(Person request)
        {
            var person = people.Find(p => p.Id == request.Id);
            if (person == null)
            {
                return BadRequest("Person not found.");
            }
            else
            {
                person.FirstName = request.FirstName;
                person.LastName = request.LastName;
                person.Place = request.Place;
            }
            return Ok(people);
        }

        [HttpDelete("{id}")]//Delete particular person by id
        public async Task<ActionResult<List<Person>>> Delete(int id)
        {
            var person = people.Find(p => p.Id == id);
            if (person == null)
            {
                return BadRequest("Person not found.");
            }
            else
            {
                people.Remove(person);
                return Ok(people);
            }
        }
    }
}
