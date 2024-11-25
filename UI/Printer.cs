using System.Text;
using ConsoleEx;
using UI;
using static ConsoleEx.ConsoleHelper;

// ReSharper disable PossibleMultipleEnumeration

namespace ORM;

public class Printer : IPrinter
{
    public Node<INaming>? ActiveNode { get; set; }

    private const ConsoleColor activeNodeColor = ConsoleColor.White;

    private const ConsoleColor welcomeColor = ConsoleColor.Gray;

    private readonly IGraphFormatter<INaming> graphFormatter;


    private readonly Dictionary<Type, ConsoleColor> colors = new Dictionary<Type, ConsoleColor>
    {
        [typeof(Group)] = ConsoleColor.Yellow,
        [typeof(Student)] = ConsoleColor.Magenta,
        [typeof(Teacher)] = ConsoleColor.Blue,
        [typeof(Empty)] = ConsoleColor.Cyan,
        [typeof(Discipline)] = ConsoleColor.Green
    };


    public Printer(IGraphFormatter<INaming> graphFormatter)
    {
        Console.OutputEncoding = Encoding.UTF8;
        this.graphFormatter = graphFormatter;
    }

    public void Print()
    {
        Console.Clear();
        var formattedGraph = graphFormatter.Format();
        var (welcomeStr, _) = formattedGraph.First();
        PrintWithColor(welcomeStr, welcomeColor);
        foreach (var (str, node) in formattedGraph.Skip(1))
        {
            var color = ReferenceEquals(ActiveNode, node) ? activeNodeColor : GetColorOf(node.Element);
            PrintWithColor(str, color);
        }
    }

    public void PrintError(string message) => ConsoleHelper.PrintError(message);

    private ConsoleColor GetColorOf<T>() => GetColorOf(typeof(T));

    private ConsoleColor GetColorOf(Type type)
    {
        return colors.TryGetValue(type, out var color)
            ? color
            : throw new ArgumentException($"No color for {nameof(type)} type");
    }

    private ConsoleColor GetColorOf<T>(T element) => GetColorOf(element!.GetType());
}