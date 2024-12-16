namespace DAL.Entities;

public class Student : Person
{
    public int GroupId { get; set; }
    public Group? Group { get; set; }

    public Student(string name, int groupId) : base(name)
    {
        GroupId = groupId;
    }
}