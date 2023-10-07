namespace AngerDetector.Service
{
    using System.Collections.Generic;

    internal class AngerDetector : IAngerDetector
    {
        private const int INTERVAL_IN_SECONDS = 5;
        private readonly SortedSet<long> keyStrokes = new SortedSet<long>();

        public void RegisterKeyStroke()
        {
            lock (keyStrokes)
            {
                keyStrokes.Add(DateTime.Now.Ticks);
            }
        }

        public int CalculateKeyStrokesPerMinuteAverage()
        {
            long lastTickToInclude = DateTime.Now.Ticks - INTERVAL_IN_SECONDS * TimeSpan.TicksPerSecond;
            lock (keyStrokes)
            {
                keyStrokes.RemoveWhere(k => k < lastTickToInclude);
            }
            return keyStrokes.Count * (60 / INTERVAL_IN_SECONDS);
        }
    }
}
