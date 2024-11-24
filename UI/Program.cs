using UI;

namespace ORM;

public class Program
{
    private static void Main(string[] args)
    {
        /*var testGraph = new Graph<INaming>();
        var root = testGraph.AddNodeByValue(new Empty("Выберите категорию:"), true);
        var groupNode = root.AddNodeByValue(new Empty("Группы:"), true);
        var teachers = root.AddNodeByValue(new Empty("Преподаватели:"), true);
        var b21 = groupNode.AddNodeByValue(new Group("Б21-021-1:", 1), true);
        groupNode.AddNodeByValue(new Group("Б22-021-1:", 1), true);
        var b23 = groupNode.AddNodeByValue(new Group("Б23-021-1:", 1), true);
        var students2 = b23.AddNodeByValue(new Empty("Студенты:"), true);
        students2.AddNodeByValue(new Student("Михаил", 2), true);
        students2.AddNodeByValue(new Student("Ой бой", 2), true);
        students2.AddNodeByValue(new Student("Шип", 2), true);
        var students = b21.AddNodeByValue(new Empty("Студенты:"), true);
        students.AddNodeByValue(new Student("Алексей", 1), true);
        students.AddNodeByValue(new Student("Антон", 1), true);
        students.AddNodeByValue(new Student("Сергей", 1), true);
        var graphFormatter = new GraphFormatter<INaming>(testGraph);
        var format = graphFormatter.Format();
        Console.Write(format);*/
        GetManager().Start();
    }


    private static IManager GetManager() => new Manager();
}