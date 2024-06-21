using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class Town : GenericBaseEntity
{
    //Relationship: Many To One with City
    public int CityId { get; set; }
    public City? City { get; set; }
    //Relationship: One To Many with Employee
    [JsonIgnore]
    public List<Employee>? Employees { get; set; }
}