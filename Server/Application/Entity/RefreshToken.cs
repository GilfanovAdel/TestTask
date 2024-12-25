using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class RefreshToken :BaseEntity
    {
        public string Token { get; set; } 

        public Guid UserId { get; set; }

        public Users User { get; set; } 
    }
}
