using CleanArch.Application.DTOs.People;
using CleanArch.Core.Entities.Person;

namespace CleanArch.Application.MapperProfiles
{
    public class ApplicationMapperProfile : Profile
    {
        #region Ctor

        public ApplicationMapperProfile()
        {
            #region People

            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Person, CreatePersonDTO>().ReverseMap();
            CreateMap<Person, EditPersonDTO>().ReverseMap();
            CreateMap<Person, DetailsPersonDTO>().ReverseMap();

            #endregion
        }

        #endregion
    }
}
