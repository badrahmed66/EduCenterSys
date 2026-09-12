
using EduCenterSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCenterSys.Infrastructure.Data.Configurations;

public class TeacherQualificationConfiguration : IEntityTypeConfiguration<TeacherQualification>
{
    public void Configure(EntityTypeBuilder<TeacherQualification> builder)
    {
        builder.HasOne(t => t.Teacher)
                .WithMany(teach => teach.Qualifications)
                .HasForeignKey(t => t.TeacherQualificationId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        builder.HasOne(t => t.Grade)
                .WithMany(g => g.TeacherQualifications)
                .HasForeignKey(t => t.TeacherQualificationId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        builder.HasOne(t => t.Subject)
                .WithMany(s => s.TeacherQualifications)
                .HasForeignKey(t => t.TeacherQualificationId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
    }
}