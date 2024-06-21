using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class VacationType : GenericBaseEntity
{
    //One-to-many relationship with Vacation
    [JsonIgnore]
    public List<Vacation>? Vacations { get; set; }
}