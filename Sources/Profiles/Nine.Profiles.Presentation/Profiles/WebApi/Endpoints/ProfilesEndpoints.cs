using System.Security.Claims;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using Nine.Profiles.Application.Profiles.Commands.Create;
using Nine.Profiles.Presentation.Profiles.WebApi.Requests;
using Nine.Profiles.Presentation.Profiles.WebApi.Responses;
using Nine.Shared.Application.Abstractions.Messaging;

namespace Nine.Profiles.Presentation.Profiles.WebApi.Endpoints;

public static class ProfilesEndpoints
{
    public static IEndpointRouteBuilder MapProfilesWebApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/v1/ProfileWebApi", Create);

        return endpoints;
    }

    private static async Task<IResult> Create(
        CreateProfileRequestV1 request,
        ClaimsPrincipal user,
        ICommandBus commandBus,
        CancellationToken cancellationToken)
    {
        var command = new CreateProfileCommandV1(
            Name: request.Name,
            Handle: request.Handle,
            AvatarObjectKey: request.AvatarObjectKey,
            AvatarMediaType: request.AvatarMediaType,
            Bio: request.Bio,
            OwnerId: request.OwnerId
        );

        var profileId = await commandBus.Send(command, cancellationToken);

        return Results.Json(
            new CreateProfileResponseV1(profileId.ToString()),
            statusCode: StatusCodes.Status201Created);
    }
}
