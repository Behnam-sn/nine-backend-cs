namespace Nine.WebApi.Configurations;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        AddPresentation(services);
        AddInfrastructure(services);

        return services;
    }

    private static void AddPresentation(IServiceCollection services)
    {
    }

    private static void AddInfrastructure(IServiceCollection services)
    {
    }
}
