namespace RBP.Data.DTO.Branding;

public sealed class BrandingAddressDto
{
    public string Line1 { get; init; } = string.Empty;

    public string Line2 { get; init; } = string.Empty;

    public string PostTown { get; init; } = string.Empty;

    public string County { get; init; } = string.Empty;

    public string PostCode { get; init; } = string.Empty;
}
