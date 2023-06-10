using CleanArch.Application.DTOs.People;

namespace CleanArch.Application.IServices
{
    public interface IPeopleService : IAsyncDisposable
    {
        Task<Result<IEnumerable<PersonDTO>>> GetAllPeopleAsync();

        Task<Result<Guid>> CreatePersonAsync(CreatePersonDTO createPersonDto);
    }
}
