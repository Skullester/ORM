using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapping.Entities;

internal class DisciplineEntityMapper : IEntityMapper<Discipline, DisciplineDTO>
{
    public DisciplineDTO Map(Discipline from) => new DisciplineDTO(from.Name)
    {
        Id = from.Id,
    };
}