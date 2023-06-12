using CleanArch.Application.DTOs.People;
using CleanArch.Application.IServices;
using CleanArch.Core.Entities.Common.Enums;
using CleanArch.Core.Entities.Person;

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

        #region Create person

        public async Task<Result<Guid>> CreatePersonAsync(CreatePersonDTO createPersonDto)
        {
            var result = await _uow.PeopleRepository
                .AddAsync(_mapper.Map<Person>(createPersonDto));

            if (result.Succeeded)
                await _uow.SaveAsync();

            return await Task.FromResult(result);
        }

        #endregion

        #region Get person by id

        public async Task<Result<EditPersonDTO>> GetPersonByIdAsync(Guid id)
            => await Task.FromResult(await _uow.PeopleRepository.GetAsync(expression: p => p.Id == id,
                selector: p => _mapper.Map<EditPersonDTO>(p)));

        #endregion

        #region Edit person

        public async Task<Result<Guid>> EditPersonAsync(EditPersonDTO editPersonDto)
        {
            var result = _uow.PeopleRepository
                .Update(_mapper.Map<Person>(editPersonDto));

            if (result.Succeeded)
                await _uow.SaveAsync();

            return await Task.FromResult(result);
        }

        #endregion

        #region Get person details by id

        public async Task<Result<DetailsPersonDTO>> GetPersonDetailsByIdAsync(Guid id)
            => await Task.FromResult(await _uow.PeopleRepository.GetAsync(expression: p => p.Id == id,
                selector: p => _mapper.Map<DetailsPersonDTO>(p)));

        #endregion

        #region Delete person

        public async Task<Result> DeletePersonByIdAsync(Guid id)
        {
            var result = new Result();

            var getResult = await _uow.PeopleRepository.GetAsync(c => c.Id == id);
            if (!getResult.Succeeded)
            {
                result.SetType(getResult.Type, getResult.Message);
                return await Task.FromResult(result);
            }

            if (getResult.Data == null)
            {
                result.SetType(ResultType.NotFoundError, $"Person not found by id = {id}");
                return await Task.FromResult(result);
            }

            result = _uow.PeopleRepository.Delete(getResult.Data);
            if (result.Succeeded)
                await _uow.SaveAsync();

            return await Task.FromResult(result);
        }

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            await _uow.DisposeAsync();
        }

        #endregion
    }
}
