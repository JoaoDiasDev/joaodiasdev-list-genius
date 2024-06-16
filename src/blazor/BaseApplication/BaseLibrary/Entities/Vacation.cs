using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class Vacation : EmployeeBaseEntity
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required] public int NumberOfDays { get; set; }
    public DateTime EndDate => StartDate.AddDays(NumberOfDays);

    //Many-to-one relationship with Vacation Type
    public VacationType? VacationType { get; set; }
    public int VacationTypeId { get; set; }
}