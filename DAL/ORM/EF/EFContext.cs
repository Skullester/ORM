using DAL.Entities;
using DAL.ORM.Configurations;
using Microsoft.EntityFrameworkCore;

// ReSharper disable VirtualMemberCallInConstructor

namespace DAL.ORM;

public class EFContext : DbContext
{
    public EFContext()
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<GradeStudentDiscipline> GradesStudentDisciplines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Database = ORM3");
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new SemesterConfiguration());
        modelBuilder.ApplyConfiguration(new GradeConfiguration());
    }
}