using Loovi.Test.Common.Auth.Interfaces;
using Loovi.Test.Common.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Common.Auth.Services
{
    public class UserFakeService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _tokenGenerator;

        public UserFakeService(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResult?> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            if (username != "admin" || password != "admin")
                return null;


            var expiresAt = DateTime.UtcNow.AddHours(1);

            var user = new UserDto
            {
                Id = Guid.Parse("03c401a0-11be-4acb-b619-ad98f8d0bcb2"),
                Username = username,
                Role = "Admin",
                Claims = new List<string> { "read", "write" }
            };

            var token = _tokenGenerator.GenerateToken(user, expiresAt);

            var response = new AuthResult
            {
                ExpiresAt = expiresAt,
                Token = token,
                User = user
            };

            return await Task.FromResult(response);
        }
    }
}
