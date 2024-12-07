using System.Text;
using BLL;
using BLL.DTO;
using BLL.Cache;
using BLL.Distributors;
using BLL.Providers.Container;

// ReSharper disable SwitchStatementMissingSomeEnumCasesNoDefault
// ReSharper disable LocalVariableHidesMember
namespace UI;

public class Manager : IManager
{
    private readonly Graph<IElementDTO> graph;
    public IPrinter Printer { get; }
    private readonly IProviderContainer providerContainer;
    private readonly ICacher cacher;
    private readonly IDistributor distributor;
    private readonly ICloser closer;

    public Manager(IDistributor distributor, IProviderContainer providerContainer, ICacher cacher, ICloser closer)
    {
        this.distributor = distributor;
        this.providerContainer = providerContainer;
        this.cacher = cacher;
        this.closer = closer;
        graph = BuildGraph();
        var formatter = new GraphFormatter<IElementDTO>(graph);
        Printer = new Printer(formatter, Encoding.UTF8);
    }

    public void Start()
    {
        var root = graph.Root!;
        var pointer = 0;
        var stack = new Stack<(Node<IElementDTO> node, int pointer)>();
        ConsoleKeyInfo cki;
        do
        {
            var activeNode = root.SubNodes![pointer];
            Printer.Print(activeNode);
            cki = Console.ReadKey(true);
            switch (cki.Key)
            {
                case ConsoleKey.DownArrow:
                    pointer++;
                    break;
                case ConsoleKey.UpArrow:
                    pointer--;
                    break;
                case ConsoleKey.RightArrow when activeNode.SubNodes != null:
                    stack.Push((root, pointer));
                    activeNode.IsSubNodesOpened = true;
                    root = activeNode;
                    pointer = 0;
                    break;
                case ConsoleKey.LeftArrow when stack.Count != 0:
                    root.IsSubNodesOpened = false;
                    var (node, parentPointer) = stack.Pop();
                    root = node;
                    pointer = parentPointer;
                    break;
                case ConsoleKey.Enter:
                    SetGrade(activeNode.Element);
                    break;
            }

            SetPointer(ref pointer, root.SubNodes);
        } while (cki.Key != ConsoleKey.Q);

        Close();
    }

    private Graph<IElementDTO> BuildGraph()
    {
        var graph = Graph<IElementDTO>.Build(new Empty("Выберите:"), true);
        var root = graph.Root!;
        cacher.CacheAllEntities();
        var groupsNode = Node<IElementDTO>.Build(new Empty("Группы:"), distributor.Get<GroupDTO>(), root.DeepLevel + 1);
        var teachersNode = Node<IElementDTO>.Build(new Empty("Преподаватели:"), distributor.Get<TeacherDTO>(),
            root.DeepLevel + 1);
        foreach (var subNode in groupsNode.SubNodes!)
        {
            var studentsOfGroup = providerContainer.StudentProvider.GetStudentsByGroup(subNode.Element.Id);
            var build = Node<IElementDTO>.Build(new Empty("Студенты:"), studentsOfGroup, subNode.DeepLevel + 1);
            var studentsNode = subNode.AddNode(build);
            foreach (var studentNode in studentsNode.SubNodes!)
            {
                var student = studentNode.Element as StudentDTO;
                var semesterId = student!.Group!.SemesterId;
                var disciplines = providerContainer.DisciplineProvider.GetDisciplinesBySemester(semesterId);
                studentNode.AddNodesByValues(disciplines);
                foreach (var disciplineNode in studentNode.SubNodes!)
                {
                    var disciplineElement = disciplineNode.Element;
                    var gsd =
                        providerContainer.GradeStudentDisciplineProvider.GetByStudentAndDiscipline(student.Id,
                            disciplineElement.Id) ??
                        new GradeStudentDisciplineDTO(student.Id, disciplineElement.Id, null);
                    disciplineNode.AddNodeByValue(gsd);
                }
            }
        }

        root.AddNode(groupsNode);
        root.AddNode(teachersNode);
        return graph;
    }

    private void Close() => closer.Dispose();

    private void SetGrade(IElementDTO element)
    {
        if (element is not GradeStudentDisciplineDTO gdd) return;
        var gradeName = GetInputGrade();
        providerContainer.GradeProvider.SetGradeTo(gdd, gradeName);
    }

    private string GetInputGrade()
    {
        string gradeText;
        bool isError;
        do
        {
            gradeText = Console.ReadLine()!;
            isError = !int.TryParse(gradeText, out var grade) || grade < 2 || grade > 5;
            if (isError) Printer.PrintError("Неверная оценка!");
        } while (isError);

        return gradeText;
    }

    private void SetPointer(ref int pointer, IReadOnlyList<Node<IElementDTO>>? subNodes)
    {
        if (subNodes is null) return;
        pointer = pointer == subNodes.Count ? 0 : pointer < 0 ? pointer = subNodes.Count - 1 : pointer;
    }
}