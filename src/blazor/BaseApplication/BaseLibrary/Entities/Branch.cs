using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class Branch : GenericBaseEntity
{
    //Relationship: Many To One with Department
    public Department? Department { get; set; }
    public int? DepartmentId { get; set; }
    //Relationship: One To Many with Employee
    public List<Employee>? Employees { get; set; }
}

