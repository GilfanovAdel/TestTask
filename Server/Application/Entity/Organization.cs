using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Organization :BaseEntity
    {
        public string Name { get; set; }
        public List<Users> Users { get; set; } = new List<Users>();
    }
}
