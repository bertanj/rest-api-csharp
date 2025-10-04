using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Data.Dto.V1;
using RestWithASPNET.Services; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")] //api/person/v1
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonDto>>> FindAllAsync()
        {
            var people = await _personService.FindAllAsync();
            return Ok(people); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> FindByIdAsync(long id)
        {
            var person = await _personService.FindByIdAsync(id);
            if (person == null)
            {
                return NotFound(); 
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreateAsync([FromBody] PersonDto personDto)
        {
            if (personDto == null) return BadRequest();
            var createdPerson = await _personService.CreateAsync(personDto);
            return CreatedAtAction(nameof(FindByIdAsync), new { id = createdPerson.Id }, createdPerson);
        }

        [HttpPut]
        public async Task<ActionResult<PersonDto>> UpdateAsync([FromBody] PersonDto personDto)
        {
            if (personDto == null) return BadRequest();
            var updatedPerson = await _personService.UpdateAsync(personDto);
            if (updatedPerson == null) return NotFound();
            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var book = await _personService.FindByIdAsync(id);
            if (book == null) return NotFound();
            await _personService.DeleteAsync(id);
            return NoContent();
        }
    }
}