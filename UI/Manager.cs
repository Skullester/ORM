using System.Text;
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
    private Graph<IElement> graph = null!;

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
        var formatter = new GraphFormatter<IElement>(graph);
        Printer = new Printer(formatter, Encoding.UTF8);
    }

    private Graph<IElement> BuildGraph()
    {
        // ReSharper disable once LocalVariableHidesMember
        var graph = new Graph<IElement>();
        var root = graph.AddNodeByValue(new Empty("Выберите:"), true);
        var groupsNode = Node<IElement>.Build(cachedGroups, new Empty("Группы:"), root.DeepLevel + 1);
        var teachersNode = Node<IElement>.Build(cachedTeachers, new Empty("Преподаватели:"), root.DeepLevel + 1);
        var studentsLookup = cachedStudents.ToLookup(x => x.GroupId, x => x as IElement);
        var studentsLookup2 = cachedDisciplines.ToLookup(x => x.SemesterId, x => x as IElement);
        foreach (var subNode in groupsNode.SubNodes!)
        {
            var studentsOfGroup = studentsLookup[subNode.Element.Id];
            var studentsNode = BuildNode(subNode, studentsOfGroup, new Empty("Студенты:"));
            foreach (var studentNode in studentsNode.SubNodes!)
            {
                var student = studentNode.Element as Student;
                var semesterId = student!.Group!.SemesterId;
                var disciplines = studentsLookup2[semesterId];
                studentNode.AddNodesByValues(disciplines);
                foreach (var disciplineNode in studentNode.SubNodes!)
                {
                    var disciplineElement = disciplineNode.Element;
                    var grade = cachedGradesStudentDisciplines.FirstOrDefault(x =>
                                    x.DisciplineId == disciplineElement.Id &&
                                    x.StudentId == student.Id) ??
                                new GradeStudentDiscipline(student.Id, disciplineElement.Id, null);
                    disciplineNode.AddNodeByValue(grade);
                }
            }
        }

        root.AddNode(groupsNode);
        root.AddNode(teachersNode);
        return graph;

        static Node<IElement> BuildNode(Node<IElement> currNode, IEnumerable<IElement> subNodes, IElement element)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            var build = Node<IElement>.Build(subNodes, element, currNode.DeepLevel + 1);
            return currNode.AddNode(build);
        }
    }

    public void Start()
    {
        var root = graph.Root!;
        var pointer = 0;
        var stack = new Stack<(Node<IElement> node, int pointer)>();
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

            ValidatePointer(ref pointer, root);
        } while (cki.Key != ConsoleKey.Q);
    }

    private void SetGrade(IElement element)
    {
        var gdd = element as GradeStudentDiscipline;
        if (gdd is null) return;
        var gradeString = GetInputGrade();
        gdd.Grade = cachedGrades.FirstOrDefault(x => x.Name == gradeString);
        if (!cachedGradesStudentDisciplines.Contains(gdd))
            context.GradesStudentDisciplines.Add(gdd);
        context.SaveChanges();
    }

    private string GetInputGrade()
    {
        string gradeString;
        bool isError;
        do
        {
            gradeString = Console.ReadLine()!;
            isError = !int.TryParse(gradeString, out var grade) || grade < 2 || grade > 5;
            if (isError) Printer.PrintError("Неверная оценка!");
        } while (isError);

        return gradeString;
    }

    private void ValidatePointer(ref int pointer, Node<IElement> node)
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