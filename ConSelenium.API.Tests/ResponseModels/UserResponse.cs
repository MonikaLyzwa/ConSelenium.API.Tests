using ConSelenium.API.Tests.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConSelenium.API.Tests.ResponseModels
{
    internal class UserResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserAddress? Address { get; set; }
    }
}
