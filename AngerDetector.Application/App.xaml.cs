namespace AngerDetector
{
    using AngerDetector.Service;
    using AngerDetector.Views;
    using Microsoft.Extensions.DependencyInjection;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            services
                .AddApplication()
                .AddService();
            _serviceProvider = services.BuildServiceProvider();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            SendEmailWindow mainWindow = _serviceProvider.GetService<SendEmailWindow>()!;
            mainWindow.Show();
        }
    }
}
