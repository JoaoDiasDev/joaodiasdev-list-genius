namespace BaseLibrary.DTOs;

public class EmployeeGrouping2Dto
{
    [Required] public string JobName { get; set; } = string.Empty;
    [Required, Range(1, 99999, ErrorMessage = "You must select a branch")] public int BranchId { get; set; }
    [Required, Range(1, 99999, ErrorMessage = "You must select a town")] public int TownId { get; set; }
    public string? Other { get; set; }

}