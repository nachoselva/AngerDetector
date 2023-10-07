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
        #region Private Variables

        private ServiceProvider _serviceProvider;

        #endregion

        #region Constructors

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            services
                .AddApplication()
                .AddService();
            _serviceProvider = services.BuildServiceProvider();
        }

        #endregion

        #region Private Members

        private void OnStartup(object sender, StartupEventArgs e)
        {
            SendEmailWindow mainWindow = _serviceProvider.GetService<SendEmailWindow>()!;
            mainWindow.Show();
        }

        #endregion
    }
}
