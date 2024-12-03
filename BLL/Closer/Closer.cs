using DAL.ORM;

namespace BLL;

public class Closer : ICloser
{
    public IUnitWork Database { get; }

    public Closer(IUnitWork database)
    {
        Database = database;
    }

    void IDisposable.Dispose() => Database.Dispose();
}