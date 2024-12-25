using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Organization :BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
