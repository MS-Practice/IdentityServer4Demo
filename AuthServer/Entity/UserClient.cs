using BBSS.Platform.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Entity
{
    public class UserClient : IEntity<long>
    {
        public long Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string Password { get; set; }
    }
}
