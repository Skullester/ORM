﻿namespace DAL.Entities;

public class Person : IElement
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}