using ORM;

namespace UI;

public class GraphFormatter<T> : IGraphFormatter<T>
{
    public Graph<T> Graph { get; }

    public GraphFormatter(Graph<T> graph)
    {
        Graph = graph;
    }

    public IEnumerable<(string, Node<T>)> Format()
    {
        var root = Graph.Root!;
        return root?.IsSubNodesOpened ?? false ? FormatNode(root) : Enumerable.Empty<(string, Node<T>)>();
    }

    private IEnumerable<(string, Node<T>)> FormatNode(Node<T> node, int prevLevelLength = 0)
    {
        var elementName = node.Element!.ToString()!;
        var element = elementName.PadLeft(prevLevelLength + elementName.Length);
        prevLevelLength = element.Length;
        yield return (element + Environment.NewLine, node);
        var subNodes = node.SubNodes ?? Enumerable.Empty<Node<T>>();
        if (!node.IsSubNodesOpened) yield break;
        foreach (var item in subNodes)
        {
            foreach (var str in FormatNode(item, prevLevelLength))
            {
                yield return str;
            }
        }
    }
}