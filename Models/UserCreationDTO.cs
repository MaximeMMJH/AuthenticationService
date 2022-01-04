using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Models
{
    public class UserCreationDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public Guid subId { get; set; }
    }
}
