using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Domain.Common.Interfaces
{
    public interface ISoftDeletable
    {
        bool Active { get; set; }
        void Activate();
        void Deactivate();
    }
}
