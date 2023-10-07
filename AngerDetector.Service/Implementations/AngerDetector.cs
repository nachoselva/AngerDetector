namespace AngerDetector.Service.Contracts
{
    using System.Collections.Generic;

    public class AngerDetector : IAngerDetector
    {
        private const int INTERVAL_IN_SECONDS = 5;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly SortedSet<long> keyStrokes = new SortedSet<long>();

        public AngerDetector(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public void RegisterKeyStroke()
        {
            lock (keyStrokes)
            {
                keyStrokes.Add(_dateTimeProvider.Now.Ticks);
            }
        }

        public int CalculateKeyStrokesPerMinuteAverage()
        {
            long lastTickToInclude = _dateTimeProvider.Now.Ticks - INTERVAL_IN_SECONDS * TimeSpan.TicksPerSecond;
            lock (keyStrokes)
            {
                keyStrokes.RemoveWhere(k => k < lastTickToInclude);
            }
            return keyStrokes.Count * (60 / INTERVAL_IN_SECONDS);
        }
    }
}
