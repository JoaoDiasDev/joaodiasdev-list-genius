namespace BaseLibrary.Entities.Base;

public class GenericBaseEntity
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
}