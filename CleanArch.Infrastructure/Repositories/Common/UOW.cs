
namespace CleanArch.Infrastructure.Repositories.Common
{
    public class UOW : IUOW
    {
        #region Ctor

        private readonly ApplicationDbContext _context;

        public UOW(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Save

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        #endregion
    }
}
