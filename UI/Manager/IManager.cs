namespace UI;

public interface IManager
{
    IPrinter? Printer { get; }
    void Start();
}