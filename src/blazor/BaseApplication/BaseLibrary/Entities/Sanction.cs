using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class Sanction : EmployeeBaseEntity
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public string Punishment { get; set; } = string.Empty;
    [Required]
    public DateTime PunishmentDate { get; set; }

    //Many-to-one relationship with Sanction Type
    public SanctionType? SanctionType { get; set; }
    [Required]
    public int SanctionTypeId { get; set; }

}