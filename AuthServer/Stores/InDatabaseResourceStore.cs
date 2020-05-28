using AuthServer.Repository;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Stores
{
    public class InDatabaseResourceStore : IResourceStore
    {
        private readonly ApiResourceRepository _repository;
        private readonly IEnumerable<IdentityResource> _identities;
        public InDatabaseResourceStore(
            ApiResourceRepository repository,
            IEnumerable<IdentityResource> identityResources)
        {
            _repository = repository;
            _identities = identityResources;
        }
        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var bbssApiResouce = await _repository.FindAsync(p => p.Name == name);

            return bbssApiResouce.ToApiResouce();
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var bbssApiResouce = await _repository.GetListByScopeAsync(string.Join(',', scopeNames));

            var apis = bbssApiResouce.ConvertAll(bbssApi => bbssApi.ToApiResouce());

            return apis;
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var identity = from i in _identities
                           where scopeNames.Contains(i.Name)
                           select i;

            return Task.FromResult(identity);
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var bbssApiResources = await _repository.GetAllAsync();
            var apiResouces = bbssApiResources.ConvertAll(api => api.ToApiResouce());
            var result = new Resources(_identities, apiResouces);

            return await Task.FromResult(result);
        }
    }
}
