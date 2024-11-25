using UI;

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
    private IList<Grade> cachedGrades = null!;
    private IList<GradeStudentDiscipline> cachedGradesStudentDisciplines = null!;
    private Graph<INaming> graph = null!;

    public IPrinter Printer { get; private set; } = null!;

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
        cachedGrades = context.Grades.ToList();
        cachedGradesStudentDisciplines = context.GradesStudentDisciplines.ToList();
        graph = BuildGraph();
        var formatter = new GraphFormatter<INaming>(graph);
        Printer = new Printer(formatter);
    }

    private Graph<INaming> BuildGraph()
    {
        var graph = new Graph<INaming>();
        var root = graph.AddNodeByValue(new Empty("Выберите:"), true);
        var groupsNode = Node<INaming>.Build(cachedGroups, new Empty("Группы:"), root.DeepLevel + 1);
        var teachersNode = Node<INaming>.Build(cachedTeachers, new Empty("Преподаватели:"), root.DeepLevel + 1);
        var studentsLookup = cachedStudents.ToLookup(x => x.GroupId, x => x as INaming);
        var studentsLookup2 = cachedDisciplines.ToLookup(x => x.SemesterId, x => x as INaming);
        foreach (var subNode in groupsNode.SubNodes!)
        {
            var studentsOfGroup = studentsLookup[(subNode.Element as Group)!.Id];
            var studentsNode = BuildNode(subNode, studentsOfGroup, new Empty("Студенты:"));
            foreach (var studentNode in studentsNode.SubNodes!)
            {
                var student = studentNode.Element as Student;
                var semesterId = student!.Group!.SemesterId;
                var disciplines = studentsLookup2[semesterId];
                studentNode.AddNodesByValues(disciplines);
                foreach (var disciplineNode in studentNode.SubNodes!)
                {
                    var discipline = disciplineNode.Element as Discipline;
                    var grade = cachedGradesStudentDisciplines.FirstOrDefault(x =>
                        x.DisciplineId == discipline!.Id &&
                        x.StudentId == student.Id) ?? new GradeStudentDiscipline(student.Id, discipline.Id, null);
                    disciplineNode.AddNodeByValue(grade);
                }
            }
        }

        root.AddNode(groupsNode);
        root.AddNode(teachersNode);
        return graph;

        static Node<INaming> BuildNode(Node<INaming> currNode, IEnumerable<INaming> subNodes, INaming element)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            var build = Node<INaming>.Build(subNodes, element, currNode.DeepLevel + 1);
            return currNode.AddNode(build);
        }
    }

    public void Start()
    {
        var root = graph.Root!;
        var pointer = 0;
        var stack = new Stack<(Node<INaming> node, int pointer)>();
        ConsoleKeyInfo cki;
        do
        {
            var activeNode = root.SubNodes![pointer];
            Printer.ActiveNode = activeNode;
            Printer.Print();
            cki = Console.ReadKey(true);
            switch (cki.Key)
            {
                case ConsoleKey.DownArrow:
                    pointer++;
                    break;
                case ConsoleKey.UpArrow:
                    pointer--;
                    break;
                case ConsoleKey.RightArrow:
                    if (activeNode.SubNodes != null)
                    {
                        stack.Push((root, pointer));
                        activeNode.IsSubNodesOpened = true;
                        root = activeNode;
                        pointer = 0;
                    }

                    break;
                case ConsoleKey.LeftArrow:
                    if (stack.Count != 0)
                    {
                        root.IsSubNodesOpened = false;
                        var (node, p) = stack.Pop();
                        root = node;
                        pointer = p;
                    }

                    break;
                case ConsoleKey.Enter:
                    if (activeNode.Element is GradeStudentDiscipline gdd)
                    {
                        SetGrade(gdd);
                    }

                    break;
            }

            ValidatePointer(ref pointer, root);
        } while (cki.Key != ConsoleKey.Q);
    }

    private void SetGrade(GradeStudentDiscipline gdd)
    {
        int grade;
        bool isError;
        do
        {
            isError = !int.TryParse(Console.ReadLine()!, out grade) || grade < 2 || grade > 5;
            if (isError) Printer.PrintError("Неверная оценка!");
        } while (isError);

        gdd.Grade = context.Grades.FirstOrDefault(x => x.Name == grade.ToString());
        if (!context.GradesStudentDisciplines.Contains(gdd))
            context.GradesStudentDisciplines.Add(gdd);
        context.SaveChanges();
    }

    private void ValidatePointer(ref int pointer, Node<INaming> node)
    {
        var subNodes = node.SubNodes;
        if (subNodes is null) return;
        if (pointer == subNodes.Count)
            pointer = 0;
        else if (pointer < 0)
            pointer = subNodes.Count - 1;
    }

    void IDisposable.Dispose() => context.Dispose();
}