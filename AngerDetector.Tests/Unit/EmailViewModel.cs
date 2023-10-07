using AngerDetector.Application.ViewModels;
using AngerDetector.Service.Contracts;
using Moq;
using Xunit;

namespace AngerDetector.Tests.Unit
{
    public class EmailViewModelTests
    {
        [Fact]
        public void DefaultConstructor_InitializesProperties()
        {
            // Arrange
            var viewModel = new SendEmailViewModel();

            // Act & Assert
            Assert.Equal(string.Empty, viewModel.To);
            Assert.Equal(string.Empty, viewModel.Subject);
            Assert.Equal(string.Empty, viewModel.Body);
            Assert.Equal(0, viewModel.StrokesPerMinute);
        }

        [Fact]
        public void ParameterizedConstructor_InitializesProperties()
        {
            // Arrange
            var angerDetectorMock = new Mock<IAngerDetectorService>();
            var viewModel = new SendEmailViewModel(angerDetectorMock.Object);

            // Act & Assert
            Assert.Equal(string.Empty, viewModel.To);
            Assert.Equal(string.Empty, viewModel.Subject);
            Assert.Equal(string.Empty, viewModel.Body);
            Assert.Equal(0, viewModel.StrokesPerMinute);
        }

        [Fact]
        public void PropertyChanged_EventIsRaised()
        {
            // Arrange
            var viewModel = new SendEmailViewModel();
            bool eventRaised = false;
            viewModel.PropertyChanged += (sender, args) => eventRaised = true;

            // Act
            viewModel.To = "test";

            // Assert
            Assert.True(eventRaised);
        }

        [Fact]
        public void PropertyChanged_EventIsNotRaised_WhenValueIsNotChanged()
        {
            // Arrange
            var viewModel = new SendEmailViewModel();
            bool eventRaised = false;
            viewModel.PropertyChanged += (sender, args) => eventRaised = true;

            // Act
            viewModel.To = "";

            // Assert
            Assert.False(eventRaised);
        }

        [Fact]
        public void Body_PropertyInvokesRegisterKeyStrokeMethod()
        {
            // Arrange
            var angerDetectorMock = new Mock<IAngerDetectorService>();
            var viewModel = new SendEmailViewModel(angerDetectorMock.Object);

            // Act
            viewModel.Body = "Test Body";

            // Assert
            angerDetectorMock.Verify(d => d.RegisterKeyStroke(), Times.Once);
        }

        [Fact]
        public void IsAngry_ReturnsTrue_WhenStrokesPerMinuteExceedsMaximum()
        {
            // Arrange
            var viewModel = new SendEmailViewModel();
            viewModel.StrokesPerMinute = 401;

            // Act & Assert
            Assert.True(viewModel.IsAngry);
        }

        [Fact]
        public void IsAngry_ReturnsFalse_WhenStrokesPerMinuteIsBelowMaximum()
        {
            // Arrange
            var viewModel = new SendEmailViewModel();
            viewModel.StrokesPerMinute = 399;

            // Act & Assert
            Assert.False(viewModel.IsAngry);
        }
    }
}