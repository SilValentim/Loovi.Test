using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Domain.Common.Interfaces
{
    public interface IUserIdentity
    {
        Guid UserId { get; set; }
    }
}
