using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

internal class GradeDTOEntityMapper : IEntityMapper<GradeDTO, Grade>
{
    public Grade Map(GradeDTO from) => new Grade(from.Name) { Id = from.Id };
}