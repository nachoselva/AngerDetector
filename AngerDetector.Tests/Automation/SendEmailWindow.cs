namespace AngerDetector.Tests.Automation
{
    using FlaUI.Core;
    using FlaUI.Core.AutomationElements;
    using FlaUI.Core.Input;
    using FlaUI.UIA3;
    using System;
    using System.Threading;
    using Xunit;

    public class SendEmailWindow
    {
        const float EXPECTED_EXECUTION_TIME_IN_SECONDS = 5f;

        [Fact]
        public void No_Text_Input_Success()
        {
            Application app = Application.Launch("AngerDetector.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);

                var txtBody = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBody"))?.AsTextBox()!;
                var btnSend = window.FindFirstDescendant(cf => cf.ByAutomationId("btnSend"))?.AsButton()!;
                btnSend.Click();
                Wait.UntilInputIsProcessed();
                var popup = window.FindFirstChild(c => c.ByName("Email succeded").And(c.ByControlType(FlaUI.Core.Definitions.ControlType.Window)));
                Assert.NotNull(popup);
                var okButton = popup.FindFirstChild(c => c.ByName("OK").And(c.ByControlType(FlaUI.Core.Definitions.ControlType.Button)));
                okButton.Click();
                app.Close();
            }
        }

        [Theory]
        [InlineData(200)]
        [InlineData(300)]
        [InlineData(500)]
        public void Slow_Text_Input_Success(int sleepInMilliseconds)
        {
            Application app = Application.Launch("AngerDetector.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                var txtBody = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBody"))?.AsTextBox()!;
                var btnSend = window.FindFirstDescendant(cf => cf.ByAutomationId("btnSend"))?.AsButton()!;
                DateTime startedOn = DateTime.Now;
                do
                {
                    txtBody.Text += "a";
                    Thread.Sleep(sleepInMilliseconds);
                } while ((DateTime.Now - startedOn).TotalSeconds < EXPECTED_EXECUTION_TIME_IN_SECONDS);
                btnSend.Click();
                Wait.UntilInputIsProcessed();
                var popup = window.FindFirstChild(c => c.ByName("Email succeded").And(c.ByControlType(FlaUI.Core.Definitions.ControlType.Window)));
                Assert.NotNull(popup);
                var okButton = popup.FindFirstChild(c => c.ByName("OK").And(c.ByControlType(FlaUI.Core.Definitions.ControlType.Button)));
                okButton.Click();
                app.Close();
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(20)]
        public void Fast_Text_Input_Failure(int sleepInMilliseconds)
        {
            Application app = Application.Launch("AngerDetector.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                var txtBody = window.FindFirstDescendant(cf => cf.ByAutomationId("txtBody"))?.AsTextBox()!;
                var btnSend = window.FindFirstDescendant(cf => cf.ByAutomationId("btnSend"))?.AsButton()!;
                DateTime startedOn = DateTime.Now;
                do
                {
                    txtBody.Text += "a";
                    Thread.Sleep(sleepInMilliseconds);
                } while ((DateTime.Now - startedOn).TotalSeconds < EXPECTED_EXECUTION_TIME_IN_SECONDS);
                btnSend.Click();
                Wait.UntilInputIsProcessed();
                var popup = window.FindFirstChild(c => c.ByName("Email failed").And(c.ByControlType(FlaUI.Core.Definitions.ControlType.Window)));
                Assert.NotNull(popup);
                var okButton = popup.FindFirstChild(c => c.ByName("OK").And(c.ByControlType(FlaUI.Core.Definitions.ControlType.Button)));
                okButton.Click();
                app.Close();
            }
        }
    }
}