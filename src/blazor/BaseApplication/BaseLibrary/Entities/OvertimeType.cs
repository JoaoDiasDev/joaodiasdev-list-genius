using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class OvertimeType : GenericBaseEntity
{
    //One-to-many relationship with Overtime
    [JsonIgnore]
    public List<Overtime>? Overtimes { get; set; }
}