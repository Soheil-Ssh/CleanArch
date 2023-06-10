using CleanArch.Infrastructure.IRepositories.People;

namespace CleanArch.Infrastructure.IRepositories.Common
{
    public interface IUOW : IAsyncDisposable
    {
        IPeopleRepository PeopleRepository { get; }

        Task SaveAsync();
    }
}
