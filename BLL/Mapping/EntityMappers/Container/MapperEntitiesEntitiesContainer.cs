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
    private IEntityMapper<GradeInfo, GradeInfoDTO> gradeInfoMapper;
    private IEntityMapper<GradeInfoDTO, GradeInfo> gradeInfoDTOMapper;
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
        gradeInfoMapper = new GradeInfoMapper(mapper);
        postMapper = new PostEntityMapper();
        gradeInfoDTOMapper = new GradeInfoDTOEntityMapper(mapper);
        gradeDTOMapper = new GradeDTOEntityMapper();
    }

    public TEntity Get<TEntity>() where TEntity : IEntityMapper =>
        type.Get<TEntity>(this);
}