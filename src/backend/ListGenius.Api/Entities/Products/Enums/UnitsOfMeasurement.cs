namespace ListGenius.Api.Entities.Products.Enums
{
    public enum UnitsOfMeasurement
    {
        [Description("unspecified")]
        Unspecified = 0,
        [Description("M")]
        Meter = 1,
        [Description("M²")]
        SquareMeter = 2,
        [Description("M³")]
        CubicMeter = 3,
        [Description("UN")]
        Unit = 4,
    }
}
