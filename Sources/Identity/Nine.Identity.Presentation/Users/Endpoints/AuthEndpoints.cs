using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

using Nine.Identity.Application.Auth.Commands.RegisterUserWithPassword;
using Nine.Identity.Domain.Contracts.Users.ValueObjects;
using Nine.Identity.Domain.Users.Entities;
using Nine.Identity.Presentation.Users.Requests;
using Nine.Shared.Application.Abstractions.Messaging;

namespace Nine.Identity.Presentation.Users.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/v1/Auth/Register", Register);
        endpoints.MapPost("api/v1/Auth/Login", Login);
        return endpoints;
    }

    private static async Task Register(RegisterUserRequestV1 request, ICommandBus commandBus, CancellationToken cancellationToken)
    {
        var command = new RegisterUserWithPasswordCommandV1(
            EmailAddress: EmailAddress.Create(request.Email),
            Password: PlainPassword.Create(request.Password)
        );

        await commandBus.Send(command, cancellationToken);

        Results.Ok();
    }

    private static async Task Login(LoginRequestV1 request, SignInManager<User> signInManager, CancellationToken cancellationToken)
    {
        signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;
        
        var result = await signInManager.PasswordSignInAsync(
            request.Email,
            request.Password,
            isPersistent: true,
            lockoutOnFailure: true
        );
        
        if (!result.Succeeded)
        {
            Results.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            return;
        }

        Results.Ok();
    }
}
