namespace ORM;

public class Program
{
    private static void Main(string[] args)
    {
        using var manager = GetManager();
        manager.Start();
    }

    private static IManager GetManager() => new Manager();
}