using System.Net;

namespace ConSelenium.API.Tests.RequestModels
{
    internal class User
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
        public string? Email { get; set; }

        public UserAddress? Address { get; set; }
    }
}
