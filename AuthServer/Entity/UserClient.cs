using MS.Microservice.Domain;

namespace AuthServer.Entity
{
    public class UserClient : IEntity<long>
    {
        public long Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string Password { get; set; }
        public string[] RedirectUris { get; set; }
    }
}
