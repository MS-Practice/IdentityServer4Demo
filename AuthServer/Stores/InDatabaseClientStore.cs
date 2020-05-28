using AuthServer.Entity;
using AuthServer.Repository;
using BBSS.Platform.Core.Repository;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Stores
{
    public class InDatabaseClientStore : IClientStore
    {
        private readonly UserClientRepository _repository;
        private List<UserClient> users;
        public InDatabaseClientStore(UserClientRepository repository)
        {
            _repository = repository;
            LoadTestData();
        }

        private void LoadTestData()
        {
            users = new List<UserClient>()
            {
                new UserClient{
                    Id = 1,
                    ClientId = "marsonshine",
                    ClientName = "Marson Shine"
                },
                new UserClient{
                    Id = 2,
                    ClientId = "summerzhu",
                    ClientName = "Summer Zhu",
                },
                new UserClient{
                    Id = 3,
                    ClientId = "bluceli",
                    ClientName = "Bluce Li"
                }
            };
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = users.FirstOrDefault(p => p.ClientId == clientId);
            Client c = client == null ?
                null :
                new Client
                {
                    ClientId = client.ClientId,
                    ClientName = client.ClientName,
                    ClientSecrets = {
                        UserSecret.Shared
                    },
                    AllowedGrantTypes = {
                        GrantType.ClientCredentials,
                        //GrantType.AuthorizationCode
                    },
                    AllowedScopes = {
                        "api1"
                    },
                    Enabled = true
                };

            return await Task.FromResult(c);
        }
    }
}
