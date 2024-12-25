using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Serivces.Abstraction
{
    public interface IJwtService
    {
        public string GenerateToken(Users user);

        public string GenerateRefreshToken();
    }
}
