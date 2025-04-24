using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Common.Auth.Interfaces
{
    public interface IUserAccessor
    {
        string? GetUserId();
        string? GetUsername();
        string? GetRole();
    }
}
