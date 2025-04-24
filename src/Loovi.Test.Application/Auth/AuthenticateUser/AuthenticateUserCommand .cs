using Loovi.Test.Common.Auth.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Application.Auth.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<AuthResult>
    {
        public AuthenticateUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
