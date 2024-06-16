using BaseLibrary.Entities.Base;

namespace BaseLibrary.Entities;

public class Doctor : EmployeeBaseEntity
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public string MedicalDiagnose { get; set; } = string.Empty;
    [Required]
    public string MedicalRecommendation { get; set; } = string.Empty;
}