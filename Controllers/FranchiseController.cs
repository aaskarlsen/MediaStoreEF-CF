using AutoMapper;
using MediaStoreEF_CF.Data;
using MediaStoreEF_CF.Models;
using MediaStoreEF_CF.Models.DTOs.Character;
using MediaStoreEF_CF.Models.DTOs.Franchise;
using MediaStoreEF_CF.Models.DTOs.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MediaStoreEF_CF.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchiseController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;
        public FranchiseController(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Generating CRUD endpoints
        /// <summary>
        /// Search and return all registrated franchise(s). If there is no registrated franchise in the database,
        /// you will get the return message "Not found".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<FranchiseReadDTO>>> GetAllFranchises()
        {
            var domainFranchises = _mapper.Map<List<FranchiseReadDTO>>(await _context.franchises
                .Include(fran => fran.Movies)
                .ToListAsync());

            if (domainFranchises == null)
            {
                return NotFound("There is no franchise in the database of MediaStore");
            }

            return Ok(domainFranchises);
        }

        /// <summary>
        /// Search and return for a specific franchise by franchiseId. If the Id is false, 
        /// you will get the return message "Not Found".
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetById(int id)
        {
            if (FranchiseExists(id) == false)
            {
                return NotFound("The franchiseId doesn't exist in the MediaStore database");
            }

            var domainFranchise = await _context.franchises
                .Include(fran => fran.Movies)
                .SingleAsync(mov => mov.Id == id);

            FranchiseReadDTO franchiseDTO = _mapper.Map<FranchiseReadDTO>(domainFranchise);

            return Ok(franchiseDTO);
        }

        /// <summary>
        /// Add a new franchise. You will get a franchiseId in return, but for now you will not have the possibility
        /// to link the franchise to movie(s).
        /// </summary>
        /// <param name="newFranchise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FranchiseReadDTO>> PostFranchise([FromBody] FranchiseCreateDTO newFranchise)
        {
            var domainFranchise = _mapper.Map<Franchise>(newFranchise);

            _context.franchises.Add(domainFranchise);
            await _context.SaveChangesAsync();

            var readFranchise = _mapper.Map<FranchiseReadDTO>(domainFranchise);

            return CreatedAtAction("GetById", new { id = domainFranchise.Id }, readFranchise);
        }

        /// <summary>
        /// Delete a specific franchise by franchiseId. If OK, you will get return message "Success".
        /// If no success, you will get a specific message about wrong franchiseId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var franchise = await _context.franchises.FindAsync(id);

            if (franchise == null)
            {
                return NotFound("The franchiseId you enter, does not exist. Please enter a valid franchiseId");
            }

            _context.franchises.Remove(franchise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Update specific information about a franchise based on the franchiseId. If OK, you will get return message "Success". 
        /// If no success, you will get a specific message about wrong franchiseId.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedFranchiseInfo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFranchise(int id, [FromBody] FranchiseUpdateDTO updatedFranchiseInfo)
        {
            if (id != updatedFranchiseInfo.Id)
            {
                return BadRequest("The franchiseId's from your input is not identical. Please check your input and try again");
            }

            if (FranchiseExists(id) == false) 
            {
                return NotFound("The franchiseId you enter, does not exist.Please enter a valid franchiseId");
            }

            var domainFranchise = _mapper.Map<Franchise>(updatedFranchiseInfo);

            _context.Entry(domainFranchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Report - Get all movies based on the franchiseId
        /// <summary>
        /// Search and return all registrated movie(s) based on the franchiseId. 
        /// If the franchiseId doesn't exist in the database, you will get the return message "Not found".
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <returns></returns>
        [HttpGet("report/{franchiseId}/movies")]
        public async Task<ActionResult<MovieReadDTO>> GetAllMoviesByFranchiseId(int franchiseId) 
        {
            if (FranchiseExists(franchiseId) == false)
            {
                return NotFound("The franchiseId you enter, does not exist. Please enter av valid franchiseId");
            }

            var queryAllMoviesInFranchise = _mapper.Map<List<MovieReadDTO>>(await _context.franchises   
                .Where(m => m.Id == franchiseId)                        
                .SelectMany(m => m.Movies) 
                .Include(m => m.Characters)
                .ToListAsync());

            return Ok(queryAllMoviesInFranchise);
        }
        #endregion

        #region Report - Get all characters based on the franchiseId

        /// <summary>
        /// Search and return all registrated character(s) based on the franchiseId. 
        /// If the franchiseId doesn't exist in the database, you will get the return message "Not found".
        /// </summary>
        /// <param name="franchiseId"></param>
        /// <returns></returns>
        [HttpGet("report/{franchiseId}/characters")]
        public async Task<ActionResult<CharacterReadDTO>> GetAllCharactersByFranchiseId(int franchiseId)
        {
            if (FranchiseExists(franchiseId) == false)
            {
                return NotFound("The franchiseId you enter, does not exist. Please enter a valid franchiseId");
            }

            var query = _mapper.Map <List<CharacterReadDTO>> (await _context.franchises   
                .Where(m => m.Id == franchiseId)
                .SelectMany(m => m.Movies)                                  
                .SelectMany(c => c.Characters)
                .Include(m => m.Movies)
                .ToListAsync());

            // If a character participates in several movies in a franchise - the report only shows the character once.
            // The reports also informs about which movieId the character has participated in.
            var queryAllCharactersInFranchise = query
                .GroupBy(m => m.Id)
                .Select(c => c.FirstOrDefault());

            return Ok(queryAllCharactersInFranchise);
        }

        #endregion

        #region Existence check
        private bool FranchiseExists(int id)
        {
            return _context.franchises.Any(e => e.Id == id);
        }
        #endregion
        
    }
}
