using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class GeneralDepartment : GenericBaseEntity
{
    //Relationship: One To Many with Department
    [JsonIgnore]
    public List<Department>? Departments { get; set; }
}