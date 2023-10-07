namespace AngerDetector.Service.Contracts
{
    using System;

    /// <summary>
    /// Datetime service, it provides current datetime
    /// </summary>
    public interface IDateTimeService
    {
        /// <summary>
        /// Now datetime
        /// </summary>
        DateTime Now { get; }
    }
}
