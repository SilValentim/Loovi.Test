using Loovi.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loovi.Test.Domain.Common
{
    public class Paginated<Entity> where Entity : BaseEntity
    {
        public IList<Entity> Items { get; set; } = new List<Entity>();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
