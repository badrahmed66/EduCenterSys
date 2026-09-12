

using System.Reflection;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EduCenterSys.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<StudentEnrollment> StudentEnrollments { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    public AppDbContext()
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=EduCenterSysDb;User Id=sa;Password=AhmedBadr@2026;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}