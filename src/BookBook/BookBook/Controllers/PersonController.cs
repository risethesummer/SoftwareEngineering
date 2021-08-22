using BookBook.Dtos.Person;
using BookBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using BookBook.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BookBook.Controllers
{

    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpGet("{id}")]
        public ActionResult GetPerson(Guid id)
        {
            Person person = personRepository.GetPerson(id);
            if (person != null)
                return new JsonResult(person.AsDto());
            return BadRequest();
        }

        [HttpGet]
        public IEnumerable<PersonDto> GetPeople()
        {
            foreach (Person person in personRepository.GetPeople())
                yield return person.AsDto();
        }

       
        [HttpPost]
        public ActionResult CreatePerson(CreatePersonDto create)
        {
            try
            {
                Person person = new()
                {
                    ID = Guid.NewGuid(),
                    Name = create.Name,
                    DayOfBirth = create.DayOfBirth,
                    Description = create.Description,
                    Nation = create.Nation,
                    ImageID = create.ImageID
                };
                personRepository.CreatePerson(person);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
