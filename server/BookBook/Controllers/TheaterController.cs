using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Repositories;
using BookBook.Dtos.Theater;
using BookBook.Models;

namespace BookBook.Controllers
{
    [ApiController]
    [Route("api/theater")]
    public class TheaterController : ControllerBase
    {
        private readonly ITheaterRepository theaterRepository;

        public TheaterController(ITheaterRepository theaterRepository)
        {
            this.theaterRepository = theaterRepository;
        }

        [HttpGet]
        public IEnumerable<TheaterDto> GetTheaters()
        {
            foreach (var theater in theaterRepository.GetTheaters())
            {
                yield return theater.AsDto();
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetTheater(Guid id)
        {
            var find = theaterRepository.GetTheater(id);
            if (find != null)
                return new JsonResult(find.AsDto());
            return BadRequest();
        }

        [HttpPost]
        public ActionResult CreateTheater(CreateTheaterDto create)
        {
            try
            {
                Theater theater = new()
                {
                    ID = Guid.NewGuid(),
                    Name = create.Name,
                    Location = create.Location,
                };
                theaterRepository.AddTheater(theater);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
