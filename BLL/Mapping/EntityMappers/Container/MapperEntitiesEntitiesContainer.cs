using BLL.DTO;
using BLL.Extensions;
using BLL.Mapping.Entities;
using DAL.Entities;

namespace BLL.Mapping;

public class MapperEntitiesEntitiesContainer : IMapperEntitiesContainer
{
    private readonly IMapper mapper;
    private IEntityMapper<Grade, GradeDTO> gradeMapper;
    private IEntityMapper<Group, GroupDTO> groupMapper;
    private IEntityMapper<Teacher, TeacherDTO> teacherMapper;
    private IEntityMapper<Student, StudentDTO> studentMapper;
    private IEntityMapper<Discipline, DisciplineDTO> disciplineMapper;
    private IEntityMapper<Semester, SemesterDTO> semesterMapper;
    private IEntityMapper<Post, PostDTO> postMapper;
    private IEntityMapper<GradeStudentDiscipline, GradeStudentDisciplineDTO> gsdMapper;
    private IEntityMapper<GradeStudentDisciplineDTO, GradeStudentDiscipline> gsdDtoMapper;
    private IEntityMapper<GradeDTO, Grade> gradeDTOMapper;
    private readonly Type type;

    public MapperEntitiesEntitiesContainer(IMapper mapper)
    {
        this.mapper = mapper;
        type = GetType();
        gradeMapper = new GradeEntityMapper();
        groupMapper = new GroupEntityMapper(mapper);
        teacherMapper = new TeacherEntityMapper(mapper);
        studentMapper = new StudentEntityMapper(mapper);
        disciplineMapper = new DisciplineEntityMapper();
        semesterMapper = new SemesterEntityMapper();
        gsdMapper = new GradeStudentDisciplineEntityMapper(mapper);
        postMapper = new PostEntityMapper();
        gsdDtoMapper = new GsdDTOEntityMapper(mapper);
        gradeDTOMapper = new GradeDTOEntityMapper();
    }

    public TEntity Get<TEntity>() where TEntity : IEntityMapper =>
        type.Get<TEntity>(this);
}