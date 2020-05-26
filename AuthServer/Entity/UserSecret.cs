using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Entity
{
    public class UserSecret
    {
        public static Secret Shared => new Secret("secret", "common shared secret", DateTime.Now.AddHours(2));
    }
}
