using ListGenius.Api.Entities.Products.Enums;

namespace ListGenius.Api.Entities.Bases;

public abstract class BaseProduct : BaseEntity
{
    public decimal Value { get; set; } = decimal.Zero;

    public string Description { get; set; } = string.Empty;

    public byte[] Qrcode { get; set; } = [];

    public byte[] Image { get; set; } = [];

    public string Link { get; set; } = string.Empty;

    public UnitsOfMeasurement Unit { get; set; } = UnitsOfMeasurement.Unspecified;

    public BaseProduct() { }

    public BaseProduct(string name, bool enabled, DateTime createdDate, DateTime updatedDate,
                      decimal value, string description, byte[] qrcode, byte[] image, string link, UnitsOfMeasurement unit)
       : base(name, enabled, createdDate, updatedDate)
    {
        Value = value;
        Description = description;
        Qrcode = qrcode;
        Image = image;
        Link = link;
        Unit = unit;
    }
}
