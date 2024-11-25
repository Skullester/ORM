using System.Collections;

namespace ORM;

public class Node<T> : IEnumerable<Node<T>>
{
    public IReadOnlyList<Node<T>>? SubNodes => subNodes?.AsReadOnly();
    public T Element { get; }
    public bool IsSubNodesOpened { get; set; }
    public int DeepLevel { get; }
    private List<Node<T>>? subNodes;

    public Node(T element, bool isSubNodesOpened = false, int deepLevel = 0)
    {
        Element = element;
        DeepLevel = deepLevel;
        IsSubNodesOpened = isSubNodesOpened;
    }

    private Node(IEnumerable<Node<T>> elements, T element, int deepLevel, bool isSubNodesOpened = false) : this(element,
        isSubNodesOpened, deepLevel)
    {
        subNodes = elements.ToList();
    }

    public Node<T> AddNodeByValue(T value, bool isSubNodesOpened = false)
    {
        var newNode = new Node<T>(value, isSubNodesOpened, DeepLevel + 1);
        ValidateListSubnodes();
        subNodes!.Add(newNode);
        return newNode;
    }

    public Node<T> AddNode(Node<T> node)
    {
        if (node.DeepLevel <= DeepLevel)
            throw new ArgumentException("Уровень глубины добавляемого узла должен быть больше текущего");
        ValidateListSubnodes();
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

    private void ValidateListSubnodes()
    {
        subNodes ??= new List<Node<T>>();
    }

    public static Node<T> Build(IEnumerable<T> subNodes, T element, int level, bool isNodeOpened = false) =>
        new Node<T>(subNodes.Select(x => new Node<T>(x, false, level + 1)), element, level, isNodeOpened);

    public IEnumerator<Node<T>> GetEnumerator()
    {
        yield return this;
        if (subNodes is null) yield break;
        foreach (var node in subNodes)
        {
            foreach (var subNode in node)
            {
                yield return subNode;
            }
        }
    }

    public Node<T>? this[Node<T> node] => GetNode(node, x => x);

    public Node<T>? this[T nodeElement] => GetNode(nodeElement, x => x.Element);

    private Node<T>? GetNode<TValue>(TValue finding, Func<Node<T>, TValue> func)
    {
        if (ReferenceEquals(func(this), finding)) return this;
        if (subNodes is null) return null;
        foreach (var item in subNodes)
        {
            var foundNode = item.GetNode(finding, func);
            if (foundNode != null)
                return foundNode;
        }

        return null;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}