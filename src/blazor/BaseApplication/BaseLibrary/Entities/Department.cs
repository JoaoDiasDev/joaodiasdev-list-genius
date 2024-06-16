using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;
public class Department : GenericBaseEntity
{
    //Relationship: Many To One with GeneralDepartment
    public GeneralDepartment? GeneralDepartment { get; set; }
    public int? GeneralDepartmentId { get; set; }
    //Relationship: One To Many with Branch
    public List<Branch>? Branches { get; set; }
}
