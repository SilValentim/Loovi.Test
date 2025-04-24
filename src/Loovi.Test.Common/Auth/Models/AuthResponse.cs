using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Common.Auth.Models
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
    }
}
