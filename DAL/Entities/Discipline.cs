﻿// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace DAL.Entities;

public class Discipline
{
    public int Id { get; set; }

    public string Name { get; set; }
    public int SemesterId { get; set; }

    public Semester? Semester { get; set; }

    public Discipline(string name, int semesterId)
    {
        Name = name;
        SemesterId = semesterId;
    }

    public override string ToString() => Name;
}