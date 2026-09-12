
using EduCenterSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCenterSys.Infrastructure.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(s => s.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(s => s.Address)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(s => s.GuardianPhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(s => new { s.Name, s.GradeId })
                .IsUnique();

        // 1 -> M [Grade]
        builder.HasOne(s => s.Grade)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GradeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
    }
}