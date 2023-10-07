namespace AngerDetector.Service
{
    using AngerDetector.Views;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<SendEmailWindow>();
            return serviceCollection;
        }
    }
}
