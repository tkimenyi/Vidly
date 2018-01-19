using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        //Get /api/Movies
        public IHttpActionResult GetMovies()
        {
            return Ok(_context.Movies.Include(m => m.Genre).ToList());
        }

        // GET /api/Movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        //POST /api/Movies
        [HttpPost]
        public IHttpActionResult CreateMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movie);
        }

        // PUT /api/Movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
            {
                return NotFound();
            }

            // we can use a tool like automapper to do this mapping for us
            movieInDb.Name = movie.Name;
            movieInDb.NumberInStock = movie.NumberInStock;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            movieInDb.DateAdded = movie.DateAdded;
            movieInDb.Genre.Name = movie.Genre.Name;
            _context.SaveChanges();

            return Ok(movieInDb);
        }

        // DELETE /api/Movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieInDb);
            return Ok(_context.SaveChanges());
        }
    }
}
