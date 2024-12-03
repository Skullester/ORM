using System.Text;
using BLL.DTO;

namespace UI;

public interface IPrinter
{
    IReadOnlyDictionary<Type, ConsoleColor> Colors { get; }
    public Encoding Encoding { get; }
    void Print(Node<IElementDTO> activeNode);
    void PrintError(string message);
}