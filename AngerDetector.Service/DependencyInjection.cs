namespace AngerDetector.Service
{
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAngerDetector, AngerDetector>();
            return serviceCollection;
        }
    }
}
