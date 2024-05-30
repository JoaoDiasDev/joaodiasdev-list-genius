namespace ListGenius.Api.Entities.Bases;

public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets or sets the name of the entity.
    /// </summary>
    public string Name { get; protected init; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the entity is enabled.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedDate { get; protected init; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// </summary>
    public DateTime UpdatedDate { get; protected set; } = DateTime.Now;

    /// <summary>
    /// Updates the UpdatedDate property to the current date and time.
    /// </summary>
    public void UpdateTimestamp()
    {
        UpdatedDate = DateTime.Now;
    }
    protected BaseEntity(string name, bool enabled, DateTime createdDate, DateTime updatedDate)
    {
        Name = name;
        Enabled = enabled;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
    }

    protected BaseEntity()
    {

    }

}
