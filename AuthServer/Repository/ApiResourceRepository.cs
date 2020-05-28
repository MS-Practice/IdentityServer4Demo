using AuthServer.Entity;
using BBSS.Platform.Core.Repository;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.Repository
{
    public class ApiResourceRepository : BasicRepositoryBase<BBSSApiResource, int>
    {
        private List<BBSSApiResource> source;
        public ApiResourceRepository()
        {
            InitSourceFromDb();
        }

        private void InitSourceFromDb()
        {
            source = new List<BBSSApiResource>
            {
                new BBSSApiResource{
                    Id = 1,
                    Name = "bbss.openapi",
                    DisplayName = "开放平台API",
                },
                new BBSSApiResource{
                    Id = 2,
                    Name = "jxgy.app",
                    DisplayName = "家校共育APP",
                },
                new BBSSApiResource{
                    Id = 2,
                    Name = "api1",
                    DisplayName = "api1",
                }
            };
        }

        public async Task<List<BBSSApiResource>> GetListByScopeAsync(string scope)
        {
            var list = source.Where(p => p.Name == scope).ToList();
            return await Task.FromResult(list);
        }

        public override Task<bool> DeleteAsync([NotNull] int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteAsync([NotNull] Expression<Func<BBSSApiResource, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BBSSApiResource>> GetAllAsync()
        {
            return await Task.FromResult(source);
        }

        public override async Task<BBSSApiResource> FindAsync([NotNull] Expression<Func<BBSSApiResource, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var apiResouce = source.SingleOrDefault(predicate.Compile());
            return await Task.FromResult(apiResouce);
        }

        public override Task<BBSSApiResource> GetAsync([NotNull] int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<BBSSApiResource> InsertAsync([NotNull] BBSSApiResource entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<BBSSApiResource> UpdateAsync([NotNull] BBSSApiResource entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
