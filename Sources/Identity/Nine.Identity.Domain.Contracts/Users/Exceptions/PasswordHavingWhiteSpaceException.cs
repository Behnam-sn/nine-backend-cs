namespace Nine.Identity.Domain.Contracts.Users.Exceptions;

public sealed class PasswordHavingWhiteSpaceException : Exception
{
    public PasswordHavingWhiteSpaceException()
        : base("Password must not contain any white space.")
    {
    }
}
