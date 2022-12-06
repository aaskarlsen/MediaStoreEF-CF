using AutoMapper;
using MediaStoreEF_CF.Models.DTOs.Franchise;
using System.Linq;

namespace MediaStoreEF_CF.Models.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>()
                // Takes the id from the movie and put it in an array
                .ForMember(franDTO => franDTO.Movies, opt => opt
                .MapFrom(fran => fran.Movies.Select(franObj => franObj.Id).ToArray()));
            CreateMap<FranchiseCreateDTO, Franchise>();
            CreateMap<FranchiseUpdateDTO, Franchise>();
        }
    }
}
