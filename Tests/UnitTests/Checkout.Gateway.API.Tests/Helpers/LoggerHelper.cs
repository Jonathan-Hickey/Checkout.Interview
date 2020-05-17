using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace Checkout.Gateway.API.Tests.Helpers
{
    public static class LoggerHelper
    {
        public static ILogger<T> CreateLogger<T>()
        {
            var moqLogger = new Mock<ILogger<T>>();

            moqLogger.Setup(l =>
                l.Log<T>(It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<T>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<T, Exception, string>>()
                ));

            return moqLogger.Object;
        }
    }
}