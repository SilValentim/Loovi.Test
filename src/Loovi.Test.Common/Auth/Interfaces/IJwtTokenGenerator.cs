using Loovi.Test.Common.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Common.Auth.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserDto user, DateTime ExpiresAt);
    }
}
