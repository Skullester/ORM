﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ORM;

public class Context : DbContext
{
    public Context()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }

    public DbSet<Semester> Semesters { get; set; }


    // public DbSet<Grade> Grades { get; set; }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<GradeStudentDiscipline> GradeStudentDiscipline { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Database = ORM3");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new SemesterConfiguration());
    }
}