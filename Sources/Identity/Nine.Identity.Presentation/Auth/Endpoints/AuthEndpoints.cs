using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

using Nine.Identity.Application.Auth.Commands.SignUpWithPassword;
using Nine.Identity.Domain.Contracts.Users.ValueObjects;
using Nine.Identity.Domain.Users.Entities;
using Nine.Identity.Presentation.Auth.Requests;
using Nine.Shared.Application.Abstractions.Messaging;

namespace Nine.Identity.Presentation.Auth.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/v1/Auth/SignUp", SignUp);
        endpoints.MapPost("api/v1/Auth/Login", Login);
        return endpoints;
    }

    private static async Task SignUp(SignUpRequestV1 request, ICommandBus commandBus, CancellationToken cancellationToken)
    {
        var command = new SignUpWithPasswordCommandV1(
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
