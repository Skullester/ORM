namespace ORM;

public interface IManager : IDisposable
{
    IPrinter? Printer { get; }
    void Start();
}