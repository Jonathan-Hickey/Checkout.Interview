using System;
using System.Linq;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetMerchantId(this ClaimsPrincipal principal)
        {
            return Guid.Parse(principal.Claims.FirstOrDefault(c => c.Type == "client_id").Value);
        }
    }
}
