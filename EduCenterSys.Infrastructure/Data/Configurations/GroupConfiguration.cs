using EduCenterSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCenterSys.Infrastructure.Data.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(g => g.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

        // 1 -> M [Grade]
        builder.HasOne(g => g.Grade)
                .WithMany(gr => gr.Groups)
                .HasForeignKey(g => g.GradeId)
                .IsRequired();

        // 1 -> M [Subject]
        builder.HasOne(g => g.Subject)
                .WithMany(sub => sub.Groups)
                .HasForeignKey(g => g.SubjectId)
                .IsRequired();
        
        // 1 -> M [Teacher]
        builder.HasOne(g => g.Teacher)
                .WithMany(t => t.Groups)
                .HasForeignKey(t => t.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}