using CleanArch.Application.DTOs.People;
using CleanArch.Application.IServices;

namespace CleanArch.Application.Services
{
    public class PeopleService : IPeopleService
    {
        #region Ctor

        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public PeopleService(IUOW uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        #endregion

        #region Get all people

        public async Task<Result<IEnumerable<PersonDTO>>> GetAllPeopleAsync()
            => await Task.FromResult(await _uow.PeopleRepository.GetAllAsync(selector: p => _mapper.Map<PersonDTO>(p)));

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            await _uow.DisposeAsync();
        }

        #endregion
    }
}
