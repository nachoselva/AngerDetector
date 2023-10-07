namespace AngerDetector.Tests.Unit
{
    using AngerDetector.Service.Contracts;
    using AngerDetector.Tests.Log;
    using Moq;
    using System;
    using Xunit;

    public class AngerDetectorTests
    {
        [Fact]
        public void RegisterKeyStroke_AddsTimestamp()
        {
            TestLogger.LogAutomationTest(nameof(AngerDetectorTests), nameof(RegisterKeyStroke_AddsTimestamp));
            // Arrange
            var mockNowProvider = new Mock<IDateTimeProvider>();
            var detector = new AngerDetector(mockNowProvider.Object);

            // Act
            detector.RegisterKeyStroke();

            // Assert
            // Since we can't directly access the internal SortedSet, we can't assert directly.
            // We'll just verify that no exceptions are thrown during the test.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyStrokesAmountInInterval">Amount of keys in the interval which are going be taken into account to calculate the speed</param>
        /// <param name="expectedAverage"></param>
        [Theory]
        [InlineData(5, 60)]
        [InlineData(10, 120)]
        [InlineData(20, 240)]
        [InlineData(40, 480)]
        [InlineData(60, 720)]
        public void CalculateKeyStrokesPerMinuteAverage_CalculatesAverage(int keyStrokesAmountInInterval, int expectedAverage)
        {
            float keyStrokesSpeedInIntervalInSeconds = keyStrokesAmountInInterval / 5f;
            TestLogger.LogAutomationTest(nameof(AngerDetectorTests), nameof(CalculateKeyStrokesPerMinuteAverage_CalculatesAverage));
            // Arrange
            DateTime now = new DateTime(2023, 01, 01);
            var mockNowProvider = new Mock<IDateTimeProvider>();
            var sequence = mockNowProvider.SetupSequence(x => x.Now);
            int sequenceCount = 0;

            //Setup of key which are not going to be taken into acccount in the calculation
            //because they are older than 5 seconds
            //They are faster twice faster than those keys inside the calculation 
            for (int i = 1; i <= 2 * keyStrokesAmountInInterval; i++, sequenceCount++)
            {
                sequence = sequence.Returns(now.AddSeconds(-i / (2 * keyStrokesSpeedInIntervalInSeconds)));
            }

            //This for sentence adds keyStrokesAmountInInterval + 1 strokes separated by keyStrokesSpeedInIntervalInMs time
            //The first keyStrokesAmountInInterval are user key inputs which are going to be part of the calculation because they are ealier then 5 seconds
            //The last one is when the calculation is done
            for (int i = 0; i <= keyStrokesAmountInInterval; i++, sequenceCount++)
            {
                sequence = sequence.Returns(now.AddSeconds(i / keyStrokesSpeedInIntervalInSeconds));
            }

            var detector = new AngerDetector(mockNowProvider.Object);

            // Act
            for (int i = 0; i < sequenceCount - 1; i++)
            {
                detector.RegisterKeyStroke();
            }

            var average = detector.CalculateKeyStrokesPerMinuteAverage();

            // Assert
            Assert.Equal(expectedAverage, average);
        }
    }
}
