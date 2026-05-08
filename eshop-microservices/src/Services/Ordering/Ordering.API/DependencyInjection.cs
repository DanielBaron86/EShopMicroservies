namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }

    public static WebApplication UseApiServices(WebApplication app)
    {
        return app;
    }
}