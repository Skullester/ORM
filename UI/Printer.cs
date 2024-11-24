using System.Text;
using UI;
using static ConsoleEx.ConsoleHelper;

namespace ORM;

public class Printer : IPrinter
{
    private const ConsoleColor inputColor = ConsoleColor.DarkCyan;
    private const ConsoleColor defaultColor = ConsoleColor.White;
    private IGraphFormatter<INaming> graphFormatter;

    private readonly Dictionary<Type, ConsoleColor> colors = new Dictionary<Type, ConsoleColor>
    {
        [typeof(Group)] = ConsoleColor.Yellow,
        [typeof(Student)] = ConsoleColor.Magenta,
        [typeof(Semester)] = ConsoleColor.Green,
        [typeof(Teacher)] = ConsoleColor.Blue,
        [typeof(Empty)] = ConsoleColor.Cyan,
    };

    public Printer(IGraphFormatter<INaming> graphFormatter)
    {
        this.graphFormatter = graphFormatter;
        Console.OutputEncoding = Encoding.UTF8;
    }

    public void PrintGroups(IEnumerable<Group> groups)
    {
        var consoleColor = GetColorOf<Group>();
        foreach (var group in groups)
        {
            PrintLineWithColor('\u25ba' + group.Name, consoleColor);
        }
    }

    public void Print()
    {
        var formattedGraph = graphFormatter.Format();
        foreach (var (str, node) in formattedGraph)
        {
            var strColor = GetColorOf(node.Element);
            PrintWithColor(str, strColor);
        }
    }

    private ConsoleColor GetColorOf<T>()
    {
        return colors.TryGetValue(typeof(T), out var color)
            ? color
            : throw new ArgumentException($"No color for {nameof(T)} type");
    }

    private ConsoleColor GetColorOf<T>(T element)
    {
        return colors.TryGetValue(element.GetType(), out var color)
            ? color
            : throw new ArgumentException($"No color for {typeof(T)} type");
    }

    /*
    public void PrintGroupInfo(Group group)
    {
        PrintLineWithColor($"Группа {group.Name}:", GetColorOf<Group>());
        tab += 1;
        PrintLineWithColor("Студенты:", GetColorOf<Student>());
        // СolorContainer.Save();
        PrintStudents(group.Students);
        PrintLineWithColor("Семестр: ", GetColorOf<Semester>());
        foreach (var discipline in group.Semester!.Disciplines)
        {
            PrintId(discipline.Id);
            var consoleColor = GetColorOf<Discipline>();
            var info = discipline.Name;
            PrintLineWithColor(info, consoleColor);
        }
    }
    */


    /*
    private void PrintStudents(IEnumerable<Student> students)
    {
        foreach (var student in students)
        {
            PrintId(student.Id);
            var consoleColor = GetColorOf<Student>();
            var info = student.Name;
            PrintLineWithColor(info, consoleColor);
        }
    }
*/
}