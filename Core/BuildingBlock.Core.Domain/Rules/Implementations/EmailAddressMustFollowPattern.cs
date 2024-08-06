using System.Net.Mail;
using BuildingBlock.Core.Domain.Rules.Abstractions;

namespace BuildingBlock.Core.Domain.Rules.Implementations;

public class EmailAddressMustFollowPattern : IBusinessRule
{
    private readonly string _email;

    public EmailAddressMustFollowPattern(string email)
    {
        _email = email;
    }

    public string? Message { get; private set; }

    public bool IsBroken()
    {
        try
        {
            var address = new MailAddress(_email);
            return address.Address != _email;
        }
        catch (FormatException)
        {
            Message = $"Email address: '{_email}' is not valid.";
            return true;
        }
        catch (ArgumentException)
        {
            Message = $"Email address: '{_email}' can not be empty.";
            return true;
        }
    }
}