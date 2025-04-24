using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Common.Auth.Interfaces
{
    public interface IUserAccessor
    {
        Guid GetUserId();
        string? GetUsername();
        string? GetRole();
    }
}
