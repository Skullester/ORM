namespace ORM;

public class Student : Person
{
    public int GroupId { get; set; }

    public Student(string name, int groupId) : base(name)
    {
        GroupId = groupId;
    }
}