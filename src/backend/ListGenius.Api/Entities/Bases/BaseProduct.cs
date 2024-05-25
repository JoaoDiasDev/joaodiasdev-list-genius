namespace ListGenius.Api.Entities.Bases;

public abstract class BaseProduct : BaseEntity
{
    public decimal Value { get; set; } = decimal.Zero;

    public string Description { get; set; } = string.Empty;

    public byte[] Qrcode { get; set; } = [];

    public byte[] Image { get; set; } = [];

    public string Link { get; set; } = string.Empty;

    public string Unit { get; set; } = string.Empty;

}
