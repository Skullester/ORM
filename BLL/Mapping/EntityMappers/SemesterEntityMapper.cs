using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

public class SemesterEntityMapper : IEntityMapper<Semester, SemesterDTO>
{
    public SemesterDTO Map(Semester from) => new SemesterDTO(from.Name) { Id = from.Id };
}