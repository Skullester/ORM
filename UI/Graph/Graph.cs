using System.Collections;

namespace UI;

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

    public Graph(T nodeElement)
    {
        Root = new Node<T>(nodeElement, 0, false);
    }

    public Node<T> AddNodeByValue(T value, bool isSubNodesOpened = false)
    {
        if (Root is null)
            return Root = new Node<T>(value, 0, isSubNodesOpened);
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

    public static Graph<T> Build(T element, IEnumerable<T> subNodesElements, bool isNodeOpened = false) =>
        Build(element, subNodesElements.Select(el => new Node<T>(el, 1, false)), isNodeOpened);

    public static Graph<T> Build(T element, IEnumerable<Node<T>> subNodes, bool isNodeOpened = false) =>
        new Graph<T>(new Node<T>(element, 0, subNodes, isNodeOpened));

    public static Graph<T> Build(T element, bool isNodeOpened = false) =>
        new Graph<T>(new Node<T>(element, 0, isNodeOpened));

    public Node<T>? this[Node<T> node] => Root?[node];
    public Node<T>? this[T element] => Root?[element];

    public IEnumerator<Node<T>> GetEnumerator() =>
        Root is null ? Enumerable.Empty<Node<T>>().GetEnumerator() : Root.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}