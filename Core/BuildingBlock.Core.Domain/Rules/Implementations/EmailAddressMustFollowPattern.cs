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

    public bool IsBroken()
    {
        try
        {
            var address = new MailAddress(_email);
            return address.Address != _email;
        }
        catch (FormatException)
        {
            return true;
        }
    }

    public string Message => $"Email address: '{_email}' is not valid.";
}