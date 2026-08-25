namespace RBP.Data.DTO.Branding;

public sealed class BrandingDto
{
    public string Name { get; init; } = string.Empty;

    public string LogoUrl { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string Directions { get; init; } = string.Empty;

    public BrandingMapDto Map { get; init; } = new();

    public BrandingContactDto Contact { get; init; } = new();

    public BrandingAddressDto Address { get; init; } = new();
}
