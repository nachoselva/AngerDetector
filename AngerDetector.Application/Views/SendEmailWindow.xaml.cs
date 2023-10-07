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
        #region Constructors

        public SendEmailWindow(IAngerDetectorService angerDetector)
        {
            ViewModel = new SendEmailViewModel(angerDetector);
            DataContext = ViewModel;
            InitializeComponent();

            Binding bodyBinding = new Binding();
            bodyBinding.Source = ViewModel;
            bodyBinding.Path = new PropertyPath(nameof(SendEmailViewModel.Body));
            bodyBinding.Mode = BindingMode.TwoWay;
            bodyBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(txtBody, TextBox.TextProperty, bodyBinding);
        }

        #endregion

        #region Private members

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

        #endregion

        #region Public members

        public SendEmailViewModel ViewModel { get; }

        #endregion
    }
}
