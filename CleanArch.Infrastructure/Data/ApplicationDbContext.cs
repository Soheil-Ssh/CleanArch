using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Ctor

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { }

        #endregion

        #region DbSets

        public DbSet<Person> People { get; set; }

        #endregion
    }
}
