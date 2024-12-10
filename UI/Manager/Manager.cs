using System.Text;
using BLL;
using BLL.DTO;
using BLL.Cache;
using BLL.Distributors;
using BLL.Services.Container;

// ReSharper disable SwitchStatementMissingSomeEnumCasesNoDefault
// ReSharper disable LocalVariableHidesMember
namespace UI;

public class Manager : IManager
{
    public IPrinter Printer { get; }
    private readonly Graph<IElementDTO> graph;
    private readonly IServiceContainer serviceContainer;
    private readonly ICacher cacher;
    private readonly IDistributor distributor;
    private readonly ICloser closer;

    public Manager(IDistributor distributor, IServiceContainer serviceContainer, ICacher cacher, ICloser closer)
    {
        this.distributor = distributor;
        this.serviceContainer = serviceContainer;
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
            var studentsOfGroup = serviceContainer.StudentService.GetStudentsByGroup(subNode.Element.Id);
            var build = Node<IElementDTO>.Build(new Empty("Студенты:"), studentsOfGroup, subNode.DeepLevel + 1);
            var studentsNode = subNode.AddNode(build);
            foreach (var studentNode in studentsNode.SubNodes!)
            {
                var student = studentNode.Element as StudentDTO;
                var semesterId = student!.Group!.SemesterId;
                var disciplines = serviceContainer.DisciplineService.GetDisciplinesBySemester(semesterId);
                studentNode.AddNodesByValues(disciplines);
                foreach (var disciplineNode in studentNode.SubNodes!)
                {
                    var disciplineElement = disciplineNode.Element;
                    var gradeInfoDto =
                        serviceContainer.GradeService.GetGradeInfo(student.Id,
                            disciplineElement.Id) ??
                        new GradeInfoDTO(student.Id, disciplineElement.Id, null);
                    disciplineNode.AddNodeByValue(gradeInfoDto);
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
        if (element is not GradeInfoDTO gradeInfo) return;
        var gradeName = GetInputGrade();
        serviceContainer.GradeService.SetGradeTo(gradeInfo, gradeName);
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

    private void SetPointer(ref int pointer, IReadOnlyCollection<Node<IElementDTO>>? subNodes)
    {
        if (subNodes is not null)
            pointer = pointer == subNodes.Count ? 0 : pointer < 0 ? pointer = subNodes.Count - 1 : pointer;
    }
}