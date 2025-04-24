using Loovi.Test.Common.Auth.Interfaces;
using Loovi.Test.Common.Auth.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Auth.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthResult>
    {
        private readonly IAuthenticationService _authService;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthenticateUserCommandHandler(
            IAuthenticationService authService,
            IJwtTokenGenerator tokenGenerator)
        {
            _authService = authService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var expiresAt = DateTime.UtcNow.AddHours(1);

            var token = _tokenGenerator.GenerateToken(user.User, expiresAt);

            return new AuthResult
            {
                User = user.User,
                Token = token,
                ExpiresAt = expiresAt
            };
        }
    }
}
