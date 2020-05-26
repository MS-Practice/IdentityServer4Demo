using AuthServer.Entity;
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
        private readonly IBasicRepository<UserClient, long> _repository;
        private List<UserClient> users;
        public InDatabaseClientStore(IBasicRepository<UserClient, long> repository)
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
            return await Task.FromResult(new Client
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                ClientSecrets = {
                    UserSecret.Shared
                },
                AllowedScopes = {
                    "api1"
                }
            });
        }
    }
}
