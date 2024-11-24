using System.Collections;
using System.Text;

namespace ORM;

public class Node<T> : IEnumerable<Node<T>>
{
    public IReadOnlySet<Node<T>>? Nodes => nodes;
    public T Element { get; }
    private HashSet<Node<T>>? nodes;
    public bool IsOpened { get; set; }
    public int DeepLevel { get; }

    public Node(T element, bool isOpened = false, int deepLevel = 0)
    {
        Element = element;
        DeepLevel = deepLevel;
        IsOpened = isOpened;
    }

    private Node(IEnumerable<Node<T>> elements, T element, int deepLevel, bool isOpened = false) : this(element,
        isOpened, deepLevel)
    {
        nodes = elements.ToHashSet();
    }

    public Node<T> AddNodeByValue(T value, bool isNodeOpened = false)
    {
        var newNode = new Node<T>(value, isNodeOpened, DeepLevel + 1);
        nodes ??= new HashSet<Node<T>>();
        nodes.Add(newNode);
        return newNode;
    }

    public Node<T> AddNode(Node<T> node)
    {
        if (node.DeepLevel <= DeepLevel)
            throw new ArgumentException("Уровень глубины добавляемого узла должен быть больше текущего");
        nodes ??= new HashSet<Node<T>>();
        nodes.Add(node);
        return node;
    }

    public static Node<T> Build(IEnumerable<T> subNodes, T element, int level, bool isNodeOpened = false) =>
        new Node<T>(subNodes.Select(x => new Node<T>(x, false, level + 1)), element, level, isNodeOpened);

    public IEnumerator<Node<T>> GetEnumerator()
    {
        yield return this;
        if (nodes is null) yield break;
        foreach (var node in nodes)
        {
            foreach (var subNode in node)
            {
                yield return subNode;
            }
        }

        // return nodes?.Prepend(this).GetEnumerator() ?? Enumerable.Empty<Node>().GetEnumerator();
    }

    public Node<T>? this[Node<T> node] => GetNode(node, x => x);

    public Node<T>? this[T nodeElement] => GetNode(nodeElement, x => x.Element);

    private Node<T>? GetNode<TValue>(TValue finding, Func<Node<T>, TValue> func)
    {
        if (ReferenceEquals(func(this), finding)) return this;
        if (nodes is null) return null;
        foreach (var item in nodes)
        {
            var foundNode = item.GetNode(finding, func);
            if (foundNode != null)
                return foundNode;
        }

        return null;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}