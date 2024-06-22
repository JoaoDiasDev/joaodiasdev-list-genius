namespace BaseLibrary.DTOs;

public class EmployeeGrouping2
{
    [Required] public string JobName { get; set; } = string.Empty;
    [Required, Range(1, 99999, ErrorMessage = "You must select a branch")] public string BranchId { get; set; } = string.Empty;
    [Required, Range(1, 99999, ErrorMessage = "You must select a town")] public string TownId { get; set; } = string.Empty;
    public string? Other { get; set; }

}