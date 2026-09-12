
using EduCenterSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduCenterSys.Infrastructure.Data.Configurations;

public class GroupScheduleConfiguration : IEntityTypeConfiguration<GroupSchedule>
{
    public void Configure(EntityTypeBuilder<GroupSchedule> builder)
    {
        builder.Property(g => g.Day)
                .HasConversion<string>()
                .HasMaxLength(10)
                .IsRequired();

        builder.HasOne(g => g.Group)
                .WithMany(g => g.Schedules)
                .HasForeignKey(g => g.GroupScheduleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


    }
}