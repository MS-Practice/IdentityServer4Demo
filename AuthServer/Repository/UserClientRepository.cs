using AuthServer.Entity;
using BBSS.Platform.Core.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AuthServer.Repository
{
    public class UserClientRepository : BasicRepositoryBase<UserClient, long>
    {
        public override Task<bool> DeleteAsync([NotNull] long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteAsync([NotNull] Expression<Func<UserClient, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<UserClient> FindAsync([NotNull] Expression<Func<UserClient, bool>> predicate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<UserClient> GetAsync([NotNull] long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<UserClient> InsertAsync([NotNull] UserClient entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<UserClient> UpdateAsync([NotNull] UserClient entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
