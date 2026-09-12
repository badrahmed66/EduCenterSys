
using EduCenterSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCenterSys.Infrastructure.Data.Configurations;

public class StudentEnrollmentConfiguration : IEntityTypeConfiguration<StudentEnrollment>
{
    public void Configure(EntityTypeBuilder<StudentEnrollment> builder)
    {
        builder.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(10)
                .IsRequired();

        builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Group)
                .WithMany(g => g.Enrollments)
                .HasForeignKey(e => e.GroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        
        //builder.HasQueryFilter(s => !s.IsDeleted)
    }
}