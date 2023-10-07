namespace AngerDetector.Application.ViewModels
{
    using AngerDetector.Service;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Timers;

    public class EmailViewModel : INotifyPropertyChanged
    {
        private const int MAXIMUM_KEYSTROKE_ALLOWED_PER_MINUTE = 400;
        private const int TYPING_SPEED_REFRESH_INTERVAL = 1000;
        private readonly IAngerDetector _angerDetector;
        private readonly Timer _timerCheck;
        private string _to;
        private string _subject;
        private string _body;
        private float _strokesPerMinute;

        public EmailViewModel()
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

        public EmailViewModel(IAngerDetector angerDetector) : this()
        {
            _angerDetector = angerDetector;
        }

        public string To
        {
            get => _to;
            set => OnPropertyChanged(ref _to, value);
        }
        public string Subject
        {
            get => _subject;
            set => OnPropertyChanged(ref _subject, value);
        }
        public string Body
        {
            get => _body;
            set
            {
                _angerDetector.RegisterKeyStroke();
                OnPropertyChanged(ref _body, value);
            }
        }
        public float StrokesPerMinute
        {
            get => _strokesPerMinute;
            set => OnPropertyChanged(ref _strokesPerMinute, value);
        }

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
            StrokesPerMinute = _angerDetector.CalculateKeyStrokesPerMinuteAverage();
        }

        public bool IsAngry => StrokesPerMinute > MAXIMUM_KEYSTROKE_ALLOWED_PER_MINUTE;
    }
}
