namespace AngerDetector.Service
{
    public interface IAngerDetector
    {
        public void RegisterKeyStroke();
        public int CalculateKeyStrokesPerMinuteAverage();
    }
}
