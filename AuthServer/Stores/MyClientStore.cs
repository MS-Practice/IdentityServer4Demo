using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Stores
{
    public class MyClientStore : IClientStore
    {
        private readonly IEnumerable<Client> _clients;
        public MyClientStore(IEnumerable<Client> clients)
        {
            _clients = clients;
        }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            if (_clients == null || !_clients.Any())
                throw new Exception("找不到指定得 clientId");
            foreach (Client client in _clients)
            {
                if (client.ClientId == clientId)
                    return await Task.FromResult(client);
            }
            return null;
        }
    }
}
