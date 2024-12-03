using DAL.ORM;

namespace BLL;

public interface ICloser : IDisposable
{
    IUnitWork Database { get; }
}