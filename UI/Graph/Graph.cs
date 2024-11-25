using System.Collections;

namespace ORM;

public class Graph<T> : IEnumerable<Node<T>>
{
    public Node<T>? Root { get; private set; }

    public Graph()
    {
    }

    public Graph(Node<T> root)
    {
        Root = root;
    }

    public Node<T> AddNodeByValue(T value, bool isSubNodesOpened = false)
    {
        if (Root is null)
            return Root = new Node<T>(value, isSubNodesOpened);
        return Root.AddNodeByValue(value);
    }

    public Node<T> AddNode(Node<T> node)
    {
        if (Root is null)
            return Root = node;
        return Root.AddNode(node);
    }

    public void AddNodes(IEnumerable<Node<T>> nodes)
    {
        foreach (var subNode in nodes)
        {
            AddNode(subNode);
        }
    }

    public void AddNodesByValue(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            AddNodeByValue(value);
        }
    }

    public Node<T>? this[Node<T> node] => Root?[node];
    public Node<T>? this[T element] => Root?[element];

    public IEnumerator<Node<T>> GetEnumerator() =>
        Root is null ? Enumerable.Empty<Node<T>>().GetEnumerator() : Root.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}