using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.StudentApp.Models;

namespace SchoolPortal.StudentApp.Data.Configuration;

public class StudentConfigration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.FirstName)
               .IsRequired();

        builder.Property(s => s.LastName)
               .IsRequired();



        builder.HasIndex(s => s.Email)
               .IsUnique();

        builder.Property(s => s.DateOfBirth);

        builder.Property(s => s.EnrollmentDate);
        builder.HasQueryFilter(u => !u.IsDeleted);
    }

  
}
