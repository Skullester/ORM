using System.Text;

namespace ORM;

public interface IPrinter
{
    IReadOnlyDictionary<Type, ConsoleColor> Colors { get; }
    public Encoding Encoding { get; }

    void Print(Node<IElement> activeNode);
    void PrintError(string message);
}