namespace BLL.Distributors;

public interface IDistributor
{
    IEnumerable<T> Get<T>();
}