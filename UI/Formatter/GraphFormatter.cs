using System.Text;
using ORM;

namespace UI;

public class GraphFormatter<T> : IGraphFormatter<T>
{
    public Graph<T> Graph { get; }

    public GraphFormatter(Graph<T> graph)
    {
        this.Graph = graph;
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
        return graphRoot.IsOpened ? FormatNode(graphRoot) : Enumerable.Empty<(string, Node<T>)>();
    }

    private IEnumerable<(string, Node<T>)> FormatNode(Node<T> node, int prevLevelLength = 0)
    {
        var elementName = node.Element!.ToString()!;
        var element = elementName.PadLeft(prevLevelLength + elementName.Length);
        prevLevelLength = element.Length;
        yield return (element + Environment.NewLine, node);
        var nodes = node.Nodes?.Where(x => x.IsOpened) ?? Enumerable.Empty<Node<T>>();
        foreach (var item in nodes)
        {
            foreach (var str in FormatNode(item, prevLevelLength))
            {
                yield return str;
            }
        }
    }
}