namespace AngerDetector.Service
{
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MainWindow>();
            return serviceCollection;
        }
    }
}
