using RBP.Data.DTO.Message;
using RestfulBooker.Data.Builders.Base;

namespace RBP.Data.Builders.Message;

public sealed class ContactMessageRequestBuilder : BaseBuilder<ContactMessageRequest>
{
    private string _name = "Test Guest";
    private string _email = "test.guest@example.com";
    private string _phone = "01234567890";
    private string _subject = "Automated test subject line";
    private string _description = "Automated test message body with enough length to pass validation.";

    public ContactMessageRequestBuilder WithName(string value)
    {
        _name = value;

        return this;
    }

    public ContactMessageRequestBuilder WithEmail(string value)
    {
        _email = value;

        return this;
    }

    public ContactMessageRequestBuilder WithPhone(string value)
    {
        _phone = value;

        return this;
    }

    public ContactMessageRequestBuilder WithSubject(string value)
    {
        _subject = value;

        return this;
    }

    public ContactMessageRequestBuilder WithDescription(string value)
    {
        _description = value;

        return this;
    }

    public override ContactMessageRequest Build()
    {
        return new ContactMessageRequest
        {
            Name = _name,
            Email = _email,
            Phone = _phone,
            Subject = _subject,
            Description = _description
        };
    }
}
