namespace AngerDetector.Service.Contracts
{
    /// <summary>
    /// Logic to register stroke and calculate stroke average
    /// </summary>
    public interface IAngerDetectorService
    {
        /// <summary>
        /// Register individual stroke at Now time
        /// </summary>
        public void RegisterKeyStroke();

        /// <summary>
        /// Calulate stroke average or speed based in the last 5 seconds.
        /// It cleans the stroke collection removing older than 5 seconds regiters.
        /// </summary>
        /// <returns>Average calculated in interval</returns>
        public int CalculateKeyStrokesPerMinuteAverage();
    }
}
