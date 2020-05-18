using System;

namespace Checkout.Gateway.API.Services
{
    public class DatetimeService : IDatetimeService
    {
        public DateTime GetUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
