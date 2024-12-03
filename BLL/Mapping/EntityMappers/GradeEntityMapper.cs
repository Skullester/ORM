using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

internal class GradeEntityMapper : IEntityMapper<Grade, GradeDTO>
{
    public GradeDTO Map(Grade from) => new GradeDTO(from.Name) { Id = from.Id };
}