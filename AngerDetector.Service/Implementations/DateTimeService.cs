namespace AngerDetector.Service
{
    using AngerDetector.Service.Contracts;
    using System;

    /// <inheritdoc/>
    public class DateTimeService : IDateTimeService
    {
        #region Public Members

        /// <inheritdoc/>
        public DateTime Now => DateTime.Now;

        #endregion
    }
}
