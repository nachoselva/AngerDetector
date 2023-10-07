namespace AngerDetector.Service.Contracts
{
    using System;

    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
