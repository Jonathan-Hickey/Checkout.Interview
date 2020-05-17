using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Gateway.API.Tests.Helpers
{
    public static class ControllerContextFactory
    {
        public static ControllerContext CreateControllerContextForClient(Guid merchantId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("client_id", $"{merchantId}"),
            }, "fake"));


            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

    }
}
