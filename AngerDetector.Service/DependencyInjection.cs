namespace AngerDetector.Service
{
    using Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAngerDetectorService, AngerDetectorService>();
            serviceCollection.AddScoped<IDateTimeService, DateTimeService>();
            return serviceCollection;
        }
    }
}
