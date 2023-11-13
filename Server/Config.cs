using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.Models;
using ApiResource = Duende.IdentityServer.Models.ApiResource;
using ApiScope = Duende.IdentityServer.Models.ApiScope;
using Client = Duende.IdentityServer.Models.Client;
using IdentityResource = Duende.IdentityServer.Models.IdentityResource;
using Secret = Duende.IdentityServer.Models.Secret;

namespace Server
{
    public class Config
    {
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

        public static IEnumerable<ApiScope> ApiScopes => new[] { new ApiScope("CoffeeAPI.read"), new ApiScope("CoffeeAPI.write") };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("CoffeeAPI",new List<string> { "role" })
            {
                Scopes = new List<string> { "CoffeeAPI.read", "CoffeeAPI.write" },
                ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) }
            }
        };

        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                AllowedScopes = {"CoffeeAPI.read","CoffeeAPI.write"}
            },

            new Client
            {
                ClientId = "interactive",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                RedirectUris = {"https://localhost:5002/signin-oidc"},
                FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                PostLogoutRedirectUris = {"https://localhost:5002/signout-callback-oidc"},
                AllowOfflineAccess = true,
                AllowedScopes = {"openid","profile","CoffeeAPI.read"},
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            }
        };
    }
}
