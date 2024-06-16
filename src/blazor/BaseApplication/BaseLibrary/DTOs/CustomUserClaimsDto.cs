namespace BaseLibrary.DTOs;

public record CustomUserClaimsDto(string Id = null!,
    string Name = null!,
    string Email = null!,
    string Role = null!);
