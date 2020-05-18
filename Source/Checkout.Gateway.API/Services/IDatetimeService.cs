using System;

namespace Checkout.Gateway.API.Services
{
    public interface IDatetimeService
    {
        DateTime GetUtc();
    }
}