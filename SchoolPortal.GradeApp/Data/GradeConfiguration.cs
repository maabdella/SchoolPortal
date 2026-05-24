using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolPortal.GradeApp.Models;

namespace SchoolPortal.GradeApp.Data;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.Property(g => g.CourseName)
        .IsRequired();

        builder.Property(g => g.CourseName)
           .IsRequired();

        builder.Property(g => g.Score)
               .HasColumnType("decimal(18,2)");

        builder.Property(g => g.Notes);

    }
}
