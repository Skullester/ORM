// ReSharper disable PossibleMultipleEnumeration

namespace UI.Infrastructure;

public static class ConsoleHelper
{
    public static class СolorContainer
    {
        private static ConsoleColor color;

        public static void Save()
        {
            color = Console.ForegroundColor;
        }

        public static void Set()
        {
            SetColor(color);
        }
    }

    public static class CursorPositionContainer
    {
        private static int left;
        private static int top;

        public static void Save()
        {
            left = Console.CursorLeft;
            top = Console.CursorTop;
        }

        public static (int, int) Get() => (left, top);

        public static void Set()
        {
            Console.SetCursorPosition(left, top);
        }
    }

    public static void Clean()
    {
        Console.Clear();
    }

    public static void SetColor(ConsoleColor color) => Console.ForegroundColor = color;

    public static void PrintLineWithColor(string text, ConsoleColor newColor, bool saveOldColor = true)
    {
        PrintWithColor(text + Environment.NewLine, newColor, saveOldColor);
    }

    public static void PrintWithColor<T>(T text, ConsoleColor newColor, bool saveOldColor = true)
    {
        var color = Console.ForegroundColor;
        SetColor(newColor);
        Print(text);
        if (saveOldColor)
            SetColor(color);
    }

    public static void PrintLine<T>(T text)
    {
        Print(text + Environment.NewLine);
    }

    public static void Print<T>(T text)
    {
        Console.Write(text);
    }

    public static void PrintError(string text) => PrintLineWithColor(text, ConsoleColor.Red);
}