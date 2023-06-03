using CleanArch.Infrastructure.IRepositories.People;
using CleanArch.Infrastructure.Repositories.Common;

namespace CleanArch.Infrastructure.Repositories.People
{
    public class PeopleRepository :  BaseRepository<Person>, IPeopleRepository
    {
        #region Cotr

        public PeopleRepository(ApplicationDbContext context)
            : base(context)
        { }

        #endregion
    }
}
