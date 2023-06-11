using CleanArch.Application.DTOs.People;

namespace CleanArch.Application.IServices
{
    public interface IPeopleService : IAsyncDisposable
    {
        Task<Result<IEnumerable<PersonDTO>>> GetAllPeopleAsync();

        Task<Result<Guid>> CreatePersonAsync(CreatePersonDTO createPersonDto);

        Task<Result<EditPersonDTO>> GetPersonByIdAsync(Guid id);

        Task<Result<Guid>> EditPersonAsync(EditPersonDTO editPersonDto);

        Task<Result<DetailsPersonDTO>> GetPersonDetailsByIdAsync(Guid id);

    }
}
