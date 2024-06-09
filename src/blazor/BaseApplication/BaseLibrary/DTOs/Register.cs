using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;
public class Register : AccountBase
{
    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public required string? FullName { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Required]
    public required string? ConfirmPassword { get; set; }
}
