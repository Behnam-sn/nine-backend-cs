using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Nine.Identity.Domain.Contracts.Users.Exceptions;
using Nine.Shared.Presentation.WebApi.ExceptionHandlers;

namespace Nine.Identity.Presentation.Users.ExceptionHandlers;

public sealed class AuthExceptionHandler : BaseExceptionHandler
{
    private readonly ILogger<AuthExceptionHandler> _logger;

    public AuthExceptionHandler(ILogger<AuthExceptionHandler> logger)
        : base(logger)
    {
        _logger = logger;
    }


    protected override (int StatusCode, string Title) MapException(Exception exception)
    {
        return exception switch
        {
            PasswordEmptyException
                => (StatusCodes.Status400BadRequest, "Invalid password"),
            
            PasswordHavingWhiteSpaceException
                => (StatusCodes.Status400BadRequest, "Invalid password"),
            
            PasswordMissingDigitException
                => (StatusCodes.Status400BadRequest, "Invalid Password"),
            
            PasswordMissingLowercaseException
                => (StatusCodes.Status400BadRequest, "Invalid Password"),
            
            PasswordMissingSpecialCharacterException
                => (StatusCodes.Status400BadRequest, "Invalid Password"),
            
            PasswordMissingUppercaseException
                => (StatusCodes.Status400BadRequest, "Invalid password"),
            
            PasswordTooShortException
                => (StatusCodes.Status400BadRequest, "Invalid password"),

            ArgumentException
                => (StatusCodes.Status400BadRequest, "Invalid request"),

            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };
    }
}
