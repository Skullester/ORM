using Infrastructure;
using UI;
using static ConsoleEx.ConsoleHelper;

namespace ORM;

public class Manager : IManager
{
    private readonly Context context = new Context();
    private IList<Group> cachedGroups = null!;
    private IList<Student> cachedStudents = null!;
    private IList<Discipline> cachedDisciplines = null!;
    private IList<Teacher> cachedTeachers = null!;
    private IList<Post> cachedPosts = null!;
    private IList<Semester> cachedSemesters = null!;

    // private IList<Grade> cachedGrades = null!;
    public IPrinter? Printer { get; private set; }

    public Manager()
    {
        Initialize();
    }

    private void Initialize()
    {
        cachedStudents = context.Students.ToList();
        cachedGroups = context.Groups.ToList();
        cachedDisciplines = context.Disciplines.ToList();
        cachedSemesters = context.Semesters.ToList();
        cachedTeachers = context.Teachers.ToList();
        cachedPosts = context.Posts.ToList();
        // cachedGrades = context.Grades.ToList();
        var graph = BuildGraph();
        var formatter = new GraphFormatter<INaming>(graph);
        Printer = new Printer(formatter);
    }

    private Graph<INaming> BuildGraph()
    {
        var graph = new Graph<INaming>();
        var root = graph.AddNodeByValue(new Empty("Выберите:"), true);
        var groupsNode = Node<INaming>.Build(cachedGroups, new Empty("Группы:"), root.DeepLevel + 1, true);
        var teachersNode = Node<INaming>.Build(cachedTeachers, new Empty("Преподаватели:"), root.DeepLevel + 1, true);
        var lookup = cachedStudents.ToLookup(x => x.GroupId, x => x as INaming);
        foreach (var subNode in groupsNode.Nodes!)
        {
            var studentsOfGroup = lookup[(subNode.Element as Group)!.Id];
            BuildNode(subNode, studentsOfGroup, new Empty("Студенты:"));
        }

        root.AddNode(groupsNode);
        root.AddNode(teachersNode);
        graph.Select(x => x.IsOpened = true).ToList();
        return graph;

        static void BuildNode(Node<INaming> node, IEnumerable<INaming> lookup, INaming naming)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            var build = Node<INaming>.Build(lookup, naming, node.DeepLevel + 1);
            node.AddNode(build);
        }
    }

    public void Start()
    {
        Printer.Print();
        // printer.PrintOffers();
        /*printer.PrintOffer("Выберите группу: ");
        printer.PrintGroups(cachedGroups);
        printer.SetInputColor();
        var group = GetGroupByInput();
        // ResetColor();
        printer.PrintGroupInfo(group);
    */
    }

    private Group GetGroupByInput() => GetByInput<Group>("Неверно указанная группа!");

    private T GetByInput<T>(string errorMsg) where T : INaming
    {
        while (true)
        {
            var input = Console.ReadLine()!;
            var list = GetType().GetIListWithOf<T>(this);
            var candidates = list.Where(x => x.Name.StartsWith(input, StringComparison.Ordinal)).ToArray();
            var isError = string.IsNullOrEmpty(input) || candidates.Length != 1;
            if (isError)
                PrintError(errorMsg);
            else return candidates[0];
        }
    }

    void IDisposable.Dispose() => context.Dispose();
}