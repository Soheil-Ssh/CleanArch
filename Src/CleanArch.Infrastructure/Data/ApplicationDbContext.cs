using CleanArch.Core.Entities.ToDo;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ToDo> ToDos { get; set; }
}