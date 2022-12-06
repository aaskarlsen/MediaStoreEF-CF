using AutoMapper;
using MediaStoreEF_CF.Models.DTOs.Movie;
using System.Linq;

namespace MediaStoreEF_CF.Models.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieReadDTO>()
                // for the characters Icollection, take the id from the character and put it in an array
                .ForMember(movDTO => movDTO.Characters, opt => opt
                .MapFrom(mov => mov.Characters.Select(movObj => movObj.Id).ToArray()));            
            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<MovieUpdateDTO, Movie>();
        }
    }
}
