﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(), 
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("PaymentGateway", "Checkout.com Payment Gateway")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) }
                }
            };
        
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "b074e29b-54bc-4085-a97d-5a370cafa598",
                    ClientName = "MerchantApi",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("CodingInterview".Sha256())
                    },

                    AccessTokenType = AccessTokenType.Reference,
                    // scopes that client has access to
                    AllowedScopes = { "PaymentGateway" },
                    
                }
            };
        
    }
}