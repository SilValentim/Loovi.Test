using Loovi.Test.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Domain.Common
{
    public class UserOwnedEntity : BaseEntity, IUserIdentity
    {
        /// <summary>
        /// Unique identifier for the user who owns the entity.
        /// </summary>
        public Guid UserId { get; set; }
        public override int CompareTo(BaseEntity? other)
        {
            if (other == null) return 1;
            else return Id.CompareTo(other.Id);
        }
    }
    
}
