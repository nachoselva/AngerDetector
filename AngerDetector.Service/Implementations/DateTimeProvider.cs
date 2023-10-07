namespace AngerDetector.Service
{
    using global::AngerDetector.Service.Contracts;
    using System;

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
