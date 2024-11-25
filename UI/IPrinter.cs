namespace ORM;

public interface IPrinter
{
    void Print();
    Node<INaming>? ActiveNode { get; set; }
    void PrintError(string message);
}