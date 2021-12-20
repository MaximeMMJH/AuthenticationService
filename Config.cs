using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationService
{
    public static class Config
    {
        public static List<TestUser> Users
        {
            get
            {
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "742389",
                        Username = "alice",
                        Password = "alice",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.Role, "user")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "462894",
                        Username = "bob",
                        Password = "bob",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.Role, "user")
                        }
                    }
                };
            }
        }

        public static IEnumerable<Client> Clients 
        { 
            get
            {
                return new List<Client>
                {
                    new Client
                    {
                        ClientId = "js.client",
                        ClientName = "interactive client",

                        AllowedGrantTypes = GrantTypes.Code,

                        ClientSecrets = { new Secret("MGPASS".Sha256()) },

                        AllowedScopes = { "mggateway.read", "mggateway.write" }
                    }
                };
            }
        }

        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("mggateway.read"),
            new ApiScope("mggateway.write")
        };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("mggateway")
            {
                Scopes = new List<string> { "mggateway.read", "mggateway.write" },
                ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                UserClaims = new List<string> { "role" }
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> { "role" }
            }
        };
    }
}
