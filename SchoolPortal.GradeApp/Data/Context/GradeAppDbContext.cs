using Microsoft.EntityFrameworkCore;
using SchoolPortal.GradeApp.Models;

namespace SchoolPortal.GradeApp.Data.Context;

public class GradeAppDbContext : DbContext
{
    public GradeAppDbContext(DbContextOptions<GradeAppDbContext> options)
   : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GradeAppDbContext).Assembly);
    }

    public DbSet<Grade> Grades { get; set; }
}
