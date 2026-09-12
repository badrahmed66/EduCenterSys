
using EduCenterSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCenterSys.Infrastructure.Data.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

        builder.Property(t => t.Name)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

        builder.Property(t => t.Address)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

        builder.Property(t => t.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
    }
}