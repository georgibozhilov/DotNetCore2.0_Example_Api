namespace DNC.Example.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc
        ;
    using DNC.Example.DAL.Context;
    using DNC.Example.DAL.Models;
    using DNC.Example.DAL.DTOs;

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieListDbContext _context;

        public MoviesController(MovieListDbContext context)
        {
            this._context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            var listOfmovies = _context.Movies.ToList();

            return listOfmovies;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id)
        {
            var foundMovie = _context.Movies.ToList().FirstOrDefault(x => x.Id == id);

            return foundMovie;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] MovieDTO value)
        {            
            if (ModelState.IsValid)
            {
                //remove: data:image/png;base64,
                var onlyContent = value.ImageAsBase64.Substring(22);
                byte[] image = Convert.FromBase64String(onlyContent);

                Movie newMovie = new Movie
                {
                    Title = value.Title,
                    Year = value.Year,
                    ScoreRating = value.ScoreRating,
                    Image = image
                };

                _context.Add(newMovie);
                _context.SaveChanges();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] MovieDTO value)
        {
            if (ModelState.IsValid)
            {
                //remove: data:image/png;base64,
                var onlyContent = value.ImageAsBase64.Substring(22);
                byte[] image = Convert.FromBase64String(onlyContent);

                var foundMovie = _context.Movies.ToList().FirstOrDefault(x => x.Id == id);

                if (foundMovie != null)
                {
                    foundMovie.Title = value.Title;
                    foundMovie.Year = value.Year;
                    foundMovie.ScoreRating = value.ScoreRating;
                    foundMovie.Image = image;

                    _context.Movies.Update(foundMovie);
                    _context.SaveChanges();
                }
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var foundMovie = _context.Movies.ToList().FirstOrDefault(x => x.Id == id);
            if (foundMovie != null)
            {
                _context.Movies.Remove(foundMovie);
                _context.SaveChanges();
            }

        }
    }
}
