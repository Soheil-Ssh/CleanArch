namespace CleanArch.Infrastructure.IRepositories.Common
{
    public interface IUOW : IAsyncDisposable
    {
        Task SaveAsync();
    }
}
