using AutoMapper;
using MediaStoreEF_CF.Data;
using MediaStoreEF_CF.Models;
using MediaStoreEF_CF.Models.DTOs.Character;
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
    [ApiConventionType(typeof(DefaultApiConventions))] // Means that we don't need to document the different responstypes
    public class CharacterController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly IMapper _mapper;

        public CharacterController(MovieDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Generating CRUD endpoints

        /// <summary>
        /// Search and return all registrated character(s). If there is no registrated character in the database,
        /// you will get the return message "Not found".
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetAllCharacters()
        {
            var domainCharacters = _mapper.Map<List<CharacterReadDTO>>(await _context.characters
                .Include(ch => ch.Movies)
                .ToListAsync());

            if (domainCharacters == null)
            {
                return NotFound("There is no character in the database of MediaStore");
            }
            return Ok(domainCharacters);
        }

        /// <summary>
        /// Search and return for a specific character by characterId. If the Id's are false,
        /// you will the return message "Not Found". 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetById(int id)
        {
            if (CharacterExists(id) == false)
            {
                return NotFound("The characterId doesn't exist in the MediaStore database");
            }

            var domainCharacter = await _context.characters
                .Include(ch => ch.Movies)
                .SingleAsync(mov => mov.Id == id);

            CharacterReadDTO characterDTO = _mapper.Map<CharacterReadDTO>(domainCharacter);

            return Ok(characterDTO);
        }

        /// <summary>
        /// Add a new character. You will get a characterId in return, but for now you will not have the possibility 
        /// to link the character to movie(s).
        /// </summary>
        /// <param name="newCharacter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CharacterReadDTO>> PostCharacter([FromBody] CharacterCreateDTO newCharacter)
        {
            var domainCharacter = _mapper.Map<Character>(newCharacter);

            _context.characters.Add(domainCharacter);
            await _context.SaveChangesAsync();

            var readCharacter = _mapper.Map<CharacterReadDTO>(domainCharacter);

            return CreatedAtAction("GetById", new { id = domainCharacter.Id }, readCharacter);
        }

        /// <summary>
        /// Delete a specific character by characterId. If OK, you will get "204 Success".
        /// If no success, you will get a specific message about wrong characterId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var character = await _context.characters.FindAsync(id);

            if (character == null)
            {
                return NotFound("The characterId you enter, does not exist. Please enter a valid characterId");
            }

            _context.characters.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Update specific information about a character based on the characterId. If OK, you will get "204 Success". 
        /// If no success, you will get a specific message about wrong characterId.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedCharacterInfo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCharacter(int id, [FromBody] CharacterUpdateDTO updatedCharacterInfo)
        {
            if (id != updatedCharacterInfo.Id)
            {
                return BadRequest("The characterId's from your input is not identical. Please check your input and try again");
            }

            if (CharacterExists(id) == false) 
            {
                return NotFound("The characterId you enter, does not exist. Please enter a valid characterId");
            }

            var domainCharacter = _mapper.Map<Character>(updatedCharacterInfo);

            _context.Entry(domainCharacter).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Update character from a specific movie
        /// <summary>
        /// Update character from movie based on movieId and characterId. If OK, you will get "204 Success". 
        /// If no success, you will get a specific message about wrong movieId or characterId.
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="characterId"></param>
        /// <param name="updateCharacterInfo"></param>
        /// <returns></returns>
        [HttpPut("movie/{movieId}")]
        public async Task<ActionResult> UpdateCharacterInMovie(int movieId,int characterId, [FromBody] CharacterUpdateDTO updateCharacterInfo)
        {

            if (MovieExists(movieId) == false)
            {
                return NotFound("The movie doesn't exist in the MediaStore database");
            }

            if (CharacterExists(characterId) == false)
            {
                return NotFound("The character doesn't exist in the MediaStore database");
            }

            if (characterId != updateCharacterInfo.Id)
            {
                return BadRequest("You have enter different characterId in the query field and in the request body");
            }

            var domainCharacter = _mapper.Map<Character>(updateCharacterInfo);

            _context.Entry(domainCharacter).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Existence check
        private bool CharacterExists(int id)
        {
            return _context.characters.Any(c => c.Id == id);
        }

        private bool MovieExists(int id)
        {
            return _context.movies.Any(m => m.Id == id);
        }
        #endregion

    }
}
