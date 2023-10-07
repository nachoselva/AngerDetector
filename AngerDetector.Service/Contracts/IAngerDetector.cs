namespace AngerDetector.Service.Contracts
{
    public interface IAngerDetector
    {
        public void RegisterKeyStroke();
        public int CalculateKeyStrokesPerMinuteAverage();
    }
}
