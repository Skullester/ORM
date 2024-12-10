// ReSharper disable UseCollectionExpression

using BLL.DTO;

namespace UI;

public class GraphFormatter<T> : IGraphFormatter<T> where T : IElementDTO
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
        var elementName = node.Element.Name ?? "null";
        var element = elementName.PadLeft(prevLevelLength + elementName.Length);
        prevLevelLength = element.Length;
        yield return (element + Environment.NewLine, node);
        if (!node.IsSubNodesOpened) yield break;
        var subNodes = node.SubNodes ?? Enumerable.Empty<Node<T>>();
        foreach (var item in subNodes.SelectMany(x => FormatNode(x, prevLevelLength)))
        {
            yield return item;
        }
    }
}