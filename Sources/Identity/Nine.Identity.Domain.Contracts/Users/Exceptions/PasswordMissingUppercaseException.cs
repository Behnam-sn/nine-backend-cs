namespace Nine.Identity.Domain.Contracts.Users.Exceptions;

public sealed class PasswordMissingUppercaseException : Exception
{
    public PasswordMissingUppercaseException()
        : base("Password must contain at least one uppercase letter.")
    {
    }
}
