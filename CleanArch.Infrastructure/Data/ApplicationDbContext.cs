using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

        #region Model creating

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            Expression<Func<BaseEntity, bool>> filterExpr = be => !be.IsDelete;
            foreach (var mutableEntityType in builder.Model.GetEntityTypes())
            {
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(BaseEntity)))
                {
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }
        }

        #endregion
    }
}
