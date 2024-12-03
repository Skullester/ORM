﻿using System.Text;
using BLL.DTO;
using UI.Infrastructure;
using static UI.Infrastructure.ConsoleHelper;

// ReSharper disable PossibleMultipleEnumeration

namespace UI;

public class Printer : IPrinter
{
    public Encoding Encoding { get; }
    public IReadOnlyDictionary<Type, ConsoleColor> Colors => colors.AsReadOnly();
    private const ConsoleColor activeNodeColor = ConsoleColor.White;
    private const ConsoleColor welcomeColor = ConsoleColor.Gray;
    private readonly IGraphFormatter<IElementDTO> graphFormatter;

    private readonly Dictionary<Type, ConsoleColor> colors = new Dictionary<Type, ConsoleColor>
    {
        [typeof(GroupDTO)] = ConsoleColor.Yellow,
        [typeof(StudentDTO)] = ConsoleColor.Magenta,
        [typeof(TeacherDTO)] = ConsoleColor.Blue,
        [typeof(Empty)] = ConsoleColor.Cyan,
        [typeof(DisciplineDTO)] = ConsoleColor.Green
    };

    public Printer(IGraphFormatter<IElementDTO> graphFormatter, Encoding encoding)
    {
        Encoding = encoding;
        this.graphFormatter = graphFormatter;
        SetEncoding();
    }

    private void SetEncoding()
    {
        Console.OutputEncoding = Encoding;
    }

    public void Print(Node<IElementDTO>? activeNode)
    {
        Clean();
        var formattedGraph = graphFormatter.Format();
        PrintWelcomeNode(formattedGraph.First().Item1);
        foreach (var (str, node) in formattedGraph.Skip(1))
        {
            var color = ReferenceEquals(activeNode, node) ? activeNodeColor : GetColorOf(node.Element);
            PrintWithColor(str, color);
        }
    }

    private static void PrintWelcomeNode(string welcomeText) => PrintWithColor(welcomeText, welcomeColor);

    public void PrintError(string message) => ConsoleHelper.PrintError(message);

    private ConsoleColor GetColorOf(Type type)
    {
        return colors.TryGetValue(type, out var color)
            ? color
            : throw new ArgumentException($"No color for {nameof(type)} type");
    }

    private ConsoleColor GetColorOf<T>(T element) => GetColorOf(element!.GetType());
    private ConsoleColor GetColorOf<T>() => GetColorOf(typeof(T));
}