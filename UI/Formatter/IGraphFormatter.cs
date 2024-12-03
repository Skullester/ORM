namespace UI;

public interface IGraphFormatter<T>
{
    Graph<T> Graph { get; }
    IEnumerable<(string, Node<T>)> Format();
}