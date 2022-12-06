using AutoMapper;
using MediaStoreEF_CF.Models.DTOs.Character;
using System.Linq;

namespace MediaStoreEF_CF.Models.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>()
                // for the movies Icollection, take the id from the movie and put it in an array
                .ForMember(chDTO => chDTO.Movies, opt => opt
                .MapFrom(ch => ch.Movies.Select(chObj => chObj.Id).ToArray()));
            CreateMap<CharacterCreateDTO, Character>();
            CreateMap<CharacterUpdateDTO, Character>();            
        }
    }
}
