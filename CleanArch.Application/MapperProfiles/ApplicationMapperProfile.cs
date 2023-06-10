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

            #endregion
        }

        #endregion
    }
}
