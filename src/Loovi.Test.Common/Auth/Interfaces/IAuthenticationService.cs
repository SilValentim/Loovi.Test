using Loovi.Test.Common.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Common.Auth.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResult?> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default);
    }
}
