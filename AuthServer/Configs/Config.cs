using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Configs
{
    public partial class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            //var api1IdentityResource = new IdentityResource(
            //    name: "api1",
            //    displayName: "Custom profile",
            //    claimTypes: new[] { "name", "email", "status" });

            return new IdentityResource[] {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
                //api1IdentityResource
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }, 

                    // scopes that client has access to
                    AllowedScopes = {
                        "api1"
                    }
                },
                new Client
                {
                    ClientId = "MvcClient",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    // 登录成功之后重定向的地址
                    RedirectUris = { "http://localhost:5002/signin-oidc","http://localhost:5003/signin-oidc" },

                    // 退出登录重定向的地址
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true   // 支持刷新令牌
                }
            };
        }
    }
}
