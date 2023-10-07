namespace AngerDetector.Application.ViewModels
{
    using AngerDetector.Service.Contracts;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Timers;

    /// <summary>
    /// Send Email View Model to be used in 
    /// </summary>
    public class SendEmailViewModel : INotifyPropertyChanged
    {
        #region Private variables

        private const int MAXIMUM_KEYSTROKE_ALLOWED_PER_MINUTE = 400;
        private const int TYPING_SPEED_REFRESH_INTERVAL = 1000;
        private readonly IAngerDetectorService? _angerDetector;
        private readonly Timer _timerCheck;
        private string _to;
        private string _subject;
        private string _body;
        private float _strokesPerMinute;

        #endregion

        #region Constructors

        /// <summary>
        /// It is intented to be used by Visual Studio to build WPF visual components.
        /// Please call <see cref="SendEmailViewModel(IAngerDetectorService)"/> from the code.
        /// </summary>
        public SendEmailViewModel()
        {
            _timerCheck = new Timer()
            {
                Interval = TYPING_SPEED_REFRESH_INTERVAL,
                AutoReset = true,
                Enabled = true
            };
            _timerCheck.Elapsed += RefreshTypingSpeed;
            _to = string.Empty;
            _subject = string.Empty;
            _body = string.Empty;
            _strokesPerMinute = 0;
        }

        public SendEmailViewModel(IAngerDetectorService angerDetector) : this()
        {
            _angerDetector = angerDetector;
        }

        #endregion

        #region Private members

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged<T>(ref T currentValue, T newValue, [CallerMemberName] string? name = null)
            where T : IEquatable<T>
        {
            if (!newValue.Equals(currentValue))
            {
                currentValue = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void RefreshTypingSpeed(object? sender, ElapsedEventArgs e)
        {
            StrokesPerMinute = _angerDetector!.CalculateKeyStrokesPerMinuteAverage();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Email address of the receiveer
        /// </summary>
        public string To
        {
            get => _to;
            set => OnPropertyChanged(ref _to, value);
        }

        /// <summary>
        /// Email subject
        /// </summary>
        public string Subject
        {
            get => _subject;
            set => OnPropertyChanged(ref _subject, value);
        }

        /// <summary>
        /// Body of the email, it is going to be analyzed to check Anger
        /// </summary>
        public string Body
        {
            get => _body;
            set
            {
                _angerDetector!.RegisterKeyStroke();
                OnPropertyChanged(ref _body, value);
            }
        }

        /// <summary>
        /// Speed to key strokes
        /// </summary>
        public float StrokesPerMinute
        {
            get => _strokesPerMinute;
            set => OnPropertyChanged(ref _strokesPerMinute, value);
        }

        /// <summary>
        /// Indicate if the user is angry or not
        /// </summary>
        public bool IsAngry => StrokesPerMinute > MAXIMUM_KEYSTROKE_ALLOWED_PER_MINUTE;

        #endregion
    }
}
