namespace AngerDetector.Service.Contracts
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public class AngerDetectorService : IAngerDetectorService
    {
        #region Private Variables

        private const int INTERVAL_IN_SECONDS = 5;
        private readonly IDateTimeService _dateTimeProvider;
        private readonly SortedSet<long> keyStrokes = new SortedSet<long>();

        #endregion

        #region Constructors

        public AngerDetectorService(IDateTimeService dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        #endregion

        #region Public Members

        /// <inheritdoc/>
        public void RegisterKeyStroke()
        {
            lock (keyStrokes)
            {
                keyStrokes.Add(_dateTimeProvider.Now.Ticks);
            }
        }

        /// <inheritdoc/>
        public int CalculateKeyStrokesPerMinuteAverage()
        {
            long lastTickToInclude = _dateTimeProvider.Now.Ticks - INTERVAL_IN_SECONDS * TimeSpan.TicksPerSecond;
            lock (keyStrokes)
            {
                keyStrokes.RemoveWhere(k => k < lastTickToInclude);
            }
            return keyStrokes.Count * (60 / INTERVAL_IN_SECONDS);
        }

        #endregion
    }
}
