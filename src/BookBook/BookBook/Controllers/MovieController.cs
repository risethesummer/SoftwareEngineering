using Microsoft.AspNetCore.Mvc;
using BookBook.Dtos;
using BookBook.Repositories;
using BookBook.Manager;
using System.Collections;
using BookBook.Dtos.Movies;
using System.Collections.Generic;
using System;
using BookBook.Models;
using BookBook.Dtos.Person;
using BookBook.Dtos.Theater;
using Microsoft.AspNetCore.Http;

namespace BookBook.Controllers
{
    [ApiController]
    [Route("api/movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;
        private readonly IMovieStaffRepository movieStaffRepository;

        public MovieController(IMovieRepository movieRepository, IMovieStaffRepository movieStaffRepository)
        {
            this.movieStaffRepository = movieStaffRepository;
            this.movieRepository = movieRepository;
        }

        [HttpPost]
        public ActionResult CreateMovie(CreateMovieDto create)
        {
            try
            { 
                Movie movie = new()
                {
                    ID = Guid.NewGuid(),
                    Name = create.Name,
                    Genre = create.Genre,
                    Duration = create.Duration,
                    Description = create.Description,
                    RequiredAge = create.RequiredAge,
                    IMDBStar = create.IMDBStar,
                    Nation = create.Nation,
                    Year = create.Year,
                    ImageID = create.ImageID,
                    YoutubeLink = create.YoutubeLink
                };
                movieRepository.CreateMovie(movie);

                foreach (Guid actor in create.Actors)
                {
                    MovieStaff movieStaff = new()
                    {
                        MovieID = movie.ID,
                        PersonID = actor,
                        Role = "Actor"
                    };
                    movieStaffRepository.AddMovieStaff(movieStaff);
                }

                MovieStaff directorStaff = new()
                {
                    MovieID = movie.ID,
                    PersonID = create.DirectorID,
                    Role = "Director"
                };
                movieStaffRepository.AddMovieStaff(directorStaff);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
            foreach (Movie movie in movieRepository.GetMovies())
                yield return GetMovieDto(movie);
        }

        [HttpGet("top/{amount}")]
        public IEnumerable<MovieDto> GetHotMovies(int amount)
        {
            foreach (Movie movie in movieRepository.GetTopMovies(amount))
                yield return GetMovieDto(movie);
        }

        private MovieDto GetMovieDto(Movie movie)
        {
            if (movie != null)
            {
                List<CompactPersonDto> actors = new();
                foreach (var staff in movieStaffRepository.GetStaffOfMovie(movie.ID, "Actor"))
                {
                    actors.Add(staff.AsCompactDto());
                }

                CompactPersonDto director = movieStaffRepository.GetAStaffOfMovie(movie.ID, "Director").AsCompactDto();
                return movie.AsDto(director, actors);
            }
            return null;
        }

        [HttpGet("id/{id}")]
        public ActionResult GetMovie(Guid id)
        {
            Movie movie = movieRepository.GetMovie(id);
            if (movie != null)
                return new JsonResult(GetMovieDto(movie));
            return BadRequest(new JsonResponse { State = "Fail" });
        }

        [HttpGet("name/{name}")]
        public IEnumerable<MovieDto> GetMovieByName(string name)
        {
            foreach (var movie in movieRepository.GetMovieByName(name))
                yield return GetMovieDto(movie);
        }
    }
}
