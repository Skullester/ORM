using System.Collections;

// ReSharper disable UseCollectionExpression

namespace UI;

public class Node<T> : IEnumerable<Node<T>>
{
    public IReadOnlyList<Node<T>>? SubNodes => subNodes?.AsReadOnly();
    public T Element { get; }
    public bool IsSubNodesOpened { get; set; }
    public int DeepLevel { get; }
    private List<Node<T>>? subNodes;

    public Node(T element, int deepLevel, bool isSubNodesOpened = false)
    {
        Element = element;
        DeepLevel = deepLevel;
        IsSubNodesOpened = isSubNodesOpened;
    }

    internal Node(T element, int deepLevel, IEnumerable<Node<T>> subNodes, bool isSubNodesOpened = false) : this(
        element,
        deepLevel, isSubNodesOpened)
    {
        AddNodes(subNodes);
    }

    public Node<T> AddNodeByValue(T value, bool isSubNodesOpened = false)
    {
        var newNode = new Node<T>(value, DeepLevel + 1, isSubNodesOpened);
        ValidateSubNodes();
        subNodes!.Add(newNode);
        return newNode;
    }

    public Node<T> AddNode(Node<T> node)
    {
        if (node.DeepLevel <= DeepLevel)
            throw new ArgumentException("Уровень глубины добавляемого узла должен быть больше текущего");
        ValidateSubNodes();
        subNodes!.Add(node);
        return node;
    }

    public void AddNodes(IEnumerable<Node<T>> nodes)
    {
        foreach (var subNode in nodes)
        {
            AddNode(subNode);
        }
    }

    public void AddNodesByValues(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            AddNodeByValue(value);
        }
    }

    private void ValidateSubNodes()
    {
        subNodes ??= new List<Node<T>>();
    }

    public static Node<T> Build(T element, IEnumerable<T> subNodes, int level, bool isNodeOpened = false) =>
        Build(element, subNodes.Select(x => new Node<T>(x, level + 1, false)), level, isNodeOpened);

    public static Node<T> Build(T element, IEnumerable<Node<T>> subNodes, int level, bool isNodeOpened = false) =>
        new Node<T>(element, level, subNodes, isNodeOpened);

    public IEnumerator<Node<T>> GetEnumerator()
    {
        yield return this;
        foreach (var subNode in subNodes?.SelectMany(n => n) ?? Enumerable.Empty<Node<T>>())
        {
            yield return subNode;
        }
    }

    public Node<T>? this[Node<T> node] => GetNode(node, x => x);

    public Node<T>? this[T nodeElement] => GetNode(nodeElement, x => x.Element);

    private Node<T>? GetNode<TValue>(TValue finding, Func<Node<T>, TValue> func)
    {
        if (ReferenceEquals(func(this), finding)) return this;
        return subNodes?.Select(item => item.GetNode(finding, func)).OfType<Node<T>>().FirstOrDefault();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}