using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class Overtime : EmployeeBaseEntity
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public int NumberOfDays => (EndDate - StartDate).Days;

    //Many-to-one relationship with OvertimeType
    public OvertimeType? OvertimeType { get; set; }
    [Required]
    public int OvertimeTypeId { get; set; }
}