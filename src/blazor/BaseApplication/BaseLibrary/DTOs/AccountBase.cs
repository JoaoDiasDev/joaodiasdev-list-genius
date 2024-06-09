using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;
public class AccountBase
{
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [Required]
    public required string? Email { get; set; }

    [DataType(DataType.Password)]
    [Required]
    public required string? Password { get; set; }
}
