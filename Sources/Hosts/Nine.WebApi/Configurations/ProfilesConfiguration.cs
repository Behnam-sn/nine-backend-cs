using Nine.Profiles.Presentation.Profiles.WebApi.ExceptionHandlers;

namespace Nine.WebApi.Configurations;

public static class ProfilesConfiguration
{
    public static IServiceCollection AddProfiles(this IServiceCollection services)
    {
        AddInfrastructure(services);
        AddPresentation(services);

        return services;
    }

    private static void AddInfrastructure(IServiceCollection services)
    {

    }

    private static void AddPresentation(IServiceCollection services)
    {
        services.AddExceptionHandler<ProfileExceptionHandler>();
        services.AddProblemDetails();
    }
}
