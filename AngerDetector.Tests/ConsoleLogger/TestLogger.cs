namespace AngerDetector.Tests.Log
{
    using System;
    using System.Linq;
    using System.Text;

    internal class TestLogger
    {
        public static void LogAutomationTest(string className, string methodName, params object[] parameters)
        {
            StringBuilder message = new StringBuilder();
            message.Append($"Class: {className}");
            message.Append($" /// ");
            message.Append($"Method: {methodName}");
            if (parameters.Any())
            {
                message.Append($" /// ");
                message.Append($"Params: {string.Join(",", parameters)}");
            }
            Console.WriteLine(message.ToString());
        }
    }
}
