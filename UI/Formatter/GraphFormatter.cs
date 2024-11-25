using ORM;

namespace UI;

public class GraphFormatter<T> : IGraphFormatter<T>
{
    public Graph<T> Graph { get; }

    public GraphFormatter(Graph<T> graph)
    {
        Graph = graph;
        CheckGraphCapacity();
    }

    private void CheckGraphCapacity()
    {
        if (!Graph.Any())
        {
            throw new Exception("Graph is empty");
        }
    }

    public IEnumerable<(string, Node<T>)> Format()
    {
        var graphRoot = Graph.Root!;
        return graphRoot.IsSubNodesOpened ? FormatNode(graphRoot) : Enumerable.Empty<(string, Node<T>)>();
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