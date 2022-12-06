using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediaStoreEF_CF.Data;
using MediaStoreEF_CF.Models;
using AutoMapper;
using MediaStoreEF_CF.Models.DTOs.Movie;
using System.Net.Mime;
using MediaStoreEF_CF.Models.DTOs.Character;

namespace MediaStoreEF_CF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public MovieController(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Generating CRUD endpoints
        /// <summary>
        /// Search and return all registrated movie(s). If there is no registrated movie 
        /// in the database, you will get the return message "Not found".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetAllMovies()
        {
            var domainMovies = _mapper.Map<List<MovieReadDTO>>(await _context.movies
                .Include(mov => mov.Characters)
                .ToListAsync());

            if (domainMovies == null)
            {
                return NotFound("There is no movies in the database of MediaStore");
            }

            return Ok(domainMovies);
        }

        /// <summary>
        /// Search and return for a specific movie by movieId. If the movieId is false,
        /// you will the return message "Not Found". 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetById(int id)
        {
            if (MovieExists(id) == false)
            {
                return NotFound("The movieId doesn't exist in the MediaStore database");
            }

            var domainMovie = await _context.movies
                .Include(mov => mov.Characters)
                .SingleAsync(ch => ch.Id == id);

            MovieReadDTO movieDTO = _mapper.Map<MovieReadDTO>(domainMovie);

            return Ok(movieDTO);
        }

        /// <summary>
        /// Add a new movie. You will get a movieId in return. For now you will not have the possibility
        /// to tie the movie to character(s) and franchise.
        /// </summary>
        /// <param name="newMovie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MovieReadDTO>> PostMovie([FromBody] MovieCreateDTO newMovie)
        {
            var domainMovie = _mapper.Map<Movie>(newMovie);

            _context.movies.Add(domainMovie);
            await _context.SaveChangesAsync();

            var readMovie = _mapper.Map<MovieReadDTO>(domainMovie);

            return CreatedAtAction("GetById", new { id = domainMovie.Id }, readMovie);

        }

        /// <summary>
        /// Delete a specific movie by movieId. If OK, no returned message. If not, you will get a message about wrong movieId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var movie = await _context.movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound("The movieId you enter, does not exist. Please enter a valid movieId");
            }

            _context.movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Update specific information about a movie based on the movieId. If OK, no returned message. If not, 
        /// you will get a message about wrong movieId.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedMovieInfo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDTO updatedMovieInfo)
        {
            if (id != updatedMovieInfo.Id)
            {
                return BadRequest("The movieId's from your input is not identical. Please check your input and try again");
            }

            if (MovieExists(id) == false)
            {
                return NotFound("The movieId you enter, does not exist. Please enter a valid movieId");
            }

            var domainMovie = _mapper.Map<Movie>(updatedMovieInfo);

            _context.Entry(domainMovie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Update movie from a specific franchise
        /// <summary>
        /// Update movie from franchise in based on franchiseId and movieId. If OK, you will get return message "Success".  
        /// If no success, you will get a specific message about wrong franchiseId or movieId.
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <param name="movieId"></param>
        /// <param name="updateMovieInfo"></param>
        /// <returns></returns>
        [HttpPut("franchise/{franchiseId}")]
        public async Task<ActionResult> UpdateMovieInFranchise(int franchiseId, int movieId, [FromBody] MovieUpdateDTO updateMovieInfo)
        {

            if (FranchiseExists(franchiseId) == false)
            {
                return NotFound("The franchise doesn't exist in the MediaStore database");
            }

            if (movieId != updateMovieInfo.Id)
            {
                return BadRequest("You have enter different MovieId in the query field and in the request body");
            }

            if (MovieExists(movieId) == false)
            {
                return NotFound("The movie doesn't exist in the MediaStore database");
            }

            var domainMovie = _mapper.Map<Movie>(updateMovieInfo);

            _context.Entry(domainMovie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Report - Get all characters based on the moviesId
        /// <summary>
        /// Search and return all registrated character(s) based on the movieId. 
        /// If the movieId doesn't exist in the database, you will get the return message "Not found". 
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpGet("report/{movieId}/characters")]

        public async Task<ActionResult<CharacterReadDTO>> GetAllCharacterByMovieId(int movieId)
        {
            if (MovieExists(movieId) == false)
            {
                return NotFound("The movieId you enter, does not exist. Please enter av valid MovieId");
            }

            var queryAllCharactersInMovie = _mapper.Map<List<CharacterReadDTO>> (await _context.movies   
                .Where(m => m.Id == movieId)                        
                .SelectMany(m => m.Characters)
                .Include(m => m.Movies)
                .ToListAsync());

            return Ok(queryAllCharactersInMovie);
        }

        #endregion

        #region Existence checks
        private bool MovieExists(int id)
        {
            return _context.movies.Any(e => e.Id == id);
        }
        private bool FranchiseExists(int id)
        {
            return _context.franchises.Any(e => e.Id == id);
        }
        #endregion

    }
}
