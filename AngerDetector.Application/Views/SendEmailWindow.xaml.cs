namespace AngerDetector.Views
{
    using AngerDetector.Application.ViewModels;
    using AngerDetector.Service.Contracts;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SendEmailWindow : Window
    {
        public EmailViewModel ViewModel { get; }
        public SendEmailWindow(IAngerDetector angerDetector)
        {
            ViewModel = new EmailViewModel(angerDetector);
            DataContext = ViewModel;
            InitializeComponent();

            Binding bodyBinding = new Binding();
            bodyBinding.Source = ViewModel;
            bodyBinding.Path = new PropertyPath(nameof(EmailViewModel.Body));
            bodyBinding.Mode = BindingMode.TwoWay;
            bodyBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(txtBody, TextBox.TextProperty, bodyBinding);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsAngry)
            {
                MessageBox.Show("We detected you are angry, please take a tea and try again later", "Email failed", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                MessageBox.Show("Email was correctly sent", "Email succeded", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
