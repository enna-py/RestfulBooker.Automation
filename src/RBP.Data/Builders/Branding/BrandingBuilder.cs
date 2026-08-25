using RBP.Data.DTO.Branding;
using RestfulBooker.Data.Builders.Base;

namespace RBP.Data.Builders.Branding;

public sealed class BrandingBuilder : BaseBuilder<BrandingDto>
{
    private string _name = "Updated B&B Name";
    private string _logoUrl = string.Empty;
    private string _description = "Updated description of the property for automated testing.";
    private string _directions = "Updated directions to the property for automated testing.";
    private double _latitude = 52.6351204;
    private double _longitude = 1.2733774;
    private string _contactName = "Updated Contact Name";
    private string _contactPhone = "01111222333";
    private string _contactEmail = "updated.contact@test.com";
    private string _line1 = "Updated Address Line 1";
    private string _line2 = "Updated Address Line 2";
    private string _postTown = "Updated Town";
    private string _county = "Updated County";
    private string _postCode = "UP1 1DT";

    public BrandingBuilder WithName(string value)
    {
        _name = value;

        return this;
    }

    public BrandingBuilder WithLogoUrl(string value)
    {
        _logoUrl = value;

        return this;
    }

    public BrandingBuilder WithDescription(string value)
    {
        _description = value;

        return this;
    }

    public BrandingBuilder WithDirections(string value)
    {
        _directions = value;

        return this;
    }

    public BrandingBuilder WithContact(string name, string phone, string email)
    {
        _contactName = name;
        _contactPhone = phone;
        _contactEmail = email;

        return this;
    }

    public BrandingBuilder WithAddress(string line1, string line2, string postTown, string county, string postCode)
    {
        _line1 = line1;
        _line2 = line2;
        _postTown = postTown;
        _county = county;
        _postCode = postCode;

        return this;
    }

    public override BrandingDto Build()
    {
        return new BrandingDto
        {
            Name = _name,
            LogoUrl = _logoUrl,
            Description = _description,
            Directions = _directions,
            Map = new BrandingMapDto
            {
                Latitude = _latitude,
                Longitude = _longitude
            },
            Contact = new BrandingContactDto
            {
                Name = _contactName,
                Phone = _contactPhone,
                Email = _contactEmail
            },
            Address = new BrandingAddressDto
            {
                Line1 = _line1,
                Line2 = _line2,
                PostTown = _postTown,
                County = _county,
                PostCode = _postCode
            }
        };
    }
}
